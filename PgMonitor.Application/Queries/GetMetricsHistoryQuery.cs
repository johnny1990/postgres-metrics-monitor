using MediatR;
using PgMonitor.Domain.Entities;

namespace PgMonitor.Application.Queries
{
    public record GetMetricsHistoryQuery : IRequest<List<DatabaseMetric>>
    {
        public int Count { get; set; }

        public GetMetricsHistoryQuery(int count)
        {
            Count = count;
        }
    }
}
