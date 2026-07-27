using Orizon.UI.Models.Designer;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Studio.Designer.Commands;

public sealed class InsertWidgetCommand : IDesignerCommand
{
    private readonly DesignerCanvas _canvas;
    private readonly WidgetPlacement _placement;
    private readonly int _order;

    public InsertWidgetCommand(DesignerCanvas canvas, WidgetPlacement placement, int order)
    {
        _canvas = canvas;
        _placement = placement;
        _order = order;
    }

    public string Name => "Insert widget";
    public void Execute() => _canvas.Insert(_placement, _order);
    public void Undo() => _canvas.Remove(_placement);
}
