namespace MicroserviceDemo.Infrastructure.WorkUnits;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;
using MicroserviceDemo.Infrastructure.Configurations;
using MicroserviceDemo.Infrastructure.Seedwork;
using Microsoft.EntityFrameworkCore;

public class PostsContext : EFUnitOfWork
{

    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public PostsContext(
        IMediator mediator,
        DbContextOptions<PostsContext> options) 
        : base(mediator, options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
    }

}
