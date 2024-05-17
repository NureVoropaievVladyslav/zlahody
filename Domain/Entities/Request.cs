using Domain.Enums;

namespace Domain.Entities;

public class Request : BaseEntity
{
    public required string Content { get; set; }
    
    public RequestType RequestType { get; set; }
    
    public bool IsApproved { get; set; }
    
    public Guid VictimId { get; set; }
    
    public required User Victim { get; set; }
    
    public Guid? OrganizationId { get; set; }
    
    public Organization? Organization { get; set; }
}