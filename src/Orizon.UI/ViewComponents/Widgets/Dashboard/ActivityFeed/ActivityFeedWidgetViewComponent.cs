using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Widgets.Dashboard.ActivityFeed;

namespace Orizon.UI.ViewComponents.Widgets.Dashboard.ActivityFeed;

public sealed class ActivityFeedWidgetViewComponent
    : WidgetViewComponent<ActivityFeedWidgetModel>
{
    public IViewComponentResult Invoke(ActivityFeedWidgetModel model)
    {
        return RenderWidget("Default", model);
    }
}
