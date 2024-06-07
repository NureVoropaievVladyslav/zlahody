using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Queries.GetPersonalOrganization;

public sealed class GetPersonalOrganizationQueryHandler : IRequestHandler<GetPersonalOrganizationQuery, Guid>
{
    private readonly IOrganizationService _organizationService;

    public GetPersonalOrganizationQueryHandler(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    public async Task<Guid> Handle(GetPersonalOrganizationQuery request, CancellationToken cancellationToken)
    {
        return await _organizationService.GetPersonalOrganizationAsync(cancellationToken);
    }
}