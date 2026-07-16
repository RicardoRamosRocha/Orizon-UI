(() => {
    "use strict";

    const focusableSelector = [
        "a[href]",
        "button:not([disabled])",
        "input:not([disabled])",
        "select:not([disabled])",
        "textarea:not([disabled])",
        "[tabindex]:not([tabindex='-1'])"
    ].join(",");

    let activeModal = null;
    let previousFocus = null;

    const findModal = (selector) => {
        try {
            return selector ? document.querySelector(selector) : null;
        } catch {
            return null;
        }
    };

    const closeModal = modal => {
        if (!modal) {
            return;
        }

        modal.hidden = true;
        modal.classList.remove("is-open");
        activeModal = null;

        if (previousFocus instanceof HTMLElement) {
            previousFocus.focus();
        }
    };

    const openModal = modal => {
        previousFocus = document.activeElement;
        modal.hidden = false;
        modal.classList.add("is-open");
        activeModal = modal;

        const firstFocusable = modal.querySelector(focusableSelector);
        (firstFocusable || modal.querySelector(".orizon-modal__panel"))?.focus();
    };

    document.addEventListener("click", event => {
        const openTrigger = event.target.closest("[data-orizon-modal-target]");
        if (openTrigger) {
            const modal = findModal(openTrigger.getAttribute("data-orizon-modal-target"));
            if (modal) {
                openModal(modal);
            }
        }

        const closeTrigger = event.target.closest("[data-orizon-modal-close]");
        if (closeTrigger) {
            closeModal(closeTrigger.closest("[data-orizon-modal]"));
        }
    });

    document.addEventListener("keydown", event => {
        if (!activeModal) {
            return;
        }

        if (event.key === "Escape") {
            event.preventDefault();
            closeModal(activeModal);
            return;
        }

        if (event.key !== "Tab") {
            return;
        }

        const focusable = [...activeModal.querySelectorAll(focusableSelector)];
        if (focusable.length === 0) {
            event.preventDefault();
            activeModal.querySelector(".orizon-modal__panel")?.focus();
            return;
        }

        const first = focusable[0];
        const last = focusable[focusable.length - 1];

        if (event.shiftKey && document.activeElement === first) {
            event.preventDefault();
            last.focus();
        } else if (!event.shiftKey && document.activeElement === last) {
            event.preventDefault();
            first.focus();
        }
    });
})();
