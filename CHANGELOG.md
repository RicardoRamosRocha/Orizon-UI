# Changelog

## [1.0.0-rc.1] - 2026-07-21

### Changed

- Package metadata completed for NuGet validation, including SourceLink, symbols, deterministic builds, project URL, icon, README, license, and XML documentation output.
- Installation, versioning, themes, components, Studio, Patterns, Templates, and contribution guidance reviewed for the 1.0 release candidate.
- XML documentation added to the central Enterprise Grid provider contracts and icon registry without changing public signatures.

### Validated

- Release build with zero errors and zero warnings.
- Existing automated Enterprise Grid suite.
- Local `.nupkg` and `.snupkg` generation.
- Clean ASP.NET Core MVC consumer using only the package's Tag Helpers and static web assets.
- Light/dark tokens, responsive breakpoints, keyboard patterns, focus-visible states, reduced motion, and ARIA state handling.

### Compatibility

- No public API, Tag Helper name, documented token, CSS class, or runtime behavior was intentionally changed.

## Theme Manager - pre-release history

- Oito temas oficiais e seis cores primarias semanticas.
- Runtime `OrizonAppearance` com preview instantaneo, LocalStorage e sincronizacao remota.
- Densidade, sidebar, tipografia, radius, sombras e movimento configuraveis por tokens.
- Tag Helpers `orizon-theme-card`, `orizon-appearance-option` e `orizon-preview-panel`.
- Catalogo e documentacao da Sprint 4.2.

## [1.0.0-mvp] - 2026-07-15

### Added

- Design System foundations with tokens, typography, utilities, and shared styling.
- Admin Layout with sidebar, topbar, breadcrumb, footer, responsive behavior, and dark mode.
- Core components: Buttons, Cards, Alerts, and Badges.
- Form components: Input, Select, and Textarea.
- Table component with responsive scrolling and display variants.
- Navigation and feedback components: Modal, Dropdown, Tabs, Pagination, Avatar, Spinner, Progress, Empty State, and Toast.
- Sandbox catalog for component validation and visual review.

### Improved

- Accessibility coverage for semantic markup, ARIA attributes, focus states, keyboard navigation, and alert/toast messaging.
- Production polish for CSS imports, static assets, JavaScript initialization, and README usage guidance.

### Notes

- Package version: `1.0.0-mvp`.
- NuGet package generation is local only; no automatic publication is performed.
