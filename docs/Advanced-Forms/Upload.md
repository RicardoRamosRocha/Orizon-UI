# Upload

`orizon-file-upload` e `orizon-drag-drop-upload` suportam `multiple`, `accept`, `max-size`, `preview` e API comum.
```razor
<orizon-drag-drop-upload asp-for="Files" label="Anexos" multiple accept="image/*,.pdf" />
```
Usam input file real, lista acessível e evento `orizon:files-change`. Tipo/tamanho e segurança devem ser validados no servidor.
