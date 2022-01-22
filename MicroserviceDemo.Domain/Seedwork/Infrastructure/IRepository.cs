namespace MicroserviceDemo.Domain.Seedwork.Infrastructure;

using MicroserviceDemo.Domain.Seedwork.Entities;

public interface IRepository<T> where T : IAggregateRoot
{

    IUnitOfWork UnitOfWork { get; }

    Task AddAsync(T entity);

    Task RemoveAsync(T entity);

}
