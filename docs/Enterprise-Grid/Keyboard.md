# Keyboard

## Objetivo
Oferecer navegação de grade comparável a aplicações desktop.

## API e comandos
Setas movem a célula; Tab/Shift+Tab avançam; Enter desce; Home/End e Ctrl+Home/Ctrl+End navegam; Page Up/Down saltam uma viewport; Ctrl/Command+A seleciona; Ctrl/Command+C copia; Ctrl/Command+V cola quando a coluna e o modo permitem; Esc limpa seleção. Em cabeçalhos, Alt+G agrupa, Alt+setas reordena e Shift+setas redimensiona.

```razor
<orizon-enterprise-grid cell-selection="true" edit-mode="cell">
 <orizon-grid-column field="Quantity" editable />
</orizon-enterprise-grid>
```

## Boas práticas, desempenho e acessibilidade
Não capture atalhos fora do foco da grade. A célula ativa usa roving tabindex e é rolada para a janela virtual. Paste deve ser validado no servidor antes da persistência.
