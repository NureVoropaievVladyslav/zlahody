namespace Application.Abstractions.Interfaces;

public interface IContextProcedures
{
    void MarkMessagesAsRead(Guid messageId, Guid chatId);
}