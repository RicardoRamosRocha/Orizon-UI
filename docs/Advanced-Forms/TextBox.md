# TextBox

Campo textual geral. API comum mais `clearable`, `maxlength` e `counter`.
```razor
<orizon-text-box asp-for="Product.Name" label="Nome" helper-text="Obrigatório" required clearable />
```
Prefira `asp-for` para binding e validação. Label, helper e erro são associados por ARIA; não use placeholder como único rótulo.
