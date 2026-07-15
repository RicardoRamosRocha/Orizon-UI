(() => {
    "use strict";

    const dismissAlert = (button) => {
        const alert = button.closest(".orizon-alert");

        if (!alert) {
            return;
        }

        alert.classList.add("is-closing");

        window.setTimeout(() => {
            alert.remove();
        }, 170);
    };

    document.addEventListener("click", (event) => {
        const button = event.target.closest("[data-orizon-alert-dismiss]");

        if (!button) {
            return;
        }

        dismissAlert(button);
    });
})();