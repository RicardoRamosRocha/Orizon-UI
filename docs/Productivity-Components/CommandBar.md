# CommandBar

Barra responsiva de contexto e ações. Aceita `title`, `subtitle`, `breadcrumb`, `search`, `search-placeholder`, `primary-text`, `primary-href`, `sticky` e `keyboard`. O conteúdo interno forma as ações secundárias.

```razor
<orizon-command-bar title="Clientes" subtitle="Gestão da carteira" breadcrumb="CRM / Clientes" search primary-text="Novo cliente" sticky>
  <button type="button">Exportar</button>
</orizon-command-bar>
```

Com `keyboard`, a tecla `/` move o foco para a pesquisa quando o usuário não está digitando em outro campo.
