namespace Application.Features.Users.Queries;

public record GetUserRoleQuery(string email) : IRequest<string>;
