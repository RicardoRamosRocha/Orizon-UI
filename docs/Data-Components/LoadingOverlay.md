# LoadingOverlay

Bloqueia visualmente uma região. API: `active`, `text`, `blur`, `opacity` e conteúdo.

```razor
<orizon-loading-overlay active text="Salvando...">...</orizon-loading-overlay>
```

Usa `aria-busy`/`role=status`. Controle em runtime com `OrizonUI.setLoading(element, active)`; não bloqueia requisições concorrentes por conta própria.
