using Application.Features.Organizations.Commands.Accept;
using Application.Features.Organizations.Commands.CreateOrganization;
using Application.Features.Organizations.Commands.JoinOrganization;
using Application.Features.Organizations.Commands.KickUser;
using Application.Features.Organizations.Commands.LeaveOrganization;
using Application.Features.Organizations.Queries.Get;
using Application.Features.Organizations.Queries.GetApplications;
using Application.Features.Organizations.Queries.GetOrganizationApplications;
using Application.Features.Organizations.Queries.GetPersonalOrganization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrganizations(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganizationsQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("applications")]
        public async Task<ActionResult> GetApplications(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetApplicationsQuery(), cancellationToken);
            return Ok(response);
        }
        
        [HttpGet("personal")]
        public async Task<ActionResult> GetPersonalOrganization(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPersonalOrganizationQuery(), cancellationToken);
            return Ok(response);
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
        
        [HttpGet("{organizationId}/applications")]
        public async Task<ActionResult> GetApplications(Guid organizationId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganizationApplicationsQuery(organizationId), cancellationToken);
            return Ok(response);
        }
    }
}
