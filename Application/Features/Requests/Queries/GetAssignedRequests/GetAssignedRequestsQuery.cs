using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetAssignedRequests;

public record GetAssignedRequestsQuery() : IRequest<ICollection<RequestResponse>>;
