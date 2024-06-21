using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetSentRequests;

public record GetSentRequestsQuery() : IRequest<IList<RequestResponse>>;