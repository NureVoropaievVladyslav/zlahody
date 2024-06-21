using Application.Abstractions.Interfaces;
using Domain.Entities;
using Domain.Enums;
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
    public async Task AssignRequestAsync(Guid requestId, CancellationToken cancellationToken)
    {
        var userEmail = GetUserEmailFromContext();
        
        if (await requestAssignmentRepository.GetQueryable().AnyAsync(a => a.RequestId == requestId, cancellationToken))
        {
            throw new ConflictException("Request is already assigned.");
        }

        var user = await userRepository.GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken)
                        ?? throw new NotFoundException("User not found.");
        var request = await requestRepository.GetAsync(requestId, cancellationToken)
                        ?? throw new NotFoundException("Request not found.");

        request.OrganizationId = user.OrganizationId;
        request.IsApproved = true;
  
        var assignment = new RequestAssignment
        {
            UserId = user.Id,
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

    public async Task<ICollection<Request>> GetAssignedRequestsAsync(CancellationToken cancellationToken)
    {
        var userEmail = GetUserEmailFromContext();
        var user = await userRepository.GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken)
            ?? throw new NotFoundException("User not found.");

        var assignedRequests = await requestAssignmentRepository.GetQueryable()
                                .Where(request => request.UserId == user.Id)
                                .Select(requestAssignment => requestAssignment.Request)
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);

        return assignedRequests;
    }

    public async Task<ICollection<Request>> GetAvaliableRequestsAsync(CancellationToken cancellationToken)
    {
        var getRequestsQuery = requestRepository.GetQueryable()
            .AsNoTracking()
            .Where(request => request.OrganizationId == null && request.RequestType != RequestType.PsychologicalSupport);

        return await getRequestsQuery.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Request>> GetOrganisationRequestsAsync(CancellationToken cancellationToken)
    {
        var userEmail = GetUserEmailFromContext();
        var user = await userRepository.GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken)
            ?? throw new NotFoundException("User not found.");
        
        var getRequestsQuery = requestRepository.GetQueryable()
            .AsNoTracking()
            .Where(request => request.OrganizationId == user.OrganizationId && request.RequestType != RequestType.PsychologicalSupport);

        return await getRequestsQuery.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Request>> GetPsychologicalRequestsAsync(CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var psychologistEmail = GetUserEmailFromContext();

        var psychologist = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == psychologistEmail, cancellationToken)
                       ?? throw new NotFoundException("Psychologist not found.");

        if (psychologist.Role == Role.Psychologist)
        {
            var getRequestsQuery = requestRepository.GetQueryable()
            .AsNoTracking()
            .Where(request => request.RequestType == RequestType.PsychologicalSupport);

            return await getRequestsQuery.ToListAsync(cancellationToken);
        }
        else 
        {
            throw new ForbiddenException("You are not a psychologist");
        }
    }

    public async Task<ICollection<Request>> GetSentRequestsAsync(CancellationToken cancellationToken)
    {
        var userEmail = GetUserEmailFromContext();
        return await requestRepository.GetQueryable()
            .AsNoTracking()
            .Where(r => r.Victim.Email == userEmail)
            .ToListAsync(cancellationToken);
    }

    private string GetUserEmailFromContext()
    {
        const string firebaseEmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == firebaseEmailClaim)!.Value;
    }
}
