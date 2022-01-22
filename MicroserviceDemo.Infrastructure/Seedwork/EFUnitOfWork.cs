namespace MicroserviceDemo.Infrastructure.Seedwork;

using MediatR;
using MicroserviceDemo.Domain.Models.PostAggregate;
using MicroserviceDemo.Domain.Seedwork.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class EFUnitOfWork : DbContext, IUnitOfWork
{

    private readonly IMediator mediator;

    public EFUnitOfWork(
        IMediator mediator,
        DbContextOptions options)
        : base(options)
    {
        this.mediator = mediator;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB
        //    - Will make a single transaction
        //    - Changes by event handlers are in the same transaction
        //    - Event handlers use the same DbContext with "InstancePerLifetimeScope" or "Scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB
        //    - Will make multiple transactions
        //    - You will need to handle eventual consistency
        //    - You will need to handle compensatory actions in case of failures in any of the event handlers 
        await DispatchDomainEventsAsync();
        await SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task DispatchDomainEventsAsync()
    {
        var posts = ChangeTracker.Entries<Post>();
        foreach (var post in posts)
        {
            var domainEvents = post.Entity.GetDomainEvents();
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
            post.Entity.ClearDomainEvents();
        }
    }

}
