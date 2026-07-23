# Public API

> Orizon UI Enterprise Library

Version 2.0

---

# Objetivo

Este documento define toda a API pública da Orizon UI.

Qualquer recurso documentado aqui é considerado suportado e pode ser utilizado pelos projetos consumidores.

Toda API pública deve preservar compatibilidade entre versões.

---

# Filosofia

Projetos consumidores nunca devem conhecer a implementação interna.

Devem conhecer apenas a API pública.

Exemplo

Correto

```razor
<orizon-dashboard />
```

Errado

```html
<div class="dashboard">
```

---

# API Pública

A API pública é composta por:

- Tag Helpers
- View Components
- Models
- Options
- Services
- Design Tokens públicos

---

# Tag Helpers

## Dashboard

```razor
<orizon-dashboard />
```

Parâmetros

```
layout

theme

density

sidebar

topbar

footer

notifications

title

subtitle
```

---

## Dashboard Header

```razor
<orizon-dashboard-header />
```

---

## Sidebar

```razor
<orizon-dashboard-sidebar />
```

---

## Hero

```razor
<orizon-dashboard-hero />
```

---

## KPI Grid

```razor
<orizon-kpi-grid />
```

---

## Activity Feed

```razor
<orizon-activity-feed />
```

---

## Timeline

```razor
<orizon-timeline />
```

---

## Calendar

```razor
<orizon-calendar-widget />
```

---

## Quick Actions

```razor
<orizon-quick-actions />
```

---

## CRUD

```razor
<orizon-crud />
```

---

## Analytics

```razor
<orizon-analytics />
```

---

## Reports

```razor
<orizon-reports />
```

---

## Wizard

```razor
<orizon-wizard />
```

---

## Kanban

```razor
<orizon-kanban />
```

---

## Login

```razor
<orizon-login />
```

---

## Register

```razor
<orizon-register />
```

---

## Forgot Password

```razor
<orizon-forgot-password />
```

---

## Profile

```razor
<orizon-user-profile />
```

---

## Settings

```razor
<orizon-settings />
```

---

# View Components

Cada Tag Helper deverá possuir um View Component correspondente.

Exemplo

DashboardViewComponent

DashboardHeroViewComponent

CrudWorkspaceViewComponent

AnalyticsWorkspaceViewComponent

---

# Models

Toda API pública deverá possuir Model próprio.

Exemplos

DashboardModel

DashboardOptions

CrudWorkspaceModel

AnalyticsModel

---

# Convenções

Toda API pública deverá:

- possuir documentação XML
- possuir exemplos
- preservar compatibilidade
- utilizar Semantic Versioning

---

# Breaking Changes

Qualquer alteração nesta API deverá ocorrer somente em versões MAJOR.

---

# Objetivo Final

Permitir que consumidores utilizem a biblioteca sem depender da implementação interna.