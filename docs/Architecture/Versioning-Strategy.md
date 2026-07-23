# Versioning Strategy

> Orizon UI Enterprise Library

Version 2.0

---

# Objetivo

Este documento define a estratégia oficial de versionamento, compatibilidade, evolução e publicação da Orizon UI.

Todos os releases da biblioteca deverão seguir estas diretrizes para garantir estabilidade aos projetos consumidores.

---

# Princípios

A evolução da Orizon UI deve ser:

- previsível
- incremental
- compatível
- documentada
- segura

Toda alteração deve considerar o impacto nos sistemas que utilizam a biblioteca.

---

# Semantic Versioning

A Orizon UI utiliza Semantic Versioning.

Formato:

```

MAJOR.MINOR.PATCH

```

Exemplo

```

1.0.0

1.1.0

1.2.4

2.0.0

```

---

# MAJOR

Incrementado quando houver Breaking Changes.

Exemplos

- remoção de APIs públicas
- alteração incompatível de comportamento
- remoção de componentes
- alteração obrigatória na configuração

Exemplo

```

1.x.x

↓

2.0.0

```

---

# MINOR

Incrementado quando novas funcionalidades forem adicionadas mantendo compatibilidade.

Exemplos

- novo componente
- novo widget
- novo template
- novo layout
- novo tema

Exemplo

```

1.1.0

↓

1.2.0

```

---

# PATCH

Incrementado para correções.

Exemplos

- bug fix
- melhorias internas
- ajustes de CSS
- correções de acessibilidade
- otimizações

Exemplo

```

1.2.0

↓

1.2.1

```

---

# Pré-Releases

Durante o desenvolvimento poderão existir versões especiais.

Formato

```

1.0.0-alpha.1

1.0.0-alpha.2

1.0.0-beta.1

1.0.0-beta.2

1.0.0-rc.1

1.0.0

```

---

# Alpha

Objetivo

Implementação inicial.

Pode conter funcionalidades incompletas.

Não recomendado para produção.

---

# Beta

Objetivo

Funcionalidade praticamente concluída.

Utilizada para validação.

Pode sofrer pequenas alterações.

---

# Release Candidate

Objetivo

Versão candidata ao release final.

Permitidas apenas:

- correções críticas
- documentação
- pequenos ajustes

Nenhuma nova funcionalidade deverá ser adicionada.

---

# Stable

Versão oficial.

Indicada para produção.

---

# Compatibilidade

A compatibilidade é prioridade máxima.

Sempre que possível:

- manter APIs públicas
- preservar comportamento
- evitar migrações

---

# Breaking Changes

Breaking Changes devem ocorrer somente em versões MAJOR.

Exemplos

- remoção de propriedades públicas
- alteração de assinatura
- alteração incompatível de Tag Helpers
- remoção de Design Tokens públicos

Toda Breaking Change deverá possuir documentação de migração.

---

# Depreciação

Antes de remover uma funcionalidade ela deverá passar pelo processo de depreciação.

Fluxo

```

Stable

↓

Deprecated

↓

Obsolete

↓

Removed

```

---

# Obsolete

Utilizar atributos do .NET.

Exemplo

```csharp
[Obsolete(
    "Use DashboardWidget instead.",
    false)]
```

Nunca remover imediatamente uma API pública.

---

# Ciclo de Vida

Toda funcionalidade deverá seguir este fluxo.

```

Planejamento

↓

Desenvolvimento

↓

Review

↓

Sandbox

↓

Projeto Consumidor

↓

Release Candidate

↓

Stable

```

---

# Processo de Release

Cada release deverá executar obrigatoriamente:

Build

↓

Testes

↓

Sandbox

↓

Projeto consumidor

↓

Documentação

↓

Package

↓

GitHub Release

↓

NuGet

---

# Checklist

Antes de publicar verificar:

☐ Build sem erros

☐ Zero warnings

☐ Testes aprovados

☐ Sandbox atualizado

☐ Projeto consumidor validado

☐ Documentação atualizada

☐ CHANGELOG atualizado

☐ Package gerado

☐ Tag criada

---

# Git Flow

Estratégia recomendada.

```

main

↓

release

↓

feature/sprint

```

Cada Sprint deverá possuir sua própria branch.

Exemplos

```

feature/dashboard-framework

feature/widget-framework

feature/template-login

```

---

# CHANGELOG

Todo release deverá atualizar o CHANGELOG.

Modelo

```

## Added

Novos recursos.

## Changed

Mudanças.

## Fixed

Correções.

## Deprecated

Recursos depreciados.

## Removed

Recursos removidos.

```

---

# Publicação

Fluxo oficial.

```

GitHub

↓

Release

↓

NuGet

↓

Projeto Consumidor

↓

Validação

```

---

# Projeto Consumidor

Nenhuma versão será considerada concluída sem validação em um projeto real.

Projetos recomendados para validação:

- Renova
- Orizon Distribuidora
- Orizon CRM
- Orizon Imobiliário

Esses projetos funcionam como ambientes oficiais de homologação.

---

# Política de Suporte

A biblioteca manterá suporte para:

- última versão Stable
- último Release Candidate
- versão Major anterior (quando aplicável)

Correções críticas terão prioridade.

---

# Roadmap

Toda evolução deve ser registrada em:

```

docs/Roadmap

```

Nenhuma funcionalidade relevante deverá ser implementada sem planejamento.

---

# Objetivo Final

A estratégia de versionamento existe para garantir que a Orizon UI evolua continuamente sem comprometer a estabilidade dos sistemas consumidores.

Compatibilidade, previsibilidade e qualidade são prioridades permanentes da biblioteca.