using Moq;
using PgMonitor.Application.Commands;
using PgMonitor.Application.Handlers;
using PgMonitor.Domain.Entities;
using PgMonitor.Infrastructure.Contracts;

namespace PgMonitor.Tests.Handlers
{
    public class SaveMetricsHandlerTests : IDisposable
    {
        private Mock<IMetricsRepository> _repoMock;
        private SaveMetricsHandler _handler;

        public SaveMetricsHandlerTests()
        {
            _repoMock = new Mock<IMetricsRepository>();
            _handler = new SaveMetricsHandler(_repoMock.Object);
        }
        public void Dispose()
        {
            _repoMock = null;
            _handler = null;

        }

        [Test]
        public async Task Should_Save_Metric()
        {
            // Arrange
            _repoMock.Setup(r => r.AddAsync(It.IsAny<DatabaseMetric>())).Returns(Task.CompletedTask);

            var command = new SaveMetricsCommand(10, 200);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repoMock.Verify(r => r.AddAsync(It.IsAny<DatabaseMetric>()), Times.Once);
        }

        [Test]
        public async Task Should_Throw_Exception_When_Command_Is_Null()
        {
           // Act & Assert
           Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
        }

    }
}
