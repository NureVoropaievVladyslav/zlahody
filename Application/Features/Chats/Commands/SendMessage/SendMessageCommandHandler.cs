using Application.Abstractions.Interfaces;

namespace Application.Features.Chats.Commands.SendMessage;

public sealed class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(IChatService chatService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _chatService = chatService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var message = _mapper.Map<Message>(request);
        await _chatService.SendMessageAsync(message, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}