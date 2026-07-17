# DevIdeasApp

A .NET backend architecture sandbox. The domain (a car registry: cars, users,
documents, pictures, document history) is deliberately simple — the point of the
repo is the **backend patterns** built around it:

- **Pluggable multi-database data access** — one abstraction, three interchangeable
  providers: EF Core (SQL Server / SQLite), MongoDB, and embedded RavenDB.
  See [docs/data-access.md](docs/data-access.md).
- **Generic CRUD API** — one generic controller base gives every entity a full
  REST surface (paged reads, file-based create, error probes) with per-entity
  controllers reduced to a few lines. See [docs/api.md](docs/api.md).
- **Self-contained auth library** — ASP.NET Core Identity + JWT with refresh
  tokens, packaged as a reusable project. See [docs/auth.md](docs/auth.md).
- **Multiple consumers of the same service layer** — Web API, WPF (both .NET 5
  and .NET Framework 4.7.2), Blazor experiments, and a console runner all sit on
  the same `DataServiceProvider`.

## Layout at a glance

| Area | Projects |
|---|---|
| API | `WebApi` (ASP.NET Core, net5.0) |
| Service layer | `DataServiceProvider` (Autofac, AutoMapper, service factories) |
| Data access | `DataAccess.Core`, `DataAccess` (domain models), `DataAccess.Sql`, `DataAccess.MongoDb`, `DateAccess.RavenDb`, `DataAccess.Manager` |
| Auth | `AuthenticationLibrary` (Identity + JWT + refresh tokens) |
| Shared | `SharedCodeLibrary`, `MapperSharedLibrary` (DTOs), `FakerSharedLibrary` (test data), `LoggerLibrary`, `LoggingLibrary` |
| Consumers | `TestingConsole`, `WpfUi.DataServicesLoader`, `WpfUi.NF.DataServicesLoader` |
| Auth experiments | `BlazorAuthApp`, `Blazor.Auth2`, `WebAppWithAuth` |
| Tests / scratch | `DataAccess.Core.Test`, `DataAccess.Text`, `DataAccessServices.Test`, `WebApiTemplate` |

## Docs

- [docs/getting-started.md](docs/getting-started.md) — build, configure, run
- [docs/architecture.md](docs/architecture.md) — solution structure and layering
- [docs/data-access.md](docs/data-access.md) — the multi-provider data-access design
- [docs/api.md](docs/api.md) — endpoints and request pipeline
- [docs/auth.md](docs/auth.md) — authentication library and token flow
