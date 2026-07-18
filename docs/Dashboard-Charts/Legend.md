# ChartLegend

Legenda reutilizável. API: `items` JSON (`name`, `color`), `toggleable`, `label`.
```razor
<orizon-chart-legend items='[{"name":"Vendas","color":"var(--orizon-chart-1)"}]' />
```
Botões usam `aria-pressed`; legendas internas alternam séries e emitem `orizon:chart-legend`.
