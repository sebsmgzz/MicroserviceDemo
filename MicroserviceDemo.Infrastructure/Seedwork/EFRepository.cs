namespace MicroserviceDemo.Infrastructure.Seedwork;

using MicroserviceDemo.Domain.Seedwork.Entities;
using MicroserviceDemo.Domain.Seedwork.Infrastructure;
using System.Threading.Tasks;

public class EFRepository<T, U> : IRepository<T>
    where T : class, IAggregateRoot
    where U : EFUnitOfWork
{

    protected readonly U dbContext;

    public IUnitOfWork UnitOfWork => dbContext;

    public EFRepository(U dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual async Task AddAsync(T entity)
    {
        await dbContext.AddAsync(entity!);
    }

    public virtual async Task RemoveAsync(T entity)
    {
        await Task.Run(() =>
            dbContext.Remove(entity!));
    }

}
