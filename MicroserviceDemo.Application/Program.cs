using MicroserviceDemo.Application;
using MicroserviceDemo.Application.Services;

// Creat web-app builder
var builder = WebApplication.CreateBuilder(args);

// Configure services
var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);

// Configure HTTP request pipeline
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSeeder();

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run application
app.Run();
