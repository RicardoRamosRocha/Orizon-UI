# Currency

Entrada monetária sem dependências. API adicional: `symbol`, `decimal-separator`, `thousand-separator`.
```razor
<orizon-currency asp-for="Price" label="Preço" symbol="R$" />
```
A máscara é de apresentação; normalize/converta segundo a cultura no binding do servidor.
