# Toolbar

## Objetivo
Concentrar pesquisa e ações sobre a grade.

## API e parâmetros
`search`, `add`, `edit`, `delete`, `export` e `refresh`; conteúdo interno adiciona ações customizadas.

```razor
<orizon-grid-toolbar search add edit delete export refresh>
 <button type="button" data-grid-action="archive">Arquivar</button>
</orizon-grid-toolbar>
```

## Boas práticas, desempenho e acessibilidade
Mostre somente ações relevantes e desabilite operações sem seleção na aplicação. A busca filtra sem reconstruir a origem. A toolbar possui nome acessível e controles nativos; no mobile as ações recebem rolagem controlada.
