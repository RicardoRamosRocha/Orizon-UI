# Sprint T3

# Workspace Templates

Status

Planned

Priority

Critical

---

# Objetivo

Criar a camada de Workspaces da Orizon UI.

Os Workspaces representam telas completas reutilizáveis para aplicações corporativas, compostas por Dashboard, Widgets e Components.

Ao término desta Sprint, aplicações consumidoras deverão conseguir construir módulos completos utilizando apenas Templates da biblioteca.

---

# Visão

Atualmente, cada sistema implementa suas próprias telas de:

- Cadastro
- Pesquisa
- Analytics
- Kanban
- Relatórios
- Assistentes (Wizard)

A partir desta Sprint, essas estruturas passam a fazer parte da Orizon UI.

As aplicações consumidoras passam apenas a fornecer dados e regras de negócio.

---

# Arquitetura

```

Applications

↓

Workspace Templates

↓

Dashboard Framework

↓

Widgets

↓

Components

↓

Design Tokens

```

---

# Estrutura

```
Templates/

    Crud/

    Analytics/

    Reports/

    Wizard/

    Kanban/

    MasterDetail/

ViewComponents/

    Crud/

    Analytics/

    Reports/

    Wizard/

    Kanban/

TagHelpers/

    Crud/

    Analytics/

    Reports/

    Wizard/

    Kanban/

Models/

    Workspaces/

Services/

    Workspaces/
```

---

# Workspaces Planejados

## CRUD Workspace

Template padrão para operações de cadastro.

Inclui:

- Header
- Toolbar
- Search
- Filters
- Grid
- Pagination
- Empty State
- Loading
- Footer

API

```razor
<orizon-crud />
```

---

## Analytics Workspace

Template para indicadores e gráficos.

Inclui:

- Dashboard Hero
- KPIs
- Charts
- Timeline
- Cards
- Comparativos
- Insights

API

```razor
<orizon-analytics />
```

---

## Reports Workspace

Template para consultas e relatórios.

Inclui:

- Filtros
- Tabela
- Exportação
- Impressão
- Visualização
- Histórico

API

```razor
<orizon-reports />
```

---

## Kanban Workspace

Template para gestão visual.

Inclui:

- Colunas
- Cards
- Drag & Drop
- Indicadores
- Responsáveis
- Etiquetas
- Filtros

API

```razor
<orizon-kanban />
```

---

## Wizard Workspace

Template para processos guiados.

Inclui:

- Steps
- Progress
- Navegação
- Validação
- Resumo
- Conclusão

API

```razor
<orizon-wizard />
```

---

## Master Detail Workspace

Template para relacionamento entre listas e detalhes.

Inclui:

- Grid principal
- Painel lateral
- Tabs
- Histórico
- Ações rápidas

API

```razor
<orizon-master-detail />
```

---

# Componentes Reutilizados

Todos os Workspaces utilizarão exclusivamente:

- Dashboard Framework
- Hero Widget
- KPI Grid
- Activity Feed
- Timeline
- Quick Actions
- Enterprise Grid
- Forms Premium
- Navigation Experience

Nenhum Workspace implementará componentes próprios.

---

# Configuração

Cada Workspace possuirá um objeto de configuração.

Exemplo

```csharp
new CrudWorkspaceOptions
{
    EnableSearch = true,
    EnableFilters = true,
    EnableExport = true,
    EnablePagination = true
}
```

---

# Responsabilidades

Os Workspaces devem:

✔ reutilizar Widgets

✔ reutilizar Components

✔ reutilizar Layouts

✔ reutilizar Design Tokens

✔ nunca acessar banco de dados

✔ nunca conhecer regras de negócio

---

# Não Faz Parte

Esta Sprint NÃO implementa:

- Login
- Registro
- Administração
- Configurações
- Perfis
- Publicação NuGet

Esses itens pertencem às próximas Sprints.

---

# Critérios de Aceite

Todos os Workspaces deverão:

✔ possuir Tag Helper

✔ possuir View Component

✔ possuir documentação

✔ funcionar em Light e Dark

✔ ser responsivos

✔ reutilizar exclusivamente Widgets e Components

---

# Testes

Validar:

- Renderização
- Responsividade
- Acessibilidade
- Performance
- Navegação
- Integração no Sandbox
- Validação em projeto consumidor

---

# Checklist

☐ Build limpo

☐ Zero warnings

☐ CRUD Workspace

☐ Analytics Workspace

☐ Reports Workspace

☐ Kanban Workspace

☐ Wizard Workspace

☐ Master Detail Workspace

☐ Tag Helpers

☐ View Components

☐ Models

☐ Sandbox atualizado

☐ Projeto consumidor validado

☐ Documentação atualizada

---

# Resultado Esperado

Ao término da Sprint T3, a Orizon UI deixará de fornecer apenas componentes e dashboards.

Ela passará a disponibilizar Workspaces Enterprise completos, reutilizáveis e independentes de qualquer aplicação específica.

Os sistemas consumidores poderão montar módulos inteiros utilizando apenas Templates públicos da biblioteca, reduzindo drasticamente duplicação de código, tempo de desenvolvimento e custo de manutenção.