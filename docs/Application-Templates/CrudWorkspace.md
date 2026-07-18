# CRUD Workspace

## Objetivo
Gerenciar entidades com busca, filtragem, seleção e feedback de operações.

## Layout e componentes
`CommandBar`, `Toolbar`, `SearchBox`, `FilterBar`, `DataTable`, `Drawer`, `ConfirmDialog`, `LoadingOverlay` e `QuickActions`.

## Exemplo Razor
```razor
<orizon-command-bar title="Clientes" search primary-text="Novo cliente" />
<orizon-data-table label="Clientes" selectable sortable pageable>...</orizon-data-table>
```

## Boas práticas
Preserve filtros após operações, confirme exclusões e represente loading, vazio e erro na região dos dados. No mobile, permita rolagem apenas dentro da tabela. Use rótulos de seleção específicos e devolva o foco ao fechar Drawer ou diálogo.
