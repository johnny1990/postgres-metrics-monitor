using MediatR;
using PgMonitor.Application.Commands;
using PgMonitor.Domain.Entities;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Application.Handlers
{
    public class SaveMetricsHandler : IRequestHandler<SaveMetricsCommand>
    {
        private readonly IMetricsRepository _repository;

        public SaveMetricsHandler(IMetricsRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SaveMetricsCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var metric = new DatabaseMetric
            {
                Id = Guid.NewGuid(),
                CpuUsage = request.CpuUsage,
                MemoryUsage = request.MemoryUsage,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(metric);
        }
    }
}
