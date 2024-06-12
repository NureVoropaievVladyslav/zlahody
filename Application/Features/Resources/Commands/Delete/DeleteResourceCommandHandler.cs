

using Application.Abstractions.Interfaces;

namespace Application.Features.Resources.Commands.Delete;

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, Unit>
{
    private readonly IResourceService _resourceService;
    private IUnitOfWork _unitOfWork;

    public DeleteResourceCommandHandler(IResourceService resourceService, IUnitOfWork unitOfWork)
    {
        _resourceService = resourceService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        await _resourceService.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
