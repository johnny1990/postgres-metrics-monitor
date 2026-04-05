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

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(SaveMetricsCommand).Assembly));

builder.Services.AddSingleton<ISystemMetricsCollectorRepository, SystemMetricsCollectorRepository>();
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
