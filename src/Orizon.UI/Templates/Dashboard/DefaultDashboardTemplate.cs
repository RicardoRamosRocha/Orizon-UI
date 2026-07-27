using Orizon.UI.Builders;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;
using Orizon.UI.Models.Widgets.Dashboard.DashboardHero;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Models.Widgets.Dashboard.QuickActions;

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
            .Named(Name)
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
            .Hero(new DashboardHeroWidgetModel())
            .Kpis(new KpiWidgetModel())
            .QuickActions(new QuickActionsWidgetModel())
            .ActivityFeed(new ActivityFeedWidgetModel())
            .Build();
    }
}
