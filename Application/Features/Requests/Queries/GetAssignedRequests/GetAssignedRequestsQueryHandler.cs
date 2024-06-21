using Application.Abstractions.Interfaces;
using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetAssignedRequests;

public class GetAssignedRequestsQueryHandler : IRequestHandler<GetAssignedRequestsQuery, ICollection<RequestResponse>>
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public GetAssignedRequestsQueryHandler(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    public async Task<ICollection<RequestResponse>> Handle(GetAssignedRequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestService.GetAssignedRequestsAsync(cancellationToken);
        return _mapper.Map<ICollection<RequestResponse>>(requests);
    }
}
