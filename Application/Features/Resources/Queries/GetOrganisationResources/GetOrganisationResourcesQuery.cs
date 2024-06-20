using Domain.Enums;

namespace Application.Features.Resources.Queries.GetOrganisationResources;

public record GetOrganisationResourcesQuery(
    Guid OrganizationId
    ) : IRequest<ICollection<ResourceResponse>>;

public class ResourceResponse
{
    public Guid Id { get; init; }

    public string Title { get; set; }

    public string Description { get; set; }

    public ResourceType Type { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public Guid VolunteerId { get; set; }

    public UserResponse Volunteer { get; set; }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Resource, ResourceResponse>();
        }
    }
}

public class UserResponse
{
    public Guid Id { get; init; }

    public Guid? OrganizationId { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();

        }
    }
}

