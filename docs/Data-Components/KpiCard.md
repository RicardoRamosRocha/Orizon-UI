# KpiCard

Indicador executivo derivado visualmente do StatCard, sem modificar sua API. Aceita `title`, `subtitle`, `value`, `indicator`, `icon`, `percentage`, `trend-type`, `color`, `loading`.

```razor
<orizon-kpi-card title="MRR" subtitle="Atual" value="R$ 84.200" percentage="+14%" trend-type="positive" indicator="74%" />
```

Percentuais combinam seta e texto; `indicator` aceita uma largura CSS percentual confiável definida pela aplicação.
