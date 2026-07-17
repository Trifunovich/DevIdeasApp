# Getting started

## Prerequisites

- **.NET 5 SDK** (the API and most consumers target `net5.0`; .NET 5 is
  end-of-life, so treat this repo as a reference/sandbox rather than a
  production baseline).
- Optional, provider-dependent: SQL Server (or nothing — SQLite works), MongoDB.
  RavenDB is embedded and needs no install.
- `WpfUi.NF.DataServicesLoader` additionally needs .NET Framework 4.7.2
  (Windows only).

## Build

```
dotnet restore DevIdeasApp.sln
dotnet build DevIdeasApp.sln
```

(The WPF projects require Windows; everything else builds anywhere.)

## Configure

`WebApi/appsettings.json`:

- **`JWT`** — fill `ValidAudience`, `ValidIssuer`, `Secret`, `RefreshSecret`,
  `Expires`, `RefreshExpires` with your own values. Never commit real secrets.
- **`Serilog`** — the file sinks point at a local log directory
  (`F:\Logs\...` by default); adjust the paths for your machine or remove the
  file sinks and keep console logging.

Database migrations (SQL provider and auth context) apply via EF Core tooling
(`dotnet ef database update`) or on first run, depending on provider selection.

## Run

```
dotnet run --project WebApi
```

- Swagger UI: `https://localhost:<port>/swagger`
- Health: `/health/ready`, `/health/live`
- Smoke test: `/weatherforecast`

Register a user via `POST /authentication/register`, log in via
`POST /authentication/login`, then call the entity endpoints (`/car`,
`/caruser`, …) with the bearer token — every data route is `[Authorize]`-gated.

## Other entry points

- `TestingConsole` — exercise the service layer without HTTP.
- `WpfUi.DataServicesLoader` / `WpfUi.NF.DataServicesLoader` — desktop clients
  over the same services (seed data via `FakerSharedLibrary`).
