# EnterpriseGrid

## Objetivo
Renderizar coleções extensas com produtividade de desktop e sem bloquear a interface.

## API e parâmetros
`items`, `key-field`, `label`, `height`, `row-height`, `virtual-scroll`, `virtual-columns`, `selectable`, `cell-selection`, `sortable`, `filterable`, `groupable`, `persist-key`, `infinite-scroll`, `server-mode`, `server-url` e `edit-mode`.

```razor
<orizon-enterprise-grid items="Model.Items" key-field="Id" height="36rem"
 virtual-scroll persist-key="orders" edit-mode="readonly">...</orizon-enterprise-grid>
```

## Boas práticas, desempenho e acessibilidade
Use chave estável e altura explícita. Para grandes volumes prefira virtualização sem pager. Dê um `label` contextual; a grade expõe roles, contagens ARIA, foco e região live. Server mode é contrato preparado e exige integração da aplicação.
