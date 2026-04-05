using Microsoft.EntityFrameworkCore;
using PgMonitor.Domain.Entities;

namespace PgMonitor.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<DatabaseMetric> DatabaseMetrics => Set<DatabaseMetric>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseMetric>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.CpuUsage).IsRequired();
                entity.Property(x => x.MemoryUsage).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();

                entity.Property(x => x.CreatedAt)
                      .HasDefaultValueSql("NOW()");
            });
        }
    }
}
