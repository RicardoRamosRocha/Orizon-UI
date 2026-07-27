using Orizon.UI.Models.Designer;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Studio.Designer.Commands;

public sealed class DeleteWidgetCommand : IDesignerCommand
{
    private readonly DesignerCanvas _canvas;
    private readonly WidgetPlacement _placement;
    private readonly int _order;

    public DeleteWidgetCommand(DesignerCanvas canvas, WidgetPlacement placement)
    {
        _canvas = canvas;
        _placement = placement;
        _order = placement.Order ?? 0;
    }

    public string Name => "Delete widget";
    public void Execute() => _canvas.Remove(_placement);
    public void Undo() => _canvas.Insert(_placement, _order);
}
