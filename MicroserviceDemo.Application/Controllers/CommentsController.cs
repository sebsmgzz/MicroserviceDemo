namespace MicroserviceDemo.Application.Controllers;

using MediatR;
using MicroserviceDemo.Application.Models;
using MicroserviceDemo.Application.Services.CommentsCommands;
using MicroserviceDemo.Application.Services.CommentsQueries;
using MicroserviceDemo.Domain.Models.PostAggregate;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("posts/{postId}/[controller]")]
public class CommentsController : ControllerBase
{

    private readonly IMediator mediator;
    private readonly ILogger logger;

    public CommentsController(
        IMediator mediator,
        ILogger<CommentsController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Post>>> GetCommentsAsync(Guid postId)
    {
        try
        {
            var query = new GetCommentsQuery(postId);
            var comments = await mediator.Send(query);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message, Array.Empty<object>());
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCommentAsync(Guid postId, [FromBody] CreateCommentDto commentDto)
    {
        try
        {
            var authorId = Guid.NewGuid(); // TODO: Read user's id from jwt
            var command = new CreateCommentCommand(postId, commentDto.Content, authorId);
            var comment = await mediator.Send(command);
            // TODO: Return CreatedAt and include uri to resource
            return new ObjectResult(comment) 
            {
                StatusCode = StatusCodes.Status201Created,
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message, Array.Empty<object>());
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Post>> GetCommentAsync(Guid postId, Guid id)
    {
        try
        {
            var query = new GetCommentByIdQuery(postId, id);
            var comment = await mediator.Send(query);
            return Ok(comment);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message, Array.Empty<object>());
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCommentAsync(Guid postId, Guid id, [FromBody] CreateCommentDto commentDto)
    {
        try
        {
            var command = new UpdateCommentCommand(postId, id, commentDto.Content);
            await mediator.Send(command);
            return Ok();
        }
        catch(ArgumentNullException ex)
        {
            logger.LogError(ex, ex.Message, Array.Empty<object>());
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message, Array.Empty<object>());
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCommentAsync(Guid postId, Guid id)
    {
        try
        {
            var command = new DeleteCommentCommand(postId, id);
            await mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message, Array.Empty<object>());
            return NotFound(ex.Message);
        }
    }

}
