# TreeView

Estrutura hierárquica sem limite de profundidade. Aceita `label`, `selection` e `lazy`.

Forneça uma lista, itens `role="treeitem"`, grupos aninhados `role="group"` e controles `data-tree-toggle`. O Tag Helper atribui `role="tree"` ao contêiner. Use `aria-expanded` nos nós pai. Setas percorrem, expandem e recolhem; `Home`, `End`, `Enter` e espaço controlam foco e seleção. O modo `lazy` expõe o estado para carregamento sob demanda pela aplicação.
