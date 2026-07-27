using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Templates;

namespace Orizon.UI.ViewComponents.Templates;

public sealed class DashboardTemplatePreviewViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(DashboardTemplateModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return View("Default", new DashboardTemplatePreviewModel
        {
            Template = model,
            Manifest = model.Manifest ?? DashboardTemplateManifest.FromModel(model),
            UsageCode = CreateUsageCode(model)
        });
    }

    private static string CreateUsageCode(DashboardTemplateModel template)
    {
        return $$"""
                @using Orizon.UI.Registry
                @{
                    var registry = new DashboardTemplateRegistry();
                    var template = registry.GetByName("{{template.Name}}");
                }

                @await Component.InvokeAsync("DashboardTemplate", new { model = template })
                """;
    }
}
