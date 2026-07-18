# ChartTooltip

Tooltip reutilizável com `id`, `text`, `visible`. Charts criam uma instância interna quando `tooltip=true`.
```razor
<orizon-chart-tooltip id="chart-help" text="Valor: 120" visible />
```
Tooltips internos respondem a ponteiro e foco, usam `role=tooltip`, `aria-live` e posicionamento limitado ao canvas.
