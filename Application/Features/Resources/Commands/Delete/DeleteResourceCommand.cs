namespace Application.Features.Resources.Commands.Delete;

public record DeleteResourceCommand(Guid Id) : IRequest<Unit>;
