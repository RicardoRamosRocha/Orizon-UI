using Orizon.UI.Models.Layout;
using Orizon.UI.Models.Widgets;

namespace Orizon.UI.Models.Designer;

/// <summary>Read-only canvas projection of a registered widget and its placement.</summary>
public sealed class DesignerWidget
{
    public required IWidgetModel Model { get; init; }
    public WidgetPlacement? Placement { get; init; }
    public string Id => Model.Id;
    public string Name => Model.GetType().Name.Replace("WidgetModel", string.Empty);
}
