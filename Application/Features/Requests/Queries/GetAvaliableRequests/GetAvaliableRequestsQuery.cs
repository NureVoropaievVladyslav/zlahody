using Domain.Enums;

namespace Application.Features.Requests.Queries.GetAvaliableRequest;

public record GetAvaliableRequestsQuery() : IRequest<ICollection<RequestResponse>>
{ }

public class RequestResponse
{
    public Guid Id { get; init; }

    public RequestType RequestType { get; set; }

    public bool IsApproved { get; set; }

    public Guid VictimId { get; set; }

    public required UserResponse Victim { get; set; }

    public Guid? OrganizationId { get; set; }

    public OrganizationResponse? Organization { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, RequestResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Organization, OrganizationResponse>();
        }
    }
}

public class UserResponse
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public Role Role { get; set; }
}

public class OrganizationResponse
{
    public  string Name { get; set; }
}
