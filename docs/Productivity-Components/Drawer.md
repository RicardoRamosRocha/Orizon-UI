# Drawer

Painel em `left`, `right`, `top` ou `bottom`, com overlay ou modo persistente. Aceita `id`, `title`, `position`, `overlay`, `persistent`, `responsive`, `open` e `width`.

```razor
<button data-drawer-target="filters">Filtros</button>
<orizon-drawer id="filters" title="Filtros" position="right">
 Conteúdo do painel
</orizon-drawer>
```

Elementos com `data-drawer-close`, o overlay e `Esc` fecham o painel. O foco inicial e o retorno ao acionador são gerenciados automaticamente.
