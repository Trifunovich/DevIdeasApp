# Authentication

`AuthenticationLibrary` (net5.0) is a self-contained auth project: ASP.NET Core
Identity for user storage plus JWT bearer tokens with refresh-token rotation.
`WebApi` consumes it via `AddAuthenticationLibraryInternals` in `Startup`.

## Pieces

- **`AuthenticationUserModel`** — the Identity user
  (`AspNetCore.Identity.EntityFrameworkCore`).
- **`ApplicationDbContext`** — `IdentityDbContext<AuthenticationUserModel>` plus
  a `DbSet<RefreshModel>` for issued refresh tokens. Own EF migrations
  (`Migrations/`: initial schema + refresh-token revisions).
- **`IAuthenticationService`** — issues/validates the JWT pair.

## Token flow

1. `POST /authentication/login` — credentials → access token + refresh token.
2. `POST /authentication/refresh` — valid refresh token → new pair (rotation is
   tracked in `RefreshModel`).
3. `DELETE /authentication/logout` (`[Authorize]`) — revokes the refresh token.
4. `POST /authentication/register` / `POST /authentication/register/admin` —
   user / admin registration.

Role-gated endpoints use standard `[Authorize(Roles = ...)]` (see
`ManagerPanelController`, admin-only).

## Configuration

The `JWT` section of `WebApi/appsettings.json` (values are per-deployment;
supply your own — do not commit real secrets):

| Key | Meaning |
|---|---|
| `ValidAudience` / `ValidIssuer` | Standard JWT validation parameters |
| `Secret` | Access-token signing key |
| `RefreshSecret` | Refresh-token signing key |
| `Expires` / `RefreshExpires` | Lifetimes of the two tokens |

## Related experiments

`BlazorAuthApp`, `Blazor.Auth2`, and `WebAppWithAuth` are standalone hosts for
trying auth flows in Blazor/Razor UIs; they don't reference the service layer.
