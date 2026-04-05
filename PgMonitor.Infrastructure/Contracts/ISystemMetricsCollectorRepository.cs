using PgMonitor.Infrastructure.DTOs;

namespace PgMonitor.Infrastructure.Contracts
{
    public interface ISystemMetricsCollectorRepository
    {
        Task<SystemMetricsDto> GetMetricsAsync();
    }
}
