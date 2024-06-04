using Application.Features.Chats.Commands;
using Application.Features.Chats.Commands.Create;
using Application.Features.Chats.Queries.Get;
using Application.Features.Chats.Queries.GetMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult> Get(CancellationToken cancellationToken)
    {
        var chats = await _mediator.Send(new GetChatsQuery(), cancellationToken);
        return Ok(chats);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateChatCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
    
    [HttpGet("{id}/messages")]
    public async Task<ActionResult> GetMessages(Guid id, CancellationToken cancellationToken)
    {
        var chats = await _mediator.Send(new GetMessagesQuery(id), cancellationToken);
        return Ok(chats);
    }
    
    [HttpPost("messages")]
    public async Task<ActionResult> CreateMessage(Guid id, [FromBody] SendMessageCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
}