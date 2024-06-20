using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetPsychologicalRequests;

public record GetPsychologicalRequestsQuery : IRequest<ICollection<RequestResponse>>;
