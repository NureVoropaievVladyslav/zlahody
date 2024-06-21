using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetOrganisationRequests;

public record GetOrganisationRequestsQuery() : IRequest<ICollection<RequestResponse>>;
