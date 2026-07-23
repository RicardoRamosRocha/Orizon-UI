# Sprint T1

# Foundation + Widget Framework

Status

Planned

---

# Objetivo

Criar a infraestrutura que permitirá a construção de Templates Enterprise reutilizáveis.

Até esta Sprint a biblioteca possui Components.

Ao término desta Sprint ela também possuirá Widgets reutilizáveis.

Esta Sprint cria a base para todas as próximas.

---

# Motivação

Hoje aplicações como:

- Renova
- Distribuidora
- CRM
- Imobiliário

acabam implementando Dashboard, KPIs, Timeline e Quick Actions manualmente.

A proposta desta Sprint é encapsular esses blocos em Widgets reutilizáveis.

---

# Resultado Esperado

Após esta Sprint será possível escrever:

```razor

<orizon-kpi-grid />

<orizon-activity-feed />

<orizon-dashboard-hero />

<orizon-quick-actions />

<orizon-calendar-widget />

```

sem necessidade de copiar HTML.

---

# Arquitetura

```

Templates

↓

Widgets

↓

Components

↓

Design Tokens

```

Esta Sprint implementa apenas a camada Widgets.

---

# Estrutura

```

src/

Orizon.UI/

Widgets/

Dashboard/

Hero/

Kpi/

Timeline/

ActivityFeed/

QuickActions/

Calendar/

Analytics/

Shared/

```

---

# Widgets Planejados

## Dashboard Hero

Responsável pelo cabeçalho do Dashboard.

Conteúdo

- título
- subtítulo
- breadcrumbs
- ações
- avatar
- notificações

API

```razor

<orizon-dashboard-hero />

```

---

## KPI Grid

Responsável por apresentar indicadores.

Utiliza:

- Card
- Badge
- Icon
- Progress

API

```razor

<orizon-kpi-grid />

```

---

## KPI Card

Representa um único indicador.

Campos

Título

Valor

Variação

Ícone

Cor

---

## Activity Feed

Linha do tempo de atividades.

Utiliza

Timeline

Avatar

Card

Date

Button

API

```razor

<orizon-activity-feed />

```

---

## Timeline

Widget reutilizável para eventos cronológicos.

API

```razor

<orizon-timeline />

```

---

## Calendar Widget

Calendário compacto.

Utilizado em Dashboards.

API

```razor

<orizon-calendar-widget />

```

---

## Quick Actions

Lista de ações rápidas.

Exemplo

Novo Cliente

Novo Produto

Novo Pedido

Nova Venda

Nova Tarefa

API

```razor

<orizon-quick-actions />

```

---

## Analytics Widget

Container para gráficos.

Não implementa gráficos.

Apenas organiza:

Chart

Legend

Toolbar

Filters

---

# View Components

Cada Widget possuirá View Component próprio.

Exemplo

```

DashboardHeroViewComponent

```

```

KpiGridViewComponent

```

```

ActivityFeedViewComponent

```

---

# Tag Helpers

Todo Widget deverá possuir Tag Helper.

Exemplo

```razor

<orizon-dashboard-hero />

<orizon-kpi-grid />

<orizon-activity-feed />

```

---

# Models

Cada Widget deverá possuir Model próprio.

Exemplo

```

DashboardHeroModel

```

```

KpiGridModel

```

```

ActivityFeedModel

```

---

# Convenções

Um Widget deve:

- possuir responsabilidade única;
- reutilizar Components;
- nunca acessar banco de dados;
- nunca conhecer aplicações consumidoras.

---

# Não Faz Parte

Esta Sprint NÃO implementa:

Dashboard

CRUD

Login

Settings

Templates

Layouts

Esses itens pertencem às próximas Sprints.

---

# Critérios de Aceite

Todos os Widgets:

✔ renderizam corretamente;

✔ possuem Tag Helper;

✔ possuem View Component;

✔ utilizam Design Tokens;

✔ funcionam em Light e Dark;

✔ possuem documentação.

---

# Testes

Cada Widget deverá possuir:

- renderização;
- acessibilidade;
- responsividade;
- validação visual;
- integração no Sandbox.

---

# Checklist

☐ Build limpo

☐ Zero warnings

☐ Widgets implementados

☐ Tag Helpers

☐ View Components

☐ Models

☐ Sandbox atualizado

☐ Documentação atualizada

☐ Projeto consumidor validado

---

# Resultado Final

Esta Sprint estabelece a camada de Widgets da Orizon UI.

Todas as próximas funcionalidades reutilizarão estes Widgets como blocos fundamentais para a construção de Templates Enterprise.