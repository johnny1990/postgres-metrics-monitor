using PgMonitor.Domain.Entities;

namespace PgMonitor.Infrastructure.Contracts
{
    public interface IMetricsRepository
    {
        Task AddAsync(DatabaseMetric metric);
        Task<DatabaseMetric?> GetLatestAsync();
        Task<List<DatabaseMetric>> GetHistoryAsync(int count);
    }
}
