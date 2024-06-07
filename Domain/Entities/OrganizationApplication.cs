namespace Domain.Entities;

public class OrganizationApplication : BaseEntity
{
    public Guid VolunteerId { get; set; }
    public User Volunteer { get; set; } = null!;
    public Guid OrganisationId { get; set; }
    public bool IsAccepted { get; set; } = false;
}
