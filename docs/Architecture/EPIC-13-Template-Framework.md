# EPIC 13

# Orizon Template Framework

---

>Status

Planned

---

# Visão

A Orizon UI evoluirá de uma biblioteca de componentes para uma plataforma completa de Templates Enterprise reutilizáveis.

A partir desta EPIC, aplicações ASP.NET Core MVC poderão construir interfaces completas utilizando apenas componentes da biblioteca.

Exemplo:

```razor
<orizon-dashboard layout="renova" />
```

ou

```razor
<orizon-crud entity="Products" />
```

sem copiar HTML entre projetos.

---

# Missão

Eliminar duplicação de interface entre aplicações.

Todos os sistemas deverão consumir uma única biblioteca.

```
Renova
Distribuidora
Imobiliário
CRM
ERP
RH
Healthcare

        │

        ▼

     Orizon UI
```

A biblioteca passa a ser o único ponto de evolução visual.

---

# Objetivos Estratégicos

## Reutilização

Todo Template deverá ser reutilizável.

---

## Padronização

Todos os sistemas compartilham a mesma linguagem visual.

---

## Produtividade

Novos projetos devem iniciar com Templates prontos.

---

## Evolução Centralizada

Uma melhoria realizada na biblioteca beneficia todos os consumidores.

---

## Compatibilidade

Nenhuma quebra para consumidores existentes.

---

# Princípios Arquiteturais

## Component First

Templates nunca implementam HTML próprio.

Eles apenas reutilizam Components.

```
Templates

↓

Widgets

↓

Components

↓

Design Tokens
```

---

## Composition over Copy

Nunca copiar código.

Sempre compor.

---

## Theme Driven

Toda aparência será baseada em Design Tokens.

Nenhuma cor fixa.

---

## Widget Driven

Templates são compostos exclusivamente por Widgets.

---

## Layout Driven

O comportamento visual muda apenas alterando Layouts.

---

# Arquitetura

```
Applications

↓

Templates

↓

Layouts

↓

Widgets

↓

Components

↓

Design Tokens
```

---

# Estrutura Oficial

```
src/

Orizon.UI/

    Components/

    Widgets/

    Templates/

    Layouts/

    Themes/

    TagHelpers/

    ViewComponents/

    Services/

    Models/
```

---

# Componentes

Responsáveis pela menor unidade reutilizável.

Exemplos

- Button

- Card

- Badge

- Input

- Grid

- Modal

- Sidebar

- Topbar

---

# Widgets

Agrupam componentes.

Exemplos

- Hero

- KPI Grid

- KPI Card

- Activity Feed

- Timeline

- Calendar

- Quick Actions

- Finance Widget

- CRM Widget

- Analytics Widget

---

# Templates

Agrupam Widgets.

Exemplos

- Dashboard

- CRUD

- Analytics

- Reports

- Wizard

- Login

- Settings

- Profile

- Kanban

---

# Layouts

Organizam Templates.

Exemplos

- Enterprise

- Compact

- Executive

- Operations

- Analytics

---

# Themes

Especializam aparência.

Exemplos

- Light

- Dark

- Corporate

- Renova

- Distribution

- RealEstate

- Healthcare

---

# API Pública

A API pública deverá ser simples.

Exemplo

```razor
<orizon-dashboard />
```

Nunca

```html
<div class="dashboard">
```

---

# Convenções

Componentes

```
OrizonButton
```

Widgets

```
KpiGridWidget
```

Templates

```
DashboardTemplate
```

Layouts

```
EnterpriseLayout
```

Tag Helpers

```razor
<orizon-dashboard>

<orizon-crud>

<orizon-login>
```

---

# Regras

Nunca duplicar HTML.

Nunca duplicar CSS.

Nunca duplicar JavaScript.

Nunca criar Templates específicos para um único projeto.

Sempre reutilizar Widgets.

Sempre reutilizar Components.

---

# Compatibilidade

Toda evolução será incremental.

Nenhuma API pública existente será removida.

Breaking Changes somente em versões major.

---

# Performance

Prioridades

- Lazy Rendering

- CSS Compartilhado

- JavaScript Modular

- Static Web Assets

- Cache

- View Components leves

---

# Qualidade

Cada Sprint deverá terminar com:

- Build limpo

- Zero warnings

- Sandbox funcionando

- Projeto consumidor funcionando

- Compatibilidade preservada

- Documentação atualizada

---

# Roadmap Oficial

## Sprint T1

Foundation + Widget Framework

---

## Sprint T2

Dashboard Framework

---

## Sprint T3

Workspace Templates

---

## Sprint T4

Authentication & Administration

---

## Sprint T5

Sandbox & Consumer

---

## Sprint T6

Packaging & Documentation

---

# Resultado Esperado

Ao final da EPIC qualquer aplicação ASP.NET Core MVC poderá ser construída utilizando apenas Templates.

Exemplo

```razor
<orizon-dashboard layout="enterprise"/>

<orizon-crud entity="Products"/>

<orizon-login/>
```

Todos os Templates deverão reutilizar exclusivamente:

Templates

↓

Widgets

↓

Components

↓

Design Tokens

garantindo padronização, reutilização e evolução centralizada para toda a plataforma Orizon.