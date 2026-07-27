using Orizon.UI.Models.Designer;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Studio.Designer.Commands;

public sealed class ReorderWidgetCommand : MoveWidgetCommand
{
    public ReorderWidgetCommand(
        DesignerCanvas canvas,
        WidgetPlacement placement,
        LayoutZone zone,
        string region,
        int order) : base(canvas, placement, zone, region, order) { }

    public override string Name => "Reorder widget";
}
