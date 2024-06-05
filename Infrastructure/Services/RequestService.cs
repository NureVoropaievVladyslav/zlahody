using Application.Abstractions.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class RequestService(
    IRepository<User> userRepository,
    IRepository<Request> requestRepository,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestService
{
    public async Task CreateRequestAsync(Request request, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var victimEmail = GetUserEmailFromContext();

        var victim = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == victimEmail, cancellationToken)
                       ?? throw new NotFoundException("Victim not found.");

        request.VictimId = victim.Id;


        await requestRepository.AddAsync(request, cancellationToken);

    }

    public async Task<ICollection<Request>> GetAvaliableRequestsAsync(CancellationToken cancellationToken)
    {
        var getRequestsQuery = requestRepository.GetQueryable()
            .Where(request => request.OrganizationId == null)
            .Include(request => request.Victim);

        return await getRequestsQuery.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<Request>> GetOrganisationRequestsAsync(Guid organisationId, CancellationToken cancellationToken)
    {
        var getRequestsQuery = requestRepository.GetQueryable()
            .Where(request => request.OrganizationId == organisationId)
            .Include(request => request.Victim)
            .Include(request => request.Organization);

        return await getRequestsQuery.ToListAsync(cancellationToken);
    }

    private string GetUserEmailFromContext()
    {
        const string firebaseEmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == firebaseEmailClaim)!.Value;
    }
}
