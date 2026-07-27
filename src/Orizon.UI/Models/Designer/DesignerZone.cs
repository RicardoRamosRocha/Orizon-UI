using Orizon.UI.Models.Layout;
using Orizon.UI.Models.Templates;

namespace Orizon.UI.Models.Designer;

/// <summary>Read-only canvas projection of a responsive layout zone.</summary>
public sealed class DesignerZone
{
    public required LayoutRegion Region { get; init; }
    public string Name => Region.Name;
    public LayoutZone Kind => Region.Zone;
    public IReadOnlyList<DesignerWidget> Widgets { get; init; } = [];
    public IReadOnlyList<TemplateSlot> Slots { get; init; } = [];
}
