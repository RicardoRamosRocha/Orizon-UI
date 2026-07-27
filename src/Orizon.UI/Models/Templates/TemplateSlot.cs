using System.Collections.ObjectModel;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Widgets;

namespace Orizon.UI.Models.Templates;

/// <summary>
/// Represents an unbounded widget slot within a named template region.
/// </summary>
public sealed class TemplateSlot
{
    public string Name { get; set; } = string.Empty;
    public TemplateRegion Region { get; set; } = TemplateRegion.Content;
    public Collection<IWidgetModel> Widgets { get; } = [];
}
