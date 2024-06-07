namespace Application.Features.Organizations.Queries.Get;

public record GetOrganizationsQuery() : IRequest<ICollection<OrganizationResponse>>;

public class OrganizationResponse
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public int NumberOfMembers { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationResponse>()
                .ForMember(dest => dest.NumberOfMembers, opt => opt.MapFrom(src => src.Volunteers.Count));
        }
    }
}
