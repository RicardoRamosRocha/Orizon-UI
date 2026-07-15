(() => {
    "use strict";

    const shell = document.querySelector("[data-admin-layout]");

    if (!shell) {
        return;
    }

    const mediaQuery = window.matchMedia("(min-width: 1024px)");

    const updateLayoutMode = () => {

        if (mediaQuery.matches) {

            shell.classList.remove("is-sidebar-open");

        } else {

            shell.classList.remove("is-sidebar-collapsed");

        }

    };

    updateLayoutMode();

    mediaQuery.addEventListener("change", updateLayoutMode);

})();