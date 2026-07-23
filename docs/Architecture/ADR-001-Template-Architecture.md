# ADR-001

# Template Architecture

---

Status

Accepted

---

Authors

Orizon UI Team

---

Date

2026

---

# Context

A Orizon UI iniciou sua evolução como uma biblioteca de componentes reutilizáveis para ASP.NET Core MVC.

Com a expansão da plataforma Orizon, surgiram diversos sistemas compartilhando a mesma identidade visual:

- Renova
- Distribuidora
- CRM
- Imobiliário
- ERP
- Healthcare
- Novos produtos futuros

Embora todos utilizem os mesmos componentes, ainda existe duplicação de páginas completas entre aplicações.

Cada sistema possui sua própria implementação de:

- Dashboard
- CRUD
- Login
- Analytics
- Configurações
- Kanban

Isso aumenta:

- custo de manutenção
- divergência visual
- tempo de desenvolvimento
- dificuldade de evolução

Era necessária uma arquitetura que permitisse reutilizar páginas completas, mantendo flexibilidade e compatibilidade.

---

# Problema

Como reutilizar interfaces completas sem duplicar HTML entre aplicações?

---

# Decisão

A arquitetura oficial da Orizon UI passa a ser baseada em cinco níveis de abstração.

```

Applications

↓

Templates

↓

Widgets

↓

Components

↓

Design Tokens

```

Cada camada possui responsabilidade única.

---

# Arquitetura

## 1 — Design Tokens

Responsável por:

- cores
- tipografia
- spacing
- radius
- elevation
- motion
- accessibility

Nunca conhece Components.

---

## 2 — Components

Menor unidade reutilizável.

Exemplos

- Button
- Card
- Input
- Badge
- Grid
- Modal

Nunca conhecem Templates.

Podem conhecer apenas Design Tokens.

---

## 3 — Widgets

Widgets agrupam Components.

Exemplo

```

KPI Widget

├── Card

├── Icon

├── Badge

└── Progress

```

Outro exemplo

```

Activity Feed

├── Avatar

├── Timeline

├── Card

└── Button

```

Widgets nunca implementam regras específicas de um sistema.

---

## 4 — Templates

Templates agrupam Widgets.

Exemplo

```

Dashboard

├── Hero

├── KPI Grid

├── Analytics

├── Timeline

├── Calendar

├── Activity Feed

└── Quick Actions

```

Templates não implementam componentes básicos.

Sempre reutilizam Widgets.

---

## 5 — Applications

Aplicações apenas consomem Templates.

Exemplo

```razor
<orizon-dashboard />
```

ou

```razor
<orizon-crud />
```

---

# Responsabilidades

## Design Tokens

Visual.

---

## Components

Interface básica.

---

## Widgets

Blocos funcionais.

---

## Templates

Fluxo da aplicação.

---

## Applications

Dados.

---

# Dependências Permitidas

```

Applications

↓

Templates

↓

Widgets

↓

Components

↓

Design Tokens

```

Dependências invertidas são proibidas.

Exemplo

Component não pode conhecer Template.

Widget não pode conhecer Application.

---

# Regras Arquiteturais

## Regra 1

Templates nunca implementam HTML duplicado.

---

## Regra 2

Widgets nunca possuem identidade de negócio.

Errado

```

RenovaDashboardWidget

```

Correto

```

DashboardHeroWidget

```

---

## Regra 3

Components nunca acessam banco de dados.

---

## Regra 4

Templates nunca conhecem Entity Framework.

---

## Regra 5

Toda aparência deve vir dos Design Tokens.

---

## Regra 6

Nenhum CSS poderá utilizar cores literais.

Errado

```

#FFFFFF

```

Correto

```

var(--orizon-color-surface)

```

---

## Regra 7

Todo Template deverá possuir API pública simples.

Exemplo

```razor
<orizon-dashboard />
```

Nunca

```razor
<div class="dashboard-layout">
```

---

# Estrutura Oficial

```

src/

Orizon.UI

Components/

Widgets/

Templates/

Layouts/

Themes/

Services/

Models/

TagHelpers/

ViewComponents/

```

---

# Benefícios

## Reutilização

Um Dashboard atende todos os sistemas.

---

## Consistência

Todos os produtos compartilham identidade visual.

---

## Evolução

Uma melhoria beneficia todos os consumidores.

---

## Produtividade

Novas aplicações começam prontas.

---

## Testabilidade

Widgets podem ser testados isoladamente.

Templates podem ser testados independentemente.

---

# Consequências

A biblioteca passa a ser responsável por:

- Layouts

- Dashboards

- CRUD

- Login

- Analytics

- Administração

Aplicações deixam de implementar HTML.

Passam apenas a fornecer dados.

---

# Compatibilidade

Esta arquitetura preserva compatibilidade com consumidores existentes.

Nenhuma API pública será removida.

Toda evolução será incremental.

---

# Status

Esta decisão torna-se a arquitetura oficial da Orizon UI.

Toda nova funcionalidade deverá respeitar este documento.

---

# Referências

EPIC-13-Template-Framework.md

Developer-Guide.md

Coding-Standards.md

Versioning-Strategy.md