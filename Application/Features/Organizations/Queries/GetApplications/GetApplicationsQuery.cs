namespace Application.Features.Organizations.Queries.GetApplications;

public record GetApplicationsQuery() : IRequest<List<Application>>;

public class Application()
{
    public Guid OrganisationId { get; set; }
    
    public bool IsAccepted { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrganizationApplication, Application>();
        }
    }
}