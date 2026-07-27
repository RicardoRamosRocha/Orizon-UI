using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Models.Widgets;

namespace Orizon.UI.ViewComponents.Widgets;

/// <summary>
/// Provides shared rendering infrastructure for widget view components.
/// </summary>
/// <remarks>
/// A widget coordinates state and content composition. It does not imply a dashboard
/// dependency and is not a replacement for the visual Card surface contract.
/// </remarks>
public abstract class WidgetViewComponent : ViewComponent
{
    /// <summary>
    /// Creates a view result for a widget model.
    /// </summary>
    /// <param name="viewName">The name or path of the widget view.</param>
    /// <param name="model">The widget model supplied to the view.</param>
    /// <returns>A view component result configured with the supplied widget model.</returns>
    protected IViewComponentResult RenderWidget(string viewName, WidgetModelBase model)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(viewName);
        ArgumentNullException.ThrowIfNull(model);

        return View(viewName, model);
    }
}

/// <summary>
/// Provides strongly typed rendering infrastructure for widget view components.
/// </summary>
/// <typeparam name="TModel">The widget model handled by the view component.</typeparam>
/// <remarks>
/// Derive future widget view components from this type to retain their concrete model
/// type while preserving the non-generic <see cref="WidgetViewComponent"/> contract.
/// </remarks>
public abstract class WidgetViewComponent<TModel> : WidgetViewComponent
    where TModel : WidgetModelBase
{
    /// <summary>
    /// Creates a view result for a strongly typed widget model.
    /// </summary>
    /// <param name="viewName">The name or path of the widget view.</param>
    /// <param name="model">The strongly typed widget model supplied to the view.</param>
    /// <returns>A view component result configured with the supplied widget model.</returns>
    protected IViewComponentResult RenderWidget(string viewName, TModel model)
    {
        return base.RenderWidget(viewName, model);
    }
}
