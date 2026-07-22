namespace Orizon.UI.Sandbox.Models;

public sealed record StudioSearchItem(string Title, string Kind, string Url, string Keywords = "");

public sealed record StudioIconItem(string Name, string Category);

public sealed record StudioComponentDocumentation(
    string Slug,
    string Name,
    string Description,
    string Razor,
    string Html,
    string Css,
    string CSharp,
    IReadOnlyList<string> Anatomy,
    IReadOnlyList<string> Variations,
    IReadOnlyList<string> States,
    IReadOnlyDictionary<string, string> Properties,
    IReadOnlyList<string> Accessibility);

public sealed class StudioPageViewModel
{
    public required string Section { get; init; }
    public IReadOnlyList<StudioSearchItem> SearchItems { get; init; } = [];
    public IReadOnlyList<StudioComponentDocumentation> Components { get; init; } = [];
    public StudioComponentDocumentation? Component { get; init; }
    public IReadOnlyList<StudioIconItem> Icons { get; init; } = [];
}
