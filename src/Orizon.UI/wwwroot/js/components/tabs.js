(() => {
    "use strict";

    const activateTab = tab => {
        const tabs = tab.closest("[data-orizon-tabs]");
        const target = document.getElementById(tab.getAttribute("aria-controls"));

        if (!tabs || !target) {
            return;
        }

        tabs.querySelectorAll("[role='tab']").forEach(item => {
            item.setAttribute("aria-selected", item === tab ? "true" : "false");
            item.setAttribute("tabindex", item === tab ? "0" : "-1");
        });

        tabs.querySelectorAll("[role='tabpanel']").forEach(panel => {
            panel.hidden = panel !== target;
        });

        tab.focus();
    };

    document.addEventListener("click", event => {
        const tab = event.target.closest("[role='tab']");
        if (tab?.closest("[data-orizon-tabs]")) {
            activateTab(tab);
        }
    });

    document.addEventListener("keydown", event => {
        const tab = event.target.closest("[role='tab']");
        if (!tab?.closest("[data-orizon-tabs]")) {
            return;
        }

        const tabs = [...tab.closest("[role='tablist']").querySelectorAll("[role='tab']")];
        const index = tabs.indexOf(tab);
        const next = event.key === "ArrowRight" ? tabs[index + 1] || tabs[0] : null;
        const previous = event.key === "ArrowLeft" ? tabs[index - 1] || tabs[tabs.length - 1] : null;
        const first = event.key === "Home" ? tabs[0] : null;
        const last = event.key === "End" ? tabs[tabs.length - 1] : null;

        if (next || previous || first || last) {
            event.preventDefault();
            activateTab(next || previous || first || last);
        }
    });
})();
