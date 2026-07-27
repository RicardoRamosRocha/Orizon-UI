using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Dashboard;

namespace Orizon.UI.ViewComponents.Dashboard;

public sealed class DashboardSectionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(DashboardSectionModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return View("Default", model);
    }
}
