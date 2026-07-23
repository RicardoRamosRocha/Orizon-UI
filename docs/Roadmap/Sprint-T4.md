# Sprint T4

# Authentication & Administration

Status

Planned

Priority

High

---

# Objetivo

Criar os Templates oficiais para autenticação, perfil do usuário e administração da plataforma.

A partir desta Sprint nenhuma aplicação deverá criar telas próprias para Login, Perfil ou Configurações.

Todas deverão consumir a Orizon UI.

---

# Templates

## Login

```razor
<orizon-login />
```

Inclui

- Logo
- Background
- Login
- Remember Me
- Social Login
- Forgot Password
- Dark Mode

---

## Register

```razor
<orizon-register />
```

---

## Forgot Password

```razor
<orizon-forgot-password />
```

---

## User Profile

```razor
<orizon-user-profile />
```

Inclui

- Avatar
- Dados pessoais
- Segurança
- Sessões
- Preferências
- Aparência

---

## Settings

```razor
<orizon-settings />
```

Inclui

- Geral
- Aparência
- Notificações
- Idioma
- Segurança
- Integrações

---

## Administration

```razor
<orizon-admin />
```

Inclui

- Usuários
- Perfis
- Permissões
- Auditoria
- Logs
- Configurações

---

# Layouts

Authentication Layout

Administration Layout

Profile Layout

---

# Critérios

✔ Responsivo

✔ Dark Mode

✔ Acessibilidade

✔ Design Tokens

✔ Documentação

✔ Tag Helpers

✔ View Components

---

# Resultado

A biblioteca passa a fornecer todas as telas institucionais reutilizáveis.