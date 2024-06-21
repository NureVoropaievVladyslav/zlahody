using Application.Features.Users.Commands.Register;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpGet("role/{email}")]
    public async Task<ActionResult> GetRole(string email,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserRoleQuery(email), cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("Authorized");
    }
}