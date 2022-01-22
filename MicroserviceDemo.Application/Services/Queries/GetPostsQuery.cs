namespace MicroserviceDemo.Application.Services.PostQueries;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class GetPostsQuery : IRequest<IEnumerable<Post>>
{
}
public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<Post>>
{

    private readonly IPostsRepository postsRepository;

    public GetPostsQueryHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<IEnumerable<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return await postsRepository.GetAllAsync();
    }

}
