using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Dashboard;

namespace Orizon.UI.ViewComponents.Dashboard;

public sealed class DashboardLayoutViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(DashboardLayoutModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return View("Default", model);
    }
}
