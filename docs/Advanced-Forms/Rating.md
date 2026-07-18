# Rating

Avaliação por estrelas com `value`, `max`, `required`, `disabled` e `readonly`.
```razor
<orizon-rating asp-for="Rating" label="Avaliação" max="5" />
```
Botões têm `aria-pressed`; setas alteram o valor. O evento `orizon:rating-change` informa a seleção.
