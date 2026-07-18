# Master Detail

## Objetivo
Explorar uma coleção e seus detalhes mantendo contexto e seleção.

## Layout e componentes
`CommandBar` e `SplitView`; pesquisa e `TreeView` no painel mestre; Cards, Tabs, Timeline, ActivityFeed e Drawer no detalhe.

## Exemplo Razor
```razor
<orizon-split-view initial-size="34" resizable persist-key="accounts">
 <section data-split-primary>Lista</section>
 <section data-split-secondary>Detalhes</section>
</orizon-split-view>
```

## Boas práticas
Use URLs ou estado da aplicação para preservar a seleção. Em mobile, empilhe os painéis e ofereça retorno claro. Nomeie árvore e abas; o separador deve funcionar com setas e as mudanças de seleção devem anunciar o novo contexto.
