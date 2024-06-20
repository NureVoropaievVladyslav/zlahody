namespace Domain.Entities;

public class Organization : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<User> Volunteers { get; } = new List<User>();

    public ICollection<Request> Requests { get; } = new List<Request>();
}