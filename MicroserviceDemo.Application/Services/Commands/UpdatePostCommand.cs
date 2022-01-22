namespace MicroserviceDemo.Application.Services.PostCommands;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class UpdatePostCommand : IRequest
{

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public UpdatePostCommand()
    {
    }

    public UpdatePostCommand(Guid id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
    }

}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{

    private readonly IPostsRepository postsRepository;

    public UpdatePostCommandHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.Id!);
        post!.Title = request.Title;
        post!.Content = request.Content;
        await postsRepository.UnitOfWork.CommitAsync(cancellationToken);
        return Unit.Value;
    }

}