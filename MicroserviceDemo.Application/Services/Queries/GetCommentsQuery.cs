namespace MicroserviceDemo.Application.Services.CommentsQueries;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class GetCommentsQuery : IRequest<IEnumerable<Comment>>
{

    public Guid PostId { get; }

    public GetCommentsQuery()
    {
    }

    public GetCommentsQuery(Guid postId)
    {
        PostId = postId;
    }

}
public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<Comment>>
{

    private readonly IPostsRepository postsRepository;

    public GetCommentsQueryHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<IEnumerable<Comment>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.PostId);
        return post!.Comments;
    }

}