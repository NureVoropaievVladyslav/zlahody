using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Commands.KickUser;

public class KickUserCommandHandler : IRequestHandler<KickUserCommand, Unit>
{
    private readonly IOrganizationService _organizationService;
    private readonly IUnitOfWork _unitOfWork;

    public KickUserCommandHandler(IOrganizationService organizationService, IUnitOfWork unitOfWork)
    {
        _organizationService = organizationService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(KickUserCommand request, CancellationToken cancellationToken)
    {
        await _organizationService.KickUserAsync(request.volunteerId, request.OrganizationId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
