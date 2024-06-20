using Application.Abstractions.Interfaces;
using Domain.Entities;
using FirebaseAdmin.Auth;

namespace Infrastructure.Services;

public class UserService(IRepository<User> repository) : IUserService
{
    public async Task<string> GetUserRoleAsync(string email, CancellationToken cancellationToken)
    {
        var getUsersQuery = repository.GetQueryable().AsNoTracking();
        var user = await getUsersQuery.FirstOrDefaultAsync(u => u.Email == email, cancellationToken)
                       ?? throw new NotFoundException("User not found.");

        return user.Role.ToString();
    }

    public async Task RegisterUserAsync(User user, string password, CancellationToken cancellationToken)
    {
        if (await repository.GetQueryable().AnyAsync(u => u.Email == user.Email, cancellationToken))
        {
            throw new ConflictException("User with such email is already registered.");
        }
        
        var userArgs = new UserRecordArgs()
        {
            Email = user.Email,
            Password = password
        };
        
        await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs, cancellationToken);
        await repository.AddAsync(user, cancellationToken);
    }
}