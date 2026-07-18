# Skeleton

`orizon-skeleton` representa carregamento nas variantes `text`, `circle`, `rectangle`, `card` e `table-row`. Aceita `width`, `height`, `lines`, `radius`, `animated` e `aria-label`.

```razor
<orizon-skeleton variant="text" width="100%" lines="3" />
```

É oculto da árvore acessível por padrão; com rótulo, torna-se `status`. O shimmer respeita `prefers-reduced-motion`.
