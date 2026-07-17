# Architecture

## Layering

```
WebApi (net5.0)                  WpfUi.* / TestingConsole / (Blazor experiments)
   │                                   │
   ├── AuthenticationLibrary           │
   └── DataServiceProvider ◄───────────┘
           │      (Autofac DI, AutoMapper, service factories)
           ▼
     DataAccess.Manager
           │      (selects/orchestrates a concrete provider)
           ▼
 ┌─────────────┬──────────────────┬──────────────────┐
 │ DataAccess.Sql │ DataAccess.MongoDb │ DateAccess.RavenDb │
 │ (EF Core:      │ (MongoDB.Driver)   │ (RavenDB Embedded) │
 │  SqlServer +   │                    │                    │
 │  SQLite)       │                    │                    │
 └─────────────┴──────────────────┴──────────────────┘
           │
           ▼
   DataAccess (domain models: Car, CarUser, CarDocument, CarPicture,
               CarDocumentHistory) + DataAccess.Core (base abstractions)
```

Everything below `WebApi` targets **netstandard2.0**, so the same service layer
is consumed by net5.0 hosts *and* the .NET Framework 4.7.2 WPF client.

## Key projects

| Project | Role |
|---|---|
| `DataAccess.Core` | Base abstractions: `DataModelBase`, `IDataModelBase`; depends only on `SharedCodeLibrary` |
| `DataAccess` | Domain models (Car aggregate) built on the core abstractions |
| `DataAccess.Sql` | EF Core 3.1 provider (SQL Server + SQLite), schema migrations included |
| `DataAccess.MongoDb` | MongoDB provider (official driver) |
| `DateAccess.RavenDb` | Embedded RavenDB provider *(project name typo is historical)* |
| `DataAccess.Manager` | References all providers; orchestrates which implementation serves a request |
| `DataServiceProvider` | The composition root for consumers: Autofac container, AutoMapper profiles, per-entity CRUD service factories |
| `MapperSharedLibrary` | Input/output DTOs (`Car`, `CarUser`, `CarDocument`, `CarPicture`, `CarDocumentHistory`) |
| `SharedCodeLibrary` | Cross-cutting types: `PagingParameters`, `IInputDto`, `IOutputDto`, `IChangeAppliable` |
| `AuthenticationLibrary` | Identity + JWT + refresh tokens (own `DbContext` + migrations) — see [auth.md](auth.md) |
| `FakerSharedLibrary` | Fake/seed data generation for consumers and tests |

## Consumers

- **`WebApi`** — the primary host; REST + Swagger + health checks ([api.md](api.md)).
- **`TestingConsole`** — minimal console consumer of `DataServiceProvider`.
- **`WpfUi.DataServicesLoader`** (net5.0-windows) and **`WpfUi.NF.DataServicesLoader`**
  (.NET Framework 4.7.2) — desktop clients exercising the same service layer,
  proving the netstandard2.0 contract from two runtimes.
- **`BlazorAuthApp` / `Blazor.Auth2` / `WebAppWithAuth`** — standalone auth
  experiments; not wired to the service layer.

## Tests / scratch projects

`DataAccess.Core.Test`, `DataAccessServices.Test` (service-layer tests via the
faker), `DataAccess.Text` (SQL provider scratch tests), `WebApiTemplate`
(netcoreapp3.1 template kept for comparison).
