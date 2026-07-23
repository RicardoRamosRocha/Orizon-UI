# Coding Standards

> Orizon UI Enterprise Library

Version 2.0

---

# Objetivo

Este documento define os padrões oficiais de desenvolvimento da Orizon UI.

Todo código enviado para a biblioteca deverá seguir estas regras.

Os objetivos são:

- consistência
- legibilidade
- reutilização
- compatibilidade
- desempenho
- manutenção

Este documento complementa:

- EPIC-13 Template Framework
- ADR-001 Template Architecture
- Developer Guide

---

# Filosofia

A Orizon UI é uma biblioteca Enterprise.

Toda implementação deve priorizar:

- simplicidade
- reutilização
- baixo acoplamento
- alta coesão
- compatibilidade

---

# Organização do Projeto

Nunca criar estruturas aleatórias.

Sempre respeitar:

```

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

# Componentes

Cada componente deve possuir responsabilidade única.

Exemplo

Correto

```
Button
```

Errado

```
ButtonAndModal
```

---

# Widgets

Widgets agrupam Components.

Nunca conter regras específicas de negócio.

Correto

```
ActivityFeedWidget
```

Errado

```
RenovaFeedWidget
```

---

# Templates

Templates agrupam Widgets.

Nunca conter HTML duplicado.

---

# Layouts

Layouts apenas organizam Templates.

Nunca implementar Components.

---

# Themes

Themes alteram aparência.

Nunca estrutura.

---

# C#

## Namespaces

Sempre seguir

```csharp
Orizon.UI.Components

Orizon.UI.Widgets

Orizon.UI.Templates
```

Nunca utilizar namespaces genéricos.

---

## Classes

Uma classe por arquivo.

Nome igual ao arquivo.

Correto

```
DashboardWidget.cs

class DashboardWidget
```

---

## Métodos

Preferir métodos pequenos.

Máximo recomendado:

30 linhas.

---

## Injeção de Dependência

Sempre utilizar constructor injection.

Nunca Service Locator.

---

## Interfaces

Criar interfaces apenas quando existir benefício claro.

Evitar abstrações desnecessárias.

---

# Razor

Utilizar Tag Helpers sempre que possível.

Correto

```razor
<orizon-button />
```

Evitar HTML repetido.

---

# HTML

Nunca utilizar estilos inline.

Errado

```html
<div style="color:red">
```

Correto

```html
<div class="orizon-card">
```

---

# CSS

Nunca utilizar cores literais.

Errado

```css
color:#ffffff;
```

Correto

```css
color:var(--orizon-color-text-primary);
```

---

Nunca utilizar

```css
margin:17px;
```

Sempre utilizar tokens.

---

Nunca duplicar regras CSS.

---

# JavaScript

Preferir JavaScript modular.

Nunca poluir window.

Errado

```javascript
window.dashboard = ...
```

Correto

```javascript
export class Dashboard {
}
```

---

# Design Tokens

Toda aparência deve utilizar Design Tokens.

Correto

```
var(--orizon-radius-md)

var(--orizon-spacing-lg)

var(--orizon-shadow-md)
```

Nunca valores fixos.

---

# Acessibilidade

Todo componente deve possuir:

- navegação por teclado

- focus

- aria

- contraste

- suporte ao modo escuro

---

# Performance

Evitar:

- renderizações duplicadas

- listeners desnecessários

- CSS não utilizado

- JavaScript global

---

# Compatibilidade

Nunca remover APIs públicas.

Mudanças incompatíveis somente em versões major.

---

# Testes

Toda Sprint deve finalizar com:

Build

0 erros

0 warnings

Sandbox funcionando

Projeto consumidor funcionando

---

# Pull Requests

Todo PR deverá responder:

- Existe duplicação?

- Existe quebra?

- Existe impacto em consumidores?

- Existem testes?

- Existe documentação?

---

# Checklist

Antes do merge verificar:

☐ Build

☐ Testes

☐ Documentação

☐ Compatibilidade

☐ Performance

☐ Acessibilidade

☐ Design Tokens

☐ Sem duplicação

☐ API consistente

---

# Regra Principal

Sempre que existir dúvida entre criar algo novo ou reutilizar um componente existente:

Escolha reutilizar.

A reutilização é o princípio mais importante da Orizon UI.