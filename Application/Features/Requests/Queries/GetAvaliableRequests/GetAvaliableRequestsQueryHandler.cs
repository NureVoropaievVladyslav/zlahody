
using Application.Abstractions.Interfaces;

namespace Application.Features.Requests.Queries.GetAvaliableRequest;

public class GetAvaliableRequestsQueryHandler : IRequestHandler<GetAvaliableRequestsQuery, ICollection<RequestResponse>>
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public GetAvaliableRequestsQueryHandler(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    public async Task<ICollection<RequestResponse>> Handle(GetAvaliableRequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestService.GetAvaliableRequestsAsync(cancellationToken);
        return _mapper.Map<ICollection<RequestResponse>>(requests);
    }
}
