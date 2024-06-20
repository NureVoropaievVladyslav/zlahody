namespace Application.Abstractions.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(User user, string password, CancellationToken cancellationToken);
}