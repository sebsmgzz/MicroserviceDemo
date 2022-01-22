namespace MicroserviceDemo.Domain.Models.PostAggregate;

using MicroserviceDemo.Domain.Seedwork.Infrastructure;
using System.Collections.Generic;

public interface IPostsRepository : IRepository<Post>
{

    Task<IEnumerable<Post>> GetAllAsync();

    Task<Post?> GetByIdAsync(Guid id);

}
