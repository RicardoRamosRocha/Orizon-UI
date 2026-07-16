# Tema e Cores

## Objetivo

Definir a identidade visual padrão do Orizon UI utilizando Design Tokens baseados em variáveis CSS, permitindo reutilização entre todos os produtos da plataforma.

---

# Estratégia

O Design System utilizará CSS Custom Properties (`--variables`) como fonte única de verdade para toda a interface.

Nenhum componente deverá utilizar valores de cor diretamente.

Toda cor deverá ser obtida através dos tokens definidos pelo tema.

---

# Estrutura

## Tema Light (Padrão)

O primeiro tema será o Light Theme.

Ele servirá como referência para todos os componentes da biblioteca.

Posteriormente será criada uma implementação completa para Dark Theme sem necessidade de alterar os componentes.

---

## Categorias de cores

### Primary

Cor principal da plataforma.

Utilizada em:

* Botões primários
* Links
* Elementos ativos
* Indicadores principais

---

### Secondary

Cor complementar utilizada para elementos de apoio.

---

### Success

Utilizada para:

* Operações concluídas
* Mensagens positivas
* Indicadores de sucesso

---

### Warning

Utilizada para:

* Alertas
* Pendências
* Atenção

---

### Danger

Utilizada para:

* Exclusões
* Erros
* Operações críticas

---

### Info

Utilizada para:

* Informações
* Dicas
* Mensagens neutras

---

### Neutral

Escala de cinzas utilizada em:

* Textos
* Bordas
* Divisores
* Ícones
* Fundos neutros

---

### Background

Cor utilizada como fundo principal da aplicação.

---

### Surface

Utilizada em:

* Cards
* Modais
* Menus
* Sidebar
* Dropdowns

---

# Princípios

O sistema de cores deve priorizar:

* Consistência
* Alto contraste
* Boa legibilidade
* Acessibilidade
* Fácil manutenção
* Reutilização entre aplicações

---

# Futuras evoluções

* Dark Theme
* Temas personalizados por produto
* Temas por cliente (White Label)
* Alto contraste
* Preferências do usuário
