# Sprint T2

# Dashboard Framework

Status

Planned

Priority

Critical

---

# Objetivo

Criar o primeiro Template Enterprise oficial da Orizon UI.

O Dashboard deverá ser completamente reutilizável, altamente configurável e independente de qualquer sistema consumidor.

Ao final desta Sprint qualquer aplicação ASP.NET Core MVC deverá conseguir construir um Dashboard apenas utilizando a API pública da biblioteca.

---

# Visão

Hoje o Dashboard é implementado dentro de cada sistema.

Após esta Sprint o Dashboard passa a ser responsabilidade exclusiva da Orizon UI.

Aplicações apenas fornecerão dados.

Toda estrutura visual será reutilizada.

---

# Arquitetura

```

Applications

↓

Dashboard Template

↓

Widgets

↓

Components

↓

Design Tokens

```

Nenhum Dashboard poderá implementar HTML diretamente.

---

# Estrutura

```
Templates/

    Dashboard/

        DashboardTemplate.cs

        DashboardModel.cs

        DashboardOptions.cs

Layouts/

    Dashboard/

        EnterpriseDashboardLayout.cshtml

ViewComponents/

    Dashboard/

        DashboardViewComponent.cs

TagHelpers/

    Dashboard/

        DashboardTagHelper.cs

Models/

    Dashboard/

Services/

    Dashboard/

wwwroot/

    css/dashboard/

    js/dashboard/
```

---

# API Pública

O principal objetivo desta Sprint é disponibilizar uma API extremamente simples.

Exemplo mínimo

```razor
<orizon-dashboard />
```

---

Dashboard com layout

```razor
<orizon-dashboard
    layout="enterprise" />
```

---

Dashboard com tema

```razor
<orizon-dashboard
    layout="enterprise"
    theme="corporate" />
```

---

Dashboard completo

```razor
<orizon-dashboard
    layout="enterprise"
    theme="renova"
    density="comfortable"
    sidebar="true"
    topbar="true"
    footer="true"
    notifications="true" />
```

---

# Estrutura Interna

Todo Dashboard será composto pelos seguintes blocos.

```
Dashboard

│

├── Header

├── Sidebar

├── Hero

├── KPI Grid

├── Dashboard Sections

├── Widgets

├── Footer
```

Cada bloco será reutilizável.

---

# Dashboard Header

Responsável por:

- título

- subtítulo

- breadcrumbs

- pesquisa

- notificações

- usuário

API

```razor
<orizon-dashboard-header />
```

---

# Dashboard Sidebar

Responsável por:

- menu

- favoritos

- atalhos

- collapse

- navegação

API

```razor
<orizon-dashboard-sidebar />
```

---

# Dashboard Hero

Reutiliza o Widget Hero.

API

```razor
<orizon-dashboard-hero />
```

---

# Dashboard Sections

Permite organizar Widgets.

Exemplo

```razor
<orizon-dashboard-section
    title="Financeiro">

</orizon-dashboard-section>
```

---

# Dashboard Widgets

Todo Widget deverá poder ser inserido dentro de uma Section.

Exemplo

```razor
<orizon-dashboard-section>

    <orizon-kpi-grid />

    <orizon-activity-feed />

    <orizon-calendar-widget />

</orizon-dashboard-section>
```

---

# Dashboard Footer

Rodapé reutilizável.

API

```razor
<orizon-dashboard-footer />
```

---

# Layouts

A Sprint define os primeiros layouts oficiais.

Enterprise

Compact

Executive

Analytics

Operations

Cada Layout controla apenas posicionamento.

Nunca aparência.

---

# Dashboard Options

Objeto responsável pela configuração.

Exemplo

```csharp
new DashboardOptions
{
    Theme = "Corporate",
    Layout = "Enterprise",
    Sidebar = true,
    Notifications = true
}
```

---

# Responsabilidades

O Dashboard deve:

✔ organizar Widgets

✔ organizar Layouts

✔ reutilizar Components

✔ nunca acessar banco

✔ nunca conhecer aplicações

---

# Não Faz Parte

Esta Sprint NÃO implementa:

CRUD

Login

Settings

Analytics Workspace

Kanban

Wizard

---

# Critérios de Aceite

O Dashboard deverá:

✔ funcionar em Light

✔ funcionar em Dark

✔ utilizar apenas Widgets

✔ utilizar apenas Components

✔ utilizar apenas Design Tokens

✔ possuir documentação

✔ possuir Tag Helper

✔ possuir View Component

---

# Testes

Validar:

- renderização

- responsividade

- acessibilidade

- performance

- tema

- layouts

- integração no Sandbox

---

# Checklist

☐ Build limpo

☐ Zero warnings

☐ Dashboard Template

☐ Dashboard Header

☐ Dashboard Sidebar

☐ Dashboard Footer

☐ Dashboard Sections

☐ Dashboard Tag Helper

☐ Dashboard View Component

☐ Dashboard Options

☐ Sandbox atualizado

☐ Projeto consumidor validado

☐ Documentação atualizada

---

# Resultado Esperado

Ao término da Sprint T2, a Orizon UI deixa de entregar apenas componentes e passa a oferecer um Dashboard Enterprise reutilizável.

Todos os sistemas da plataforma Orizon (Renova, Distribuidora, CRM, Imobiliário e futuros produtos) deverão consumir o mesmo Dashboard Framework, compartilhando a mesma arquitetura, identidade visual e capacidade de evolução centralizada.