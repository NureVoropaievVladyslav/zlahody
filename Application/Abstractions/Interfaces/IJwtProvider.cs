namespace Application.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
}