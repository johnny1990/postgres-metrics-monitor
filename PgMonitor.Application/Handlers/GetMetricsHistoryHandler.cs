using MediatR;
using PgMonitor.Application.Queries;
using PgMonitor.Domain.Entities;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Application.Handlers
{
    public class GetMetricsHistoryHandler
        : IRequestHandler<GetMetricsHistoryQuery, List<DatabaseMetric>>
    {
        private readonly IMetricsRepository _repository;

        public GetMetricsHistoryHandler(IMetricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DatabaseMetric>> Handle(
            GetMetricsHistoryQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetHistoryAsync(request.Count);
        }
    }
}
