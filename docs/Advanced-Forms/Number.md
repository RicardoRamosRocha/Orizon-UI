# Number

Inteiros e decimais com `min`, `max` e `step`.
```razor
<orizon-number asp-for="Quantity" label="Quantidade" min="0" max="999" step="1" />
```
Use `[Range]` no modelo; restrições HTML melhoram a entrada, mas o servidor permanece autoritativo.
