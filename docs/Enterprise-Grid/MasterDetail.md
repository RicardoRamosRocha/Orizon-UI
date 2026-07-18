# Master Detail

`master-detail-template` define o HTML expandido. Tokens `{{Field}}` são substituídos pelos valores da linha.

```razor
<orizon-enterprise-grid items="Model.Orders"
 master-detail-template="&lt;h3&gt;Pedido {{Code}}&lt;/h3&gt;&lt;p&gt;{{Notes}}&lt;/p&gt;">...</orizon-enterprise-grid>
```

O botão na primeira célula expõe `aria-expanded`. Templates são conteúdo confiável do desenvolvedor; valores de domínio devem ser tratados antes de incluir HTML livre.
