# Analytics Dashboard

## Objetivo
Dar suporte a decisões executivas com comparações, tendências e metas.

## Layout e componentes
`CommandBar`, `DashboardGrid`, `MetricCard`, `DashboardWidget`, Chart, Sparkline, ProgressRing, Gauge, legendas, tooltips e estados vazios.

## Exemplo Razor
```razor
<orizon-chart type="line" labels='["Jun","Jul"]'
 series='[{"name":"Receita","data":[850,920]}]' legend tooltip />
```

## Boas práticas
Informe período, unidade e atualização. Reduza séries no mobile e mantenha legenda próxima. Todo gráfico precisa de título ou rótulo acessível e cores com contraste; estados de ausência devem explicar como obter dados.
