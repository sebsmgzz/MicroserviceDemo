namespace MicroserviceDemo.Application.Services;

using MicroserviceDemo.Application.Services.Seeding;
using MicroserviceDemo.Infrastructure.WorkUnits;
using Microsoft.Extensions.Options;

public static class Extensions
{

    public static void AddSeeder(this IServiceCollection services)
    {
        services.AddOptions<SeederSettings>(nameof(Seeder));
        services.AddTransient<ISeeder, Seeder>();
    }

    public static void AddSeeder(
        this IServiceCollection services,
        Action<SeederSettings> configure)
    {
        services.AddOptions<SeederSettings>(nameof(Seeder));
        services.AddTransient<ISeeder, Seeder>(provider =>
        {
            var context = provider.GetRequiredService<PostsContext>();
            var options = provider.GetRequiredService<IOptions<SeederSettings>>();
            configure.Invoke(options.Value);
            return new Seeder(context, options);
        });
    }

    public static void UseSeeder(this IApplicationBuilder app)
    {
        var seeder = app.ApplicationServices.GetRequiredService<ISeeder>();
        seeder.SeedPosts();
    }

}
