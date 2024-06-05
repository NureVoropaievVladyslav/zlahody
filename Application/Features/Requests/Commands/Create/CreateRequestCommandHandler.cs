using Application.Abstractions.Interfaces;

namespace Application.Features.Requests.Commands.Create;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand>
{
    private readonly IRequestService _requestService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRequestCommandHandler(IRequestService requestService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _requestService = requestService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var victimRequest = _mapper.Map<Request>(request);
        await _requestService.CreateRequestAsync(victimRequest, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
