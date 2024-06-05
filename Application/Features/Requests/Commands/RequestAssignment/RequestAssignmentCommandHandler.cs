using Application.Abstractions.Interfaces;

namespace Application.Features.Requests.Commands.RequestAssignment;

public class RequestAssignmentCommandHandler : IRequestHandler<RequestAssignmentCommand, Unit>
{
    private readonly IRequestService _requestService;
    private readonly IUnitOfWork _unitOfWork;

    public RequestAssignmentCommandHandler(IRequestService requestService, IUnitOfWork unitOfWork)
    {
        _requestService = requestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RequestAssignmentCommand request, CancellationToken cancellationToken)
    {
        await _requestService.AssignRequestAsync(request.UserId, request.RequestId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

