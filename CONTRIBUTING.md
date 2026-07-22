# Contributing to Orizon UI

Orizon UI 1.0 is under feature freeze. Contributions to the release candidate must improve stability, documentation, accessibility, compatibility, testing, or packaging without changing public behavior.

## Development setup

Requirements:

- .NET 8 SDK as selected by `global.json`.
- PowerShell 7+ for repository validation scripts.

Validate a change from the repository root:

```powershell
dotnet restore Orizon.UI.sln
dotnet build Orizon.UI.sln --configuration Release
dotnet test Orizon.UI.sln --configuration Release --no-build
```

Run the catalog when a visual check is needed:

```powershell
dotnet run --project samples/Orizon.UI.Sandbox
```

## Release-candidate rules

- Preserve public type, member, Tag Helper, attribute, token, CSS class, event, and static asset names.
- Do not introduce components, dependencies, or behaviors without a separate post-1.0 proposal.
- Prefer targeted fixes backed by a failing test, accessibility finding, package validation, or reproducible defect.
- Treat design tokens and documented defaults as public contracts.
- Keep keyboard, focus, reduced-motion, light/dark theme, and responsive behavior intact.

## Pull requests

Describe the observed problem, the smallest safe correction, compatibility impact, and validation performed. Include screenshots only when visual output changes as part of an approved defect fix.

Every pull request must complete a Release build with zero warnings and pass all automated tests. Changes to public API require explicit maintainer approval and a compatibility plan.

## Documentation

Update `README.md`, `CHANGELOG.md`, or the relevant file under `docs/` whenever installation, usage, supported behavior, or release notes are affected. Public .NET contracts should include XML documentation; Tag Helper usage belongs in the component guides.

## Security

Do not open public issues containing credentials, access tokens, customer data, or undisclosed vulnerabilities. Contact the maintainers privately through the repository owner for security-sensitive reports.
