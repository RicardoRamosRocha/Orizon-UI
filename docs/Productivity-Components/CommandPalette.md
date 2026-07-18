# CommandPalette

Diálogo pesquisável aberto por `Ctrl+K` ou `Cmd+K`. Aceita `id`, `title`, `placeholder`, `shortcut`, `empty-text` e `open`.

```razor
<orizon-command-palette id="commands">
 <section data-command-group><h3>Criação</h3>
  <button role="option" data-command-item data-command="novo cliente">Novo cliente</button>
 </section>
</orizon-command-palette>
```

Use `data-command-palette-target="commands"` em um acionador. Setas navegam, `Enter` executa e `Esc` fecha. Links são navegados; botões recebem clique.
