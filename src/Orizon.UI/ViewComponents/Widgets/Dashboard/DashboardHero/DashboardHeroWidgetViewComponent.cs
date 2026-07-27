using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Widgets.Dashboard.DashboardHero;

namespace Orizon.UI.ViewComponents.Widgets.Dashboard.DashboardHero;

public sealed class DashboardHeroWidgetViewComponent
    : WidgetViewComponent<DashboardHeroWidgetModel>
{
    public IViewComponentResult Invoke(DashboardHeroWidgetModel model)
    {
        return RenderWidget("Default", model);
    }
}
