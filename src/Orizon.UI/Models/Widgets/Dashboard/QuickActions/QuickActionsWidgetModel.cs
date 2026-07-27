using System.Collections.ObjectModel;

namespace Orizon.UI.Models.Widgets.Dashboard.QuickActions;

/// <summary>
/// Represents a collection of frequently used dashboard actions.
/// </summary>
public sealed class QuickActionsWidgetModel : WidgetModelBase
{
    public Collection<WidgetAction> Actions { get; } = [];
}
