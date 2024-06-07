namespace Application.Features.Chats.Commands.MarkMessageRead;

public record MarkMessageReadCommand(Guid MessageId, Guid ChatId) : IRequest;