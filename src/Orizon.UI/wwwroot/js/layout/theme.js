(() => {
    "use strict";

    const storageKey = "orizon-ui-theme";
    const root = document.documentElement;
    const toggleButtons = document.querySelectorAll("[data-theme-toggle]");

    const getPreferredTheme = () => {
        const storedTheme = localStorage.getItem(storageKey);

        if (storedTheme === "light" || storedTheme === "dark") {
            return storedTheme;
        }

        return window.matchMedia("(prefers-color-scheme: dark)").matches
            ? "dark"
            : "light";
    };

    const updateButtons = (theme) => {
        const isDark = theme === "dark";

        toggleButtons.forEach((button) => {
            button.setAttribute("aria-pressed", String(isDark));
            button.setAttribute(
                "aria-label",
                isDark ? "Ativar tema claro" : "Ativar tema escuro"
            );

            const icon = button.querySelector("[data-theme-icon]");

            if (icon) {
                icon.style.transform = isDark
                    ? "rotate(180deg)"
                    : "rotate(0deg)";
            }
        });
    };

    const applyTheme = (theme, persist = false) => {
        root.setAttribute("data-theme", theme);
        root.style.colorScheme = theme;

        if (persist) {
            localStorage.setItem(storageKey, theme);
        }

        updateButtons(theme);
    };

    const toggleTheme = () => {
        const currentTheme = root.getAttribute("data-theme") || "light";
        const nextTheme = currentTheme === "dark" ? "light" : "dark";

        applyTheme(nextTheme, true);
    };

    applyTheme(getPreferredTheme());

    toggleButtons.forEach((button) => {
        button.addEventListener("click", toggleTheme);
    });

    window
        .matchMedia("(prefers-color-scheme: dark)")
        .addEventListener("change", (event) => {
            const storedTheme = localStorage.getItem(storageKey);

            if (!storedTheme) {
                applyTheme(event.matches ? "dark" : "light");
            }
        });
})();