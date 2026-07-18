# Toolbar

Agrupa ações, pesquisa e filtros. API: `label`, `sticky`, `wrap` e conteúdo Razor. Use `data-toolbar-group`, `data-toolbar-right` e `data-toolbar-separator` para regiões.

```razor
<orizon-toolbar sticky><div data-toolbar-group>...</div><div data-toolbar-right>...</div></orizon-toolbar>
```

Emite `role=toolbar`; mantenha nomes acessíveis nos controles. Não gerencia autorização ou execução das ações.
