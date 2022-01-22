namespace MicroserviceDemo.Domain.Events;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;

public class CommentAddedEvent : INotification
{

    public Post Post { get; }

    public Comment Comment { get; }

    public CommentAddedEvent(Post post, Comment comment)
    {
        Post = post;
        Comment = comment;
    }

}
