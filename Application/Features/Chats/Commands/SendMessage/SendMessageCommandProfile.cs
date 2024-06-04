namespace Application.Features.Chats.Commands;

public class SendMessageCommandProfile : Profile
{
    public SendMessageCommandProfile()
    {
        CreateMap<SendMessageCommand, Message>();
    }
}