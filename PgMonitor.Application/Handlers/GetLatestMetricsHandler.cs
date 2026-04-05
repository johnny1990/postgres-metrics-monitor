using MediatR;
using PgMonitor.Application.Queries;
using PgMonitor.Domain.Entities;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Application.Handlers
{
    public class GetLatestMetricsHandler : IRequestHandler<GetLatestMetricsQuery, DatabaseMetric?>
    {
        private readonly IMetricsRepository _repository;

        public GetLatestMetricsHandler(IMetricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<DatabaseMetric?> Handle(GetLatestMetricsQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            return await _repository.GetLatestAsync();
        }
    }
}
