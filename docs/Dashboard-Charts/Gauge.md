# Gauge

Indicador semicircular com `min`, `max`, `value`, `ranges`, `needle`, `label`, `unit`, `ticks`.
```razor
<orizon-gauge min="0" max="100" value="72" label="Saúde" unit="%" />
```
Emite `role=meter`; `ranges` fica disponível para extensões visuais futuras sem alterar a API.
