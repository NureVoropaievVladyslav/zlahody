using System.Security.AccessControl;

namespace Domain.Entities;

public class Resource : BaseEntity
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public ResourceType Type { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public Guid VolunteerId { get; set; }

    public required User Volunteer { get; set; }
}
