using PgMonitor.Infrastructure.Contracts;
using PgMonitor.Domain.Entities;
using PgMonitor.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace PgMonitor.Infrastructure.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        private readonly ApplicationDbContext _context;

        public MetricsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Adds a new metric to the database
        public async Task AddAsync(DatabaseMetric metric)
        {
            _context.DatabaseMetrics.Add(metric);
            await _context.SaveChangesAsync();
        }

        // Retrieves the latest metric from the database
        public async Task<DatabaseMetric?> GetLatestAsync()
        {
            return await _context.DatabaseMetrics
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

        // Retrieves a list of the most recent metrics, limited by the specified count
        public async Task<List<DatabaseMetric>> GetHistoryAsync(int count)
        {
            return await _context.DatabaseMetrics
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}
