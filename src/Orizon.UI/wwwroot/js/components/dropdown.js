(() => {
    "use strict";

    const closeDropdown = dropdown => {
        const trigger = dropdown.querySelector("[data-orizon-dropdown-trigger]");
        const menu = dropdown.querySelector("[data-orizon-dropdown-menu]");
        trigger?.setAttribute("aria-expanded", "false");
        if (menu) {
            menu.hidden = true;
        }
    };

    const openDropdown = dropdown => {
        const trigger = dropdown.querySelector("[data-orizon-dropdown-trigger]");
        const menu = dropdown.querySelector("[data-orizon-dropdown-menu]");
        trigger?.setAttribute("aria-expanded", "true");
        if (menu) {
            menu.hidden = false;
        }
    };

    document.addEventListener("click", event => {
        const trigger = event.target.closest("[data-orizon-dropdown-trigger]");
        document.querySelectorAll("[data-orizon-dropdown]").forEach(dropdown => {
            if (!trigger || dropdown !== trigger.closest("[data-orizon-dropdown]")) {
                closeDropdown(dropdown);
            }
        });

        if (!trigger) {
            return;
        }

        const dropdown = trigger.closest("[data-orizon-dropdown]");
        const expanded = trigger.getAttribute("aria-expanded") === "true";
        expanded ? closeDropdown(dropdown) : openDropdown(dropdown);
    });

    document.addEventListener("keydown", event => {
        const dropdown = event.target.closest("[data-orizon-dropdown]");
        if (!dropdown) {
            return;
        }

        const trigger = dropdown.querySelector("[data-orizon-dropdown-trigger]");
        const items = [...dropdown.querySelectorAll("[role='menuitem'], .orizon-dropdown__item")];
        const currentIndex = items.indexOf(document.activeElement);

        if (event.key === "Escape") {
            closeDropdown(dropdown);
            trigger?.focus();
        }

        if (event.key === "ArrowDown") {
            event.preventDefault();
            openDropdown(dropdown);
            items[(currentIndex + 1 + items.length) % items.length]?.focus();
        }

        if (event.key === "ArrowUp") {
            event.preventDefault();
            openDropdown(dropdown);
            items[(currentIndex - 1 + items.length) % items.length]?.focus();
        }
    });
})();
