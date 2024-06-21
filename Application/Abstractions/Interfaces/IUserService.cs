namespace Application.Abstractions.Interfaces;

public interface IUserService
{
    Task<string> GetUserRoleAsync(string email, CancellationToken cancellationToken);
    Task RegisterUserAsync(User user, string password, CancellationToken cancellationToken);
}