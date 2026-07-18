# Tooltip

`orizon-tooltip` envolve conteúdo e aceita `text`, `position`, `delay`, `disabled` e classes.

```razor
<orizon-tooltip text="Editar" position="top"><orizon-icon-button icon="pencil-simple" label="Editar" /></orizon-tooltip>
```

Surge em hover e foco, usa `role=tooltip` e `aria-describedby`. Não use tooltip como única fonte de informação crítica. Em telas estreitas, posições laterais migram para cima.
