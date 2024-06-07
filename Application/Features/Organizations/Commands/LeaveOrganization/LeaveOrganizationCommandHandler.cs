using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Commands.LeaveOrganization;

public class LeaveOrganizationCommandHandler : IRequestHandler<LeaveOrganizationCommand, Unit>
{
    private readonly IOrganizationService _organizationService;
    private readonly IUnitOfWork _unitOfWork;

    public LeaveOrganizationCommandHandler(IOrganizationService organizationService, IUnitOfWork unitOfWork)
    {
        _organizationService = organizationService;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(LeaveOrganizationCommand request, CancellationToken cancellationToken)
    {
        await _organizationService.LeaveOrganizationAsync(request.OrganizationId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
