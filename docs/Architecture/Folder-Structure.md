# Folder Structure

> Orizon UI Enterprise Library

Version 2.0

---

# Objetivo

Padronizar completamente a organização física da biblioteca.

Toda nova funcionalidade deverá respeitar esta estrutura.

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

Extensions/

Infrastructure/

Resources/

wwwroot/

samples/

docs/
```

---

# Components

Menor unidade reutilizável.

Exemplos

Button

Card

Input

Grid

Badge

---

# Widgets

Agrupam Components.

Exemplos

Hero

Timeline

ActivityFeed

KPI

Calendar

Analytics

QuickActions

---

# Templates

Agrupam Widgets.

Exemplos

Dashboard

CRUD

Analytics

Reports

Kanban

Wizard

---

# Layouts

Organizam Templates.

Nunca implementam lógica.

---

# Themes

Especializam aparência.

Nunca modificam estrutura.

---

# Tag Helpers

Toda funcionalidade pública deverá possuir Tag Helper.

---

# View Components

Renderização complexa.

Sempre reutilizável.

---

# Services

Serviços internos.

Nunca conhecer aplicações consumidoras.

---

# Models

Objetos utilizados pelos Templates e Widgets.

Nunca representar entidades de negócio.

---

# Infrastructure

Implementações internas.

Não fazem parte da API pública.

---

# Extensions

Métodos de extensão da biblioteca.

---

# Resources

Arquivos de localização.

Ícones.

Textos.

---

# wwwroot

CSS

JavaScript

Imagens

Fonts

---

# Samples

Projetos de demonstração.

Nunca conter código específico de consumidores.

---

# Docs

Toda documentação oficial.

Architecture

Roadmap

Releases

---

# Regras

✔ Uma responsabilidade por pasta

✔ Um componente por diretório

✔ Um namespace por responsabilidade

✔ Sem dependências circulares

✔ Sem duplicação

✔ Sem arquivos órfãos

---

# Organização Recomendada

Components/

```
Button/

Card/

Grid/

Input/

Modal/
```

Widgets/

```
Hero/

Timeline/

Calendar/

Analytics/

QuickActions/
```

Templates/

```
Dashboard/

Crud/

Reports/

Analytics/

Kanban/

Wizard/
```

---

# Objetivo Final

Manter uma estrutura previsível, escalável e fácil de navegar, permitindo que qualquer desenvolvedor encontre rapidamente onde criar, modificar ou estender funcionalidades da Orizon UI.