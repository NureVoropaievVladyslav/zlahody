using Application.Features.Requests.Queries.GetAssignedRequests;
using Application.Features.Requests.Queries.GetPsychologicalRequests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsychologistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PsychologistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("requests")]
        public async Task<ActionResult> GetPsychologicalRequests(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPsychologicalRequestsQuery(), cancellationToken);
            return Ok(response);
        }
    }
}
