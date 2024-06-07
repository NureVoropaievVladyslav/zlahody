namespace Application.Abstractions.Interfaces;

public interface IChatService
{
    Task CreateChatAsync(string sendeeEmail, CancellationToken cancellationToken);
    
    Task SendMessageAsync(Message message, CancellationToken cancellationToken);
    
    Task<ICollection<ChatUser>> GetChatsAsync(CancellationToken cancellationToken);
    
    Task<ICollection<Message>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken);
    Task MarkMessagesAsReadAsync(Guid messageId, Guid chatId, CancellationToken cancellationToken);
}