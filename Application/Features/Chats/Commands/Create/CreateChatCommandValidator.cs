namespace Application.Features.Chats.Commands.Create;

public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
{
    public CreateChatCommandValidator()
    {
        RuleFor(x => x.SendeeEmail)
            .EmailAddress().WithMessage("Email is not valid.");
    }
}