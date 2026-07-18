# Accessibility

## Objetivo
Garantir uso completo sem mouse e interpretação por leitor de tela.

## API
Forneça `label`, títulos de coluna claros e ações com nomes específicos. O componente aplica `grid`, `rowgroup`, `row`, `columnheader`, `gridcell`, `aria-rowcount`, `aria-colcount`, `aria-rowindex`, `aria-sort` e `aria-selected`.

```razor
<orizon-enterprise-grid label="Pedidos em aberto" items="Model.Orders">...</orizon-enterprise-grid>
```

## Boas práticas, desempenho e acessibilidade
Não comunique estado apenas por cor. Preserve contraste nos temas, foco visível e ordem lógica. Menus usam `menu/menuitem`, pager tem nome e status é `aria-live`. Animações respeitam `prefers-reduced-motion`.
