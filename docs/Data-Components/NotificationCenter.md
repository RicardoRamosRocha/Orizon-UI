# NotificationCenter

Drawer com contador e itens lidos/não lidos. Container: `id`, `title`, `unread-count`, `open`, `clear-label`. Item `orizon-notification`: `title`, `time`, `unread`, `href`.

```razor
<button data-notifications-target="#center">Abrir</button><orizon-notification-center id="center"><orizon-notification title="Pedido" unread>...</orizon-notification></orizon-notification-center>
```

Suporta marcar, marcar todas, limpar e Escape. Persistência e sincronização do contador ficam a cargo da aplicação.
