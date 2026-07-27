using Orizon.UI.Builders;
using Orizon.UI.Factories.Templates;
using Orizon.UI.Models.Templates;
using Orizon.UI.Templates.Dashboard;

namespace Orizon.UI.Templates.SDK;

/// <summary>
/// Creates the ERP dashboard through the Template SDK builder.
/// </summary>
public sealed class ERPDashboardFactory : ITemplateFactory
{
    public DashboardTemplateModel Create() =>
        ERPDashboardTemplate.Compose(
                new DashboardTemplateBuilder())
            .UseManifest(GetManifest())
            .Build();

    public bool CanCreate() => true;

    public DashboardTemplateManifest GetManifest() => ERPDashboardManifest.Create();
}
