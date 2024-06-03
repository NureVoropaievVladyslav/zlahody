namespace Domain.Entities;

public class Message : BaseEntity
{
    public required string Content { get; set; }
    
    public bool IsRead { get; set; }
    
    public Guid ChatId { get; set; }
    
    public required Chat Chat { get; set; }
    
    public Guid SenderId { get; set; }
    
    public required User Sender { get; set; }
}