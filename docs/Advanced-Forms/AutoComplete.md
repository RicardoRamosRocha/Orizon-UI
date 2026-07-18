# AutoComplete

Combobox com `items`, `debounce` e `min-length`.
```razor
<orizon-auto-complete asp-for="City" items="Model.Cities" debounce="300" />
```
Emite `orizon:autocomplete` para integração posterior. Resultados locais são limitados; valide sempre o valor final.
