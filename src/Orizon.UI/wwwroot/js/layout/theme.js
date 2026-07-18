(() => {
    "use strict";

    const storageKey = "orizon-ui-theme";
    const themes = ["light", "dark", "construction", "real-estate", "corporate"];
    const root = document.documentElement;

    const preferred = () => {
        const stored = localStorage.getItem(storageKey);
        if (themes.includes(stored)) return stored;
        return window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light";
    };

    const updateControls = (theme) => {
        document.querySelectorAll("[data-theme-toggle]").forEach(button => {
            const isDark = theme === "dark";
            button.setAttribute("aria-pressed", String(isDark));
            button.setAttribute("aria-label", isDark ? "Ativar tema claro" : "Ativar tema escuro");
        });
        document.querySelectorAll("[data-theme-value]").forEach(button => {
            const selected = button.dataset.themeValue === theme;
            button.setAttribute("aria-pressed", String(selected));
            button.classList.toggle("is-active", selected);
        });
        document.querySelectorAll("[data-theme-select]").forEach(select => { select.value = theme; });
    };

    const apply = (theme, persist = false) => {
        const selected = themes.includes(theme) ? theme : "light";
        root.dataset.theme = selected;
        root.style.colorScheme = selected === "dark" ? "dark" : "light";
        if (persist) localStorage.setItem(storageKey, selected);
        updateControls(selected);
        window.dispatchEvent(new CustomEvent("orizon:themechange", { detail: { theme: selected } }));
    };

    document.addEventListener("click", event => {
        const toggle = event.target.closest("[data-theme-toggle]");
        if (toggle) apply(root.dataset.theme === "dark" ? "light" : "dark", true);
        const option = event.target.closest("[data-theme-value]");
        if (option) apply(option.dataset.themeValue, true);
    });

    document.addEventListener("change", event => {
        if (event.target.matches("[data-theme-select]")) apply(event.target.value, true);
    });

    window.OrizonTheme = Object.freeze({ themes: [...themes], get: () => root.dataset.theme, set: theme => apply(theme, true), reset: () => { localStorage.removeItem(storageKey); apply(preferred()); } });
    apply(preferred());

    window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", event => {
        if (!localStorage.getItem(storageKey)) apply(event.matches ? "dark" : "light");
    });
})();
