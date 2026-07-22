namespace Orizon.UI.Sandbox.Models;

public sealed record PatternCodeSamples(string Razor, string Html, string Css, string CSharp);

public sealed record PatternPageViewModel(
    string Slug,
    string Title,
    string Description,
    string WhenToUse,
    string WhenToAvoid,
    IReadOnlyList<string> Benefits,
    IReadOnlyList<string> Components,
    IReadOnlyList<string> Flow,
    PatternCodeSamples Code)
{
    public static readonly IReadOnlyList<string> PracticeAreas =
    ["Acessibilidade", "Responsividade", "Performance", "UX", "Motion", "Keyboard Navigation", "Focus", "Empty States"];
}
