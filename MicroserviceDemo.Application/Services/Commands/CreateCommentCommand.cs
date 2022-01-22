namespace MicroserviceDemo.Application.Services.CommentsCommands;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class CreateCommentCommand : IRequest<Comment>
{

    public Guid PostId { get; set; }

    public string Content { get; set; }

    public Guid AuthorId { get; set; }

    public CreateCommentCommand()
    {
    }

    public CreateCommentCommand(Guid postId, string content, Guid authorId)
    {
        PostId = postId;
        Content = content;
        AuthorId = authorId;
    }

}
public class AddCommentCommandHandler : IRequestHandler<CreateCommentCommand, Comment>
{

    private readonly IPostsRepository postsRepository;

    public AddCommentCommandHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.PostId);
        var comment = new Comment(request.Content, request.AuthorId);
        post!.AddComment(comment);
        await postsRepository.UnitOfWork.CommitAsync(cancellationToken);
        return comment;
    }

}