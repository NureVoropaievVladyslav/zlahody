using Domain.Enums;

namespace Application.Features.Resources.Commands.Create;

public record CreateResourceCommand(
     string Title,
     string Description,
     ResourceType Type,
     string? Address,
     string? Phone,
     Guid VolunteerId) : IRequest<Unit>;
