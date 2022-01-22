namespace MicroserviceDemo.Domain.Events;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class PostAddedEvent : INotification
{

    public Post Post { get; }

    public PostAddedEvent(Post post)
    {
        Post = post;
    }

}
