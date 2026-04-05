using MediatR;
using PgMonitor.Domain.Entities;

namespace PgMonitor.Application.Queries
{
    public record GetLatestMetricsQuery() : IRequest<DatabaseMetric?>;
}
