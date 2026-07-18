# Orizon UI

## Theme Manager

O Orizon UI inclui personalizacao em tempo real com oito temas oficiais, cor primaria, modo, densidade, sidebar, tipografia, bordas, sombras e animacoes. Use `OrizonAppearance` para aplicar preferencias sem refresh e conecte os endpoints de persistencia da aplicacao consumidora. Consulte [docs/Sprint-4.2-Aparencia](docs/Sprint-4.2-Aparencia/README.md).

Orizon UI is a reusable ASP.NET Core Razor Class Library for MVC/Razor applications. It provides the Orizon Platform MVP design system, admin layout, Tag Helpers, CSS tokens, JavaScript behaviors, dark mode, responsive styles, and a Sandbox catalog for visual validation.

## MVP Status

Version target: `1.0.0-mvp`

This release candidate is intended for local package validation and controlled MVP adoption. It is not automatically published to NuGet.org.

## Requirements

- .NET 8 SDK
- ASP.NET Core MVC or Razor Pages application

## Repository Structure

- `src/Orizon.UI`: Razor Class Library with Tag Helpers, shared layouts, CSS, JavaScript, and static web assets.
- `samples/Orizon.UI.Sandbox`: MVC Sandbox catalog used for visual and integration validation.

## MVP Features

- Design tokens, typography, utilities, dark mode, and responsive foundations
- Admin layout with sidebar, topbar, breadcrumb, and footer
- Components: Buttons, Cards, Alerts, Badges, Forms, Tables, Modal, Dropdown, Tabs, Pagination, Avatar, Spinner, Progress, Empty State, and Toast
- Sandbox catalog at `/components`
- Keyboard and screen-reader accessibility patterns for interactive components

## Run the Sandbox

```bash
dotnet restore
dotnet build Orizon.UI.sln --configuration Release
dotnet run --project samples/Orizon.UI.Sandbox
```

Open `/components` in the Sandbox application.

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

Add the local source and install the MVP package:

```bash
dotnet nuget add source /path/to/Orizon-UI/artifacts/packages --name OrizonLocal
dotnet add package Orizon.UI --version 1.0.0-mvp --source /path/to/Orizon-UI/artifacts/packages
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

## Dark Mode

Dark mode is controlled by the `data-theme` attribute on the HTML element. The bundled theme script toggles between light and dark when the admin layout theme button is used.

```html
<html lang="en" data-theme="light">
```

## Contributing

When adding or changing components:

1. Keep public APIs stable and named consistently.
2. Use existing CSS tokens and component naming conventions.
3. Add JavaScript only for required interaction.
4. Validate keyboard navigation, focus states, dark mode, and mobile layouts.
5. Add or update Sandbox examples only when they improve validation.

## License

Package metadata currently uses the `MIT` license expression. Confirm repository licensing before publishing this package publicly.
