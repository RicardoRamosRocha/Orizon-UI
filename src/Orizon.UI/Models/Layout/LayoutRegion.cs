namespace Orizon.UI.Models.Layout;

/// <summary>
/// Associates a named template region with a responsive layout zone.
/// </summary>
public sealed class LayoutRegion
{
    public string Name { get; set; } = string.Empty;
    public LayoutZone Zone { get; set; } = LayoutZone.Main;
    public ResponsiveLayoutOptions Options { get; set; } = new();
    public IDictionary<ResponsiveBreakpoint, ResponsiveLayoutOptions> Breakpoints { get; } =
        new Dictionary<ResponsiveBreakpoint, ResponsiveLayoutOptions>();
}
