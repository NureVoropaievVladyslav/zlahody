using Application.Abstractions.Interfaces;

namespace Application.Features.Chats.Commands.MarkMessageRead;

public sealed class MarkMessageReadCommandHandler : IRequestHandler<MarkMessageReadCommand>
{
    private readonly IChatService _chatService;

    public MarkMessageReadCommandHandler(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task Handle(MarkMessageReadCommand request, CancellationToken cancellationToken)
    {
        await _chatService.MarkMessagesAsReadAsync(request.MessageId, request.ChatId, cancellationToken);
    }
}