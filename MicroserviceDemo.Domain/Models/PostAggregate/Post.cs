namespace MicroserviceDemo.Domain.Models.PostAggregate;

using MicroserviceDemo.Domain.Events;
using MicroserviceDemo.Domain.Seedwork.Entities;

public class Post : Entity<Guid>, IAggregateRoot
{
    
    private readonly List<Comment> comments = new();

    public string Title { get; set; }

    public string Content { get; set; }

    public Guid AuthorId { get; set; }

    public bool IsActive { get; private set; }

    public IReadOnlyList<Comment> Comments => comments;

    // Empty ctor required for EF
    // TODO: Remove infrastructure dependency in domain
    public Post()
    {
        if(IsTransient())
        {
            AddDomainEvent(new PostAddedEvent(this));
        }
    }

    public Post(string title, string content, Guid authorId)
        : this(Guid.Empty, title, content, authorId, true)
    {
    }

    public Post(Guid id, string title, string content, 
        Guid authorId, bool isActive) 
        : this()
    {
        Id = id;
        Title = title;
        Content = content;
        AuthorId = authorId;
        IsActive = isActive;
    }

    public void AddComment(Comment comment)
    {
        AddDomainEvent(new CommentAddedEvent(this, comment));
        comments.Add(comment);
    }

    public void RemoveComment(Comment comment)
    {
        comment.Delete();
    }

    public void Delete()
    {
        IsActive = false;
    }

}
