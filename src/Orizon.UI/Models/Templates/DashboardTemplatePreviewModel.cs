namespace Orizon.UI.Models.Templates;

/// <summary>
/// Provides presentation data for a dashboard template catalog card.
/// </summary>
public sealed class DashboardTemplatePreviewModel
{
    public required DashboardTemplateModel Template { get; init; }

    public required DashboardTemplateManifest Manifest { get; init; }

    public required string UsageCode { get; init; }
}
