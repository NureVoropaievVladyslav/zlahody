using Application.Abstractions.Interfaces;

namespace Infrastructure.Services;

public class ContextProcedures : IContextProcedures
{
    private ApplicationDbContext _context;

    public ContextProcedures(ApplicationDbContext context)
    {
        _context = context;
    }

    public void MarkMessagesAsRead(Guid messageId, Guid chatId)
    {
        _context.MarkMessagesAsRead(messageId, chatId);
    }
}