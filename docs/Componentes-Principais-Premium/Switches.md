# Switches

`orizon-switch` é um checkbox semântico com `asp-for` ou `name`/`value`. Aceita `checked`, `label`, `description`, `disabled`, `size`, `color` e `validation-message`.

```razor
<orizon-switch asp-for="Produto.Ativo" label="Produto ativo" description="Disponível nas operações." />
```

Gera o par hidden `false` + checkbox `true`, label clicável, foco visível e `aria-describedby`. A mensagem usa o ModelState atual; não substitui `asp-validation-summary`.
