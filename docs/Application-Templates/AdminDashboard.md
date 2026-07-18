# Admin Dashboard

## Objetivo
Concentrar métricas, saúde operacional, notificações e ações frequentes.

## Layout e componentes
Toolbar sobre `DashboardGrid`; `MetricCard`, `DashboardWidget`, `Chart`, `ProgressRing`, `Gauge`, `ActivityFeed`, `QuickActions` e `NotificationCenter` compõem as regiões.

## Exemplo Razor
```razor
<orizon-dashboard-grid columns="12" responsive>
 <orizon-metric-card title="Receita" value="R$ 428 mil" trend="positive" />
</orizon-dashboard-grid>
```

## Boas práticas
Priorize de três a cinco métricas e mantenha ações destrutivas fora do primeiro nível. Em telas menores o grid deve empilhar sem alterar a ordem semântica. Nomeie toolbar, gráficos e feeds; mantenha todos os acionadores acessíveis por teclado.
