using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Widgets.Dashboard.Kpi;

namespace Orizon.UI.ViewComponents.Widgets.Dashboard.Kpi;

public sealed class KpiWidgetViewComponent : WidgetViewComponent<KpiWidgetModel>
{
    public IViewComponentResult Invoke(KpiWidgetModel model)
    {
        return RenderWidget("Default", model);
    }
}
