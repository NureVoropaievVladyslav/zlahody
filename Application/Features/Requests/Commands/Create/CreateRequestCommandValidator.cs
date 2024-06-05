namespace Application.Features.Requests.Commands.Create;

public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestCommandValidator()
    {
        RuleFor(c => c.RequestType)
           .IsInEnum();
    }
}
