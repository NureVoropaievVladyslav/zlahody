namespace Application.Abstractions.Interfaces;

public interface IChatClient
{
    Task ReceiveMessageAsync(Guid chatId, string message, Guid senderId);
}