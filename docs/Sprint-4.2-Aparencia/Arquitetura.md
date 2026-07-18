# Arquitetura

1. Tokens primitivos e semanticos vivem em `design-tokens.css`.
2. Temas sobrescrevem somente tokens semanticos.
3. `OrizonAppearance` compoe preferencias e atributos globais.
4. A aplicacao consumidora implementa API e persistencia por tenant.
5. Tag Helpers renderizam seletores e previews reutilizaveis.

O runtime emite `orizon:appearancechange`, `orizon:appearancesaved` e `orizon:appearanceerror`.
