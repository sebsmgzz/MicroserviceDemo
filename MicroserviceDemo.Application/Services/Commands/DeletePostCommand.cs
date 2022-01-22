namespace MicroserviceDemo.Application.Services.CommentsCommands;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class DeletePostCommand : IRequest
{

    public Guid Id { get; set; }

    public DeletePostCommand()
    {
    }

    public DeletePostCommand(Guid id)
    {
        Id = id;
    }

}
public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{

    private readonly IPostsRepository postsRepository;

    public DeletePostCommandHandler(IPostsRepository postsRepository)
    {
        this.postsRepository = postsRepository;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetByIdAsync(request.Id);
        post!.Delete();
        await postsRepository.UnitOfWork.CommitAsync(cancellationToken);
        return Unit.Value;
    }

}
