# ConfirmDialog

Confirma ações nos tipos `danger`, `warning`, `info` e `success`. API: `id`, `title`, `message`, `type`, `confirm-text`, `cancel-text`, `open`.

```razor
<button data-confirm-target="#confirm">Excluir</button><orizon-confirm-dialog id="confirm" type="danger" />
```

Usa `alertdialog`, fecha com Escape e restaura foco. A confirmação dispara `orizon:confirm`; a operação final pertence ao consumidor.
