using System.Collections;

namespace Application.Abstractions.Interfaces;

public interface IOrganizationService
{
    Task AcceptMemberAsync(Guid volunteerId, Guid organizationId, CancellationToken cancellationToken);
    Task CreateOrganizationAsync(string name, CancellationToken cancellationToken);
    Task<ICollection<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken);
    Task JoinOrganizationAsync(Guid organizationId, CancellationToken cancellationToken);
    Task KickUserAsync(Guid volunteerId, Guid organizationId, CancellationToken cancellationToken);
    Task LeaveOrganizationAsync(Guid organizationId, CancellationToken cancellationToken);
}
