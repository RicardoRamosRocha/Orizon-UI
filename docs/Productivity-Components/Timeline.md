# Timeline

Sequência vertical ou horizontal de eventos. Aceita `orientation`, `label` e `compact`.

```razor
<orizon-timeline label="Histórico">
 <article data-timeline-item role="listitem">
  <span data-timeline-marker>✓</span>
  <div data-timeline-card><time>Hoje</time><h3>Aprovado</h3></div>
 </article>
</orizon-timeline>
```

Marcadores podem conter ícones ou estados. A composição preserva datas, cartões e descrições sem impor um modelo de dados.
