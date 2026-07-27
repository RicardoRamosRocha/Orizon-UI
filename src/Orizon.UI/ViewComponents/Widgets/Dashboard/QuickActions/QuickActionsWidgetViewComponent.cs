using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Widgets.Dashboard.QuickActions;

namespace Orizon.UI.ViewComponents.Widgets.Dashboard.QuickActions;

public sealed class QuickActionsWidgetViewComponent
    : WidgetViewComponent<QuickActionsWidgetModel>
{
    public IViewComponentResult Invoke(QuickActionsWidgetModel model)
    {
        return RenderWidget("Default", model);
    }
}
