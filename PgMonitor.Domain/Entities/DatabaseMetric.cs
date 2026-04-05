namespace PgMonitor.Domain.Entities
{
    public class DatabaseMetric
    {
        public Guid Id { get; set; }

        public double CpuUsage { get; set; }      // %
        public double MemoryUsage { get; set; }   // MB

        public DateTime CreatedAt { get; set; }
    }
}
