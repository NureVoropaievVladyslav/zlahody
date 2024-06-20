namespace Application.Features.Chats.Commands.Create;

public record CreateChatCommand(string SendeeEmail) : IRequest;