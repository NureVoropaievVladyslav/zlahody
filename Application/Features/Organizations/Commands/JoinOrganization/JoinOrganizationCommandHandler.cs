
using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Commands.JoinOrganization;

public class JoinOrganizationCommandHandler : IRequestHandler<JoinOrganizationCommand, Unit>
{
    private readonly IOrganizationService _organizationService;
    private readonly IUnitOfWork _unitOfWork;

    public JoinOrganizationCommandHandler(IOrganizationService organizationService, IUnitOfWork unitOfWork)
    {
        _organizationService = organizationService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(JoinOrganizationCommand request, CancellationToken cancellationToken)
    {
        await _organizationService.JoinOrganizationAsync(request.OrganizationId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
