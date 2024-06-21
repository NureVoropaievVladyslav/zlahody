namespace Application.Features.Organizations.Commands.KickUser;

public record KickUserCommand(Guid VolunteerId) : IRequest<Unit>;
