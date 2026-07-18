# Selection

## Objetivo
Selecionar células e linhas com mouse ou teclado.

## API e parâmetros
`mode` (`none`, `single`, `multiple`), `checkbox` e `cell`. O grid também aceita `selectable` e `cell-selection`.

```razor
<orizon-grid-selection mode="multiple" checkbox cell />
```

## Boas práticas, desempenho e acessibilidade
Use chaves estáveis; Shift seleciona intervalos, Ctrl/Command alterna e Ctrl/Command+A seleciona a visão filtrada. A seleção usa um `Set`, evitando buscas quadráticas. Linhas expõem `aria-selected` e mudanças emitem `orizon:grid-selection`.
