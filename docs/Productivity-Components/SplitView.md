# SplitView

Divide a área em dois painéis. Aceita `orientation`, `initial-size`, `min-size`, `max-size`, `resizable`, `collapsible` e `persist-key`.

```razor
<orizon-split-view initial-size="35" resizable collapsible persist-key="customer-split">
 <section data-split-primary>Lista</section>
 <section data-split-secondary>Detalhes</section>
</orizon-split-view>
```

O separador suporta arraste, setas, `Home` e `End`. O tamanho é salvo no `localStorage` quando `persist-key` é informado.
