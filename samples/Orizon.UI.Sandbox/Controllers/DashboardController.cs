using Microsoft.AspNetCore.Mvc;

namespace Orizon.UI.Sandbox.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Dashboard";
        ViewData["ApplicationName"] = "Orizon UI Sandbox";

        return View();
    }

    public IActionResult Widgets()
    {
        ViewData["Title"] = "Dashboard Widgets";
        ViewData["ApplicationName"] = "Orizon UI Sandbox";

        return View();
    }

    public IActionResult Framework()
    {
        ViewData["Title"] = "Dashboard Framework";
        ViewData["ApplicationName"] = "Orizon UI Sandbox";

        return View();
    }

    public IActionResult Templates()
    {
        ViewData["Title"] = "Dashboard Templates";
        ViewData["ApplicationName"] = "Orizon UI Sandbox";

        return View();
    }
}
