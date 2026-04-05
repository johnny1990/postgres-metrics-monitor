using MediatR;

namespace PgMonitor.Application.Commands
{
    public record SaveMetricsCommand(double CpuUsage, double MemoryUsage) : IRequest;
}
