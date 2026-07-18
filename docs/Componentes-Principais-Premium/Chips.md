# Chips

`orizon-chip` exibe filtros, estados e seleções. API: `text`, `icon`, `color`, `variant` (`solid`, `soft`, `outline`), `size`, `selected`, `selectable`, `removable`, `disabled`, `value` e `name`.

```razor
<orizon-chip text="Em estoque" color="success" selectable removable value="stock" name="filters" />
```

Seleção funciona por clique, Espaço e Enter. Dispara `orizon:chip-change` e `orizon:chip-remove`. A remoção altera o DOM; persistência remota fica a cargo da aplicação.
