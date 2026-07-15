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
}