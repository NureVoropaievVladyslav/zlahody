namespace Application.Features.Requests.Commands.RequestAssignment;

public record RequestAssignmentCommand(Guid RequestId) : IRequest<Unit>;

