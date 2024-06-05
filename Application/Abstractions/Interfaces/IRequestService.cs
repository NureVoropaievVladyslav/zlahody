namespace Application.Abstractions.Interfaces;

public interface IRequestService
{
    Task CreateRequestAsync(Request request, CancellationToken cancellationToken);
    Task<ICollection<Request>> GetAvaliableRequestsAsync(CancellationToken cancellationToken);
    Task<ICollection<Request>> GetOrganisationRequestsAsync(Guid organisationId, CancellationToken cancellationToken);
}
