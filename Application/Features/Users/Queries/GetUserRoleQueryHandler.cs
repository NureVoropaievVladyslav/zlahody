

using Application.Abstractions.Interfaces;

namespace Application.Features.Users.Queries;

public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, string>
{
    private readonly IUserService _userService;

    public GetUserRoleQueryHandler(IUserService userService) 
    {
        _userService = userService; 
    }

    public async Task<string> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserRoleAsync(request.email, cancellationToken);
    }
}
