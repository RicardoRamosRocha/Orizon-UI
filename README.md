# Orizon UI

## Theme Manager

O Orizon UI inclui personalizacao em tempo real com oito temas oficiais, cor primaria, modo, densidade, sidebar, tipografia, bordas, sombras e animacoes. Use `OrizonAppearance` para aplicar preferencias sem refresh e conecte os endpoints de persistencia da aplicacao consumidora. Consulte [docs/Sprint-4.2-Aparencia](docs/Sprint-4.2-Aparencia/README.md).

Orizon UI is a reusable ASP.NET Core Razor Class Library for MVC/Razor applications. It provides the Orizon Platform MVP design system, admin layout, Tag Helpers, CSS tokens, JavaScript behaviors, dark mode, responsive styles, and a Sandbox catalog for visual validation.

## Release Candidate Status

Current package version: `1.0.0-rc.1`; stable target: `1.0.0`.

This release candidate is intended for package validation and controlled adoption before the official `1.0.0` publication. Repository builds never publish automatically to NuGet.org.

## Requirements

- .NET 8 SDK
- ASP.NET Core MVC or Razor Pages application

## Repository Structure

- `src/Orizon.UI`: Razor Class Library with Tag Helpers, shared layouts, CSS, JavaScript, and static web assets.
- `samples/Orizon.UI.Sandbox`: MVC Sandbox catalog used for visual and integration validation.

## Included areas

- Design tokens, typography, utilities, dark mode, and responsive foundations
- Admin layout with sidebar, topbar, breadcrumb, and footer
- Components, advanced forms, data components, charts, productivity components, and Enterprise Grid
- Patterns for CRUD, dashboard, authentication, wizard, search, master/detail, data entry, approvals, Kanban, and analytics
- Application templates for admin, CRUD, forms, authentication, analytics, Kanban, master/detail, settings, profile, and errors
- Studio tools for component and icon exploration, playground, theme building, and layout building
- Sandbox catalog for component, pattern, template, and integration validation
- Static web assets and Tag Helpers packaged for direct consumer use
- Keyboard and screen-reader accessibility patterns for interactive components

## Run the Sandbox

```bash
dotnet restore
dotnet build Orizon.UI.sln --configuration Release
dotnet run --project samples/Orizon.UI.Sandbox
```

Open the Sandbox root and use its sidebar to navigate Foundations, Components, Data, Patterns, Templates, and Studio.

## Install with ProjectReference

Add a project reference from your ASP.NET Core app to `src/Orizon.UI/Orizon.UI.csproj`.

```xml
<ProjectReference Include="..\path\to\Orizon-UI\src\Orizon.UI\Orizon.UI.csproj" />
```

## Install from a Local NuGet Package

Generate the package:

```bash
dotnet pack src/Orizon.UI/Orizon.UI.csproj --configuration Release --output artifacts/packages
```

Add the local source and install the release candidate package:

```bash
dotnet nuget add source /path/to/Orizon-UI/artifacts/packages --name OrizonLocal
dotnet add package Orizon.UI --version 1.0.0-rc.1 --source /path/to/Orizon-UI/artifacts/packages
```

## Register Tag Helpers

Add this to `_ViewImports.cshtml`:

```razor
@addTagHelper *, Orizon.UI
```

## Reference CSS and JavaScript

Use the Razor Class Library static web assets path:

```html
<link rel="stylesheet" href="~/_content/Orizon.UI/css/orizon.css" />
<script type="module" src="~/_content/Orizon.UI/js/orizon.js"></script>
```

## Minimal Button

```razor
<orizon-button variant="primary" icon="ph ph-plus">
    New item
</orizon-button>
```

## Minimal Form

```razor
<form class="orizon-form">
    <orizon-input label="Name" name="name" required="true" hint="Enter the full name." />
    <orizon-select label="Plan" name="plan">
        <option value="starter">Starter</option>
        <option value="business">Business</option>
    </orizon-select>
    <orizon-textarea label="Notes" name="notes"></orizon-textarea>
    <div class="orizon-form-actions">
        <orizon-button type="submit">Save</orizon-button>
    </div>
</form>
```

## Admin Layout

Use the shared layout from the RCL in MVC views:

```razor
@{
    Layout = "_AdminLayout";
    ViewData["Title"] = "Dashboard";
}
```

The layout expects the Orizon UI CSS and JavaScript assets to be available through static web assets.

## Themes

Theme selection is controlled by `data-theme`; light and dark color modes use `data-color-mode`. The bundled theme runtime persists supported appearance preferences without requiring consumers to copy Sandbox files.

```html
<html lang="en" data-theme="light">
```

## Component documentation

Component and integration guides are maintained under [`docs/`](docs/). Start with:

- [Design System Base](docs/Design-System-Base/01-Visao-Geral.md)
- [Advanced Forms](docs/Advanced-Forms/README.md)
- [Data Components](docs/Data-Components/README.md)
- [Dashboard and Charts](docs/Dashboard-Charts/README.md)
- [Productivity Components](docs/Productivity-Components/README.md)
- [Enterprise Grid](docs/Enterprise-Grid/README.md)
- [Application Templates](docs/Application-Templates/README.md)

Patterns and Studio are interactive documentation areas in the Sandbox. Templates demonstrate complete application compositions; they are examples and are not copied into consuming projects.

## Updating versions

Use an explicit package version when updating, review [`CHANGELOG.md`](CHANGELOG.md), rebuild the consumer, and verify static web assets. Pre-release versions such as `1.0.0-rc.1` are not selected automatically when a stable version constraint is used.

## Contributing


See [`CONTRIBUTING.md`](CONTRIBUTING.md). During the 1.0 feature freeze:

1. Keep public APIs stable and named consistently.
2. Use existing CSS tokens and component naming conventions.
3. Add JavaScript only for required interaction.
4. Validate keyboard navigation, focus states, dark mode, and mobile layouts.
5. Add or update Sandbox examples only when they improve validation.

## License

Orizon UI is distributed under the [MIT License](LICENSE).
