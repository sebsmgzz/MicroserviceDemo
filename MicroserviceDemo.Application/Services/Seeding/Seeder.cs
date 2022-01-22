namespace MicroserviceDemo.Application.Services.Seeding;

using MicroserviceDemo.Domain.Models.PostAggregate;
using MicroserviceDemo.Infrastructure.WorkUnits;
using Microsoft.Extensions.Options;

public class Seeder : ISeeder
{

    private readonly PostsContext context;
    private readonly SeederSettings settings;

    public Seeder(
        PostsContext context, 
        IOptions<SeederSettings> options)
    {
        this.context = context;
        settings = options.Value;
    }

    public void SeedPosts()
    {
        if (settings.Force || context.Database.EnsureCreated())
        {
            context.AddRange(
                new Post("lorem ipsum", "carpe diem", Guid.NewGuid())
            );
            context.SaveChanges();
        }
    }

}
