using Domain.Enums;

namespace Application.Features.Requests.Commands.Create;

public record CreateRequestCommand(
    string Content,
    RequestType RequestType = RequestType.InformationRequest
    ) : IRequest;
