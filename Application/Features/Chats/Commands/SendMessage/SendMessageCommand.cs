namespace Application.Features.Chats.Commands;

public record SendMessageCommand(Guid ChatId, string Content) : IRequest;