# Buttons

`orizon-button` representa ações ou links; `orizon-icon-button` concentra ações compactas.

- Button: `text`, `icon`, `icon-position`, `variant` (`solid`, `soft`, `outline`, `ghost`, `gradient`, `glass`, `link`), `color`, `size`, `type`, `href`, `loading`, `disabled`, `full-width`/`block` e `aria-label`.
- IconButton: `icon`, `label` obrigatório, `tooltip`, `variant`, `color`, `size`, `shape`, `type`, `href`, `loading` e `disabled`.

```razor
<orizon-button text="Salvar" icon="floppy-disk" variant="gradient" color="primary" type="submit" />
<orizon-icon-button icon="pencil-simple" label="Editar produto" shape="circle" />
```

Foco é visível; loading bloqueia interação. Em links desabilitados, `aria-disabled` e `tabindex=-1` são emitidos. Limitação: `label` de IconButton deve ser sempre informado pelo consumidor.
