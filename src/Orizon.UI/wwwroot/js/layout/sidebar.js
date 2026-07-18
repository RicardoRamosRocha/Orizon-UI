(() => {

    "use strict";

    const shell = document.querySelector("[data-admin-layout]");

    if (!shell) {
        return;
    }

    const sidebarToggle =
        document.querySelector("[data-sidebar-toggle]");

    const sidebarCollapse =
        document.querySelector("[data-sidebar-collapse]");

    const backdrop =
        document.querySelector("[data-sidebar-backdrop]");

    const desktop =
        window.matchMedia("(min-width:1024px)");

    function openMobile() {

        shell.classList.add("is-sidebar-open");

    }

    function closeMobile() {

        shell.classList.remove("is-sidebar-open");

    }

    function toggleDesktop() {

        shell.classList.toggle("is-sidebar-collapsed");

        window.OrizonAppearance?.set(
            "sidebar",
            shell.classList.contains("is-sidebar-collapsed") ? "collapsed" : "expanded"
        );

    }

    sidebarToggle?.addEventListener("click", () => {

        if (desktop.matches) {

            toggleDesktop();

        } else {

            openMobile();

        }

    });

    sidebarCollapse?.addEventListener("click", toggleDesktop);

    backdrop?.addEventListener("click", closeMobile);

    document.addEventListener("keydown", (event) => {

        if (event.key === "Escape") {

            closeMobile();

        }

    });

    desktop.addEventListener("change", () => {

        closeMobile();

    });

})();
