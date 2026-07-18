# Cep

CEP com máscara `00000-000` e marcador `data-viacep-ready`; nenhuma chamada externa é realizada.
```razor
<orizon-cep asp-for="Address.Cep" label="CEP" />
```
Integrações ViaCEP futuras devem tratar consentimento, falhas e edição manual.
