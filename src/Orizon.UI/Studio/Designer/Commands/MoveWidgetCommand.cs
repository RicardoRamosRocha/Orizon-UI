using Orizon.UI.Models.Designer;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Studio.Designer.Commands;

public class MoveWidgetCommand : IDesignerCommand
{
    private readonly DesignerCanvas _canvas;
    private readonly WidgetPlacement _placement;
    private readonly LayoutZone _targetZone;
    private readonly string _targetRegion;
    private readonly int _targetOrder;
    private readonly LayoutZone _sourceZone;
    private readonly string _sourceRegion;
    private readonly int _sourceOrder;

    public MoveWidgetCommand(
        DesignerCanvas canvas,
        WidgetPlacement placement,
        LayoutZone targetZone,
        string targetRegion,
        int targetOrder)
    {
        _canvas = canvas;
        _placement = placement;
        _targetZone = targetZone;
        _targetRegion = targetRegion;
        _targetOrder = targetOrder;
        _sourceZone = placement.Zone;
        _sourceRegion = placement.Region ?? placement.Zone.ToString();
        _sourceOrder = placement.Order ?? 0;
    }

    public virtual string Name => "Move widget";
    public void Execute() => _canvas.Move(_placement, _targetZone, _targetRegion, _targetOrder);
    public void Undo() => _canvas.Move(_placement, _sourceZone, _sourceRegion, _sourceOrder);
}
