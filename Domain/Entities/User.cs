using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    public required string FullName { get; set; }
    
    public required string Email { get; set; }

    public Role Role { get; set; } = Role.Victim;
    
    public required string FirebaseUid { get; set; }
    
    public Guid? OrganizationId { get; set; }
    
    public Organization? Organization { get; set; }

    public ICollection<Request> Requests { get; } = new List<Request>();
    
    public ICollection<Message> Messages { get; } = new List<Message>();
    
    public ICollection<ChatUser> ChatUsers { get; } = new List<ChatUser>();
}