using MediatR;
using Microsoft.EntityFrameworkCore;
using PgMonitor.Application.Commands;
using PgMonitor.Infrastructure.Contracts;
using PgMonitor.Infrastructure.Repositories;
using PgMonitor.Persistence.Context;
using PgMonitor.Worker;

var builder = Host.CreateApplicationBuilder(args);

// 🔹 DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=pgmonitor;Username=postgres;Password=postgres"));

// 🔹 MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(SaveMetricsCommand).Assembly));

// 🔹 Services
builder.Services.AddSingleton<ISystemMetricsCollectorRepository, SystemMetricsCollectorRepository>();
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();

// 🔹 Worker
builder.Services.AddHostedService<MetricsWorker>();

var host = builder.Build();
host.Run();