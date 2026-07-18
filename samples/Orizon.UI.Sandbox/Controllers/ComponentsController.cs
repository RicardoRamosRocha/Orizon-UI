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

    [HttpGet("premium")]
    public IActionResult Premium() => View();

    [HttpGet("data")]
    public IActionResult Data() => View();

    [HttpGet("forms-premium")]
    public IActionResult FormsPremium() => View();

    [HttpGet("dashboard")]
    public IActionResult Dashboard() => View();

    [HttpGet("productivity")]
    public IActionResult Productivity() => View();

    [HttpGet("enterprise-grid")]
    public IActionResult EnterpriseGrid() => View();

    [HttpGet("templates")]
    public IActionResult Templates() => View("~/Views/Templates/Index.cshtml");

    [HttpGet("templates/admin")]
    public IActionResult TemplateAdmin() => View("~/Views/Templates/Admin/Index.cshtml");

    [HttpGet("templates/crud")]
    public IActionResult TemplateCrud() => View("~/Views/Templates/Crud/Index.cshtml");

    [HttpGet("templates/forms")]
    public IActionResult TemplateForms() => View("~/Views/Templates/Forms/Index.cshtml");

    [HttpGet("templates/master-detail")]
    public IActionResult TemplateMasterDetail() => View("~/Views/Templates/MasterDetail/Index.cshtml");

    [HttpGet("templates/kanban")]
    public IActionResult TemplateKanban() => View("~/Views/Templates/Kanban/Index.cshtml");

    [HttpGet("templates/analytics")]
    public IActionResult TemplateAnalytics() => View("~/Views/Templates/Analytics/Index.cshtml");

    [HttpGet("templates/settings")]
    public IActionResult TemplateSettings() => View("~/Views/Templates/Settings/Index.cshtml");

    [HttpGet("templates/auth")]
    public IActionResult TemplateAuth() => View("~/Views/Templates/Authentication/Index.cshtml");

    [HttpGet("templates/profile")]
    public IActionResult TemplateProfile() => View("~/Views/Templates/Profile/Index.cshtml");

    [HttpGet("templates/errors")]
    public IActionResult TemplateErrors() => View("~/Views/Templates/Errors/Index.cshtml");

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
