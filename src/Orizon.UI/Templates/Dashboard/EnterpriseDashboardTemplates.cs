using Orizon.UI.Builders;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Dashboard;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;
using Orizon.UI.Models.Widgets.Dashboard.DashboardHero;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Models.Widgets.Dashboard.QuickActions;
using Orizon.UI.Models.Widgets;
using Orizon.UI.Models.Layout;
using Orizon.UI.Templates.SDK;

namespace Orizon.UI.Templates.Dashboard;

public static class ExecutiveDashboardTemplate
{
    public const string Name = "executive";

    public static DashboardTemplateModel Create() =>
        EnterpriseResponsiveLayout.Configure(new DashboardTemplateBuilder())
            .UseManifest(EnterpriseDashboardManifests.Executive())
            .Header("Executive Dashboard", "Indicadores executivos")
            .PlaceWidget(
                new DashboardHeroWidgetModel
                {
                    Id = "executive-hero",
                    Title = "Visão executiva",
                    Subtitle = "Indicadores corporativos",
                    Description = "Estrutura-base reutilizável para dashboards de domínio."
                },
                LayoutZone.Hero,
                "Hero",
                order: 0,
                priority: 100)
            .PlaceWidget(Kpi("executive-performance", "Performance", "94%", "positive"), LayoutZone.Main, "KPIs", 0, 100)
            .PlaceWidget(Kpi("executive-targets", "Metas", "18/20", "positive"), LayoutZone.Main, "KPIs", 1, 90)
            .PlaceWidget(Kpi("executive-attention", "Pontos de atenção", "4", "neutral"), LayoutZone.Main, "KPIs", 2, 80)
            .PlaceWidget(QuickActions(), LayoutZone.Main, "Quick Actions", 3, 70)
            .PlaceWidget(Kpi("executive-sales", "Resultado comercial", "R$ 980 mil", "positive"), LayoutZone.Main, "Sales Overview", 4, 60)
            .PlaceWidget(Kpi("executive-operations", "Eficiência operacional", "96,4%", "positive"), LayoutZone.Main, "Inventory Overview", 5, 50)
            .PlaceWidget(Kpi("executive-finance", "Resultado consolidado", "R$ 312 mil", "positive"), LayoutZone.Main, "Financial Summary", 6, 40)
            .PlaceWidget(ActivityFeed("executive-activity", "Atualização executiva"), LayoutZone.Bottom, "Recent Activity", 7, 30)
            .PlaceWidget(ActivityFeed("executive-tasks", "Pendências executivas"), LayoutZone.Bottom, "Tasks", 8, 20)
            .BuildLayout()
            .Build();

    private static KpiWidgetModel Kpi(
        string id,
        string title,
        string value,
        string trendType) => new()
        {
            Id = id,
            Title = title,
            Value = value,
            TrendType = trendType
        };

    private static QuickActionsWidgetModel QuickActions()
    {
        var widget = new QuickActionsWidgetModel
        {
            Id = "executive-quick-actions",
            Title = "Ações rápidas"
        };
        widget.Actions.Add(new WidgetAction { Text = "Abrir resumo", Icon = "dashboard", Url = "#" });
        widget.Actions.Add(new WidgetAction { Text = "Ver indicadores", Icon = "chart-line", Url = "#" });
        return widget;
    }

    private static ActivityFeedWidgetModel ActivityFeed(string id, string title)
    {
        var widget = new ActivityFeedWidgetModel { Id = id, Title = title };
        widget.Items.Add(new ActivityItem
        {
            Icon = "activity",
            Title = "Resumo atualizado",
            Description = "Conteúdo estático do template executivo.",
            Timestamp = new DateTimeOffset(2026, 7, 27, 9, 0, 0, TimeSpan.Zero)
        });
        return widget;
    }
}

public static class OperationsDashboardTemplate
{
    public const string Name = "operations";

    public static DashboardTemplateModel Create() =>
        EnterpriseResponsiveLayout.Configure(new DashboardTemplateBuilder())
            .UseManifest(EnterpriseDashboardManifests.Operations())
            .Header("Operations Dashboard", "Acompanhamento operacional")
            .PlaceWidget(new KpiWidgetModel { Id = "operations-throughput", Title = "Throughput" }, LayoutZone.Main, "KPIs", 0, 100)
            .PlaceWidget(new KpiWidgetModel { Id = "operations-quality", Title = "Qualidade" }, LayoutZone.Main, "KPIs", 1, 90)
            .PlaceWidget(new ActivityFeedWidgetModel { Id = "operations-activity", Title = "Atividade operacional" }, LayoutZone.Bottom, "Recent Activity", 2, 80)
            .UsesWidgets("Timeline", "Widget Container")
            .BuildLayout()
            .Build();
}

public static class AnalyticsDashboardTemplate
{
    public const string Name = "analytics";

    public static DashboardTemplateModel Create() =>
        EnterpriseResponsiveLayout.Configure(new DashboardTemplateBuilder())
            .UseManifest(EnterpriseDashboardManifests.Analytics())
            .Header("Analytics Dashboard", "Métricas compostas com componentes existentes")
            .PlaceWidget(new KpiWidgetModel { Id = "analytics-primary", Title = "Métrica principal" }, LayoutZone.Main, "KPIs", 0, 100)
            .PlaceWidget(new KpiWidgetModel { Id = "analytics-secondary", Title = "Métrica secundária" }, LayoutZone.Main, "KPIs", 1, 90)
            .PlaceWidget(new KpiWidgetModel { Id = "analytics-trend", Title = "Tendência" }, LayoutZone.Main, "KPIs", 2, 80)
            .PlaceWidget(new QuickActionsWidgetModel { Id = "analytics-actions", Title = "Ações analíticas" }, LayoutZone.Bottom, "Quick Actions", 3, 70)
            .BuildLayout()
            .Build();
}

public static class WorkspaceDashboardTemplate
{
    public const string Name = "workspace";

    public static DashboardTemplateModel Create() =>
        EnterpriseResponsiveLayout.Configure(new DashboardTemplateBuilder())
            .UseManifest(EnterpriseDashboardManifests.Workspace())
            .Header("Workspace Dashboard", "Área de trabalho reutilizável")
            .PlaceWidget(new DashboardHeroWidgetModel { Id = "workspace-hero", Title = "Workspace" }, LayoutZone.Hero, "Hero", 0, 100)
            .PlaceWidget(new QuickActionsWidgetModel { Id = "workspace-actions", Title = "Ações rápidas" }, LayoutZone.Main, "Quick Actions", 1, 90)
            .PlaceWidget(new ActivityFeedWidgetModel { Id = "workspace-activity", Title = "Atividade recente" }, LayoutZone.Bottom, "Recent Activity", 2, 80)
            .UsesWidgets("Widget Container")
            .BuildLayout()
            .Build();
}

public static class BlankDashboardTemplate
{
    public const string Name = "blank";

    public static DashboardTemplateModel Create() =>
        EnterpriseResponsiveLayout.Configure(new DashboardTemplateBuilder())
            .UseManifest(EnterpriseDashboardManifests.Blank())
            .Header("Blank Dashboard", "Base mínima para novas composições")
            .Layout(layout =>
            {
                layout.Fluid = true;
                layout.Spacing = DashboardSpacing.Default;
            })
            .UsesWidgets(nameof(WidgetContainerModel))
            .BuildLayout()
            .Build();
}

internal static class EnterpriseResponsiveLayout
{
    public static DashboardTemplateBuilder Configure(DashboardTemplateBuilder builder) =>
        builder
            .UseResponsiveLayout(ResponsiveBreakpoint.Desktop)
            .AddZone(LayoutZone.Hero, "Hero", options =>
            {
                options.Columns = 1;
                options.Order = 0;
                options.Priority = 100;
            })
            .AddZone(LayoutZone.Main, "Main", options =>
            {
                options.Columns = 3;
                options.Order = 1;
                options.Gap = 3;
            })
            .AddZone(LayoutZone.Bottom, "Bottom", options =>
            {
                options.Columns = 2;
                options.Order = 2;
                options.Gap = 2;
            })
            .ConfigureBreakpoint(ResponsiveBreakpoint.Laptop, options =>
            {
                options.Columns = 3;
                options.Gap = 2;
            })
            .ConfigureBreakpoint(ResponsiveBreakpoint.Tablet, options =>
            {
                options.Columns = 2;
                options.Gap = 2;
            })
            .ConfigureBreakpoint(ResponsiveBreakpoint.Mobile, options =>
            {
                options.Columns = 1;
                options.Stack = true;
                options.Gap = 1;
            })
            .ConfigureBreakpoint(ResponsiveBreakpoint.Compact, options =>
            {
                options.Columns = 1;
                options.Stack = true;
                options.Gap = 1;
            });
}
