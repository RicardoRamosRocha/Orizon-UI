# Settings Workspace

## Objetivo
Organizar preferências gerais, segurança e integrações sem sobrecarregar a página.

## Layout e componentes
Sidebar semântica, Toolbar, Cards, Forms, Switch, Select e Accordion, com ações Salvar/Cancelar.

## Exemplo Razor
```razor
<orizon-card title="Preferências">
 <orizon-switch name="Digest" label="Resumo semanal" checked />
</orizon-card>
```

## Boas práticas
Agrupe opções por impacto, mostre alterações não salvas e confirme mudanças sensíveis. A sidebar vira grade ou lista no mobile. Use descrições nos switches, headings ordenados e foco no primeiro erro após salvar.
