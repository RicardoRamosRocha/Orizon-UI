using Orizon.UI.Builders;
using Orizon.UI.Models.Dashboard;
using Orizon.UI.Models.Widgets;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Templates.Dashboard;

/// <summary>
/// Composes the presentation-only sections used by the enterprise ERP dashboard.
/// </summary>
public static class ERPDashboardTemplate
{
    public const string Name = "erp";

    public static DashboardTemplateBuilder Compose(DashboardTemplateBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var composition = builder
            .UseBaseTemplate(ExecutiveDashboardTemplate.Create())
            .Inherit()
            .Header("Enterprise ERP Dashboard", "Visão corporativa integrada")
            .Layout(layout =>
            {
                layout.Fluid = true;
                layout.Spacing = DashboardSpacing.Spacious;
                layout.ShowHeader = true;
                layout.ShowFooter = true;
            })
            .ClearRegion("Sales Overview")
            .ClearRegion("Inventory Overview")
            .ClearRegion("Financial Summary")
            .ClearRegion("Tasks");

        PlaceWidgets(composition, "Sales Overview", LayoutZone.Main, 4, CreateSalesOverview());
        PlaceWidgets(composition, "Inventory Overview", LayoutZone.Main, 7, CreateInventoryOverview());
        PlaceWidgets(composition, "Financial Summary", LayoutZone.Main, 10, CreateFinancialSummary());
        composition.PlaceWidget(CreateTasks(), LayoutZone.Bottom, "Tasks", 8, 20);

        return composition
            .Compose()
            .Footer("Orizon UI · Enterprise ERP Dashboard")
            .BuildLayout();
    }

    private static WidgetModelBase[] CreateSalesOverview() =>
    [
        Kpi("erp-sales-month", "Vendas no mês", "R$ 742 mil", "+12,6%", "positive", "cart"),
        Kpi("erp-average-ticket", "Ticket médio", "R$ 403", "+2,8%", "positive", "credit-card"),
        Kpi("erp-open-orders", "Pedidos em aberto", "96", "18 prioritários", "neutral", "clipboard")
    ];

    private static WidgetModelBase[] CreateInventoryOverview() =>
    [
        Kpi("erp-stock-available", "Disponibilidade", "98,2%", "Dentro da meta", "positive", "packages"),
        Kpi("erp-stock-low", "Estoque baixo", "32 itens", "Requer atenção", "negative", "alert-triangle"),
        Kpi("erp-stock-transit", "Em trânsito", "1.460 itens", "12 recebimentos", "neutral", "truck")
    ];

    private static WidgetModelBase[] CreateFinancialSummary() =>
    [
        Kpi("erp-receivables", "Contas a receber", "R$ 392 mil", "Próximos 30 dias", "neutral", "arrow-down"),
        Kpi("erp-payables", "Contas a pagar", "R$ 214 mil", "Próximos 30 dias", "neutral", "arrow-up"),
        Kpi("erp-margin", "Margem operacional", "18,7%", "+1,3 p.p.", "positive", "trend-up")
    ];

    private static ActivityFeedWidgetModel CreateTasks()
    {
        var widget = new ActivityFeedWidgetModel
        {
            Id = "erp-tasks",
            Title = "Tarefas",
            Subtitle = "Lista demonstrativa de acompanhamento"
        };
        widget.Items.Add(Activity("clipboard", "Revisar previsão mensal", "Conferir os valores fictícios apresentados no dashboard.", "Hoje", "warning", 14, 0));
        widget.Items.Add(Activity("packages", "Validar inventário", "Revisar a amostra estática do estoque.", "Amanhã", "info", 16, 0));
        widget.Items.Add(Activity("chart-line", "Compartilhar resumo", "Apresentar o dashboard demonstrativo à equipe.", "Planejado", "primary", 17, 30));

        return widget;
    }

    private static KpiWidgetModel Kpi(
        string id,
        string title,
        string value,
        string trend,
        string trendType,
        string icon) => new()
        {
            Id = id,
            Title = title,
            Value = value,
            Trend = trend,
            TrendType = trendType,
            Icon = icon
        };

    private static ActivityItem Activity(
        string icon,
        string title,
        string description,
        string badge,
        string color,
        int hour,
        int minute) => new()
        {
            Icon = icon,
            Title = title,
            Description = description,
            Badge = badge,
            Color = color,
            Timestamp = new DateTimeOffset(2026, 7, 27, hour, minute, 0, TimeSpan.Zero)
        };

    private static void PlaceWidgets(
        DashboardTemplateBuilder builder,
        string region,
        LayoutZone zone,
        int order,
        IEnumerable<WidgetModelBase> widgets)
    {
        foreach (var widget in widgets)
        {
            builder.PlaceWidget(
                widget,
                zone,
                region,
                order++,
                priority: 60,
                hiddenOn: widget.Id is "erp-payables"
                    ? [ResponsiveBreakpoint.Compact]
                    : null);
        }
    }
}
