# Errors & Empty States

## Objetivo
Responder de forma humana e acionável a 401, 403, 404, 500, offline, ausência de dados, busca vazia e falha de rede.

## Layout e componentes
Uma grade de `EmptyState` com ícone, título, descrição e Button; há uma variação no tema escuro.

## Exemplo Razor
```razor
<orizon-empty-state title="404 · Página não encontrada" description="O endereço pode ter mudado.">
 <orizon-button text="Ir ao início" />
</orizon-empty-state>
```

## Boas práticas
Diga o que ocorreu e ofereça uma próxima ação segura. Em qualquer viewport, preserve uma única coluna legível quando necessário. Use heading, texto direto e links reais; não dependa apenas do código ou do ícone para comunicar o erro.
