# Entity Form

## Objetivo
Cadastrar entidades complexas com agrupamento progressivo e validação clara.

## Layout e componentes
Toolbar fixa, indicadores de etapa e Cards com TextBox, Number, Currency, Email, Phone, CPF, CNPJ, CEP, Date, Time, DateTime, Select, MultiSelect, Upload e Rating.

## Exemplo Razor
```razor
<orizon-card title="Identificação">
 <orizon-text-box name="Name" label="Razão social" required />
 <orizon-cnpj name="Cnpj" label="CNPJ" required />
</orizon-card>
```

## Boas práticas
Agrupe por intenção, sinalize campos obrigatórios e preserve rascunhos. O grid vira uma coluna em telas estreitas. Erros devem estar associados ao campo, o foco deve avançar na ordem visual e uploads precisam de instruções de formato e tamanho.
