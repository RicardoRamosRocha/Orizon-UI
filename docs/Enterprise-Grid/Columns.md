# Columns

## Objetivo
Declarar apresentação e comportamento de cada campo.

## API e parâmetros
`field`, `title`, `width`, `min-width`, `flex`, `format`, `type`, `sortable`, `filterable`, `resizable`, `pinned`, `hidden` e `editable`. Formatos: `currency`, `number`, `percent`, `date` e `boolean`.

```razor
<orizon-grid-column field="Description" title="Descrição" width="280" pinned="left" />
<orizon-grid-column field="Price" title="Preço" format="currency" type="currency" />
```

## Boas práticas, desempenho e acessibilidade
Defina larguras previsíveis e fixe poucas colunas. Resize atualiza apenas o template CSS; reorder move metadados, não dados. Títulos tornam-se nomes dos columnheaders. Evite esconder a chave de contexto essencial.
