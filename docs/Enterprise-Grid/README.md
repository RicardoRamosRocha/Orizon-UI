# Orizon Enterprise Grid

Grade de dados de alto desempenho para ASP.NET Core MVC/Razor, construída com Tag Helpers, CSS e JavaScript ES6 puros.

Recursos principais: virtualização incremental, sorting e multi-sort, filtros, agrupamento, seleção, teclado, clipboard, resize e reorder de colunas, pinning, summaries, paginação, CSV e persistência local. O contrato inclui preparação para edição, infinite scroll, Tree Data, master-detail e server mode.

```razor
<orizon-enterprise-grid items="Model.Products" key-field="Id" virtual-scroll selectable="multiple">
 <orizon-grid-column field="Code" title="Código" width="120" />
 <orizon-grid-column field="Price" title="Preço" format="currency" />
</orizon-enterprise-grid>
```

Importe `orizon.css` e `orizon.js`; nenhum pacote adicional é necessário.
