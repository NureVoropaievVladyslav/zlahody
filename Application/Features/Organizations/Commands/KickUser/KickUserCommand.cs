namespace Application.Features.Organizations.Commands.KickUser;

public record KickUserCommand(Guid volunteerId, Guid OrganizationId) : IRequest<Unit>;
