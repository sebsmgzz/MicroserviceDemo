namespace MicroserviceDemo.Application.Controllers;

using MediatR;
using MicroserviceDemo.Application.Models;
using MicroserviceDemo.Application.Services.CommentsCommands;
using MicroserviceDemo.Application.Services.PostCommands;
using MicroserviceDemo.Application.Services.PostQueries;
using MicroserviceDemo.Domain.Models.PostAggregate;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{

    private readonly IMediator mediator;
    private readonly ILogger logger;

    public PostsController(
        IMediator mediator,
        ILogger<PostsController> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostsAsync()
    {
        try
        {
            var posts = await mediator.Send(new GetPostsQuery());
            return Ok(posts);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostAsync(Guid id)
    {
        try
        {
            var query = new GetPostByIdQuery(id);
            var post = await mediator.Send(query);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostPostAsync([FromBody]CreatePostDto postDto)
    {
        try
        {
            var authorId = Guid.NewGuid(); // TODO: Read user's id from jwt
            var command = new CreatePostCommand(postDto.Title, postDto.Content, authorId);
            var post = await mediator.Send(command);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Post>> PutPostAsync(Guid id, [FromBody]UpdatePostDto postDto)
    {
        try
        {
            var command = new UpdatePostCommand(id, postDto.Title, postDto.Content);
            await mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostAsync(Guid id)
    {
        try
        {
            var command = new DeletePostCommand(id);
            await mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
