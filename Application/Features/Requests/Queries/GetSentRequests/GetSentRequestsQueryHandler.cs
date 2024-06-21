using Application.Abstractions.Interfaces;
using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetSentRequests;

public sealed class GetSentRequestsQueryHandler : IRequestHandler<GetSentRequestsQuery, IList<RequestResponse>>
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public GetSentRequestsQueryHandler(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    public async Task<IList<RequestResponse>> Handle(GetSentRequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestService.GetSentRequestsAsync(cancellationToken); 
        return _mapper.Map<IList<RequestResponse>>(requests);
    }
}