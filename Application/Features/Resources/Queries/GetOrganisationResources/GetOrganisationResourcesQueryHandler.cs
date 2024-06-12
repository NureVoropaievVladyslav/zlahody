

using Application.Abstractions.Interfaces;
using System.Collections.Generic;

namespace Application.Features.Resources.Queries.GetOrganisationResources;

public class GetOrganisationResourcesQueryHandler : IRequestHandler<GetOrganisationResourcesQuery, ICollection<ResourceResponse>>
{
    private readonly IResourceService _resourceService;
    private readonly IMapper _mapper;

    public GetOrganisationResourcesQueryHandler(IResourceService resourceService, IMapper mapper)
    {
        _resourceService = resourceService;
        _mapper = mapper;
    }

    public async Task<ICollection<ResourceResponse>> Handle(GetOrganisationResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _resourceService.GetOrganisationResourcesAsync(request.OrganizationId, cancellationToken);
        return _mapper.Map< ICollection<ResourceResponse>>(resources);
    }
}