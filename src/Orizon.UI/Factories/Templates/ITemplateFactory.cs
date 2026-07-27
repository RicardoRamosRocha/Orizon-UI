using Orizon.UI.Models.Templates;

namespace Orizon.UI.Factories.Templates;

/// <summary>
/// Creates a template through an explicit, reflection-free contract.
/// </summary>
public interface ITemplateFactory
{
    DashboardTemplateModel Create();
    bool CanCreate();
    DashboardTemplateManifest GetManifest();
}
