namespace Domain.Entities;

public class Chat : BaseEntity
{
    public ICollection<Message> Messages { get; } = new List<Message>();
    
    public ICollection<ChatUser> ChatUsers { get; } = new List<ChatUser>();
}