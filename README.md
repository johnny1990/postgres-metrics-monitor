A .NET 10 backend application that monitors CPU and memory usage and stores metrics in a PostgreSQL database.
Built using Clean Architecture, CQRS, and MediatR, with a background worker for continuous data collection.

   Features
-Collects CPU and memory usage at regular intervals
-Stores metrics in PostgreSQL using Entity Framework Core
-Implements CQRS pattern with MediatR
-Background worker service for real-time monitoring
-REST API for retrieving metrics:
-Latest metric
-Historical data (last N records)
-Resilient process monitoring with safe handling of restricted OS processes

  Architecture

The solution follows a layered architecture:

PgMonitor.Api           → Web API (Controllers)
PgMonitor.Application   → CQRS (Commands, Queries, Handlers)
PgMonitor.Domain        → Entities
PgMonitor.Infrastructure→ Repositories, Services
PgMonitor.Persistence   → DbContext & Migrations
PgMonitor.Worker        → Background Service

  Tech Stack
.NET 10
ASP.NET Core Web API
Entity Framework Core
PostgreSQL
MediatR (CQRS)
Background Services (IHostedService)
Docker-ready (optional)

  Setup & Run
1. Clone repository
git clone <your-repo-url>
cd pg-monitoring-service
2. Configure database

Update connection string in:

PgMonitor.Persistence/appsettings.json

Example:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=pgmonitor;Username=postgres;Password=yourpassword"
}
3. Apply migrations

Using Package Manager Console:

Update-Database -Project PgMonitor.Persistence -StartupProject PgMonitor.Persistence
4. Run application

Set multiple startup projects:

PgMonitor.Api → Start
PgMonitor.Worker → Start
📡 API Endpoints
🔹 Get latest metric
GET /api/Metrics/latest

Response:

{
  "cpuUsage": 12.5,
  "memoryUsage": 180,
  "createdAt": "2026-04-06T10:20:00Z"
}
🔹 Get metrics history
GET /api/Metrics/history?count=50

Returns last N records ordered by newest.

    Notes on CPU Monitoring
On Windows, PostgreSQL runs as a system service with restricted access
Direct process monitoring may result in Access Denied errors
The application safely skips inaccessible processes to ensure stability
   
   Testing

Unit tests done cover:

CQRS handlers
Repository behavior
Metrics processing logic

   Design Decisions
CQRS pattern for separation of concerns
MediatR for request handling
Background worker for continuous data collection
Repository pattern for abstraction
Defensive programming for OS-level monitoring limitations

   Repository
https://github.com/johnny1990/postgres-metrics-monitor
