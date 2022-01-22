namespace MicroserviceDemo.Infrastructure.Repositories;

using MicroserviceDemo.Domain.Models.PostAggregate;
using MicroserviceDemo.Infrastructure.Seedwork;
using MicroserviceDemo.Infrastructure.WorkUnits;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class PostsRepository : EFRepository<Post, PostsContext>, IPostsRepository
{

    public PostsRepository(PostsContext postsContext) 
        : base(postsContext)
    {
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return dbContext.Posts
            .Where(p => p.IsActive)
            .Include(p => p.Comments);
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await dbContext.Posts
            .Where(p => p.IsActive)
            .Include(p => p.Comments.Where(c => c.IsActive))
            .FirstAsync(p => p.Id == id);
    }

}
