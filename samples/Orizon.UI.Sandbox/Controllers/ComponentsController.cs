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

    [HttpGet("design-language")]
    public IActionResult DesignLanguage() => View();

    [HttpGet("appearance")]
    public IActionResult Appearance() => View();

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

    [HttpGet("modal")]
    public IActionResult Modal()
    {
        return View();
    }

    [HttpGet("dropdown")]
    public IActionResult Dropdown()
    {
        return View();
    }

    [HttpGet("tabs")]
    public IActionResult Tabs()
    {
        return View();
    }

    [HttpGet("pagination")]
    public IActionResult Pagination()
    {
        return View();
    }

    [HttpGet("avatar")]
    public IActionResult Avatar()
    {
        return View();
    }

    [HttpGet("toast")]
    public IActionResult Toast()
    {
        return View();
    }

    [HttpGet("progress")]
    public IActionResult Progress()
    {
        return View();
    }

    [HttpGet("spinner")]
    public IActionResult Spinner()
    {
        return View();
    }

    [HttpGet("empty-state")]
    public IActionResult EmptyState()
    {
        return View();
    }
}
