namespace Domain.Entities;

public class OrganizationApplication : BaseEntity
{
    public Guid VolunteerId {  get; set; }
    public Guid OrganisationId { get; set; }
    public bool IsAccepted { get; set; } = false;
}
