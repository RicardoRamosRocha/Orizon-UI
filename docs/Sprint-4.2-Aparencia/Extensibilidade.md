# Extensibilidade

Para novo tema, crie `themes/nome.css`, sobrescreva tokens semanticos, importe em `orizon.css` e registre o identificador no array `themes` do runtime. Para nova preferencia, crie um token, um atributo `data-*` e exponha a chave no contrato persistido. Componentes nao devem ser modificados.
