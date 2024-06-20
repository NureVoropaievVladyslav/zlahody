namespace Application.Features.Requests.Commands.RequestAssignment;

public record RequestAssignmentCommand(Guid UserId, Guid RequestId) : IRequest<Unit>;

