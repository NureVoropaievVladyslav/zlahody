namespace Application.Features.Organizations.Queries.GetOrganizationApplications;

public record GetOrganizationApplicationsQuery() : IRequest<List<OrganizationApplicationResponse>>;

public class OrganizationApplicationResponse
{
    public Guid VolunteerId { get; set; }
    public required string VolunteerName { get; set; }
    
    public bool IsAccepted { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrganizationApplication, OrganizationApplicationResponse>()
                .ForMember(dest => dest.VolunteerName, opt => opt.MapFrom(src => src.Volunteer.FullName));
        }
    }
}