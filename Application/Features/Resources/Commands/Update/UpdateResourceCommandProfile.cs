namespace Application.Features.Resources.Commands.Update;

public class UpdateResourceCommandProfile : Profile
{
    public UpdateResourceCommandProfile()
    {
        CreateMap<UpdateResourceCommand, Resource>();
    }
}
