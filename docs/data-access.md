# Data access — one abstraction, three databases

The central idea of this repo: the domain and service layer never know which
database they run on. A request can be served by SQL Server/SQLite (EF Core),
MongoDB, or embedded RavenDB by swapping the provider behind one manager.

## Abstractions (`DataAccess.Core`)

- `DataModelBase` / `IDataModelBase` — the base every persisted entity derives
  from; gives all providers a uniform contract (identity, change application).
- Depends only on `SharedCodeLibrary` (`PagingParameters`, `IInputDto`,
  `IOutputDto`, `IChangeAppliable`).

## Domain models (`DataAccess`)

The Car aggregate, modeled as abstract bases with provider-agnostic
relationships:

| Entity | File | Purpose |
|---|---|---|
| `CarBase` → `Car` | `Models/CarBase.cs` | Core entity; holds a `Users` list |
| `CarUser` | `Models/CarUser.cs` | Person (email, names, date of birth); owns a `Cars` list |
| `CarDocumentBase` | `Models/CarDocumentBase.cs` | Document attached to a car |
| `CarDocumentHistory` | `Models/CarDocumentHistory.cs` | Audit trail of document changes |
| `CarPictureBase` | `Models/CarPictureBase.cs` | Picture linked to a car and its user |

## Providers

| Project | Tech | Notes |
|---|---|---|
| `DataAccess.Sql` | EF Core 3.1 (SqlServer + SQLite) | Full migrations for the Car schema (`Migrations/`) |
| `DataAccess.MongoDb` | MongoDB.Driver | Document-mapped equivalents of the same models |
| `DateAccess.RavenDb` | RavenDB Embedded 5.1 | Runs in-process; browsable via the admin endpoint (see [api.md](api.md)) |

Each provider references only `DataAccess.Core` + `DataAccess` — providers do
not know about each other.

## Orchestration

- **`DataAccess.Manager`** references all three providers and decides which
  implementation backs a given service.
- **`DataServiceProvider`** is what consumers reference: an Autofac module wires
  the manager, AutoMapper maps domain models to the DTOs in
  `MapperSharedLibrary`, and per-entity **service factories** (one per aggregate
  entity) hand out CRUD services.

## Trying it

`TestingConsole` and the two `WpfUi.*DataServicesLoader` projects consume
`DataServiceProvider` directly; `FakerSharedLibrary` generates seed data so the
providers can be exercised without real input.
