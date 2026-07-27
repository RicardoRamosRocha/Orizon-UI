using Orizon.UI.Builders;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;
using Orizon.UI.Models.Widgets.Dashboard.DashboardHero;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Models.Widgets.Dashboard.QuickActions;
using Orizon.UI.Templates.SDK;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Templates.Dashboard;

/// <summary>
/// Provides the default, domain-neutral dashboard template.
/// </summary>
public static class DefaultDashboardTemplate
{
    public const string Name = "default";

    public static DashboardTemplateModel Create()
    {
        return new DashboardTemplateBuilder()
            .UseResponsiveLayout(ResponsiveBreakpoint.Desktop)
            .AddZone(LayoutZone.Hero, configure: options =>
            {
                options.Columns = 1;
                options.Order = 0;
            })
            .AddZone(LayoutZone.Main, configure: options =>
            {
                options.Columns = 3;
                options.Order = 1;
            })
            .AddZone(LayoutZone.Bottom, configure: options =>
            {
                options.Columns = 2;
                options.Order = 2;
            })
            .ConfigureBreakpoint(ResponsiveBreakpoint.Tablet, options => options.Columns = 2)
            .ConfigureBreakpoint(ResponsiveBreakpoint.Mobile, options =>
            {
                options.Columns = 1;
                options.Stack = true;
            })
            .ConfigureBreakpoint(ResponsiveBreakpoint.Compact, options =>
            {
                options.Columns = 1;
                options.Stack = true;
            })
            .UseManifest(DefaultDashboardTemplateFactory.CreateManifest())
            .Header(
                "Dashboard",
                "Visão geral",
                "Template padrão para composição de dashboards reutilizáveis.")
            .Catalog(
                displayName: "Dashboard Padrão",
                category: "Dashboard",
                version: "1.0.0",
                author: "Orizon UI")
            .Tags("dashboard", "enterprise", "reutilizável", "responsivo")
            .LayoutName("DashboardLayout")
            .PlaceWidget(new DashboardHeroWidgetModel { Id = "default-hero" }, LayoutZone.Hero, "Hero", 0, 100)
            .PlaceWidget(new KpiWidgetModel { Id = "default-kpi" }, LayoutZone.Main, "KPIs", 1, 90)
            .PlaceWidget(new QuickActionsWidgetModel { Id = "default-actions" }, LayoutZone.Main, "Quick Actions", 2, 80)
            .PlaceWidget(new ActivityFeedWidgetModel { Id = "default-activity" }, LayoutZone.Bottom, "Recent Activity", 3, 70)
            .BuildLayout()
            .Build();
    }
}
