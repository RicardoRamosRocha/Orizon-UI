# StatCard

`orizon-stat-card` apresenta métricas. API: `title`, `value`, `icon`, `description`, `trend`, `trend-type`, `color`, `href`, `action-label`, `loading` e classes.

```razor
<orizon-stat-card title="Vendas" value="R$ 24.780" trend="+12,5%" trend-type="positive" href="/Vendas" />
```

A tendência combina seta, texto acessível e cor. Hover só ocorre com link. Loading usa a linguagem visual do Skeleton; o conteúdo final deve ser atualizado pelo servidor/aplicação.
