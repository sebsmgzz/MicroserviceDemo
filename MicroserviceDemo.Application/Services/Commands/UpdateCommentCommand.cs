namespace MicroserviceDemo.Application.Services.CommentsCommands;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class UpdateCommentCommand : IRequest
{

    public Guid PostId { get; set; }

    public Guid CommentId { get; set; }

    public string Content { get; set; }

    public UpdateCommentCommand()
    {
    }
    
    public UpdateCommentCommand(Guid postId, Guid commentId, string content)
    {
        PostId = postId;
        CommentId = commentId;
        Content = content;
    }

}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{

    private readonly IPostsRepository postsRepository;

    public UpdateCommentCommandHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.PostId);
        var comment = post!.Comments.FirstOrDefault(c => c.Id == request.CommentId);
        comment!.Content = request.Content;
        return Unit.Value;
    }

}