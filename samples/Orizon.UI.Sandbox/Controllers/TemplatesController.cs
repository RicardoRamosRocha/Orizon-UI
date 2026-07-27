using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Registry;
using Orizon.UI.Enums.Templates;
using Orizon.UI.Models.Templates;
using Orizon.UI.Templates.SDK;
using Orizon.UI.Sandbox.Models;
using Orizon.UI.Models.Layout;

namespace Orizon.UI.Sandbox.Controllers;

[Route("templates")]
public sealed class TemplatesController : Controller
{
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        var registry = new DashboardTemplateRegistry();

        ViewData["Title"] = "Dashboard Templates";
        ViewData["ApplicationName"] = "Orizon UI Sandbox";

        return View(registry.GetAll());
    }

    [HttpGet("dashboard/{name}")]
    public IActionResult Preview(string name)
    {
        var registry = new DashboardTemplateRegistry();
        var template = registry.GetByName(name);

        if (template is null)
        {
            return NotFound();
        }

        ViewData["Title"] = template.DisplayName ?? template.Name;
        ViewData["ApplicationName"] = "Orizon UI Sandbox";

        return View(template);
    }

    [HttpGet("sdk")]
    public IActionResult Sdk()
    {
        var registry = new DashboardTemplateRegistry();
        var manifest = registry.GetManifest("default")!;
        var factory = registry.GetFactories().Single();
        var options = new DashboardTemplateOptions
        {
            Theme = DashboardTheme.Default,
            Fluid = true,
            ShowHeader = true,
            ShowFooter = true,
            EnableSidebar = true,
            EnableToolbar = true,
            EnableBreadcrumb = true,
            Density = "Comfortable",
            Animation = "Default"
        };
        var context = new DashboardTemplateContext
        {
            Manifest = manifest,
            Options = options
        };
        context.Rendering["mode"] = "Static SDK demonstration";

        var template = factory.Create();
        template.Context = context;
        template.Options = options;
        template.Manifest = manifest;

        ViewData["Title"] = "Template SDK";
        ViewData["FactoryName"] = factory.GetType().Name;
        return View(template);
    }

    [HttpGet("enterprise-pack")]
    public IActionResult EnterprisePack()
    {
        var registry = new DashboardTemplateRegistry();
        var order = new[] { "blank", "executive", "operations", "analytics", "workspace" };
        var factories = registry.GetFactories()
            .ToDictionary(factory => factory.GetManifest().Name, StringComparer.OrdinalIgnoreCase);
        var templates = order.Select(name => factories[name].Create()).ToArray();

        ViewData["Title"] = "Enterprise Dashboard Pack";
        return View(templates);
    }

    [HttpGet("erp-dashboard")]
    public IActionResult ERPDashboard()
    {
        var factory = new ERPDashboardFactory();

        ViewData["Title"] = factory.GetManifest().DisplayName;
        ViewData["ApplicationName"] = "Orizon UI Sandbox";
        return View(factory.Create());
    }

    [HttpGet("composition")]
    public IActionResult Composition()
    {
        ViewData["Title"] = "Template Composition";
        return View(StudioController.CreateTemplateCompositionPage());
    }

    [HttpGet("responsive-engine")]
    public IActionResult ResponsiveEngine()
    {
        var registry = new DashboardTemplateRegistry();
        var source = registry.Create("erp");
        var breakpoints = new[]
        {
            ResponsiveBreakpoint.Desktop,
            ResponsiveBreakpoint.Tablet,
            ResponsiveBreakpoint.Mobile
        };

        ViewData["Title"] = "Responsive Dashboard Engine";
        return View(new ResponsiveDashboardEngineViewModel
        {
            Previews = breakpoints
                .Select(breakpoint => new ResponsiveDashboardPreview(
                    breakpoint,
                    StudioController.CreateResponsiveTemplate(source, breakpoint)))
                .ToArray()
        });
    }
}
