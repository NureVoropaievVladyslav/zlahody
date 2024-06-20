namespace Application.Features.Users.Commands.Register;

public class RegisterUserCommandProfile : Profile
{
    public RegisterUserCommandProfile()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}