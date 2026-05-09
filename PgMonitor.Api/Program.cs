using PgMonitor.Application.Commands;
using PgMonitor.Infrastructure.Contracts;
using PgMonitor.Infrastructure.Repositories;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PgMonitor.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
});

// Configure MediatR to register handlers from the assembly containing SaveMetricsCommand
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(SaveMetricsCommand).Assembly));

// Register repositories
builder.Services.AddSingleton<ISystemMetricsCollectorRepository, SystemMetricsCollectorRepository>();
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();

// Configure DbContext to use PostgreSQL connection string from configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

// // Configure the HTTP request pipeline & Enable Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
