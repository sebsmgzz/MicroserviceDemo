namespace MicroserviceDemo.Domain.Seedwork.Infrastructure;

using System;

public interface IUnitOfWork : IDisposable
{

    Task<bool> CommitAsync(
        CancellationToken cancellationToken = default);

}
