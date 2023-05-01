using Application.Application;
using Application.Interfaces;
using Domain.Interface;
using Infrastructure.Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                //services.AddTransient<IExampleTransientService, ExampleTransientService>();
                services.AddScoped<IItemApplication, ItemApplication>();
                services.AddScoped<IItemRepository, ItemRepository>();
                //services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
                //services.AddTransient<ServiceLifetimeReporter>();
            })
            .Build();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}