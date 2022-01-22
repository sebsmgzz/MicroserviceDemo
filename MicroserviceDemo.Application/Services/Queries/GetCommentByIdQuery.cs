namespace MicroserviceDemo.Application.Services.CommentsQueries;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class GetCommentByIdQuery : IRequest<Comment>
{

    public Guid PostId { get; }

    public Guid CommentId { get; }

    public GetCommentByIdQuery()
    {
    }
    
    public GetCommentByIdQuery(Guid postId, Guid commentId)
    {
        PostId = postId;
        CommentId = commentId;
    }

}

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Comment>
{

    private readonly IPostsRepository postsRepository;

    public GetCommentByIdQueryHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Comment> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.PostId);
        var comment = post!.Comments.FirstOrDefault(c => c.Id == request.CommentId);
        return comment!;
    }

}