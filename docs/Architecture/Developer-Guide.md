# Developer Guide

> Orizon UI Enterprise Library

Version: 2.0

---

# Introdução

Bem-vindo ao guia oficial de desenvolvimento da Orizon UI.

Este documento define como novos componentes, widgets, templates e layouts devem ser desenvolvidos.

Ele existe para garantir que toda evolução da biblioteca mantenha:

- consistência
- reutilização
- compatibilidade
- alta qualidade
- desempenho
- acessibilidade

Todo código enviado para a biblioteca deverá seguir este guia.

---

# Objetivos

A Orizon UI foi criada para ser muito mais do que uma coleção de componentes.

Ela deve fornecer toda a infraestrutura visual necessária para aplicações corporativas.

O objetivo deste guia é garantir que qualquer novo recurso siga exatamente a mesma arquitetura.

---

# Filosofia da Biblioteca

A Orizon UI é baseada em cinco princípios.

## 1. Reutilização

Nunca implementar duas vezes a mesma solução.

Sempre reutilizar.

---

## 2. Composição

Interfaces são compostas.

Nunca copiadas.

---

## 3. Evolução Incremental

Novos recursos nunca quebram consumidores existentes.

---

## 4. Enterprise First

Toda funcionalidade deve considerar aplicações corporativas.

---

## 5. Simplicidade da API

Consumidores devem escrever pouco código.

Exemplo

```razor
<orizon-dashboard />
```

Nunca

```html
<div class="dashboard">
...
```

---

# Arquitetura Oficial

Toda implementação deverá seguir esta hierarquia.

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

Nenhuma camada pode violar esta estrutura.

---

# Estrutura do Projeto

```
src/

Orizon.UI

Components/

Widgets/

Templates/

Layouts/

Themes/

TagHelpers/

ViewComponents/

Services/

Models/

wwwroot/

samples/
```

Cada pasta possui responsabilidade única.

---

# Componentes

Componentes representam a menor unidade reutilizável.

Exemplos

- Button

- Card

- Badge

- Input

- Modal

- Grid

- Sidebar

- Topbar

Componentes nunca conhecem Widgets.

Nunca conhecem Templates.

Nunca conhecem aplicações.

---

# Widgets

Widgets agrupam Components.

Exemplo

```
KPI Widget

Card

Icon

Value

Trend

Badge
```

Outro exemplo

```
Timeline Widget

Timeline

Avatar

Date

Button
```

Widgets podem ser reutilizados por qualquer Template.

---

# Templates

Templates agrupam Widgets.

Exemplo

```
Dashboard

Hero

KPIs

Analytics

Calendar

Activity Feed

Timeline
```

Templates nunca implementam HTML específico de um sistema.

---

# Layouts

Layouts organizam Templates.

Exemplos

Enterprise

Executive

Compact

Analytics

Operations

Layouts controlam organização.

Não implementam Widgets.

---

# Themes

Themes alteram aparência.

Nunca alteram estrutura.

Toda aparência deve ser baseada em Design Tokens.

---

# Tag Helpers

Toda funcionalidade pública deve possuir Tag Helper.

Exemplo

```razor
<orizon-dashboard />

<orizon-crud />

<orizon-login />

<orizon-settings />
```

Consumidores não devem conhecer a implementação interna.

---

# View Components

View Components devem encapsular renderizações complexas.

Sempre que um Template exigir processamento adicional, utilizar View Components.

---

# Services

Services concentram regras reutilizáveis da biblioteca.

Nunca devem depender de aplicações consumidoras.

---

# Models

Models representam configurações internas da biblioteca.

Nunca representam entidades de negócio.

---

# Objetivo Final

Ao término da EPIC 13 qualquer aplicação ASP.NET Core MVC deverá conseguir montar uma interface completa utilizando apenas Templates públicos da Orizon UI.

Exemplo

```razor
<orizon-dashboard layout="enterprise"/>

<orizon-crud entity="Products"/>

<orizon-login/>
```

Esse é o objetivo principal da arquitetura da biblioteca.

---

# Próximos Capítulos

Este documento será expandido com os seguintes capítulos.

- Convenções de código
- Estrutura de componentes
- Estrutura de widgets
- Estrutura de templates
- Layouts
- Themes
- Tag Helpers
- Testes
- Performance
- Publicação
- Revisão de código
- Checklist de Pull Request
- Boas práticas
- Anti-patterns