
namespace Application.Abstractions.Interfaces;

public interface IResourceService
{
    Task CreateResourceAsync(Resource resource, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<ICollection<Resource>> GetOrganisationResourcesAsync(Guid organizationId, CancellationToken cancellationToken);
    Task<ICollection<Resource>> GetResourcesAsync(CancellationToken cancellationToken);
    Task UpdateResourceAsync(Resource resource, CancellationToken cancellationToken);
}
