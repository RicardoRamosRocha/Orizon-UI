using Orizon.UI.Grid;

var tests = new List<(string Name, Func<Task> Run)>
{
    ("Sorting", Sorting), ("Filtering", Filtering), ("Grouping", Grouping), ("Paging", Paging),
    ("Selection runtime", () => Runtime("enterprise-grid-selection.js", "selected", "ctrlKey")),
    ("Clipboard runtime", () => Runtime("enterprise-grid-selection.js", "clipboard", "paste")),
    ("Undo runtime", () => Runtime("enterprise-grid-advanced.js", "state.undo", "oldValue")),
    ("Redo runtime", () => Runtime("enterprise-grid-advanced.js", "state.redo", "newValue")),
    ("Virtualization runtime", () => Runtime("enterprise-grid-virtual.js", "overscan", "renderedColumnStart")),
    ("Batch runtime", () => Runtime("enterprise-grid-advanced.js", "orizon:grid-batch-save", "data-grid-batch")),
    ("Server Mode", ServerMode),
    ("Export XLSX", () => Runtime("enterprise-grid-xlsx.js", "openxmlformats", "0x04034b50"))
};
var passed = 0;
foreach (var test in tests) { try { await test.Run(); Console.WriteLine($"PASS {test.Name}"); passed++; } catch (Exception error) { Console.Error.WriteLine($"FAIL {test.Name}: {error.Message}"); } }
Console.WriteLine($"{passed}/{tests.Count} automated tests passed");
return passed == tests.Count ? 0 : 1;

static Row[] Data() => Enumerable.Range(1, 30).Select(i => new Row(i, $"Item {i:00}", i % 3 == 0 ? "A" : "B", i * 10m)).ToArray();
static async Task Sorting() { var result = await Provider(new() { Sorts = [new("Amount", "desc")], Take = 5 }); Assert(result.Items[0].Amount == 300m, "descending sort"); }
static async Task Filtering() { var result = await Provider(new() { Filters = [new() { Field = "Amount", Operator = OrizonGridFilterOperator.GreaterOrEqual, Value = 200 }], Take = 50 }); Assert(result.TotalCount == 11, "range filter"); var contains = await Provider(new() { Filters = [new() { Field = "Name", Operator = OrizonGridFilterOperator.Contains, Value = "Item 2" }], Take = 50 }); Assert(contains.TotalCount == 10, "contains filter"); }
static async Task Grouping() { var result = await Provider(new() { Groups = [new("Category")], Aggregates = [new("Amount", "sum")], Take = 50 }); Assert(result.Groups.Count == 2, "groups"); Assert(result.Groups.Sum(x => x.Count) == 30, "group counts"); }
static async Task Paging() { var result = await Provider(new() { Page = 2, PageSize = 5, Skip = 10, Take = 5 }); Assert(result.Items.Count == 5 && result.Items[0].Id == 11, "skip/take"); }
static async Task ServerMode() { var request = new OrizonGridDataRequest { Page = 1, PageSize = 10, Skip = 10, Take = 10, Search = "Item", Sorts = [new("Id", "asc")], SelectedColumns = ["Id", "Name"] }; var result = await Provider(request); Assert(result.TotalCount == 30 && result.Items.Count == 10 && result.ExecutionTime >= 0, "server result contract"); }
static Task<OrizonGridDataResult<Row>> Provider(OrizonGridDataRequest request) => new OrizonInMemoryGridDataProvider<Row>(Data()).GetDataAsync(request);
static Task Runtime(string file, params string[] markers) { var root = FindRepo(); var text = File.ReadAllText(Path.Combine(root, "src", "Orizon.UI", "wwwroot", "js", "components", file)); foreach (var marker in markers) Assert(text.Contains(marker, StringComparison.OrdinalIgnoreCase), $"{file} missing {marker}"); return Task.CompletedTask; }
static string FindRepo() { var directory = new DirectoryInfo(AppContext.BaseDirectory); while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "Orizon.UI.sln"))) directory = directory.Parent; return directory?.FullName ?? throw new DirectoryNotFoundException("Repository root"); }
static void Assert(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
internal sealed record Row(int Id, string Name, string Category, decimal Amount);
