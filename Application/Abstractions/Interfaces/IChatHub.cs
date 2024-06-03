namespace Application.Abstractions.Interfaces;

public interface IChatHub
{
    Task SendMessageAsync(Guid chatId, string content, Guid senderId);
}