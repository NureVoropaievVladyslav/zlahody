using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Queries.GetOrganizationApplications;

public sealed class GetOrganizationApplicationsQueryHandler : IRequestHandler<GetOrganizationApplicationsQuery, List<OrganizationApplicationResponse>>
{
    private readonly IOrganizationService _organizationService;
    private readonly IMapper _mapper;

    public GetOrganizationApplicationsQueryHandler(IOrganizationService organizationService, IMapper mapper)
    {
        _organizationService = organizationService;
        _mapper = mapper;
    }

    public async Task<List<OrganizationApplicationResponse>> Handle(GetOrganizationApplicationsQuery request, CancellationToken cancellationToken)
    {
        var organisationApplications = await _organizationService.GetOrganizationApplicationsAsync(cancellationToken);
        return _mapper.Map<List<OrganizationApplicationResponse>>(organisationApplications);
    }
}