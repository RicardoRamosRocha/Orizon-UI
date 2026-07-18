# MultiSelect

Seleção múltipla pesquisável com chips, `items`, `select-all` e `clearable`.
```razor
<orizon-multi-select asp-for="TagIds" items="Model.Tags" label="Tags" />
```
Emite inputs checkbox com o mesmo nome e `orizon:multi-change`. Para listas enormes, use busca remota em outro fluxo.
