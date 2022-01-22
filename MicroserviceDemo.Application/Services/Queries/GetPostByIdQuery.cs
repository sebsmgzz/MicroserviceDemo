namespace MicroserviceDemo.Application.Services.PostQueries;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class GetPostByIdQuery : IRequest<Post>
{

    public Guid Id { get; }

    public GetPostByIdQuery()
    {
    }

    public GetPostByIdQuery(Guid id)
    {
        Id = id;
    }

}

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
{

    private readonly IPostsRepository postsRepository;

    public GetPostByIdQueryHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        return await postsRepository.GetByIdAsync(request.Id);
    }

}
