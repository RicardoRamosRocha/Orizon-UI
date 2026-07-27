using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Templates;

namespace Orizon.UI.ViewComponents.Templates;

public sealed class DashboardTemplateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(DashboardTemplateModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return View("Default", model);
    }
}
