using Orizon.UI.Models.Templates;

namespace Orizon.UI.Models.Designer;

public enum DesignerSelectionKind { None, Template, Zone, Widget, Slot }

/// <summary>Stores the current designer selection without coupling it to UI events.</summary>
public sealed class DesignerSelection
{
    public DesignerSelectionKind Kind { get; private set; }
    public DashboardTemplateModel? Template { get; private set; }
    public DesignerZone? Zone { get; private set; }
    public DesignerWidget? Widget { get; private set; }
    public TemplateSlot? Slot { get; private set; }
    public object? Value => Kind switch
    {
        DesignerSelectionKind.Template => Template,
        DesignerSelectionKind.Zone => Zone,
        DesignerSelectionKind.Widget => Widget,
        DesignerSelectionKind.Slot => Slot,
        _ => null
    };

    public void SelectTemplate(DashboardTemplateModel template) { Clear(); Template = template ?? throw new ArgumentNullException(nameof(template)); Kind = DesignerSelectionKind.Template; }
    public void SelectZone(DesignerZone zone) { Clear(); Zone = zone ?? throw new ArgumentNullException(nameof(zone)); Kind = DesignerSelectionKind.Zone; }
    public void SelectWidget(DesignerWidget widget) { Clear(); Widget = widget ?? throw new ArgumentNullException(nameof(widget)); Kind = DesignerSelectionKind.Widget; }
    public void SelectSlot(TemplateSlot slot) { Clear(); Slot = slot ?? throw new ArgumentNullException(nameof(slot)); Kind = DesignerSelectionKind.Slot; }
    public void Clear() { Kind = DesignerSelectionKind.None; Template = null; Zone = null; Widget = null; Slot = null; }
}
