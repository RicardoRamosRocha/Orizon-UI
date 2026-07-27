using Orizon.UI.Models.Layout;
using Orizon.UI.Models.Templates;

namespace Orizon.UI.Models.Designer;

/// <summary>Tracks the responsive breakpoint used by the designer preview.</summary>
public sealed class DesignerViewport
{
    public ResponsiveBreakpoint Current { get; private set; } = ResponsiveBreakpoint.Desktop;
    public IReadOnlyCollection<ResponsiveBreakpoint> Available { get; private set; } = Enum.GetValues<ResponsiveBreakpoint>();
    public double ScrollLeft { get; private set; }
    public double ScrollTop { get; private set; }

    public void Configure(DashboardTemplateManifest? manifest)
    {
        Available = manifest?.SupportedBreakpoints.Count > 0
            ? manifest.SupportedBreakpoints.ToArray()
            : Enum.GetValues<ResponsiveBreakpoint>();
        Current = manifest?.DefaultBreakpoint is { } preferred && Available.Contains(preferred)
            ? preferred : Available.First();
    }

    public void Change(ResponsiveBreakpoint breakpoint)
    {
        if (!Available.Contains(breakpoint))
            throw new ArgumentOutOfRangeException(nameof(breakpoint), breakpoint, "The loaded template does not support this viewport.");
        Current = breakpoint;
    }

    public void PreserveScroll(double left, double top)
    {
        ScrollLeft = left;
        ScrollTop = top;
    }
}
