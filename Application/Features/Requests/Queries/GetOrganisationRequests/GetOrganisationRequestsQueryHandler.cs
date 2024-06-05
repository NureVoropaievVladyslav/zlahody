using Application.Abstractions.Interfaces;
using Application.Features.Requests.Queries.GetAvaliableRequest;

namespace Application.Features.Requests.Queries.GetOrganisationRequests;

public class GetOrganisationRequestsQueryHandler : IRequestHandler<GetOrganisationRequestsQuery, ICollection<RequestResponse>>
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public GetOrganisationRequestsQueryHandler(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }

    public async Task<ICollection<RequestResponse>> Handle(GetOrganisationRequestsQuery request, CancellationToken cancellationToken)
    {
        var requests = await _requestService.GetOrganisationRequestsAsync(request.OrganisationId, cancellationToken);
        return _mapper.Map<ICollection<RequestResponse>>(requests);
    }
}
