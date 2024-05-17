using Application.Abstractions.Interfaces;

namespace Application.Features.Users.Commands.Register;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        await _userService.RegisterUserAsync(user, request.Password, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}