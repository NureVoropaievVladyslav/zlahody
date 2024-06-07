namespace Application.Features.Organizations.Commands.LeaveOrganization;

public record LeaveOrganizationCommand(Guid OrganizationId) : IRequest<Unit>;
