namespace Domain.Entities;

public class Message : BaseEntity
{
    public required string Content { get; set; }
    
    public Guid SenderId { get; set; }
    
    public required User Sender { get; set; }
    
    public Guid ReceiverId { get; set; }
    
    public required User Receiver { get; set; }
    
    public bool IsRead { get; set; }
    
    public ICollection<Message> SentMessages { get; } = new List<Message>();
    
    public ICollection<Message> ReceivedMessages { get; } = new List<Message>();
}