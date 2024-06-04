using Application.Abstractions.Interfaces;

namespace Application.Features.Chats.Queries.GetMessages;

public sealed class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, ICollection<MessageResponse>>
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public GetMessagesQueryHandler(IMapper mapper, IChatService chatService)
    {
        _mapper = mapper;
        _chatService = chatService;
    }

    public async Task<ICollection<MessageResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _chatService.GetMessagesAsync(request.ChatId, cancellationToken);
        return _mapper.Map<ICollection<MessageResponse>>(messages);
    }
}