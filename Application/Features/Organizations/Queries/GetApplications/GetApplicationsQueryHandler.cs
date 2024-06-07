using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Queries.GetApplications;

public sealed class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, List<Application>>
{
    private readonly IOrganizationService _organizationService;
    private readonly IMapper _mapper;

    public GetApplicationsQueryHandler(IOrganizationService organizationService, IMapper mapper)
    {
        _organizationService = organizationService;
        _mapper = mapper;
    }

    public async Task<List<Application>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        var organizationApplications = await _organizationService.GetApplicationsAsync(cancellationToken);
        return _mapper.Map<List<Application>>(organizationApplications);
    }
}