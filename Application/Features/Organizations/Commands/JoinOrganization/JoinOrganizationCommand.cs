namespace Application.Features.Organizations.Commands.JoinOrganization;

public record JoinOrganizationCommand(Guid OrganizationId) : IRequest<Unit>;