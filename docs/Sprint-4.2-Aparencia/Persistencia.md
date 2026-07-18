# Persistencia

LocalStorage oferece resposta imediata e tolerancia offline. A API remota e a fonte duravel:

- `GET /api/appearance` retorna a composicao empresa + usuario.
- `GET /api/themes` lista temas.
- `POST /api/theme` salva tema/modo.
- `POST /api/preferences` salva preferencias pessoais.
- `POST /api/company/theme` salva identidade corporativa.

Toda consulta e escrita deve validar empresa e usuario autenticados.
