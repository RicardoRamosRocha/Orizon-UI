# Filtering

## Objetivo
Reduzir conjuntos por busca global e filtros por coluna.

## API e parâmetros
`filterable` no grid/coluna, pesquisa do toolbar e `OrizonGridFilterPanel` com `title` e `open`.

```razor
<orizon-grid-filter-panel title="Filtros avançados">
 <p>Filtros adicionais da aplicação.</p>
</orizon-grid-filter-panel>
```

## Boas práticas, desempenho e acessibilidade
Use quick filter para exploração e filtros tipados no modo servidor. O modo cliente normaliza texto em português e filtra uma vez por atualização. Rotule filtros adicionais e devolva foco ao acionador ao integrar abertura externa.
