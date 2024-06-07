using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Queries.Get;

public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, ICollection<OrganizationResponse>>
{
    private readonly IOrganizationService _organizationService;
    private readonly IMapper _mapper;

    public GetOrganizationsQueryHandler(IOrganizationService organizationService, IMapper mapper)
    {
        _organizationService = organizationService;
        _mapper = mapper;
    }

    public async Task<ICollection<OrganizationResponse>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var organizations = await _organizationService.GetOrganizationsAsync(cancellationToken);
        return _mapper.Map<ICollection<OrganizationResponse>>(organizations);
    }
}
