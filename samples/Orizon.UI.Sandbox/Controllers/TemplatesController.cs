using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Registry;

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
}
