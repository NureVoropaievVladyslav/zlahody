using Application.Features.Resources.Queries.GetOrganisationResources;

namespace Application.Features.Resources.Queries.GetResources;

public record GetResourcesQuery() : IRequest<ICollection<ResourceResponse>>;
