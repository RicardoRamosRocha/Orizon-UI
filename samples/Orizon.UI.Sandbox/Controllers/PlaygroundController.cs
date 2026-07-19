using Microsoft.AspNetCore.Mvc;

namespace Orizon.UI.Sandbox.Controllers;

[Route("playground")]
public sealed class PlaygroundController : Controller
{
    [HttpGet("")] public IActionResult Index() => RedirectToAction(nameof(Buttons));
    [HttpGet("buttons")] public IActionResult Buttons() => View();
    [HttpGet("cards")] public IActionResult Cards() => View();
    [HttpGet("badge")] public IActionResult Badge() => View();
    [HttpGet("inputs")] public IActionResult Inputs() => View();
}
