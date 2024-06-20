using Application.Abstractions.Interfaces;

namespace Application.Features.Chats.Queries.Get;

public sealed class GetChatsQueryHandler : IRequestHandler<GetChatsQuery, ICollection<ChatThumbnailResponse>>
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public GetChatsQueryHandler(IChatService chatService, IMapper mapper)
    {
        _chatService = chatService;
        _mapper = mapper;
    }

    public async Task<ICollection<ChatThumbnailResponse>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatService.GetChatsAsync(cancellationToken);
        return _mapper.Map<ICollection<ChatThumbnailResponse>>(chats);
    }
}