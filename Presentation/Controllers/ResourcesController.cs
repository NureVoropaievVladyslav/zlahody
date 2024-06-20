using Application.Features.Requests.Commands.Create;
using Application.Features.Resources.Commands.Create;
using Application.Features.Resources.Commands.Delete;
using Application.Features.Resources.Commands.Update;
using Application.Features.Resources.Queries.GetOrganisationResources;
using Application.Features.Resources.Queries.GetResources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateResourceCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteResourceCommand(id), cancellationToken);
            return Ok();
        }

        [HttpGet("organizations/{id}")]
        public async Task<ActionResult> GetOrganisationResources(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganisationResourcesQuery(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetResources(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetResourcesQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }
    }
}
