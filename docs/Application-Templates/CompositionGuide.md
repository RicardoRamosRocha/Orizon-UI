# Guia de composição

1. Comece pelo objetivo e pela ordem semântica; escolha componentes depois.
2. Use `CommandBar` para contexto e ação primária, `Toolbar` para operações sobre conteúdo e Cards para agrupamento.
3. Mantenha dados nos componentes de dados; não recrie tabelas, filtros ou estados.
4. Use grids fluidos com `minmax(0, 1fr)` e deixe overflow apenas em regiões naturalmente roláveis.
5. Use tokens para espaçamento, cor, tipografia, raio, sombra e transição.
6. Componha loading, vazio, erro e sucesso na mesma região que receberá o conteúdo.
7. Preserve identificadores estáveis em Tabs, Drawers, árvores e Kanban.
8. Respeite o tema do documento; evite valores fixos de cor.

```razor
<div class="workspace">
 <orizon-command-bar title="Workspace" search primary-text="Criar" />
 <orizon-toolbar label="Ações">...</orizon-toolbar>
 <main>...</main>
</div>
```

CSS auxiliar deve cuidar apenas do arranjo da página. Não replique estilos internos dos componentes.
