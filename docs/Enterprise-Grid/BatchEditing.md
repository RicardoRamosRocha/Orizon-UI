# Batch Editing e validação

Use `edit-mode="Batch"` e marque colunas `editable`. As mudanças permanecem em memória até `orizon:grid-batch-save`.

```razor
<orizon-grid-column field="Price" editable required min="0" max="99999"
 validation-message="Preço inválido" />
```

Também existem `regex`, `min-length` e `max-length`. Duplo clique inicia a edição; Enter confirma e Escape cancela. Ctrl+Z/Ctrl+Y operam o histórico. A aplicação deve validar novamente no servidor antes de persistir.
