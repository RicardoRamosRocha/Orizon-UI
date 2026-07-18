# Orizon Design Language (ODL)

ODL e a identidade visual oficial da Orizon Platform. A arquitetura separa valores primitivos, tokens semanticos, temas e componentes. Componentes nunca conhecem a paleta do produto: consomem somente propriedades `--orizon-*`.

## Instalacao

Use um unico bundle:

```html
<link rel="stylesheet" href="~/_content/Orizon.UI/css/orizon.css" />
<script src="~/_content/Orizon.UI/js/layout/theme.js"></script>
```

`tokens.css` e `themes/colors-dark.css` continuam disponiveis para compatibilidade. Novos projetos devem usar `design-tokens.css` ou o bundle `orizon.css`.

## Tokens

- Brand: `--orizon-brand-primary`, `-hover`, `-active`, `-soft` e `-on-primary`.
- Background e surface: `--orizon-color-background`, `-surface`, `-surface-secondary`, `-surface-elevated`.
- Sidebar: `--orizon-sidebar-background`, `-text`, `-text-active`, `-indicator`.
- Texto e borda: `--orizon-color-text-*` e `--orizon-color-border*`.
- Status: `--orizon-color-success`, `-warning`, `-danger`, `-info` e variantes `-soft`.
- Tipografia: familia Inter com fallback seguro, escala `xs` a `4xl`, pesos e alturas de linha.
- Espacamento: escala de 2 a 80 pixels por meio de `--orizon-space-*`.
- Radius: `sm`, `md`, `lg`, `xl`, `2xl` e `full`.
- Elevacao: `--orizon-shadow-xs` a `--orizon-shadow-xl`.
- Movimento: duracoes, curvas e transicoes; `prefers-reduced-motion` e respeitado.
- Camadas: dropdown, sticky, overlay, drawer, modal e toast.

## Temas

Temas oficiais: `light`, `dark`, `construction`, `real-estate` e `corporate`.

```html
<html data-theme="corporate">
```

```javascript
OrizonTheme.set("dark");
OrizonTheme.get();
OrizonTheme.reset();
```

Para adicionar um tema, crie `css/themes/nome.css`, selecione `[data-theme="nome"]` e sobrescreva apenas tokens semanticos/brand. Inclua o arquivo no bundle e registre o nome em `theme.js`. Nao altere componentes.

## Componentes e boas praticas

- Use os Tag Helpers existentes para botoes, campos, cards, alertas, badges, tabelas, modais, menus, tabs e feedback.
- Use Phosphor Icons com os tamanhos `--orizon-icon-sm`, `md` e `lg`.
- Preserve `:focus-visible`, nomes acessiveis e atributos ARIA.
- Use `aria-busy="true"` em operacoes assincronas e `aria-selected` em linhas selecionadas.
- Nao introduza hexadecimal, RGB ou medidas visuais isoladas em componentes. Crie ou reutilize um token.
- Aplicacoes consumidoras podem sobrescrever tokens semanticos depois de `orizon.css`, sem copiar CSS de componentes.

## Compatibilidade

Aliases da API anterior (`--orizon-color-primary-*` e `--orizon-color-neutral-*`) mapeiam para ODL. Classes e Tag Helpers existentes nao foram renomeados.
