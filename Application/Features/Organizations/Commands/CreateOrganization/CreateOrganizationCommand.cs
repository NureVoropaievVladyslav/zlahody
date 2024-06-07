namespace Application.Features.Organizations.Commands.CreateOrganization;

public record CreateOrganizationCommand(string Name) : IRequest<Unit>;
