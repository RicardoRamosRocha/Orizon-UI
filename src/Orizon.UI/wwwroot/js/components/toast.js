(() => {
    "use strict";

    document.addEventListener("click", event => {
        const close = event.target.closest("[data-orizon-toast-close]");
        if (!close) {
            return;
        }

        close.closest("[data-orizon-toast]")?.remove();
    });
})();
