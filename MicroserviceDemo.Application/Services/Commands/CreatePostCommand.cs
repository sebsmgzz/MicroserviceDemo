namespace MicroserviceDemo.Application.Services.PostCommands;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class CreatePostCommand : IRequest<Post>
{

    public string Title { get; set; }

    public string Content { get; set; }

    public Guid AuthorId { get; set; }

    public CreatePostCommand()
    {
    }

    public CreatePostCommand(string title, string content, Guid authorId)
    {
        Title = title;
        Content = content;
        AuthorId = authorId;
    }

}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Post>
{

    private readonly IPostsRepository postsRepository;

    public CreatePostCommandHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(request.Title, request.Content, request.AuthorId);
        await postsRepository.AddAsync(post);
        await postsRepository.UnitOfWork.CommitAsync(cancellationToken);
        return post;
    }

}