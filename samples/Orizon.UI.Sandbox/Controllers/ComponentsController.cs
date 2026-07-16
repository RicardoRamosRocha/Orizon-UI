using Microsoft.AspNetCore.Mvc;

namespace Orizon.UI.Sandbox.Controllers;

[Route("components")]
public sealed class ComponentsController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("buttons")]
    public IActionResult Buttons()
    {
        return View();
    }

    [HttpGet("cards")]
    public IActionResult Cards()
    {
        return View();
    }

    [HttpGet("alerts")]
    public IActionResult Alerts()
    {
        return View();
    }

    [HttpGet("badges")]
    public IActionResult Badges()
    {
        return View();
    }

    [HttpGet("forms")]
    public IActionResult Forms()
    {
        return View();
    }

    [HttpGet("tables")]
    public IActionResult Tables()
    {
        return View();
    }
}
