namespace Orizon.UI.Models.Widgets.Dashboard.DashboardHero;

/// <summary>
/// Represents the content displayed by a dashboard hero widget.
/// </summary>
public sealed class DashboardHeroWidgetModel : WidgetModelBase
{
    public string? Description { get; set; }

    public string? PrimaryActionText { get; set; }

    public string? PrimaryActionUrl { get; set; }

    public string? SecondaryActionText { get; set; }

    public string? SecondaryActionUrl { get; set; }

    public string BackgroundVariant { get; set; } = "primary";
}
