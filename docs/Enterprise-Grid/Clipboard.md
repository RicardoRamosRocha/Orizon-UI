# Clipboard Enterprise

Ctrl+C produz TSV compatível com Excel e Google Sheets. Ctrl+V lê texto tabular e inicia alterações quando `edit-mode` permite. CSV usa escaping de aspas e separadores.

```razor
<orizon-enterprise-grid edit-mode="Batch" cell-selection>
 <orizon-grid-column field="Quantity" editable type="number" />
</orizon-enterprise-grid>
```

Clipboard depende de contexto seguro e permissão do navegador. Valide tipos e limites durante paste; Ctrl+Z deve reverter o lote colado.
