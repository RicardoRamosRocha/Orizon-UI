# Virtualização vertical e horizontal

A janela vertical usa altura fixa, spacer e overscan. Com `virtual-columns`, grades acima de vinte colunas calculam a faixa por `scrollLeft`, renderizando somente colunas visíveis e uma margem lateral.

```razor
<orizon-enterprise-grid virtual-scroll virtual-columns row-height="44" height="40rem">...</orizon-enterprise-grid>
```

O Sandbox demonstra 300 colunas; o contrato comporta 1.000+. Evite larguras automáticas em grandes matrizes. Colunas fixas devem ser poucas, pois permanecem na composição durante a rolagem.
