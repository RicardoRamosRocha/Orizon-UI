# Virtual Scrolling

## Objetivo
Manter constante o número de elementos DOM, mesmo com milhares de registros.

## API e parâmetros
`virtual-scroll`, `virtual-columns`, `row-height`, `height` e `infinite-scroll`.

```razor
<orizon-enterprise-grid items="Model.LargeDataset" height="40rem"
 row-height="44" virtual-scroll infinite-scroll>...</orizon-enterprise-grid>
```

## Boas práticas, desempenho e acessibilidade
A altura de linha deve ser fixa. A janela inclui oito linhas de overscan e atualiza via `requestAnimationFrame`; o spacer conserva a barra de rolagem. `aria-rowcount` mantém a dimensão lógica, enquanto a região live informa o intervalo renderizado.
