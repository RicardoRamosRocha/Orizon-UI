using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Templates;
using Orizon.UI.Templates.Dashboard;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Templates.SDK;

/// <summary>
/// Provides the catalog metadata for the enterprise ERP dashboard.
/// </summary>
public static class ERPDashboardManifest
{
    public static DashboardTemplateManifest Create()
    {
        var manifest = new DashboardTemplateManifest
        {
            Name = ERPDashboardTemplate.Name,
            DisplayName = "Enterprise ERP Dashboard",
            Category = "ERP",
            Description = "Template corporativo para sistemas ERP.",
            Version = "1.0.0",
            Author = "Orizon UI",
            LayoutName = "DashboardLayout",
            PreviewColor = "#1d4ed8",
            Featured = true,
            BaseTemplate = ExecutiveDashboardTemplate.Name,
            SupportsComposition = true,
            Composable = true,
            SupportsResponsiveLayout = true,
            DefaultBreakpoint = ResponsiveBreakpoint.Desktop,
            PreferredDensity = "Comfortable"
        };
        manifest.SupportedThemes.Add(DashboardTheme.Light);
        manifest.SupportedThemes.Add(DashboardTheme.Dark);
        manifest.SupportedThemes.Add(DashboardTheme.Corporate);
        manifest.Widgets.Add("Dashboard Hero");
        manifest.Widgets.Add("KPI");
        manifest.Widgets.Add("Quick Actions");
        manifest.Widgets.Add("Activity Feed");
        manifest.InheritedWidgets.Add("Dashboard Hero");
        manifest.InheritedWidgets.Add("KPI");
        manifest.InheritedWidgets.Add("Quick Actions");
        manifest.InheritedWidgets.Add("Activity Feed");
        foreach (var breakpoint in Enum.GetValues<ResponsiveBreakpoint>())
        {
            manifest.SupportedBreakpoints.Add(breakpoint);
        }
        foreach (var zone in new[]
        {
            LayoutZone.Hero, LayoutZone.Main, LayoutZone.Left,
            LayoutZone.Center, LayoutZone.Right, LayoutZone.Bottom
        })
        {
            manifest.DefaultZones.Add(zone);
        }
        foreach (var region in new[]
        {
            "Hero", "KPIs", "Quick Actions", "Sales Overview",
            "Inventory Overview", "Financial Summary", "Recent Activity", "Tasks"
        })
        {
            manifest.Regions.Add(region);
        }
        manifest.Tags.Add("ERP");
        manifest.Tags.Add("Enterprise");
        manifest.Tags.Add("Dashboard");
        return manifest;
    }
}
