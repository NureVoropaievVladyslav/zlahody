namespace Application.Features.Chats.Queries.GetMessages;

public record GetMessagesQuery(Guid ChatId) : IRequest<ICollection<MessageResponse>>;

public class MessageResponse
{
    public required string Content { get; set; }
    
    public required string SenderEmail { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageResponse>()
                .ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email));
        }
    }
}