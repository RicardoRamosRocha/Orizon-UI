# Password

Senha com `show-toggle`, `strength` e `confirmation-for` opcional.
```razor
<orizon-password asp-for="Password" label="Senha" strength show-toggle />
```
O medidor é orientação visual, não substitui política/validação no servidor. O toggle possui nome acessível dinâmico.
