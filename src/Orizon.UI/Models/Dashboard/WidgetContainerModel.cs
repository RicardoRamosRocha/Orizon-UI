using System.Collections.ObjectModel;
using Orizon.UI.Models.Widgets;

namespace Orizon.UI.Models.Dashboard;

/// <summary>
/// Provides presentation options around any <see cref="WidgetModelBase"/>.
/// </summary>
public sealed class WidgetContainerModel
{
    public required WidgetModelBase Widget { get; init; }
    public DashboardSpacing Spacing { get; set; } = DashboardSpacing.Default;
    public DashboardSpacing Padding { get; set; } = DashboardSpacing.Default;
    public WidgetContainerHeight Height { get; set; } = WidgetContainerHeight.Auto;
    public bool ShowHeader { get; set; }
    public bool ShowToolbar { get; set; }
    public string LoadingMessage { get; set; } = "Carregando conteúdo";
    public string EmptyMessage { get; set; } = "Nenhum conteúdo disponível";
    public string ErrorMessage { get; set; } = "Não foi possível carregar o conteúdo";
    public Collection<WidgetAction> ToolbarActions { get; } = [];

    internal string WidgetComponentName =>
        Widget.GetType().Name.EndsWith("Model", StringComparison.Ordinal)
            ? Widget.GetType().Name[..^"Model".Length]
            : Widget.GetType().Name;
}
