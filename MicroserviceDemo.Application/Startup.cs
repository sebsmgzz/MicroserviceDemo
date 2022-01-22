namespace MicroserviceDemo.Application;

using MicroserviceDemo.Domain.Models.PostAggregate;
using MicroserviceDemo.Infrastructure.Repositories;
using MicroserviceDemo.Infrastructure.WorkUnits;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using MicroserviceDemo.Application.Services;

public class Startup
{

    private readonly IConfiguration config;
    private readonly IHostEnvironment env;
    
    public Startup(
        IConfiguration config,
        IHostEnvironment env)
    {
        this.config = config;
        this.env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        // Add controllers
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI
        // https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add third parties
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // Utilities
        services.AddSeeder();

        // Repositories
        services.AddTransient<IPostsRepository, PostsRepository>();

        // Contexts
        services.AddDbContext<PostsContext>(options =>
        {
            // Use AAD auth for prod
            if (false)
            {
                // TODO: AAD Auth using managed identity
                throw new NotImplementedException();
            }
            // Try direct connection of AAD is unavailable
            else
            {
                options.UseSqlServer(
                    config.GetConnectionString("PostsDatabase"));
            }
        });

    }

}
