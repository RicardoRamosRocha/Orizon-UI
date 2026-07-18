using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Grid;

namespace Orizon.UI.Sandbox.Controllers;

[Route("grid")]
public sealed class GridController : Controller
{
    private static readonly IReadOnlyList<GridDemoRow> Rows = Enumerable.Range(1, 100_000).Select(i => new GridDemoRow(i, $"ITEM-{i:000000}", $"Registro empresarial {i}", new[] { "Distribuição", "Imobiliário", "Financeiro", "CRM" }[i % 4], new[] { "Novo", "Ativo", "Pendente", "Concluído" }[i % 4], Math.Round(100 + (i * 17.91m) % 50_000, 2), (i * 13) % 1000, DateTime.Today.AddDays(-(i % 730)), i <= 8 ? null : i % 8 == 0 ? i / 8 : null)).ToArray();

    [HttpGet("")] public IActionResult Index() => View();
    [HttpGet("server-mode")] public IActionResult ServerMode() => View();
    [HttpGet("server-sorting")] public IActionResult ServerSorting() => View();
    [HttpGet("server-filtering")] public IActionResult ServerFiltering() => View();
    [HttpGet("server-grouping")] public IActionResult ServerGrouping() => View();
    [HttpGet("server-paging")] public IActionResult ServerPaging() => View();
    [HttpGet("infinite-scroll")] public IActionResult InfiniteScroll() => View();
    [HttpGet("virtual-columns")] public IActionResult VirtualColumns() => View();
    [HttpGet("tree-data")] public IActionResult TreeData() => View();
    [HttpGet("master-detail")] public IActionResult MasterDetail() => View();
    [HttpGet("batch-editing")] public IActionResult BatchEditing() => View();
    [HttpGet("validation")] public IActionResult Validation() => View();
    [HttpGet("clipboard")] public IActionResult Clipboard() => View();
    [HttpGet("export-excel")] public IActionResult ExportExcel() => View();
    [HttpGet("benchmark")] public IActionResult Benchmark() => View();

    [HttpPost("data")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Data([FromBody] OrizonGridDataRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<GridDemoRow> source = Rows;
        if (!string.IsNullOrWhiteSpace(request.ParentKey) && int.TryParse(request.ParentKey, out var parent)) source = source.Where(row => row.ParentId == parent);
        var provider = new OrizonInMemoryGridDataProvider<GridDemoRow>(source);
        return Json(await provider.GetDataAsync(request, cancellationToken));
    }

    public sealed record GridDemoRow(int Id, string Code, string Description, string Category, string Status, decimal Amount, int Quantity, DateTime Date, int? ParentId);
}
