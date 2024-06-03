using Application.Abstractions.Interfaces;

namespace Application.Features.Chats.Commands.Create;

public sealed class CreateChatCommandHandler : IRequestHandler<CreateChatCommand>
{
    private readonly IChatService _chatService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatCommandHandler(IChatService chatService, IUnitOfWork unitOfWork)
    {
        _chatService = chatService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        await _chatService.CreateChatAsync(request.SendeeEmail, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}