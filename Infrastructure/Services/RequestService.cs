using Application.Abstractions.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class RequestService(
    IRepository<User> userRepository,
    IRepository<Request> requestRepository,
    IRepository<RequestAssignment> requestAssignmentRepository,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestService
{
    public async Task AssignRequestAsync(Guid userId, Guid requestId, CancellationToken cancellationToken)
    {
        if (await requestAssignmentRepository.GetQueryable().AnyAsync(a => a.RequestId == requestId, cancellationToken))
        {
            throw new ConflictException("Request is already assigned.");
        }

        var user = await userRepository.GetAsync(userId, cancellationToken)
                        ?? throw new NotFoundException("User not found.");
        var request = await requestRepository.GetAsync(requestId, cancellationToken)
                        ?? throw new NotFoundException("Request not found.");

        request.OrganizationId = user.OrganizationId;
  
        var assignment = new RequestAssignment
        {
            UserId = userId,
            User = user,
            RequestId = requestId,
            Request = request
        };

        requestRepository.Update(request);
        await requestAssignmentRepository.AddAsync(assignment, cancellationToken);
    }

    public async Task CreateRequestAsync(Request request, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var victimEmail = GetUserEmailFromContext();

        var victim = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == victimEmail, cancellationToken)
                       ?? throw new NotFoundException("Victim not found.");

        request.VictimId = victim.Id;


        await requestRepository.AddAsync(request, cancellationToken);
    }

    public async Task<ICollection<Request>> GetAssignedRequestsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(userId, cancellationToken)
            ?? throw new NotFoundException("User not found.");

        var assignedRequests = await requestAssignmentRepository.GetQueryable()
                                .Where(request => request.UserId == userId)
                                .Select(requestAssignment => requestAssignment.Request)
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

        return assignedRequests;
    }

    public async Task<ICollection<Request>> GetAvaliableRequestsAsync(CancellationToken cancellationToken)
    {
        var getRequestsQuery = requestRepository.GetQueryable()
            .AsNoTracking()
            .Where(request => request.OrganizationId == null);

        return await getRequestsQuery.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Request>> GetOrganisationRequestsAsync(Guid organisationId, CancellationToken cancellationToken)
    {
        var getRequestsQuery = requestRepository.GetQueryable()
            .AsNoTracking()
            .Where(request => request.OrganizationId == organisationId);

        return await getRequestsQuery.ToListAsync(cancellationToken);
    }

    private string GetUserEmailFromContext()
    {
        const string firebaseEmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == firebaseEmailClaim)!.Value;
    }
}
