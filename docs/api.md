# Web API

`WebApi` (ASP.NET Core, net5.0) hosts the REST surface, Swagger, and health
checks.

## Request pipeline (`Startup`)

- **Serilog** structured logging (console + file sinks, enriched with machine /
  process / thread ids) — sink paths come from `appsettings.json`.
- **`RequestValidationMiddleware`** — request validation before MVC.
- Authentication + authorization (JWT bearer — see [auth.md](auth.md)).
- Health checks: `/health/ready` and `/health/live`.
- **Swagger UI** at `/swagger` (Swashbuckle, generated from controller XML
  summaries).

## Generic CRUD: `DataControllerBase`

All entity controllers derive from
`DataControllerBase<TServiceFactory, TInputDto, TOutputDto, TInputDtoFile>`
and inherit the full surface (all routes `[Authorize]`-gated):

| Verb | Route | Action |
|---|---|---|
| GET | `/{entity}` | List all |
| GET | `/{entity}/{id}` | Get by id |
| GET | `/{entity}` (paged) | Paged list via `PagingParameters` |
| POST | `/{entity}` | Add |
| POST | `/{entity}` (file) | Add from uploaded file |
| PUT | `/{entity}` | Update |
| DELETE | `/{entity}/{id}` | Delete |
| POST | `/{entity}/throw` | Deliberate error (exception-handling probe) |

A concrete controller is only the route attribute plus the generic base bound
to its entity's service factory and DTOs.

## Controllers

| Route | Controller | Notes |
|---|---|---|
| `/car` | `CarController` | Generic CRUD |
| `/caruser` | `CarUserController` | Generic CRUD |
| `/cardocument` | `CarDocumentController` | Generic CRUD |
| `/cardocumenthistory` | `CarDocumentHistoryController` | Generic CRUD |
| `/carpicture` | `CarPictureController` | Generic CRUD |
| `/authentication/*` | `AuthenticationController` | login, refresh, register, register/admin, logout — see [auth.md](auth.md) |
| `/managerpanel/getraven` | `ManagerPanelController` | `[Authorize(Roles = Admin)]`; opens the embedded RavenDB browser |
| `/weatherforecast` | `WeatherForecastController` | ASP.NET template sample, kept as a smoke endpoint |
