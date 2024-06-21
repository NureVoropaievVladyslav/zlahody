using Application.Features.Chats.Commands.Create;
using Application.Features.Requests.Commands.Create;
using Application.Features.Requests.Commands.RequestAssignment;
using Application.Features.Requests.Queries.GetAssignedRequests;
using Application.Features.Requests.Queries.GetAvaliableRequest;
using Application.Features.Requests.Queries.GetOrganisationRequests;
using Application.Features.Requests.Queries.GetSentRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        
        [Authorize(Roles = "OrganisationOwner, Volunteer")]
        [HttpPost("{requestId}/assign")]
        public async Task<ActionResult> AssignRequest(Guid requestId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RequestAssignmentCommand(requestId), cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAvaliableRequests(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAvaliableRequestsQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("organisation")]
        public async Task<ActionResult> GetOrganisationRequests(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganisationRequestsQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("assigned")]
        public async Task<ActionResult> GetAssignedRequests(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAssignedRequestsQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("sent")]
        public async Task<ActionResult> GetSentRequests(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetSentRequestsQuery(), cancellationToken);
            return Ok(response);
        }
    }
}
