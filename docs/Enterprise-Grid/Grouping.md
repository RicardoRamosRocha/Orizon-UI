# Grouping

## Objetivo
Organizar linhas por valores de uma coluna e permitir expandir/recolher grupos.

## API e parâmetros
Use `groupable` e `orizon-grid-group-panel`; arraste um cabeçalho ao painel. O layout salvo preserva o campo agrupado.

```razor
<orizon-grid-group-panel placeholder="Arraste uma coluna para agrupar" />
<orizon-grid-column field="Category" title="Categoria" />
```

## Boas práticas, desempenho e acessibilidade
Agrupe por campos de baixa cardinalidade. O agrupamento desativa a janela virtual para preservar a estrutura; para grupos enormes, implemente agregação no server mode. Botões de grupo expõem `aria-expanded`.
