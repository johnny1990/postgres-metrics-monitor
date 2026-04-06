using MediatR;
using PgMonitor.Application.Commands;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Worker
{
    public class MetricsWorker : BackgroundService
    {
        private readonly ILogger<MetricsWorker> _logger;
        private readonly ISystemMetricsCollectorRepository _collector;

        private readonly IServiceProvider _serviceProvider;

        public MetricsWorker(
            ILogger<MetricsWorker> logger,
            ISystemMetricsCollectorRepository collector,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _collector = collector;
            _serviceProvider = serviceProvider;
        }

        // Metrics collection and saving loop
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Metrics Worker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var metrics = await _collector.GetMetricsAsync();

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        await mediator.Send(new SaveMetricsCommand(
                            metrics.CpuUsage,
                            metrics.MemoryUsage
                        ));
                    }

                    _logger.LogInformation(
                        "Saved metrics: CPU {cpu}% | Memory {mem} MB",
                        metrics.CpuUsage,
                        metrics.MemoryUsage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error collecting metrics");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
