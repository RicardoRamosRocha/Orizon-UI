# DashboardWidget

Container com `title`, `subtitle`, `icon`, `loading`, `empty`, `error`, `refresh`, `collapsible`, `collapsed`, `fullscreen`, `column-span`, `row-span`, `footer`.
```razor
<orizon-dashboard-widget title="Receita" refresh collapsible fullscreen column-span="8">...</orizon-dashboard-widget>
```
Refresh dispara `orizon:widget-refresh`; finalize pelo callback `event.detail.complete()`. Collapse e fullscreen funcionam por teclado e Escape.
