# Tree Data

`tree-data`, `parent-field`, `children-field` e `lazy-load` configuram hierarquias. Nós usam indentação, ícones, `aria-expanded` e estado de expansão.

```razor
<orizon-enterprise-grid items="Model.Nodes" key-field="Id"
 tree-data parent-field="ParentId" lazy-load persist-key="organization-tree">...</orizon-enterprise-grid>
```

Para lazy loading remoto, envie `ParentKey` no `OrizonGridDataRequest` e retorne somente filhos diretos. Use chaves estáveis para preservar expansão.
