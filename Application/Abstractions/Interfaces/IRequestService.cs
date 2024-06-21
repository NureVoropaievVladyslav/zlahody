namespace Application.Abstractions.Interfaces;

public interface IRequestService
{
    Task AssignRequestAsync(Guid requestId, CancellationToken cancellationToken);
    Task CreateRequestAsync(Request request, CancellationToken cancellationToken);
    Task<ICollection<Request>> GetAssignedRequestsAsync(CancellationToken cancellationToken);
    Task<ICollection<Request>> GetAvaliableRequestsAsync(CancellationToken cancellationToken);
    Task<ICollection<Request>> GetOrganisationRequestsAsync(CancellationToken cancellationToken);
    Task<ICollection<Request>> GetPsychologicalRequestsAsync(CancellationToken cancellationToken);
    Task<ICollection<Request>> GetSentRequestsAsync(CancellationToken cancellationToken);
}
