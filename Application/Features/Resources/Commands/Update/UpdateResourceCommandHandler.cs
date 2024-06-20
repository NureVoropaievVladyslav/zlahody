
using Application.Abstractions.Interfaces;

namespace Application.Features.Resources.Commands.Update;

public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, Unit>
{
    private readonly IResourceService _resourceService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateResourceCommandHandler(IResourceService resourceService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _resourceService = resourceService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = _mapper.Map<Resource>(request);
        await _resourceService.UpdateResourceAsync(resource, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
