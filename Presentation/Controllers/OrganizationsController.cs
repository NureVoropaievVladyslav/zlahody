using Application.Features.Organizations.Commands.Accept;
using Application.Features.Organizations.Commands.CreateOrganization;
using Application.Features.Organizations.Commands.JoinOrganization;
using Application.Features.Organizations.Commands.KickUser;
using Application.Features.Organizations.Commands.LeaveOrganization;
using Application.Features.Requests.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPost("{organizationId}/users")]
        public async Task<ActionResult> Join(Guid organizationId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new JoinOrganizationCommand(organizationId), cancellationToken);
            return Ok();
        }

        [HttpPost("{organizationId}/users/{volunteerId}")]
        public async Task<ActionResult> Accept(Guid volunteerId, Guid organizationId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AcceptMemberCommand(volunteerId, organizationId), cancellationToken);
            return Ok();
        }

        [HttpPut("{organizationId}/users")]
        public async Task<ActionResult> Leave(Guid organizationId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new LeaveOrganizationCommand(organizationId), cancellationToken);
            return Ok();
        }

        [HttpPut("{organizationId}/users/{volunteerId}")]
        public async Task<ActionResult> Kick(Guid volunteerId, Guid organizationId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new KickUserCommand(volunteerId, organizationId), cancellationToken);
            return Ok();
        }
    }
}
