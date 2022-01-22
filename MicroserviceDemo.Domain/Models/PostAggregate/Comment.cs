namespace MicroserviceDemo.Domain.Models.PostAggregate;

using MicroserviceDemo.Domain.Seedwork.Entities;

public class Comment : Entity<Guid>
{

    public string Content { get; set; }

    public Guid AuthorId { get; set; }

    public bool IsActive { get; private set; }

    public Comment(string content, Guid authorId)
        : this(Guid.Empty, content, authorId, true)
    {
    }

    // Empty ctor required for EF
    // TODO: Remove infrastructure dependency in domain
    public Comment()
    {
    }

    public Comment(Guid id, string content, Guid authorId, bool isActive)
    {
        Id = id;
        Content = content;
        AuthorId = authorId;
        IsActive = isActive;
    }

    public void Delete()
    {
        IsActive = false;
    }

}
