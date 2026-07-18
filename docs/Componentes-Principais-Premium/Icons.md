# Icons

`orizon-icon` padroniza os SVGs do registro interno, sem biblioteca duplicada. API: `name`, `size`, `weight` (`thin`, `light`, `regular`, `bold`, `fill`), `color`, `aria-hidden`, `aria-label`, `label`, `stroke-width` e `class`.

```razor
<orizon-icon name="shopping-cart" size="24" weight="regular" aria-hidden="true" />
```

Sem rótulo, o ícone é decorativo. Nomes ausentes no registro não geram saída; novos desenhos devem ser adicionados ao registro oficial.
