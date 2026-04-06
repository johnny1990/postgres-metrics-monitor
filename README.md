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
