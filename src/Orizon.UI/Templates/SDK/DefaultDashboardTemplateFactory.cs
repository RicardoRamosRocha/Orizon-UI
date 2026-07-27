using Orizon.UI.Enums.Templates;
using Orizon.UI.Factories.Templates;
using Orizon.UI.Models.Templates;
using Orizon.UI.Templates.Dashboard;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Templates.SDK;

/// <summary>
/// Explicit factory for the domain-neutral default template.
/// </summary>
public sealed class DefaultDashboardTemplateFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() => DefaultDashboardTemplate.Create();

    public bool CanCreate() => true;

    public DashboardTemplateManifest GetManifest() => CreateManifest();

    public static DashboardTemplateManifest CreateManifest()
    {
        var manifest = new DashboardTemplateManifest
        {
            Name = DefaultDashboardTemplate.Name,
            DisplayName = "Dashboard Padrão",
            Category = "Dashboard",
            Description = "Template padrão para composição de dashboards reutilizáveis.",
            Version = "1.0.0",
            Author = "Orizon UI",
            PreviewColor = "#4f46e5",
            LayoutName = "DashboardLayout",
            MinimumVersion = "1.0.0",
            Featured = true,
            SupportsResponsiveLayout = true,
            DefaultBreakpoint = ResponsiveBreakpoint.Desktop,
            PreferredDensity = "Comfortable"
        };
        foreach (var breakpoint in Enum.GetValues<ResponsiveBreakpoint>())
        {
            manifest.SupportedBreakpoints.Add(breakpoint);
        }
        manifest.DefaultZones.Add(LayoutZone.Hero);
        manifest.DefaultZones.Add(LayoutZone.Main);
        manifest.DefaultZones.Add(LayoutZone.Bottom);
        manifest.Tags.Add("dashboard");
        manifest.Tags.Add("enterprise");
        manifest.Tags.Add("reutilizável");
        manifest.Tags.Add("responsivo");
        manifest.SupportedThemes.Add(DashboardTheme.Default);
        manifest.SupportedThemes.Add(DashboardTheme.Light);
        manifest.SupportedThemes.Add(DashboardTheme.Dark);
        manifest.Widgets.Add("Dashboard Hero");
        manifest.Widgets.Add("KPI");
        manifest.Widgets.Add("Quick Actions");
        manifest.Widgets.Add("Activity Feed");
        manifest.Metadata["sdk"] = "Template SDK";
        return manifest;
    }
}
