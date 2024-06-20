using Domain.Enums;

namespace Application.Features.Resources.Commands.Update;

public record UpdateResourceCommand(
     Guid id,
     string Title,
     string Description,
     ResourceType Type,
     string? Address,
     string? Phone
    ) : IRequest<Unit>;
