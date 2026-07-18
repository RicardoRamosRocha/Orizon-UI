# User Profile

## Objetivo
Centralizar identidade pessoal, preferências, segurança e sessões.

## Layout e componentes
Avatar, Chip, Tabs, Cards, campos pessoais, Switch, Select, Password e botões de encerramento de sessão.

## Exemplo Razor
```razor
<orizon-avatar name="Marina Costa" size="xl" status="online" />
<orizon-phone name="Phone" label="Telefone" />
```

## Boas práticas
Separe dados públicos de segurança e confirme encerramentos remotos. Empilhe o cabeçalho no mobile. Avatar precisa de nome alternativo, abas devem suportar setas e alterações de senha devem informar requisitos antes do envio.
