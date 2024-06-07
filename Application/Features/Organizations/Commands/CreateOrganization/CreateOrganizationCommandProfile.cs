namespace Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandProfile : Profile
{
    public CreateOrganizationCommandProfile()
    {
        CreateMap<CreateOrganizationCommand, Organization>();
    }

}
