using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetOrganisationRequests;

public record GetOrganisationRequestsQuery(Guid OrganisationId) : IRequest<ICollection<RequestResponse>>;
