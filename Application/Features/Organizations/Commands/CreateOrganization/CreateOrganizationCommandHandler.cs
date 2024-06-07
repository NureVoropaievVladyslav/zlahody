
using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Unit>
{
    private readonly IOrganizationService _orhanizationService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrganizationCommandHandler(IOrganizationService orhanizationService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orhanizationService = orhanizationService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        await _orhanizationService.CreateOrganizationAsync(request.Name, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
