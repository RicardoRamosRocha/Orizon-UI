using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Dashboard;

namespace Orizon.UI.ViewComponents.Dashboard;

public sealed class WidgetContainerViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetContainerModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return View("Default", model);
    }
}
