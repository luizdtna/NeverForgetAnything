using Application.Application;
using Application.Interfaces;
using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

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
        builder.Services.AddDbContext<SqlConext>(options =>
        {
            var connection = builder.Configuration.GetConnectionString("WebApiDatabase");
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        });

        //Add the service
        builder.Services.AddScoped<IItemApplication, ItemApplication>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();

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