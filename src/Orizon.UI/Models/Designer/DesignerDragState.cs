namespace Orizon.UI.Models.Designer;

public enum DesignerDragSource { Canvas, Toolbox }

/// <summary>Tracks drag source, hover target, preview and indicator state.</summary>
public sealed class DesignerDragState
{
    public bool IsDragging { get; private set; }
    public string? WidgetId { get; private set; }
    public DesignerDragSource Source { get; private set; }
    public string? HoverZone { get; private set; }
    public int? DropIndex { get; private set; }

    public void Begin(string widgetId, DesignerDragSource source)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(widgetId);
        IsDragging = true; WidgetId = widgetId; Source = source;
    }

    public void Hover(string zone, int index)
    {
        if (!IsDragging) throw new InvalidOperationException("A drag operation has not started.");
        HoverZone = zone; DropIndex = index;
    }

    public void End()
    {
        IsDragging = false; WidgetId = null; HoverZone = null; DropIndex = null;
    }
}
