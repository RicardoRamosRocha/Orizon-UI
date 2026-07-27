using Orizon.UI.Grid;
using Orizon.UI.Builders;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Dashboard;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;
using Orizon.UI.Registry;
using Orizon.UI.Models.Layout;
using Orizon.UI.Models.Designer;
using Orizon.UI.Studio.Designer;
using Orizon.UI.Studio.Designer.Commands;

var tests = new List<(string Name, Func<Task> Run)>
{
    ("Sorting", Sorting), ("Filtering", Filtering), ("Grouping", Grouping), ("Paging", Paging),
    ("Selection runtime", () => Runtime("enterprise-grid-selection.js", "selected", "ctrlKey")),
    ("Clipboard runtime", () => Runtime("enterprise-grid-selection.js", "clipboard", "paste")),
    ("Undo runtime", () => Runtime("enterprise-grid-advanced.js", "state.undo", "oldValue")),
    ("Redo runtime", () => Runtime("enterprise-grid-advanced.js", "state.redo", "newValue")),
    ("Virtualization runtime", () => Runtime("enterprise-grid-virtual.js", "overscan", "renderedColumnStart")),
    ("Batch runtime", () => Runtime("enterprise-grid-advanced.js", "orizon:grid-batch-save", "data-grid-batch")),
    ("Server Mode", ServerMode),
    ("Export XLSX", () => Runtime("enterprise-grid-xlsx.js", "openxmlformats", "0x04034b50")),
    ("Template composition", TemplateComposition),
    ("Template registry composition", TemplateRegistryComposition),
    ("Template slot capacity", TemplateSlotCapacity),
    ("Responsive breakpoint", ResponsiveBreakpointChange),
    ("Responsive widget placement", ResponsiveWidgetPlacement),
    ("Responsive zones", ResponsiveZones),
    ("Responsive order and priority", ResponsiveOrderAndPriority),
    ("Responsive visibility", ResponsiveVisibility),
    ("Responsive registry", ResponsiveRegistry),
    ("Composition and responsive", CompositionAndResponsive),
    ("Designer model", DesignerModel),
    ("Designer selection", DesignerSelection),
    ("Designer viewport", DesignerViewport),
    ("Designer registry integration", DesignerRegistryIntegration),
    ("Designer template loading", DesignerTemplateLoading),
    ("Designer manifest loading", DesignerManifestLoading),
    ("Designer drag", DesignerDrag),
    ("Designer drop", DesignerDrop),
    ("Designer move", DesignerMove),
    ("Designer undo", DesignerUndo),
    ("Designer redo", DesignerRedo),
    ("Designer commands", DesignerCommands),
    ("Designer placement", DesignerPlacement),
    ("Designer history", DesignerHistory),
    ("Designer canvas update", DesignerCanvasUpdate),
    ("Designer viewport preservation", DesignerViewportPreservation)
};
var passed = 0;
foreach (var test in tests) { try { await test.Run(); Console.WriteLine($"PASS {test.Name}"); passed++; } catch (Exception error) { Console.Error.WriteLine($"FAIL {test.Name}: {error.Message}"); } }
Console.WriteLine($"{passed}/{tests.Count} automated tests passed");
return passed == tests.Count ? 0 : 1;

static Row[] Data() => Enumerable.Range(1, 30).Select(i => new Row(i, $"Item {i:00}", i % 3 == 0 ? "A" : "B", i * 10m)).ToArray();
static async Task Sorting() { var result = await Provider(new() { Sorts = [new("Amount", "desc")], Take = 5 }); Assert(result.Items[0].Amount == 300m, "descending sort"); }
static async Task Filtering() { var result = await Provider(new() { Filters = [new() { Field = "Amount", Operator = OrizonGridFilterOperator.GreaterOrEqual, Value = 200 }], Take = 50 }); Assert(result.TotalCount == 11, "range filter"); var contains = await Provider(new() { Filters = [new() { Field = "Name", Operator = OrizonGridFilterOperator.Contains, Value = "Item 2" }], Take = 50 }); Assert(contains.TotalCount == 10, "contains filter"); }
static async Task Grouping() { var result = await Provider(new() { Groups = [new("Category")], Aggregates = [new("Amount", "sum")], Take = 50 }); Assert(result.Groups.Count == 2, "groups"); Assert(result.Groups.Sum(x => x.Count) == 30, "group counts"); }
static async Task Paging() { var result = await Provider(new() { Page = 2, PageSize = 5, Skip = 10, Take = 5 }); Assert(result.Items.Count == 5 && result.Items[0].Id == 11, "skip/take"); }
static async Task ServerMode() { var request = new OrizonGridDataRequest { Page = 1, PageSize = 10, Skip = 10, Take = 10, Search = "Item", Sorts = [new("Id", "asc")], SelectedColumns = ["Id", "Name"] }; var result = await Provider(request); Assert(result.TotalCount == 30 && result.Items.Count == 10 && result.ExecutionTime >= 0, "server result contract"); }
static Task TemplateComposition()
{
    var registry = new DashboardTemplateRegistry();
    var executive = registry.Create("executive");
    var erp = registry.Create("erp");
    Assert(erp.Composition?.BaseTemplate?.Name == "executive", "ERP base template");
    Assert(erp.Sections.Count == executive.Sections.Count, "base structure preserved");
    Assert(erp.Sections.Select(section => section.Name).SequenceEqual(executive.Sections.Select(section => section.Name)), "region order preserved");
    Assert(erp.Composition!.InheritedRegions.SequenceEqual(new[] { "Hero", "KPIs", "Quick Actions", "Recent Activity" }), "inherited regions");
    Assert(erp.Composition.ReplacedRegions.SequenceEqual(new[] { "Sales Overview", "Inventory Overview", "Financial Summary", "Tasks" }), "replaced regions");
    Assert(erp.Sections.Single(section => section.Name == "Hero").Widgets.Single().Id == "executive-hero", "hero inherited");
    Assert(erp.Sections.Single(section => section.Name == "Sales Overview").Widgets.Any(widget => widget.Id == "erp-sales-month"), "sales replaced");
    return Task.CompletedTask;
}
static Task TemplateRegistryComposition()
{
    var registry = new DashboardTemplateRegistry();
    Assert(registry.SupportsComposition("executive"), "executive composition support");
    Assert(registry.GetBaseTemplate("erp")?.Name == "executive", "registry base lookup");
    Assert(registry.GetDerivedTemplates("executive").Any(manifest => manifest.Name == "erp"), "registry derived lookup");
    Assert(registry.GetCompositionTree().Any(node =>
        node.Template.Name == "executive" &&
        node.DerivedTemplates.Any(derived => derived.Template.Name == "erp")), "composition tree");
    return Task.CompletedTask;
}
static Task TemplateSlotCapacity()
{
    var widgets = Enumerable.Range(1, 12)
        .Select(index => new KpiWidgetModel { Id = $"slot-widget-{index}" })
        .ToArray();
    var template = new DashboardTemplateBuilder()
        .Named("slot-test")
        .UseSlot("Metrics", TemplateRegion.Widgets, widgets)
        .Build();
    Assert(template.Context!.NamedSlots["Metrics"].Widgets.Count == 12, "slot accepts 0..N widgets");
    return Task.CompletedTask;
}
static Task ResponsiveBreakpointChange()
{
    var source = new DashboardTemplateRegistry().Create("erp");
    var desktop = AtBreakpoint(source, ResponsiveBreakpoint.Desktop);
    var tablet = AtBreakpoint(source, ResponsiveBreakpoint.Tablet);
    var mobile = AtBreakpoint(source, ResponsiveBreakpoint.Mobile);
    Assert(desktop.Sections.Single(section => section.Name == "KPIs").Columns == DashboardColumns.Three, "desktop columns");
    Assert(tablet.Sections.Single(section => section.Name == "KPIs").Columns == DashboardColumns.Two, "tablet columns");
    Assert(mobile.Sections.Single(section => section.Name == "KPIs").Columns == DashboardColumns.One, "mobile stack");
    Assert(tablet.Context!.CurrentBreakpoint == ResponsiveBreakpoint.Tablet, "current breakpoint");
    return Task.CompletedTask;
}
static Task ResponsiveWidgetPlacement()
{
    var widget = new KpiWidgetModel { Id = "placement-kpi" };
    var template = new DashboardTemplateBuilder()
        .Named("placement-test")
        .UseResponsiveLayout()
        .AddZone(LayoutZone.Right, "Metrics", options => options.Columns = 2)
        .PlaceWidget(widget, LayoutZone.Right, "Metrics", order: 4, priority: 8, columnSpan: 2, rowSpan: 1, minWidth: 240, maxWidth: 720)
        .BuildLayout()
        .Build();
    var placement = template.Context!.Placements.Single();
    Assert(placement.Zone == LayoutZone.Right && placement.ColumnSpan == 2 && placement.RowSpan == 1, "placement dimensions");
    Assert(placement.MinWidth == 240 && placement.MaxWidth == 720, "placement width");
    return Task.CompletedTask;
}
static Task ResponsiveZones()
{
    var template = new DashboardTemplateBuilder()
        .Named("zone-test")
        .UseResponsiveLayout()
        .AddZone(LayoutZone.Left)
        .AddZone(LayoutZone.Center)
        .AddZone(LayoutZone.Right)
        .BuildLayout()
        .Build();
    Assert(template.Context!.Zones.Count == 3, "zone count");
    Assert(template.Context.Zones.Find(LayoutZone.Center).Count == 1, "zone lookup");
    return Task.CompletedTask;
}
static Task ResponsiveOrderAndPriority()
{
    var template = new DashboardTemplateBuilder()
        .Named("order-test")
        .UseResponsiveLayout()
        .AddZone(LayoutZone.Main, configure: options => options.Columns = 3)
        .PlaceWidget(new KpiWidgetModel { Id = "third" }, LayoutZone.Main, "Metrics", order: 2, priority: 1)
        .PlaceWidget(new KpiWidgetModel { Id = "second" }, LayoutZone.Main, "Metrics", order: 1, priority: 5)
        .PlaceWidget(new KpiWidgetModel { Id = "first" }, LayoutZone.Main, "Metrics", order: 1, priority: 10)
        .BuildLayout()
        .Build();
    Assert(template.Sections.Single().Widgets.Select(widget => widget.Id).SequenceEqual(new[] { "first", "second", "third" }), "order and priority");
    return Task.CompletedTask;
}
static Task ResponsiveVisibility()
{
    var template = new DashboardTemplateBuilder()
        .Named("visibility-test")
        .UseResponsiveLayout(ResponsiveBreakpoint.Mobile)
        .AddZone(LayoutZone.Main)
        .PlaceWidget(new KpiWidgetModel { Id = "hidden-mobile" }, LayoutZone.Main, hiddenOn: [ResponsiveBreakpoint.Mobile])
        .PlaceWidget(new KpiWidgetModel { Id = "tablet-only" }, LayoutZone.Main, visibleOn: [ResponsiveBreakpoint.Tablet])
        .PlaceWidget(new KpiWidgetModel { Id = "always" }, LayoutZone.Main)
        .BuildLayout()
        .Build();
    Assert(template.Sections.Single().Widgets.Count == 1 && template.Sections.Single().Widgets[0].Id == "always", "visibleOn and hiddenOn");
    return Task.CompletedTask;
}
static Task ResponsiveRegistry()
{
    var registry = new DashboardTemplateRegistry();
    Assert(registry.SupportsResponsive("erp"), "ERP responsive support");
    Assert(registry.GetResponsiveTemplates().Count >= 6, "enterprise responsive templates");
    Assert(registry.GetSupportedBreakpoints("erp").Count == 5, "supported breakpoints");
    return Task.CompletedTask;
}
static Task CompositionAndResponsive()
{
    var erp = new DashboardTemplateRegistry().Create("erp");
    Assert(erp.Composition?.BaseTemplate?.Name == "executive", "responsive composition base");
    Assert(erp.Context!.Placements.Any(placement => placement.WidgetId == "executive-hero"), "inherited responsive placement");
    Assert(erp.Context.Placements.Any(placement => placement.WidgetId == "erp-sales-month"), "replaced responsive placement");
    Assert(erp.Manifest?.SupportsResponsiveLayout == true, "responsive manifest");
    return Task.CompletedTask;
}
static Task DesignerModel()
{
    var designer = new VisualTemplateDesignerModel(new DashboardTemplateRegistry());
    designer.LoadTemplate("executive");
    Assert(designer.Canvas.Template?.Name == "executive", "designer template state");
    Assert(designer.Canvas.Layout is not null && designer.Canvas.Zones.Count > 0, "designer canvas projection");
    Assert(designer.Properties.Responsive is not null && designer.History.Snapshots.Count == 1, "designer supporting models");
    return Task.CompletedTask;
}
static Task DesignerSelection()
{
    var designer = new VisualTemplateDesignerModel(new DashboardTemplateRegistry());
    designer.LoadTemplate("erp");
    var zone = designer.Canvas.Zones.First();
    designer.SelectZone(zone.Name);
    Assert(designer.Selection.Kind == DesignerSelectionKind.Zone && designer.Selection.Zone == zone, "zone selection");
    var widget = designer.Canvas.Widgets.First();
    designer.SelectWidget(widget.Id);
    Assert(designer.Selection.Kind == DesignerSelectionKind.Widget && designer.Properties.Placement is not null, "widget selection");
    return Task.CompletedTask;
}
static Task DesignerViewport()
{
    var designer = new VisualTemplateDesignerModel(new DashboardTemplateRegistry());
    designer.LoadTemplate("erp");
    designer.ChangeViewport(ResponsiveBreakpoint.Mobile);
    Assert(designer.Viewport.Current == ResponsiveBreakpoint.Mobile, "designer viewport state");
    Assert(designer.Canvas.Template?.Context?.CurrentBreakpoint == ResponsiveBreakpoint.Mobile, "responsive engine refresh");
    return Task.CompletedTask;
}
static Task DesignerRegistryIntegration()
{
    var registry = new DashboardTemplateRegistry();
    var designer = new VisualTemplateDesignerModel(registry);
    Assert(designer.Templates.Count == registry.GetAll().Count, "registered templates");
    Assert(designer.Factories.Count == registry.GetFactories().Count, "registered factories");
    Assert(designer.Toolbox.Groups.Select(group => group.Name)
        .SequenceEqual(new[] { "Widgets", "Layouts", "Templates", "Zones", "Slots" }), "registry toolbox groups");
    Assert(designer.Toolbox.Groups.Single(group => group.Name == "Templates").Items.Count == registry.Names.Count, "toolbox template registry");
    return Task.CompletedTask;
}
static Task DesignerTemplateLoading()
{
    var designer = new VisualTemplateDesignerModel(new DashboardTemplateRegistry());
    var executive = designer.LoadFactory("executive");
    Assert(executive.Name == "executive", "factory loading");
    var erp = designer.LoadTemplate("erp");
    Assert(erp.Name == "erp" && designer.Canvas.Widgets.Any(widget => widget.Id == "erp-sales-month"), "ERP template loading");
    return Task.CompletedTask;
}
static Task DesignerManifestLoading()
{
    var designer = new VisualTemplateDesignerModel(new DashboardTemplateRegistry());
    var manifest = designer.LoadManifest("erp");
    Assert(manifest.Name == "erp" && designer.Properties.Manifest == manifest, "manifest property source");
    Assert(designer.Viewport.Available.Count == 5, "manifest breakpoints");
    return Task.CompletedTask;
}
static Task DesignerDrag()
{
    var designer = LoadedDesigner();
    var widget = designer.Canvas.Widgets.First();
    designer.BeginDrag(widget.Id);
    Assert(designer.Drag.IsDragging && designer.Drag.WidgetId == widget.Id, "drag source state");
    designer.Drag.Hover(designer.Canvas.Zones.First().Name, 0);
    Assert(designer.Drag.DropIndex == 0, "drag hover and indicator state");
    designer.EndDrag();
    Assert(!designer.Drag.IsDragging, "drag end state");
    return Task.CompletedTask;
}
static Task DesignerDrop()
{
    var designer = LoadedDesigner();
    var widget = designer.Canvas.Widgets.First();
    var target = designer.Canvas.Zones.Last();
    designer.BeginDrag(widget.Id);
    designer.DropWidget(target.Name, 0);
    var placement = designer.Canvas.FindPlacement(widget.Id);
    Assert(placement.Region == target.Name && placement.Order == 0, "drop target");
    Assert(designer.Selection.Widget?.Id == widget.Id, "drop selection sync");
    return Task.CompletedTask;
}
static Task DesignerMove()
{
    var designer = LoadedDesigner();
    var widget = designer.Canvas.Widgets.First();
    var target = designer.Canvas.Zones.Last();
    designer.MoveWidget(widget.Id, target.Name, 0);
    Assert(designer.Canvas.FindPlacement(widget.Id).Zone == target.Kind, "move between zones");
    Assert(designer.Properties.Widget?.Placement?.Region == target.Name, "move property panel");
    return Task.CompletedTask;
}
static Task DesignerUndo()
{
    var designer = LoadedDesigner();
    var widget = designer.Canvas.Widgets.First();
    var original = designer.Canvas.FindPlacement(widget.Id).Region;
    designer.MoveWidget(widget.Id, designer.Canvas.Zones.Last().Name, 0);
    Assert(designer.CanUndo(), "can undo");
    designer.Undo();
    Assert(designer.Canvas.FindPlacement(widget.Id).Region == original, "undo move");
    return Task.CompletedTask;
}
static Task DesignerRedo()
{
    var designer = LoadedDesigner();
    var widget = designer.Canvas.Widgets.First();
    var target = designer.Canvas.Zones.Last();
    designer.MoveWidget(widget.Id, target.Name, 0);
    designer.Undo();
    Assert(designer.CanRedo(), "can redo");
    designer.Redo();
    Assert(designer.Canvas.FindPlacement(widget.Id).Region == target.Name, "redo move");
    return Task.CompletedTask;
}
static Task DesignerCommands()
{
    var designer = LoadedDesigner();
    var zone = designer.Canvas.Zones.First();
    var widget = new KpiWidgetModel { Id = "insert-command", Title = "Inserted KPI" };
    designer.InsertWidget(widget, zone.Name, 0);
    Assert(designer.Canvas.Widgets.Any(item => item.Id == widget.Id), "insert command");
    designer.DeleteWidget(widget.Id);
    Assert(designer.Canvas.Widgets.All(item => item.Id != widget.Id), "delete command");
    designer.Undo();
    Assert(designer.Canvas.Widgets.Any(item => item.Id == widget.Id), "delete undo");
    return Task.CompletedTask;
}
static Task DesignerPlacement()
{
    var designer = LoadedDesigner();
    var zone = designer.Canvas.Zones.First();
    var placements = designer.Canvas.Widgets.Take(2).ToArray();
    designer.MoveWidget(placements[0].Id, zone.Name, 0);
    designer.MoveWidget(placements[1].Id, zone.Name, 0);
    Assert(designer.Canvas.FindPlacement(placements[1].Id).Order == 0, "placement order");
    Assert(designer.Canvas.FindPlacement(placements[0].Id).Order == 1, "placement reorder");
    return Task.CompletedTask;
}
static Task DesignerHistory()
{
    var history = new DesignerHistory();
    var command = new TestDesignerCommand();
    history.Execute(command);
    history.Snapshot("command", command.Value);
    Assert(command.Value == 1 && history.CanUndo && history.Snapshots.Count == 1, "history execute and snapshot");
    history.Undo();
    Assert(command.Value == 0 && history.CanRedo, "history undo stack");
    history.Redo();
    Assert(command.Value == 1 && history.CanUndo, "history redo stack");
    return Task.CompletedTask;
}
static Task DesignerCanvasUpdate()
{
    var designer = LoadedDesigner();
    var zone = designer.Canvas.Zones.First();
    var widget = new KpiWidgetModel { Id = "canvas-update", Title = "Canvas KPI" };
    designer.InsertWidget(widget, zone.Name, 0);
    Assert(designer.Canvas.Template!.Sections.SelectMany(section => section.Widgets)
        .Any(item => item.Id == widget.Id), "sections updated in place");
    Assert(ReferenceEquals(designer.Canvas.Template, designer.Selection.Template) == false, "widget remains active after update");
    return Task.CompletedTask;
}
static Task DesignerViewportPreservation()
{
    var designer = LoadedDesigner();
    var widget = designer.Canvas.Widgets.First();
    designer.SelectWidget(widget.Id);
    designer.Viewport.PreserveScroll(120, 340);
    var template = designer.Canvas.Template;
    designer.ChangeViewport(ResponsiveBreakpoint.Mobile);
    Assert(ReferenceEquals(template, designer.Canvas.Template), "viewport does not reconstruct template");
    Assert(designer.Selection.Widget?.Id == widget.Id, "viewport preserves selection");
    Assert(designer.Viewport.ScrollLeft == 120 && designer.Viewport.ScrollTop == 340, "viewport preserves scroll");
    return Task.CompletedTask;
}
static VisualTemplateDesignerModel LoadedDesigner()
{
    var designer = new VisualTemplateDesignerModel(new DashboardTemplateRegistry());
    designer.LoadTemplate("erp");
    return designer;
}
static Orizon.UI.Models.Templates.DashboardTemplateModel AtBreakpoint(
    Orizon.UI.Models.Templates.DashboardTemplateModel source,
    ResponsiveBreakpoint breakpoint) =>
    new DashboardTemplateBuilder()
        .Inherit(source)
        .UseManifest(source.Manifest!)
        .UseResponsiveLayout(breakpoint)
        .BuildLayout()
        .Build();
static Task<OrizonGridDataResult<Row>> Provider(OrizonGridDataRequest request) => new OrizonInMemoryGridDataProvider<Row>(Data()).GetDataAsync(request);
static Task Runtime(string file, params string[] markers) { var root = FindRepo(); var text = File.ReadAllText(Path.Combine(root, "src", "Orizon.UI", "wwwroot", "js", "components", file)); foreach (var marker in markers) Assert(text.Contains(marker, StringComparison.OrdinalIgnoreCase), $"{file} missing {marker}"); return Task.CompletedTask; }
static string FindRepo() { var directory = new DirectoryInfo(AppContext.BaseDirectory); while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "Orizon.UI.sln"))) directory = directory.Parent; return directory?.FullName ?? throw new DirectoryNotFoundException("Repository root"); }
static void Assert(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
internal sealed record Row(int Id, string Name, string Category, decimal Amount);
internal sealed class TestDesignerCommand : IDesignerCommand
{
    public string Name => "Test";
    public int Value { get; private set; }
    public void Execute() => Value++;
    public void Undo() => Value--;
}
