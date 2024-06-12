
using Application.Abstractions.Interfaces;

namespace Application.Features.Resources.Commands.Create;

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Unit>
{
    private readonly IResourceService _resourceService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateResourceCommandHandler(IResourceService resourceService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _resourceService = resourceService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = _mapper.Map<Resource>(request);
        await _resourceService.CreateResourceAsync(resource, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
