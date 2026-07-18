# Performance

## Objetivo
Sustentar 10 mil, 50 mil e 100 mil linhas sem renderização proporcional do DOM.

## Estratégias
- Janela virtual com overscan constante.
- Fragmentos DOM e `replaceChildren` em lote.
- Scroll agrupado com `requestAnimationFrame`.
- Seleção baseada em `Set` e chave estável.
- Resize via variável CSS, sem medir todas as células.
- Sorting/filtering imutáveis somente quando necessários.
- Contratos `server-mode`, `server-url` e `infinite-scroll` para volumes remotos.

## Boas práticas e acessibilidade
Para 50 mil+ linhas, pagine, filtre e agregue no servidor; envie apenas campos visíveis. Meça com dados reais e evite formatadores caros. A virtualização preserva as contagens ARIA e anuncia o intervalo visível.
