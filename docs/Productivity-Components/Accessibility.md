# Acessibilidade

- Os diálogos declaram nome acessível, modal e retorno de foco.
- Command Palette usa combobox/listbox, navegação por setas, `Enter` e `Esc`.
- Split View expõe separador, orientação e limites ARIA.
- TreeView e TreeGrid utilizam padrões ARIA hierárquicos e roving tabindex.
- Todos os controles visíveis por teclado recebem estados de foco.
- Cores derivam dos tokens de tema claro e escuro.
- Animações respeitam `prefers-reduced-motion`.
- Textos de botões de ícone devem sempre possuir `aria-label` contextual.

Ao compor conteúdo, mantenha títulos em ordem lógica, use `<time>` para datas e não dependa apenas de cor para comunicar estado.
