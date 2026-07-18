# Kanban Workspace

## Objetivo
Planejar e acompanhar trabalho visualmente entre estados do fluxo.

## Layout e componentes
`CommandBar`, `FilterBar`, `Kanban`, `ActivityFeed`, `QuickActions` e Drawer de filtros.

## Exemplo Razor
```razor
<orizon-kanban label="Roadmap" draggable persist-key="roadmap">
 <section data-kanban-column data-column-id="todo"><div data-kanban-list>...</div></section>
</orizon-kanban>
```

## Boas práticas
Identificadores de cartões e colunas devem ser estáveis. Persista no servidor ao receber `orizon:kanban-change`. No mobile, mantenha rolagem interna do quadro, sem overflow da página. Não comunique prioridade apenas por cor e ofereça alternativa de movimentação por menu na aplicação final.
