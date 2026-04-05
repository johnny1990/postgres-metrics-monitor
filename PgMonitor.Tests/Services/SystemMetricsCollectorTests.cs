using Moq;
using PgMonitor.Infrastructure.DTOs;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Tests.Services
{
    public class SystemMetricsCollectorTests : IDisposable
    {
        private Mock<ISystemMetricsCollectorRepository> _repoMock;

        public SystemMetricsCollectorTests()
        {   
            _repoMock = new Mock<ISystemMetricsCollectorRepository>();
        }
        public void Dispose()
        {
            _repoMock = null;
        }

        [Test]
        public async Task Should_Return_Metrics_Without_Exception()
        {
            // Arrange
            var expectedMetrics = new SystemMetricsDto(); 
            _repoMock.Setup(r => r.GetMetricsAsync()).ReturnsAsync(expectedMetrics);

            // Act
            var result = await _repoMock.Object.GetMetricsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Should_Return_Correct_Metrics()
        {
            // Arrange
            var expectedMetrics = new SystemMetricsDto
            {
                CpuUsage = 50.0,
                MemoryUsage = 2048,
            };
            _repoMock.Setup(r => r.GetMetricsAsync()).ReturnsAsync(expectedMetrics);

            // Act
            var result = await _repoMock.Object.GetMetricsAsync();

            // Assert
            Assert.That(result.CpuUsage, Is.EqualTo(expectedMetrics.CpuUsage));
            Assert.That(result.MemoryUsage, Is.EqualTo(expectedMetrics.MemoryUsage));
        }

    }
}
