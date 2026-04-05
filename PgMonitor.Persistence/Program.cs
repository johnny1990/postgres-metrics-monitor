using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PgMonitor.Persistence.Context;

namespace PgMonitor.Persistence;

internal class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
          .ConfigureServices((context, services) =>
          {
              services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseNpgsql(
                      "Host=localhost;Port=5432;Database=pgmonitor;Username=postgres;Password=postgres"));
          })
          .Build();

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Database.Migrate();

        Console.WriteLine("Database migrated successfully.");
    }
}