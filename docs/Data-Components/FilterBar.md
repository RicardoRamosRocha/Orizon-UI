# FilterBar

Container responsivo para chips, badges e ações. API: `label`, `count`, `clear-label`, `show-clear` e conteúdo.

```razor
<orizon-filter-bar count="2"><orizon-chip text="Ativos" removable /></orizon-filter-bar>
```

O botão dispara `orizon:filters-clear`; o consumidor controla o estado e a contagem.
