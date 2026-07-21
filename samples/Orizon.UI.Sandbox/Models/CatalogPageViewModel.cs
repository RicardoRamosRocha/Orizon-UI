namespace Orizon.UI.Sandbox.Models;

public sealed record CatalogPageViewModel(
    string Category,
    string Slug,
    string Title,
    string Description,
    IReadOnlyList<string> Sections);
