using Orizon.UI.Builders;
using Orizon.UI.Factories.Templates;
using Orizon.UI.Models.Designer;
using Orizon.UI.Models.Layout;
using Orizon.UI.Models.Templates;
using Orizon.UI.Models.Widgets;
using Orizon.UI.Registry;
using Orizon.UI.Studio.Designer.Commands;

namespace Orizon.UI.Studio.Designer;

/// <summary>Coordinates registry data and the render-only visual designer state.</summary>
public sealed class VisualTemplateDesignerModel
{
    private readonly DashboardTemplateRegistry _registry;

    public VisualTemplateDesignerModel(DashboardTemplateRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        Toolbox.Load(_registry.GetAll(), _registry.GetFactories());
    }

    public DesignerCanvas Canvas { get; } = new();
    public DesignerSelection Selection { get; } = new();
    public DesignerPropertyPanel Properties { get; } = new();
    public DesignerToolbox Toolbox { get; } = new();
    public DesignerHistory History { get; } = new();
    public DesignerViewport Viewport { get; } = new();
    public DesignerDragState Drag { get; } = new();
    public IReadOnlyCollection<DashboardTemplateModel> Templates => _registry.GetAll();
    public IReadOnlyCollection<ITemplateFactory> Factories => _registry.GetFactories();

    public DashboardTemplateModel LoadTemplate(string name) { var template = _registry.Create(name); Load(template); return template; }

    public DashboardTemplateModel LoadFactory(string name)
    {
        var factory = _registry.GetFactories().FirstOrDefault(item =>
            string.Equals(item.GetManifest().Name, name, StringComparison.OrdinalIgnoreCase))
            ?? throw new KeyNotFoundException($"Template factory '{name}' is not registered.");
        var template = factory.Create(); Load(template); return template;
    }

    public DashboardTemplateManifest LoadManifest(string name)
    {
        var manifest = _registry.GetManifest(name)
            ?? throw new KeyNotFoundException($"Template manifest '{name}' is not registered.");
        Properties.Manifest = manifest; Viewport.Configure(manifest); return manifest;
    }

    public void ChangeViewport(ResponsiveBreakpoint breakpoint)
    {
        var selectedId = Selection.Widget?.Id;
        Viewport.Change(breakpoint);
        if (Canvas.Template?.Context is { } context)
        {
            context.CurrentBreakpoint = breakpoint;
            Canvas.Recalculate();
        }
        RestoreSelection(selectedId);
        Properties.Responsive = Canvas.Template?.Context;
    }

    public void BeginDrag(string widgetId, DesignerDragSource source = DesignerDragSource.Canvas) =>
        Drag.Begin(widgetId, source);

    public void EndDrag() => Drag.End();

    public void DropWidget(string zoneName, int order)
    {
        if (!Drag.IsDragging || Drag.WidgetId is null)
            throw new InvalidOperationException("A drag operation has not started.");
        if (Drag.Source == DesignerDragSource.Canvas)
            MoveWidget(Drag.WidgetId, zoneName, order);
        else
            DuplicateWidget(Drag.WidgetId, zoneName, order);
        EndDrag();
    }

    public void MoveWidget(string widgetId, string zoneName, int order)
    {
        var placement = Canvas.FindPlacement(widgetId);
        var zone = FindZone(zoneName);
        IDesignerCommand command = placement.Zone == zone.Kind &&
            string.Equals(placement.Region, zone.Name, StringComparison.OrdinalIgnoreCase)
            ? new ReorderWidgetCommand(Canvas, placement, zone.Kind, zone.Name, order)
            : new MoveWidgetCommand(Canvas, placement, zone.Kind, zone.Name, order);
        Execute(command, widgetId);
    }

    public void InsertWidget(IWidgetModel widget, string zoneName, int order)
    {
        ArgumentNullException.ThrowIfNull(widget);
        var zone = FindZone(zoneName);
        var placement = new WidgetPlacement { Widget = widget, Zone = zone.Kind, Region = zone.Name };
        Execute(new InsertWidgetCommand(Canvas, placement, order), widget.Id);
    }

    public void DeleteWidget(string widgetId)
    {
        var placement = Canvas.FindPlacement(widgetId);
        History.Execute(new DeleteWidgetCommand(Canvas, placement));
        Selection.Clear();
        Properties.Widget = null;
        Snapshot("Delete widget");
    }

    public void DuplicateWidget(string widgetId, string zoneName, int order) =>
        InsertWidget(CloneWidget(Canvas.FindPlacement(widgetId).Widget), zoneName, order);

    public IDesignerCommand? Undo()
    {
        var selectedId = Selection.Widget?.Id;
        var command = History.Undo();
        RestoreSelection(selectedId);
        Snapshot("Undo");
        return command;
    }

    public IDesignerCommand? Redo()
    {
        var selectedId = Selection.Widget?.Id;
        var command = History.Redo();
        RestoreSelection(selectedId);
        Snapshot("Redo");
        return command;
    }

    public bool CanUndo() => History.CanUndo;
    public bool CanRedo() => History.CanRedo;

    public void SelectWidget(string widgetId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(widgetId);
        var widget = Canvas.Widgets.FirstOrDefault(item => string.Equals(item.Id, widgetId, StringComparison.OrdinalIgnoreCase))
            ?? throw new KeyNotFoundException($"Widget '{widgetId}' is not present on the canvas.");
        Selection.SelectWidget(widget); Properties.Widget = widget;
    }

    public void SelectZone(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var zone = Canvas.Zones.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
            ?? throw new KeyNotFoundException($"Zone '{name}' is not present on the canvas.");
        Selection.SelectZone(zone); Properties.Widget = null;
    }

    public void Refresh()
    {
        if (Canvas.Template is null) return;
        var selectedId = Selection.Widget?.Id;
        Canvas.Recalculate();
        Properties.Responsive = Canvas.Template.Context;
        Properties.Composition = Canvas.Template.Composition;
        RestoreSelection(selectedId);
    }

    private void Load(DashboardTemplateModel template)
    {
        var manifest = template.Manifest ?? LoadManifest(template.Name);
        Properties.Manifest = manifest; Viewport.Configure(manifest); Canvas.Load(template);
        Properties.Responsive = template.Context; Properties.Composition = template.Composition;
        Selection.SelectTemplate(template); History.Snapshot($"Loaded {template.Name}", template);
    }

    private DesignerZone FindZone(string name) =>
        Canvas.Zones.FirstOrDefault(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
        ?? throw new KeyNotFoundException($"Zone '{name}' is not present on the canvas.");

    private void Execute(IDesignerCommand command, string widgetId)
    {
        History.Execute(command);
        SelectWidget(widgetId);
        Snapshot(command.Name);
    }

    private void RestoreSelection(string? widgetId)
    {
        if (widgetId is not null && Canvas.Widgets.Any(item => item.Id == widgetId))
            SelectWidget(widgetId);
        else if (Canvas.Template is not null)
        {
            Selection.SelectTemplate(Canvas.Template);
            Properties.Widget = null;
        }
    }

    private void Snapshot(string description)
    {
        if (Canvas.Template is not null) History.Snapshot(description, Canvas.Template);
    }

    private static IWidgetModel CloneWidget(IWidgetModel source)
    {
        if (Activator.CreateInstance(source.GetType()) is not WidgetModelBase clone)
            throw new InvalidOperationException($"Widget '{source.GetType().Name}' cannot be duplicated.");
        if (source is WidgetModelBase original)
        {
            clone.Id = $"{original.Id}-copy-{Guid.NewGuid():N}"[..(original.Id.Length + 14)];
            clone.Title = original.Title;
            clone.Subtitle = original.Subtitle;
            clone.CssClass = original.CssClass;
            clone.Visible = original.Visible;
            clone.Size = original.Size;
            clone.Theme = original.Theme;
            clone.State = original.State;
        }
        return clone;
    }
}
