# Accordion

`orizon-accordion` aceita `mode` (`single` ou `multiple`). Itens aceitam `title`, `icon`, `expanded`, `disabled`, `identifier` e conteúdo Razor.

```razor
<orizon-accordion mode="single"><orizon-accordion-item title="Fiscal" expanded>Conteúdo</orizon-accordion-item></orizon-accordion>
```

Botões incluem `aria-expanded`/`aria-controls`; painéis têm IDs previsíveis por instância. Setas, Home e End navegam entre cabeçalhos. O componente não carrega conteúdo remotamente.
