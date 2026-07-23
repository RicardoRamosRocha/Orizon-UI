# Roadmap

> Orizon UI Enterprise Library

Version: 2.0

---

# Visão Geral

Este documento descreve o plano oficial de evolução da Orizon UI para a EPIC 13 — Orizon Template Framework.

O objetivo é transformar a biblioteca de uma coleção de componentes em uma plataforma completa de Templates Enterprise reutilizáveis para aplicações ASP.NET Core MVC.

Ao término desta EPIC, qualquer sistema da plataforma Orizon deverá ser capaz de construir interfaces completas utilizando apenas os Templates públicos da biblioteca.

---

# Objetivos Estratégicos

A EPIC 13 possui cinco objetivos principais.

## Reutilização

Eliminar duplicação de HTML entre aplicações.

---

## Padronização

Centralizar toda evolução visual na Orizon UI.

---

## Produtividade

Permitir que novos projetos iniciem com Templates prontos.

---

## Escalabilidade

Garantir crescimento sustentável da biblioteca.

---

## Compatibilidade

Preservar consumidores existentes durante toda a evolução.

---

# Arquitetura

Toda implementação seguirá obrigatoriamente esta hierarquia.

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

Nenhuma Sprint poderá violar esta arquitetura.

---

# Organização das Sprints

A EPIC está dividida em seis Sprints.

Cada Sprint possui objetivos claros, entregáveis e critérios de aceite.

```

T1

↓

T2

↓

T3

↓

T4

↓

T5

↓

T6

```

Cada Sprint depende da anterior.

---

# Sprint T1

## Foundation + Widget Framework

Objetivo

Criar a infraestrutura necessária para suportar Templates reutilizáveis.

Entregáveis

- Estrutura Widgets
- Hero Widget
- KPI Widget
- Activity Feed
- Timeline
- Quick Actions
- Calendar Widget
- Analytics Widget

Resultado esperado

Widgets reutilizáveis por qualquer Template.

---

# Sprint T2

## Dashboard Framework

Objetivo

Criar o Dashboard oficial da biblioteca.

Entregáveis

- Dashboard Template
- Dashboard Header
- Dashboard Sections
- Dashboard Layout
- Dashboard Widgets
- Dashboard Tag Helper

Resultado esperado

Dashboard Enterprise reutilizável.

---

# Sprint T3

## Workspace Templates

Objetivo

Criar os principais Templates utilizados em aplicações corporativas.

Entregáveis

- CRUD Workspace
- Analytics Workspace
- Reports Workspace
- Wizard Workspace
- Kanban Workspace

Resultado esperado

Templates reutilizáveis para sistemas administrativos.

---

# Sprint T4

## Authentication & Administration

Objetivo

Padronizar páginas institucionais.

Entregáveis

- Login
- Register
- Forgot Password
- User Profile
- Settings
- Administration

Resultado esperado

Fluxo completo de autenticação e administração.

---

# Sprint T5

## Sandbox & Consumer Validation

Objetivo

Validar todos os Templates em projetos consumidores.

Entregáveis

- Showcase atualizado
- Playground atualizado
- Consumer Samples
- Integração Renova
- Integração Distribuidora

Resultado esperado

Templates funcionando em aplicações reais.

---

# Sprint T6

## Packaging & Documentation

Objetivo

Preparar a versão oficial da EPIC.

Entregáveis

- Documentação completa
- NuGet Package
- Samples
- CHANGELOG
- Release Notes

Resultado esperado

Versão pronta para publicação.

---

# Critérios de Qualidade

Cada Sprint somente será considerada concluída quando atender todos os critérios abaixo.

## Build

✔ Sem erros

✔ Sem warnings

---

## Compatibilidade

✔ Consumidores preservados

---

## Sandbox

✔ Atualizado

✔ Validado

---

## Projeto Consumidor

✔ Renova validado

✔ Distribuidora validada

---

## Documentação

✔ Atualizada

---

## Testes

✔ Automatizados

✔ Manuais

---

# Fluxo de Desenvolvimento

Todo desenvolvimento seguirá este fluxo.

```

Planejamento

↓

Implementação

↓

Review

↓

Sandbox

↓

Projeto Consumidor

↓

Documentação

↓

Release

```

---

# Definição de Concluído (Definition of Done)

Uma Sprint somente estará concluída quando:

- Build sem erros
- Zero warnings
- Testes aprovados
- Sandbox atualizado
- Projeto consumidor validado
- Documentação revisada
- Compatibilidade preservada
- Pull Request aprovado

---

# Cronograma Evolutivo

```

EPIC 13

│

├── Sprint T1

├── Sprint T2

├── Sprint T3

├── Sprint T4

├── Sprint T5

└── Sprint T6

↓

Release

↓

NuGet

↓

Projetos Consumidores

```

---

# Próximos Marcos

Após a conclusão da EPIC 13, a evolução da biblioteca deverá concentrar-se em:

- Dashboard Framework 2.0
- AI Widgets
- Workflow Templates
- Mobile Responsive Templates
- Advanced Analytics
- Multi-tenant Templates
- Low-code Dashboard Builder

---

# Objetivo Final

A EPIC 13 representa uma mudança estratégica na Orizon UI.

A biblioteca deixa de fornecer apenas componentes reutilizáveis e passa a oferecer uma plataforma completa de Templates Enterprise.

Essa evolução permitirá que qualquer aplicação ASP.NET Core MVC seja construída de forma consistente, reutilizável e escalável, utilizando exclusivamente a infraestrutura fornecida pela Orizon UI.