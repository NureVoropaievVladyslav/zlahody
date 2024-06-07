namespace Application.Features.Organizations.Commands.Accept;

public record AcceptMemberCommand(Guid VolunteerId, Guid OrganizationId) : IRequest<Unit>;
