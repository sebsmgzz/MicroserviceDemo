namespace MicroserviceDemo.Application.Services.CommentsCommands;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class DeleteCommentCommand : IRequest
{

    public Guid PostId { get; set; }

    public Guid CommentId { get; set; }

    public DeleteCommentCommand()
    {
    }

    public DeleteCommentCommand(Guid postId, Guid commentId)
    {
        PostId = postId;
        CommentId = commentId;
    }

}
public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{

    private readonly IPostsRepository postsRepository;

    public DeleteCommentCommandHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.PostId);
        var comment = post!.Comments.First(c => c.Id == request.CommentId);
        post.RemoveComment(comment!);
        await postsRepository.UnitOfWork.CommitAsync(cancellationToken);
        return Unit.Value;
    }

}
