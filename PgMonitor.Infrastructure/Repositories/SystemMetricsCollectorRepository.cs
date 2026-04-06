using PgMonitor.Infrastructure.Contracts;
using PgMonitor.Infrastructure.DTOs;
using System.Diagnostics;

namespace PgMonitor.Infrastructure.Repositories
{
    public class SystemMetricsCollectorRepository : ISystemMetricsCollectorRepository
    {
        private TimeSpan _previousCpuTime = TimeSpan.Zero;
        private DateTime _previousCheckTime = DateTime.UtcNow;
        private bool _isFirstRun = true;

        // Collects system metrics (CPU and Memory usage)
        public Task<SystemMetricsDto> GetMetricsAsync()
        {
            double totalCpuMs = 0;
            double totalMemory = 0;

            foreach (var p in Process.GetProcesses())
            {
                try
                {
                    totalCpuMs += p.TotalProcessorTime.TotalMilliseconds;
                    totalMemory += p.WorkingSet64;
                }
                catch { }
                finally
                {
                    p.Dispose();
                }
            }

            var totalCpuTime = TimeSpan.FromMilliseconds(totalCpuMs);
            var currentTime = DateTime.UtcNow;

            if (_isFirstRun)
            {
                _previousCpuTime = totalCpuTime;
                _previousCheckTime = currentTime;
                _isFirstRun = false;

                return Task.FromResult(new SystemMetricsDto
                {
                    CpuUsage = 0,
                    MemoryUsage = Math.Round(totalMemory / (1024.0 * 1024.0), 2)
                });
            }

            var cpuUsedMs = (totalCpuTime - _previousCpuTime).TotalMilliseconds;
            var elapsedMs = (currentTime - _previousCheckTime).TotalMilliseconds;

            double cpuUsage = 0;

            if (elapsedMs > 0)
            {
                cpuUsage = (cpuUsedMs / (Environment.ProcessorCount * elapsedMs)) * 100;
                cpuUsage = Math.Min(cpuUsage, 100);
            }

            _previousCpuTime = totalCpuTime;
            _previousCheckTime = currentTime;

            return Task.FromResult(new SystemMetricsDto
            {
                CpuUsage = Math.Round(cpuUsage, 2),
                MemoryUsage = Math.Round(totalMemory / (1024.0 * 1024.0), 2)
            });
        }
    }
}
