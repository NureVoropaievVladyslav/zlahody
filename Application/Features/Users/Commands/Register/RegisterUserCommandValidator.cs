namespace Application.Features.Users.Commands.Register;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FullName)
            .Length(5, 55).WithMessage("Fullname must be between 5 and 55 characters.");
        
        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(c => c.Password)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$").WithMessage("Password must contain at least one letter and one digit.")
            .Length(8, 32).WithMessage("Password must be between 8 and 32 characters");
    }
}