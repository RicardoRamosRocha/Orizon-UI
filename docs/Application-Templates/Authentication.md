# Authentication

## Objetivo
Cobrir login, cadastro, recuperação, alteração, confirmação e preparação para 2FA.

## Layout e componentes
Shell centralizado com Tabs, Cards, Email, Password, TextBox, Switch, Alert e Button; há exemplos claro e escuro.

## Exemplo Razor
```razor
<orizon-email name="Email" label="E-mail" required />
<orizon-password name="Password" label="Senha" strength required />
<orizon-button type="submit" text="Entrar" />
```

## Boas práticas
Não revele se uma conta existe na recuperação, preserve autocomplete e limite tentativas no servidor. O card deve caber em 390px. Mova foco para mensagens de erro, use `aria-live` para confirmações e mantenha códigos 2FA como texto, não número.
