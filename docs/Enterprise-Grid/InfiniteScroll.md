# Infinite Scroll

Combine `server-mode` e `infinite-scroll`. Ao chegar a três linhas do fim, o runtime envia o próximo `skip/take` e anexa os itens sem limpar o bloco atual.

```razor
<orizon-enterprise-grid server-mode server-url="/grid/data"
 infinite-scroll virtual-scroll height="40rem">...</orizon-enterprise-grid>
```

O endpoint precisa garantir ordenação estável e chaves únicas. Evite mudanças de filtro durante uma requisição; o runtime descarta respostas anteriores por token de atualização.
