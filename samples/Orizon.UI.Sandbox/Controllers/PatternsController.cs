using Microsoft.AspNetCore.Mvc;
using Orizon.UI.Sandbox.Models;

namespace Orizon.UI.Sandbox.Controllers;

[Route("catalog/patterns")]
public sealed class PatternsController : Controller
{
    private static readonly IReadOnlyDictionary<string, PatternPageViewModel> Pages = CreatePages()
        .ToDictionary(page => page.Slug, StringComparer.OrdinalIgnoreCase);

    [HttpGet("")]
    public IActionResult Index() => RedirectToAction(nameof(Page), new { slug = "crud" });

    [HttpGet("{slug}")]
    public IActionResult Page(string slug)
    {
        if (!Pages.TryGetValue(slug, out var page)) return NotFound();
        ViewData["Title"] = $"{page.Title} Pattern";
        ViewData["ApplicationName"] = "Orizon UI Patterns";
        return View(page);
    }

    private static IEnumerable<PatternPageViewModel> CreatePages()
    {
        yield return Pattern("crud", "CRUD Workspace", "Organiza consulta e manutenção de registros sem perder o contexto da lista.", "Operações frequentes sobre coleções com busca, filtros e ações em lote.", "Processos lineares, sem coleção ou com edição rara.", ["Reduz trocas de contexto", "Torna estados e ações previsíveis", "Escala para grandes volumes"], ["CommandBar", "Toolbar", "SearchBox", "FilterBar", "DataTable", "Pagination", "Modal", "ConfirmDialog", "Toast"], ["Pesquisar", "Filtrar", "Selecionar", "Editar", "Salvar", "Notificação"]);
        yield return Pattern("dashboard", "Dashboard", "Compõe uma visão executiva com indicadores, tendências e ações imediatas.", "Monitoramento recorrente de metas, saúde operacional e eventos recentes.", "Quando a tarefa principal exige leitura detalhada ou entrada extensa de dados.", ["Acelera decisões", "Expõe desvios rapidamente", "Prioriza ações recorrentes"], ["CommandBar", "MetricCard", "DashboardGrid", "Chart", "Timeline", "DashboardWidget", "QuickActions", "ActivityFeed"], ["Abrir visão", "Ler KPIs", "Comparar tendência", "Investigar widget", "Executar ação"]);
        yield return Pattern("login", "Login", "Conduz autenticação, recuperação, MFA e retomada segura de sessão.", "Portas de entrada para áreas autenticadas e fluxos de recuperação de acesso.", "Experiências públicas que não precisam identificar a pessoa.", ["Centraliza estados de acesso", "Reduz abandono", "Reforça segurança sem ocultar orientação"], ["Card", "Email", "Password", "Tabs", "Alert", "Button", "Modal"], ["Identificar", "Autenticar", "Verificar MFA", "Criar sessão", "Redirecionar"]);
        yield return Pattern("wizard", "Wizard", "Divide uma tarefa complexa em etapas curtas com validação e revisão.", "Processos longos, dependentes ou com dados que precisam ser confirmados.", "Formulários curtos ou tarefas que pedem comparação livre entre seções.", ["Reduz carga cognitiva", "Mostra progresso", "Previne envio incompleto"], ["Stepper", "Progress", "Form", "Alert", "Summary", "Button", "Toast"], ["Iniciar", "Preencher", "Validar", "Revisar", "Confirmar"]);
        yield return Pattern("search", "Search Experience", "Combina busca, refinamento e ordenação para encontrar itens com precisão.", "Catálogos e bases em que a pessoa conhece total ou parcialmente o que procura.", "Coleções pequenas que podem ser compreendidas sem busca ou filtros.", ["Encurta o tempo até o resultado", "Mantém filtros visíveis", "Oferece recuperação quando nada é encontrado"], ["SearchBox", "FilterBar", "Chip", "Select", "DataTable", "EmptyState"], ["Pesquisar", "Refinar", "Ordenar", "Examinar resultados", "Abrir item"]);
        yield return Pattern("master-detail", "Master Detail", "Mantém uma coleção e o contexto detalhado do item selecionado na mesma área.", "Triagem ou consulta repetida de itens relacionados.", "Quando cada item exige uma tela ampla e um fluxo independente.", ["Preserva orientação", "Acelera comparação", "Reduz navegação de ida e volta"], ["List", "Card", "Tabs", "Timeline", "Textarea", "ActivityFeed"], ["Selecionar item", "Ler detalhes", "Alternar aba", "Comentar", "Consultar histórico"]);
        yield return Pattern("data-entry", "Data Entry", "Estrutura formulários extensos em seções claras, com validação e salvamento contínuo.", "Cadastros com muitos campos, anexos e dependências entre seções.", "Entradas simples que cabem em poucos campos.", ["Previne perda de dados", "Localiza erros", "Adapta-se a diferentes larguras"], ["Form", "Accordion", "TextBox", "Select", "Upload", "Alert", "Button"], ["Preencher seção", "Anexar", "Validar", "Salvar rascunho", "Enviar"]);
        yield return Pattern("approval-workflow", "Approval Workflow", "Organiza decisões formais com contexto, comentários e trilha de auditoria.", "Solicitações reguladas ou com responsabilidade explícita de aprovação.", "Decisões reversíveis que não precisam de histórico formal.", ["Torna decisões rastreáveis", "Explicita responsáveis", "Evita ações sem contexto"], ["CommandBar", "Card", "Badge", "Tabs", "Timeline", "Textarea", "ConfirmDialog", "Toast"], ["Receber solicitação", "Revisar", "Comentar", "Aprovar ou rejeitar", "Registrar auditoria"]);
        yield return Pattern("kanban", "Kanban Workspace", "Visualiza trabalho por estado e permite evoluir cartões ao longo do fluxo.", "Trabalho contínuo com estados claros e limites por coluna.", "Processos estritamente sequenciais ou com centenas de itens simultâneos.", ["Comunica progresso", "Expõe gargalos", "Favorece colaboração"], ["CommandBar", "FilterBar", "Chip", "Kanban", "Card", "Badge", "Timeline"], ["Filtrar quadro", "Priorizar cartão", "Mover", "Atualizar responsável", "Registrar atividade"]);
        yield return Pattern("analytics", "Analytics", "Explora métricas com comparação, filtros, detalhamento e exportação.", "Análise de tendência, desempenho e causas por dimensão.", "Quando uma única métrica operacional já orienta a ação.", ["Conecta resumo e detalhe", "Facilita comparação", "Permite compartilhar evidências"], ["CommandBar", "MetricCard", "Chart", "Select", "Tabs", "DataTable", "Button"], ["Definir período", "Comparar KPIs", "Explorar gráfico", "Drill down", "Exportar"]);
    }

    private static PatternPageViewModel Pattern(string slug, string title, string description, string use, string avoid, string[] benefits, string[] components, string[] flow)
    {
        var tag = slug switch
        {
            "crud" => "<orizon-data-table label=\"Clientes\" searchable pageable>...</orizon-data-table>",
            "dashboard" => "<orizon-metric-card title=\"Receita\" value=\"R$ 1,2 mi\" trend=\"positive\" />",
            "login" => "<orizon-email name=\"Email\" label=\"E-mail\" required />",
            "wizard" => "<orizon-progress value=\"50\" label=\"Etapa 2 de 4\" />",
            "search" => "<orizon-search-box placeholder=\"Pesquisar\" shortcut full-width />",
            "master-detail" => "<orizon-tabs label=\"Detalhes do cliente\">...</orizon-tabs>",
            "data-entry" => "<orizon-accordion>...</orizon-accordion>",
            "approval-workflow" => "<orizon-confirm-dialog id=\"approve\" title=\"Aprovar?\" />",
            "kanban" => "<orizon-kanban label=\"Sprint\" draggable>...</orizon-kanban>",
            _ => "<orizon-chart type=\"line\" labels='[...]' series='[...]' />"
        };
        var code = new PatternCodeSamples(
            $"@* {title}: composição de componentes existentes *@\n{tag}",
            $"<!-- HTML renderizado do pattern {title} -->\n<section class=\"pattern-demo\" aria-label=\"{title}\">...</section>",
            ".feature-context { /* somente layout da aplicação; componentes usam tokens Orizon */ }",
            $"public IActionResult Index() => View(new {title.Replace(" ", string.Empty)}ViewModel());");
        return new(slug, title, description, use, avoid, benefits, components, flow, code);
    }
}
