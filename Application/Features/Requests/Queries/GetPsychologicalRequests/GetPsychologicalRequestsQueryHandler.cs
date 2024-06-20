using Application.Abstractions.Interfaces;
using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetPsychologicalRequests;

public class GetPsychologicalRequestsQueryHandler : IRequestHandler<GetPsychologicalRequestsQuery, ICollection<RequestResponse>>
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public GetPsychologicalRequestsQueryHandler(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    public async Task<ICollection<RequestResponse>> Handle(GetPsychologicalRequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestService.GetPsychologicalRequestsAsync(cancellationToken);
        return _mapper.Map<ICollection<RequestResponse>>(requests);
    }
}
