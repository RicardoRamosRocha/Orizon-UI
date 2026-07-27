namespace Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;

/// <summary>
/// Represents a single entry in an activity feed.
/// </summary>
public sealed class ActivityItem
{
    public string? Icon { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string? Badge { get; set; }

    public string Color { get; set; } = "primary";
}
