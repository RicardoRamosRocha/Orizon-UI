# Orizon UI

Biblioteca oficial de interface reutilizavel da Orizon Platform.

## Estrutura

- `src/Orizon.UI`: Razor Class Library com Tag Helpers, layouts, estilos e scripts compartilhados.
- `samples/Orizon.UI.Sandbox`: aplicacao ASP.NET Core MVC usada como catalogo visual e validacao manual.

## Como executar

```bash
dotnet restore
dotnet build Orizon.UI.sln --configuration Release
dotnet run --project samples/Orizon.UI.Sandbox
```

Abra o catalogo em `/components`.

## Como referenciar

Em uma aplicacao ASP.NET Core, referencie o projeto ou pacote `Orizon.UI` e habilite os Tag Helpers:

```razor
@addTagHelper *, Orizon.UI
```

Inclua os assets publicados pela Razor Class Library:

```html
<link rel="stylesheet" href="~/_content/Orizon.UI/css/orizon.css" />
<script type="module" src="~/_content/Orizon.UI/js/orizon.js"></script>
```

## Componentes

- Buttons
- Cards
- Alerts
- Badges
- Forms
- Tables
- Modal
- Dropdown
- Tabs
- Pagination
- Empty State
- Spinner
- Progress
- Toast
- Avatar

## Sandbox

O Sandbox demonstra estados, variacoes, responsividade e dark mode dos componentes. As paginas ficam em `samples/Orizon.UI.Sandbox/Views/Components` e as rotas sao expostas por `ComponentsController`.

## Como criar novos componentes

1. Crie um Tag Helper em `src/Orizon.UI/TagHelpers`.
2. Crie o CSS em `src/Orizon.UI/wwwroot/css/components`.
3. Adicione o import em `src/Orizon.UI/wwwroot/css/orizon.css` e, se for componente isolado, em `orizon-components.css`.
4. Adicione JavaScript em `src/Orizon.UI/wwwroot/js/components` somente quando houver comportamento interativo.
5. Integre o script em `src/Orizon.UI/wwwroot/js/orizon.js`.
6. Adicione uma pagina de exemplo no Sandbox apenas quando ela ajudar a validar visualmente o componente.

Use tokens CSS existentes, preserve acessibilidade por teclado e valide claro/escuro antes de publicar.
