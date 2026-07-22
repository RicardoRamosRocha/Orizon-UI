using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Icons;
using Orizon.UI.Sandbox.Models;

namespace Orizon.UI.Sandbox.Controllers;

[Route("studio")]
public sealed class StudioController : Controller
{
    private static readonly IReadOnlyList<StudioComponentDocumentation> Components = CreateComponents();
    private static readonly IReadOnlyList<StudioIconItem> Icons = LoadIcons();

    [HttpGet("")]
    public IActionResult Index() => RedirectToAction(nameof(ComponentExplorer));

    [HttpGet("components/{component?}")]
    public IActionResult ComponentExplorer(string? component)
    {
        var selected = Components.FirstOrDefault(item => item.Slug.Equals(component, StringComparison.OrdinalIgnoreCase))
            ?? Components[0];
        return View(Page("Component Explorer", selected));
    }

    [HttpGet("playground")]
    public IActionResult Playground() => View(Page("Playground"));

    [HttpGet("icons")]
    public IActionResult IconExplorer() => View(Page("Icon Explorer"));

    [HttpGet("theme")]
    public IActionResult ThemeBuilder() => View(Page("Theme Builder"));

    [HttpGet("layout")]
    public IActionResult LayoutBuilder() => View(Page("Layout Builder"));

    private static StudioPageViewModel Page(string section, StudioComponentDocumentation? component = null) => new()
    {
        Section = section,
        Components = Components,
        Component = component,
        Icons = Icons,
        SearchItems = CreateSearchItems()
    };

    private static IReadOnlyList<StudioSearchItem> CreateSearchItems()
    {
        var items = Components.Select(item => new StudioSearchItem(item.Name, "Componente", $"/studio/components/{item.Slug}", item.Description));
        var icons = Icons.Select(item => new StudioSearchItem(item.Name, $"Ícone · {item.Category}", $"/studio/icons#{item.Name}", item.Category));
        var fixedItems = new[]
        {
            new StudioSearchItem("Admin Dashboard", "Template", "/components/templates/admin", "dashboard template"),
            new StudioSearchItem("CRUD", "Template", "/components/templates/crud", "table form"),
            new StudioSearchItem("Dashboard Layout", "Layout", "/studio/layout", "sidebar topbar cards widgets"),
            new StudioSearchItem("Design Tokens", "Foundation", "/catalog/foundations/colors", "colors radius spacing shadow"),
            new StudioSearchItem("Typography", "Foundation", "/catalog/foundations/typography", "type scale font"),
            new StudioSearchItem("Theme Builder", "Ferramenta", "/studio/theme", "dark mode tokens colors")
        };
        return items.Concat(icons).Concat(fixedItems).ToArray();
    }

    private static IReadOnlyList<StudioIconItem> LoadIcons()
    {
        // The public registry intentionally exposes lookup only. Reading its key set keeps the
        // explorer tied to the single registry source without copying or parsing SVG assets.
        var field = typeof(OrizonIconRegistry).GetField("Icons", BindingFlags.NonPublic | BindingFlags.Static);
        var registry = field?.GetValue(null) as IReadOnlyDictionary<string, string>;
        return registry?.Keys.OrderBy(name => name).Select(name => new StudioIconItem(name, Categorize(name))).ToArray() ?? [];
    }

    private static string Categorize(string name)
    {
        if (name.Contains("chart") || name is "activity" or "trend-up" or "trend-down") return "Dados";
        if (name.Contains("user") || name is "building" or "briefcase") return "Negócios";
        if (name is "lock" or "shield" or "key" or "eye" or "eye-off") return "Segurança";
        if (name is "cart" or "bag" or "package" or "credit-card") return "Comércio";
        if (name is "home" or "house" or "menu" or "chevron-left" or "chevron-right" or "arrow-left" or "arrow-right") return "Navegação";
        return "Ações e sistema";
    }

    private static IReadOnlyList<StudioComponentDocumentation> CreateComponents()
    {
        var definitions = new[]
        {
            ("button", "Button", "Ação principal com variantes, tamanhos e estados empresariais.", "<orizon-button variant=\"solid\" color=\"primary\" size=\"md\">Salvar alterações</orizon-button>", "<button type=\"button\" class=\"orizon-button orizon-button--solid orizon-button--primary orizon-button--md\"><span class=\"orizon-button__content\">Salvar alterações</span></button>"),
            ("card", "Card", "Superfície estruturada para conteúdo, métricas e ações.", "<orizon-card title=\"Receita mensal\" subtitle=\"Atualizado agora\" variant=\"elevated\">R$ 128.400</orizon-card>", "<article class=\"orizon-card orizon-card--elevated\"><div class=\"orizon-card__body\">R$ 128.400</div></article>"),
            ("input", "Input", "Entrada de dados com label, ajuda, validação e estados.", "<orizon-input name=\"Email\" label=\"E-mail\" type=\"email\" placeholder=\"nome@empresa.com\" />", "<div class=\"orizon-field\"><label for=\"Email\">E-mail</label><input id=\"Email\" name=\"Email\" type=\"email\" class=\"orizon-input\"></div>"),
            ("badge", "Badge", "Indicador compacto de status e classificação.", "<orizon-badge variant=\"success\">Ativo</orizon-badge>", "<span class=\"orizon-badge orizon-badge--success\">Ativo</span>"),
            ("alert", "Alert", "Mensagem contextual para feedback de sistema.", "<orizon-alert variant=\"info\" title=\"Atualização disponível\">Revise as novidades.</orizon-alert>", "<div role=\"status\" class=\"orizon-alert orizon-alert--info\">Atualização disponível</div>"),
            ("avatar", "Avatar", "Representação visual de pessoas e entidades.", "<orizon-avatar initials=\"OR\" size=\"md\" />", "<span class=\"orizon-avatar orizon-avatar--md\" aria-label=\"OR\">OR</span>"),
            ("modal", "Modal", "Diálogo focado para decisões e tarefas críticas.", "<orizon-modal title=\"Confirmar alteração\" open>Conteúdo</orizon-modal>", "<div class=\"orizon-modal\" role=\"dialog\" aria-modal=\"true\">Conteúdo</div>"),
            ("tabs", "Tabs", "Navegação contextual entre painéis relacionados.", "<orizon-tabs aria-label=\"Detalhes\">...</orizon-tabs>", "<div class=\"orizon-tabs\" role=\"tablist\">...</div>"),
            ("toast", "Toast", "Feedback temporário para ações concluídas.", "<orizon-toast variant=\"success\" title=\"Salvo\" />", "<div class=\"orizon-toast orizon-toast--success\" role=\"status\">Salvo</div>"),
            ("table", "Table", "Tabela responsiva para conjuntos de dados.", "<orizon-table>...</orizon-table>", "<div class=\"orizon-table-container\"><table class=\"orizon-table\">...</table></div>"),
            ("pagination", "Pagination", "Navegação previsível em coleções paginadas.", "<orizon-pagination page=\"2\" total-pages=\"8\" />", "<nav class=\"orizon-pagination\" aria-label=\"Paginação\">...</nav>"),
            ("spinner", "Spinner", "Indicador de processamento não bloqueante.", "<orizon-spinner size=\"md\" label=\"Carregando\" />", "<span class=\"orizon-spinner orizon-spinner--md\" role=\"status\">Carregando</span>"),
            ("empty-state", "Empty State", "Orientação clara quando ainda não há conteúdo.", "<orizon-empty-state title=\"Nenhum resultado\" description=\"Ajuste os filtros.\" />", "<section class=\"orizon-empty-state\"><h3>Nenhum resultado</h3><p>Ajuste os filtros.</p></section>"),
            ("select", "Select", "Seleção consistente entre opções predefinidas.", "<orizon-select name=\"Status\" label=\"Status\">...</orizon-select>", "<label>Status<select class=\"orizon-select\" name=\"Status\">...</select></label>"),
            ("dropdown", "Dropdown", "Menu contextual de ações relacionadas.", "<orizon-dropdown label=\"Ações\">...</orizon-dropdown>", "<div class=\"orizon-dropdown\"><button aria-expanded=\"false\">Ações</button></div>")
        };

        return definitions.Select(item => new StudioComponentDocumentation(
            item.Item1, item.Item2, item.Item3, item.Item4, item.Item5,
            $".{item.Item1} {{ /* customize com tokens Orizon */ }}",
            $"// Configure {item.Item2} no seu ViewModel quando necessário.",
            ["Container", "Conteúdo", "Estado visual", "Área de interação"],
            ["Default", "Compact", "Emphasis", "Responsive"],
            ["Default", "Hover", "Focus visible", "Disabled", "Loading"],
            new Dictionary<string, string> { ["variant"] = "Define a aparência visual.", ["size"] = "Controla a escala do componente.", ["disabled"] = "Impede interação e comunica indisponibilidade.", ["class"] = "Adiciona classes sem substituir as classes base." },
            ["Navegação completa por teclado", "Contraste preservado pelos design tokens", "Focus visible em toda ação", "Nome acessível e estados ARIA quando aplicáveis"])).ToArray();
    }
}
