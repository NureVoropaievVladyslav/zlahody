using Application.Abstractions.Interfaces;
using Application.Features.Resources.Queries.GetOrganisationResources;

namespace Application.Features.Resources.Queries.GetResources;

public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, ICollection<ResourceResponse>>
{
    private readonly IResourceService _resourceService;
    private readonly IMapper _mapper;

    public GetResourcesQueryHandler(IResourceService resourceService, IMapper mapper)
    {
        _resourceService = resourceService;
        _mapper = mapper;
    }

    public async Task<ICollection<ResourceResponse>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _resourceService.GetResourcesAsync(cancellationToken);
        return _mapper.Map<ICollection<ResourceResponse>>(resources);
    }
}
