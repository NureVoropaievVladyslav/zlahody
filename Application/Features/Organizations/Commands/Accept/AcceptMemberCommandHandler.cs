using Application.Abstractions.Interfaces;

namespace Application.Features.Organizations.Commands.Accept;

public class AcceptMemberCommandHandler : IRequestHandler<AcceptMemberCommand, Unit>
{
    private readonly IOrganizationService _organizationService;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptMemberCommandHandler(IOrganizationService organizationService, IUnitOfWork unitOfWork)
    {
        _organizationService = organizationService;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(AcceptMemberCommand request, CancellationToken cancellationToken)
    {
        await _organizationService.AcceptMemberAsync(request.VolunteerId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
