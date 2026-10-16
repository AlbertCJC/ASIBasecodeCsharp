# AGENTS.md

ASP.NET Core MVC educational scaffold (ASI Bridge/JumpStart). Targets `net9.0`, no tests, no CI, no linter — `dotnet build ASI.Basecode.sln` is the only verification gate.

## Build

- Requires the .NET 9 SDK. On this machine the standalone `dotnet` CLI is .NET 8 and fails with `NETSDK1045`; build via Visual Studio 2022 (open `ASI.Basecode.sln`) unless a .NET 9 SDK is installed.
- Set `ASI.Basecode.WebApp` as the startup project. It runs on SQL Server via the `DefaultConnection` connection string in `ASI.Basecode.WebApp/appsettings.json`.

## Architecture

Four projects, dependency flow: `WebApp → Services → Data → Resources` (Services and Data also reference Resources directly). `Resources` is the leaf — never reference other projects from it.

- `ASI.Basecode.WebApp` — controllers + Razor views. Controllers must stay thin; push logic down to Services.
- `ASI.Basecode.Services` — business logic, view models, managers. Services extend `ServiceBase`.
- `ASI.Basecode.Data` — EF Core repositories and `AsiBasecodeDBContext` only; no business logic. Repositories extend generic `BaseRepository`.
- `ASI.Basecode.Resources` — `Const`/`Enums`, resx message/label resources for localization.

## Wiring conventions (easy to miss)

- `Program.cs` does not configure the app directly: `Startup.cs` is a `partial StartupConfigurer` split across `Startup.DI.cs`, `Startup.AutoMapper.cs`, and `Startup.Logger.cs`. Register new services/repositories in `Startup.DI.cs` (`ConfigureOtherServices`); add AutoMapper mappings in the profile inside `Startup.AutoMapper.cs`.
- New controllers derive from `ASI.Basecode.WebApp.Mvc.ControllerBase<T>` (constructor needs `IHttpContextAccessor`, `ILoggerFactory`, `IConfiguration`, `IMapper`) — see `HomeController` for the pattern.
- `AsiBasecodeDBContext` is `partial`; keep entity config in `OnModelCreating` and don't fight the `OnModelCreatingPartial` hook.
- Web login/auth was intentionally stripped in Oct 2026; `appsettings.json` has no `TokenAuthentication` section and WebApp references no auth packages. Don't re-add JWT/bootstrap auth wiring unless asked.

## Notes

- Empty `Models/`/`Repositories/`/`Services/` subfolders are kept via `.gitkeep` files — don't delete them.
- `wwwroot/lib` contains committed vendored libraries (bootstrap, jquery); don't replace with CDN links.
- README.md documents the same layering rules; trust both, but executable code and csproj files win on any conflict.
