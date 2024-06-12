
namespace Application.Features.Resources.Commands.Create;

public class CreateResourceCommandProfile : Profile
{
    public CreateResourceCommandProfile()
    {
        CreateMap<CreateResourceCommand, Resource>();
    }
}