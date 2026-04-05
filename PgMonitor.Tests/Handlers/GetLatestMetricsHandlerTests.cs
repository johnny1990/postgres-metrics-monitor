using Moq;
using PgMonitor.Application.Handlers;
using PgMonitor.Application.Queries;
using PgMonitor.Domain.Entities;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Tests.Handlers
{
    public class GetLatestMetricsHandlerTests : IDisposable
    {
        private Mock<IMetricsRepository> _repoMock;
        private GetLatestMetricsHandler _handler;
        public GetLatestMetricsHandlerTests()
        {
            _repoMock = new Mock<IMetricsRepository>();
            _handler = new GetLatestMetricsHandler(_repoMock.Object);
        }
        public void Dispose()
        {
            _repoMock = null;
            _handler = null;
        }

        [Test]
        public async Task Should_Return_Latest_Metric()
        {

            // Arrange
            _repoMock.Setup(r => r.GetLatestAsync())
                    .ReturnsAsync(new DatabaseMetric
                    {
                        CpuUsage = 5,
                        MemoryUsage = 100
                    });

            var expected = new DatabaseMetric
            {
                CpuUsage = 5,
                MemoryUsage = 100
            };

            _repoMock.Setup(r => r.GetLatestAsync())
                    .ReturnsAsync(expected);

            var handler = _handler;

            // Act
            var result = await handler.Handle(new GetLatestMetricsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result.CpuUsage, Is.EqualTo(5));
        }

        [Test]
        public async Task Should_Throw_Exception_When_Query_Is_Null()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
        }

        [Test]
        public async Task Should_Return_Null_When_No_Metrics()
        {
            // Arrange
            _repoMock.Setup(r => r.GetLatestAsync()).ReturnsAsync((DatabaseMetric?)null);

            // Act
            var result = await _handler.Handle(new GetLatestMetricsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
