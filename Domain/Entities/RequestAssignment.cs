namespace Domain.Entities;

public class RequestAssignment : BaseEntity
{
    public required Guid UserId { get; set; }

    public required User User { get; set;}

    public required Guid RequestId { get; set; }

    public required Request Request { get; set; }
}
