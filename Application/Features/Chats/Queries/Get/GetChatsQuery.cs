namespace Application.Features.Chats.Queries.Get;

public record GetChatsQuery() : IRequest<ICollection<ChatThumbnailResponse>>;

public class ChatThumbnailResponse
{
    public Guid ChatId { get; init; }

    public required string ContactName { get; init; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChatUser, ChatThumbnailResponse>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}

public class ContactResponse
{
    public required string ContactName { get; init; }
}