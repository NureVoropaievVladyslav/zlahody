using Application.Features.Requests.Commands.Create;

namespace Application.Abstractions.Interfaces;

public interface IRequestService
{
    Task CreateRequestAsync(Request request, CancellationToken cancellationToken);
}
