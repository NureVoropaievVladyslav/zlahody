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

    public DateTimeOffset CreatedAt { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, RequestResponse>();
        }
    }
}

