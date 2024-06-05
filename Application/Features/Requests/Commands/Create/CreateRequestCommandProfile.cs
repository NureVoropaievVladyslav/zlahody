namespace Application.Features.Requests.Commands.Create;

public class CreateRequestCommandProfile : Profile
{
    public CreateRequestCommandProfile()
    {
        CreateMap<CreateRequestCommand, Request>();
    }
}
