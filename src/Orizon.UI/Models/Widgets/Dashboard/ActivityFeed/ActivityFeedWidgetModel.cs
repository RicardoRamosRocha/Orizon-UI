using System.Collections.ObjectModel;

namespace Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;

/// <summary>
/// Represents a chronological collection of dashboard activities.
/// </summary>
public sealed class ActivityFeedWidgetModel : WidgetModelBase
{
    public Collection<ActivityItem> Items { get; } = [];
}
