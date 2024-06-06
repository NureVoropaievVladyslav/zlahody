using Application.Features.Chats.Commands.Create;
using Application.Features.Requests.Commands.Create;
using Application.Features.Requests.Commands.RequestAssignment;
using Application.Features.Requests.Queries.GetAssignedRequests;
using Application.Features.Requests.Queries.GetAvaliableRequest;
using Application.Features.Requests.Queries.GetOrganisationRequests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRequestCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignRequest([FromBody] RequestAssignmentCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> CreaGetAvaliableRequests(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAvaliableRequestsQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("organisations/{organisationId}")]
        public async Task<ActionResult> GetOrganisationRequests(Guid organisationId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganisationRequestsQuery(organisationId), cancellationToken);
            return Ok(response);
        }

        [HttpGet("assigned/{userId}")]
        public async Task<ActionResult> GetAssignedRequests(Guid userId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAssignedRequestsQuery(userId), cancellationToken);
            return Ok(response);
        }
    }
}
