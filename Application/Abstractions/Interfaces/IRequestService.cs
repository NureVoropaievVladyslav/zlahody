namespace Application.Abstractions.Interfaces;

public interface IRequestService
{
    Task AssignRequestAsync(Guid userId, Guid requestId, CancellationToken cancellationToken);
    Task CreateRequestAsync(Request request, CancellationToken cancellationToken);
    Task<ICollection<Request>> GetAssignedRequestsAsync(Guid userId, CancellationToken cancellationToken);
    Task<ICollection<Request>> GetAvaliableRequestsAsync(CancellationToken cancellationToken);
    Task<ICollection<Request>> GetOrganisationRequestsAsync(Guid organisationId, CancellationToken cancellationToken);
}
