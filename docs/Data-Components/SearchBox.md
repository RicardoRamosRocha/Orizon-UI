# SearchBox

Pesquisa com limpar, debounce e atalho opcional. API: `name`, `value`, `placeholder`, `label`, `debounce`, `loading`, `full-width`, `disabled`, `shortcut`.

```razor
<orizon-search-box placeholder="Pesquisar produtos..." debounce="300" shortcut />
```

Dispara `orizon:search` com `detail.value`. Ctrl/Cmd+K foca a primeira pesquisa habilitada com shortcut; a consulta remota pertence à aplicação.
