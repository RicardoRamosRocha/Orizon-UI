# Exportação XLSX

`OrizonUI.exportToExcel("#grid")` ou um botão `data-grid-export-xlsx` gera um workbook XLSX sem bibliotecas externas.

```html
<button type="button" data-grid-export-xlsx>Exportar XLSX</button>
```

O arquivo contém as colunas visíveis e preserva células numéricas. A implementação cria as partes Open XML e o contêiner ZIP diretamente. Para exportações server-side muito grandes, gere o arquivo no backend e entregue por streaming.
