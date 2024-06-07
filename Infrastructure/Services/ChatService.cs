using System.Security.Claims;
using Application.Abstractions.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class ChatService(
    IRepository<User> userRepository,
    IRepository<Chat> chatRepository, 
    IRepository<Message> messageRepository,
    IRepository<ChatUser> chatUsersRepository,
    IHttpContextAccessor httpContextAccessor,
    IContextProcedures contextProcedures) : IChatService
{
    public async Task CreateChatAsync(string sendeeEmail, CancellationToken cancellationToken)
    {
        var getUsersQuery = userRepository.GetQueryable().AsNoTracking();
        var senderEmail = GetUserEmailFromContext();
        if (senderEmail == sendeeEmail)
        {
            throw new BadRequestException("Cannot create a chat with yourself.");
        }
        
        var sender = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == senderEmail, cancellationToken) 
                       ?? throw new NotFoundException("Sender not found.");
        var sendee = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == sendeeEmail, cancellationToken) 
                       ?? throw new NotFoundException("Sendee not found.");

        if (await chatUsersRepository.GetQueryable()
                .AnyAsync(cu => cu.UserId == sender.Id 
                                && cu.Chat.ChatUsers.Any(cu => cu.UserId == sendee.Id), cancellationToken))
        {
            throw new ConflictException("Chat with such members already exists.");
        }

        var chat = new Chat();
        chat.ChatUsers.Add(new ChatUser() { UserId = sender.Id });
        chat.ChatUsers.Add(new ChatUser() { UserId = sendee.Id });
        await chatRepository.AddAsync(chat, cancellationToken);
    }

    public async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetAsync(message.ChatId, cancellationToken) 
                   ?? throw new NotFoundException("Chat not found.");
        
        var email = GetUserEmailFromContext();
        var user = await userRepository.GetQueryable().FirstOrDefaultAsync(u => u.Email == email, cancellationToken) 
                   ?? throw new NotFoundException("User not found.");
        message.SenderId = user.Id;
        
        var isMember = await chatUsersRepository.GetQueryable()
            .AnyAsync(uc => uc.ChatId == chat.Id && uc.UserId == message.SenderId, cancellationToken);
        
        if (!isMember)
        {
            throw new ForbiddenException("You are not a member of this chat.");
        }

        await messageRepository.AddAsync(message, cancellationToken);
    }

    public async Task<ICollection<ChatUser>> GetChatsAsync(CancellationToken cancellationToken)
    {
        var userEmail = GetUserEmailFromContext();
        var user = await userRepository.GetQueryable()
            .FirstAsync(u => u.Email == userEmail);

        var chatIds = chatUsersRepository.GetQueryable().Where(cu => cu.UserId == user.Id).Select(cu => cu.ChatId).ToList();
        var chatUsers = await chatUsersRepository.GetQueryable()
            .Where(cu => chatIds.Contains(cu.ChatId) && cu.UserId != user.Id)
            .Include(cu => cu.Chat)
            .Include(cu => cu.User)
            .ToListAsync(cancellationToken);
        
        
        return chatUsers;
    }

    public async Task<ICollection<Message>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var userEmail = GetUserEmailFromContext();

        if (!await chatRepository.GetQueryable().AnyAsync(c => c.Id == chatId && c.ChatUsers.Any(cu => cu.User.Email == userEmail), cancellationToken))
        {
            throw new NotFoundException("Chat not found.");
        }
        
        return await messageRepository.GetQueryable()
            .Where(m => m.ChatId == chatId)
            .Include(m => m.Sender)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkMessagesAsReadAsync(Guid messageId, Guid chatId, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetAsync(messageId, cancellationToken) 
                      ?? throw new NotFoundException("Message not found.");
        var chat = await chatRepository.GetAsync(chatId, cancellationToken)
                    ?? throw new NotFoundException("Chat not found.");
        
        contextProcedures.MarkMessagesAsRead(messageId, chatId);
    }

    private string GetUserEmailFromContext()
    {
        const string firebaseEmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == firebaseEmailClaim)!.Value;
    }
}