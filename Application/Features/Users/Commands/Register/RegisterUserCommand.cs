namespace Application.Features.Users.Commands.Register;

public record RegisterUserCommand(string FullName, string Email, string Password) : IRequest;