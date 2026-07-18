# Kanban

Quadro de colunas e cartões com drag-and-drop nativo. Aceita `label`, `draggable`, `persist-key` e `compact`.

Use `data-kanban-column data-column-id`, `data-kanban-list` e cartões `draggable="true" data-kanban-card data-card-id`. `data-priority` pode destacar prioridade. Ao soltar um cartão, o componente atualiza a ordem, salva no `localStorage` e emite `orizon:kanban-change` com os identificadores de cartão e coluna.
