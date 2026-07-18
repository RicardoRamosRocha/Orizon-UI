(() => {
    "use strict";

    const storageKey = "orizon-ui-appearance";
    const themes = ["light", "dark", "corporate", "construction", "real-estate", "hair", "agents", "renova"];
    const defaults = { theme: "light", primaryColor: "blue", mode: "auto", density: "normal", sidebar: "expanded", fontSize: "normal", radius: "rounded", shadow: "standard", motion: "normal", language: "pt-BR" };
    const root = document.documentElement;
    let endpoints = { appearance: "/api/appearance", preferences: "/api/preferences", theme: "/api/theme", companyTheme: "/api/company/theme" };
    let state = readLocal();
    let saveTimer;

    function readLocal() {
        try { return { ...defaults, ...JSON.parse(localStorage.getItem(storageKey) || "{}") }; }
        catch { return { ...defaults }; }
    }

    function effectiveTheme(value) {
        if (value.mode === "dark") return "dark";
        if (value.mode === "light" && value.theme === "dark") return "light";
        if (value.mode === "auto" && window.matchMedia("(prefers-color-scheme: dark)").matches) return "dark";
        return themes.includes(value.theme) ? value.theme : "light";
    }

    function apply(next, persist = true) {
        state = { ...state, ...next };
        const theme = effectiveTheme(state);
        root.dataset.theme = theme;
        root.dataset.primaryColor = state.primaryColor;
        root.dataset.density = state.density;
        root.dataset.fontSize = state.fontSize;
        root.dataset.radius = state.radius;
        root.dataset.shadow = state.shadow;
        root.dataset.motion = state.motion;
        root.dataset.sidebarPreference = state.sidebar;
        root.style.colorScheme = theme === "dark" || theme === "agents" ? "dark" : "light";
        document.querySelector("[data-admin-layout]")?.classList.toggle("is-sidebar-collapsed", state.sidebar !== "expanded");
        document.body?.classList.toggle("is-sidebar-icons-only", state.sidebar === "icons");
        if (persist) localStorage.setItem(storageKey, JSON.stringify(state));
        syncControls();
        window.dispatchEvent(new CustomEvent("orizon:appearancechange", { detail: { ...state, effectiveTheme: theme } }));
        return { ...state };
    }

    function syncControls() {
        document.querySelectorAll("[data-appearance-key]").forEach(control => {
            const key = control.dataset.appearanceKey;
            const value = control.dataset.appearanceValue;
            if (control.matches("select")) control.value = state[key];
            else {
                const selected = value === state[key];
                control.classList.toggle("is-selected", selected);
                control.setAttribute("aria-pressed", String(selected));
                if (control.matches("input")) control.checked = selected;
            }
        });
        document.querySelectorAll("[data-theme-toggle]").forEach(button => {
            const dark = effectiveTheme(state) === "dark";
            button.setAttribute("aria-pressed", String(dark));
            button.setAttribute("aria-label", dark ? "Ativar tema claro" : "Ativar tema escuro");
        });
    }

    function headers() {
        const token = document.querySelector('meta[name="request-verification-token"]')?.content;
        return { "Content-Type": "application/json", ...(token ? { "RequestVerificationToken": token } : {}) };
    }

    function scheduleSave() {
        clearTimeout(saveTimer);
        saveTimer = setTimeout(async () => {
            try {
                const response = await fetch(endpoints.preferences, { method: "POST", headers: headers(), body: JSON.stringify(state) });
                window.dispatchEvent(new CustomEvent(response.ok ? "orizon:appearancesaved" : "orizon:appearanceerror"));
            } catch { window.dispatchEvent(new CustomEvent("orizon:appearanceerror")); }
        }, 350);
    }

    async function load() {
        try {
            const response = await fetch(endpoints.appearance, { headers: { "Accept": "application/json" } });
            if (response.ok) apply(await response.json());
        } catch { apply(state, false); }
        return { ...state };
    }

    function set(key, value, save = true) {
        apply({ [key]: value });
        if (save) scheduleSave();
    }

    document.addEventListener("click", event => {
        const option = event.target.closest("[data-appearance-key][data-appearance-value]");
        if (option) set(option.dataset.appearanceKey, option.dataset.appearanceValue);
        const toggle = event.target.closest("[data-theme-toggle]");
        if (toggle) set("mode", effectiveTheme(state) === "dark" ? "light" : "dark");
    });

    document.addEventListener("change", event => {
        const control = event.target.closest("[data-appearance-key]");
        if (control?.matches("select, input[type=color]")) set(control.dataset.appearanceKey, control.value);
    });

    window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", () => { if (state.mode === "auto") apply({}, false); });
    window.OrizonAppearance = Object.freeze({ themes: [...themes], defaults: { ...defaults }, get: () => ({ ...state }), set, apply, load, configure: options => { endpoints = { ...endpoints, ...options }; return window.OrizonAppearance; } });
    window.OrizonTheme = Object.freeze({ themes: [...themes], get: () => effectiveTheme(state), set: theme => set("theme", theme), reset: () => apply(defaults) });
    apply(state, false);
})();
