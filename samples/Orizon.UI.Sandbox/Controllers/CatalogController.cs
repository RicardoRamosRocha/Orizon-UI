using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Sandbox.Models;

namespace Orizon.UI.Sandbox.Controllers;

[Route("catalog")]
public sealed class CatalogController : Controller
{
    private static readonly string[] DefaultSections = ["Basic", "Variants", "Sizes", "States"];

    private static readonly IReadOnlyDictionary<string, CatalogPageViewModel> Pages =
        CreatePages().ToDictionary(page => $"{page.Category}/{page.Slug}", StringComparer.OrdinalIgnoreCase);

    [HttpGet("{category}/{slug}")]
    public IActionResult Page(string category, string slug)
    {
        if (!Pages.TryGetValue($"{category}/{slug}", out var page))
        {
            return NotFound();
        }

        ViewData["Title"] = page.Title;
        ViewData["ApplicationName"] = "Orizon UI Catalog";

        return View(page);
    }

    private static IEnumerable<CatalogPageViewModel> CreatePages()
    {
        yield return Page("foundations", "colors", "Colors", "Paleta semântica e papéis de cor do Orizon UI.", "Palette", "Semantic colors", "Usage");
        yield return Page("foundations", "typography", "Typography", "Escala tipográfica para interfaces consistentes e legíveis.", "Font family", "Scale", "Weights", "Usage");
        yield return Page("foundations", "icons", "Icons", "Biblioteca visual para ações, estados e navegação.", "Library", "Sizes", "Weights", "Usage");
        yield return Page("foundations", "spacing", "Spacing", "Escala de espaçamento para layouts e componentes.", "Scale", "Layout", "Components");
        yield return Page("foundations", "border-radius", "Border Radius", "Raios de borda para superfícies, controles e elementos circulares.", "Scale", "Surfaces", "Usage");
        yield return Page("foundations", "shadows", "Shadows", "Níveis de elevação para superfícies e overlays.", "Scale", "Surfaces", "Usage");

        yield return Page("components", "buttons", "Buttons", "Ações primárias, secundárias e contextuais.", "Basic", "Variants", "Sizes", "Icons");
        yield return Page("components", "cards", "Cards", "Superfícies para agrupar conteúdo e ações relacionadas.");
        yield return Page("components", "forms", "Forms", "Estruturas para entrada, validação e envio de dados.", "Basic", "Layout", "Validation", "States");
        yield return Page("components", "inputs", "Inputs", "Campos de texto para captura de dados.");
        yield return Page("components", "select", "Select", "Seleção de uma ou várias opções.");
        yield return Page("components", "textarea", "Textarea", "Entrada de texto longo com suporte a contador e validação.");
        yield return Page("components", "badge", "Badge", "Indicadores compactos de status e contexto.");
        yield return Page("components", "avatar", "Avatar", "Representação visual de pessoas e entidades.");
        yield return Page("components", "modal", "Modal", "Diálogos para tarefas e decisões focadas.");
        yield return Page("components", "tabs", "Tabs", "Navegação entre conjuntos relacionados de conteúdo.");
        yield return Page("components", "toast", "Toast", "Mensagens temporárias de feedback ao usuário.");
        yield return Page("components", "dropdown", "Dropdown", "Menus contextuais para ações e opções.");
        yield return Page("components", "table", "Table", "Apresentação estruturada de dados tabulares.");
        yield return Page("components", "pagination", "Pagination", "Navegação entre páginas de resultados.");
        yield return Page("components", "spinner", "Spinner", "Indicador visual para operações em andamento.");
        yield return Page("components", "progress", "Progress", "Indicador de progresso para tarefas mensuráveis.");
        yield return Page("components", "empty-state", "Empty State", "Orientação para áreas sem dados ou conteúdo.");

        yield return Page("layout", "sidebar", "Sidebar", "Navegação lateral para aplicações enterprise.", "Basic", "Collapsed", "Active states", "Responsive");
        yield return Page("layout", "topbar", "Topbar", "Barra superior para contexto e ações globais.", "Basic", "Actions", "Profile", "Responsive");
        yield return Page("layout", "breadcrumb", "Breadcrumb", "Indicação da posição atual na hierarquia.");
        yield return Page("layout", "dashboard", "Dashboard Layout", "Estrutura base para painéis administrativos.", "Structure", "Regions", "Responsive");

        yield return Page("data", "enterprise-grid", "Enterprise Grid", "Grid de alta performance para cenários empresariais.", "Basic", "Data source", "Columns", "States");
        yield return Page("data", "tables", "Tables", "Tabelas responsivas para dados estruturados e comparações.", "Styles", "Density", "States");
        yield return Page("data", "pagination", "Pagination", "Navegação acessível entre páginas de resultados.", "Basic", "Current page", "States");
        yield return Page("data", "loading-states", "Loading States", "Feedback visual para operações e carregamentos assíncronos.", "Spinner", "Progress", "Usage");
        yield return Page("data", "empty-states", "Empty States", "Orientação clara quando não há dados ou acesso disponível.", "No data", "No results", "Error", "Permission");
        yield return Page("data", "charts", "Charts", "Visualizações para métricas e tendências.", "Placeholder");
        yield return Page("data", "tree-view", "TreeView", "Visualização hierárquica de dados.", "Placeholder");

        yield return Page("patterns", "crud", "CRUD", "Padrão para criação, consulta, edição e exclusão.", "List", "Create", "Edit", "Delete");
        yield return Page("patterns", "login", "Login", "Fluxo de autenticação para aplicações Orizon.", "Basic", "Validation", "States");
        yield return Page("patterns", "dashboard", "Dashboard", "Composição de indicadores, dados e ações.", "Overview", "Metrics", "Content");
        yield return Page("patterns", "wizard", "Wizard", "Fluxos orientados por etapas.", "Basic", "Steps", "Validation");
        yield return Page("patterns", "settings", "Settings", "Organização de preferências e configurações.", "Navigation", "Sections", "Actions");

        yield return Page("templates", "erp", "ERP", "Estrutura inicial para gestão de recursos empresariais.", "Overview", "Navigation", "Pages");
        yield return Page("templates", "crm", "CRM", "Estrutura inicial para relacionamento com clientes.", "Overview", "Navigation", "Pages");
        yield return Page("templates", "real-estate", "Real Estate", "Estrutura inicial para operações imobiliárias.", "Overview", "Navigation", "Pages");
        yield return Page("templates", "distribution", "Distribution", "Estrutura inicial para distribuição e logística.", "Overview", "Navigation", "Pages");
        yield return Page("templates", "hair", "Hair", "Estrutura inicial para gestão de salões e serviços.", "Overview", "Navigation", "Pages");
        yield return Page("templates", "healthcare", "Healthcare", "Estrutura inicial para operações de saúde.", "Overview", "Navigation", "Pages");
    }

    private static CatalogPageViewModel Page(
        string category,
        string slug,
        string title,
        string description,
        params string[] sections) =>
        new(category, slug, title, description, sections.Length == 0 ? DefaultSections : sections);
}
