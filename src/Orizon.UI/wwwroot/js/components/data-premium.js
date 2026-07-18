let confirmReturnFocus = null;
let notificationReturnFocus = null;

function openPanel(panel, trigger) { if (!panel) return; panel.hidden = false; panel.classList.add("is-open"); panel.dataset.triggerId = trigger?.id || ""; panel.querySelector("[tabindex='-1'], button, a, input")?.focus(); }
function closePanel(panel, focus) { if (!panel) return; panel.hidden = true; panel.classList.remove("is-open"); focus?.focus(); }

document.addEventListener("input", event => {
    const input = event.target.closest("[data-orizon-search] input"); if (!input) return;
    const root = input.closest("[data-orizon-search]"); root.querySelector("[data-search-clear]").hidden = !input.value;
    clearTimeout(root._orizonTimer); root._orizonTimer = setTimeout(() => root.dispatchEvent(new CustomEvent("orizon:search", { bubbles:true, detail:{ value:input.value } })), Number(root.dataset.debounce) || 0);
});

document.addEventListener("click", event => {
    const clearSearch = event.target.closest("[data-search-clear]"); if (clearSearch) { const root = clearSearch.closest("[data-orizon-search]"); const input = root.querySelector("input"); input.value = ""; clearSearch.hidden = true; input.focus(); root.dispatchEvent(new CustomEvent("orizon:search", { bubbles:true, detail:{ value:"" } })); }
    const clearFilters = event.target.closest("[data-filter-clear]"); if (clearFilters) clearFilters.closest("[data-orizon-filter-bar]").dispatchEvent(new CustomEvent("orizon:filters-clear", { bubbles:true }));
    const confirmTrigger = event.target.closest("[data-confirm-target]"); if (confirmTrigger) { confirmReturnFocus = confirmTrigger; openPanel(document.querySelector(confirmTrigger.dataset.confirmTarget), confirmTrigger); }
    const confirmCancel = event.target.closest("[data-confirm-cancel]"); if (confirmCancel) closePanel(confirmCancel.closest("[data-orizon-confirm]"), confirmReturnFocus);
    const confirmAccept = event.target.closest("[data-confirm-accept]"); if (confirmAccept) { const dialog = confirmAccept.closest("[data-orizon-confirm]"); dialog.dispatchEvent(new CustomEvent("orizon:confirm", { bubbles:true })); closePanel(dialog, confirmReturnFocus); }
    const notificationTrigger = event.target.closest("[data-notifications-target]"); if (notificationTrigger) { notificationReturnFocus = notificationTrigger; openPanel(document.querySelector(notificationTrigger.dataset.notificationsTarget), notificationTrigger); }
    const notificationClose = event.target.closest("[data-notifications-close]"); if (notificationClose) closePanel(notificationClose.closest("[data-orizon-notifications]"), notificationReturnFocus);
    const read = event.target.closest("[data-notification-read]"); if (read) { const item = read.closest("[data-notification]"); item.classList.remove("is-unread"); item.dataset.unread = "false"; item.dispatchEvent(new CustomEvent("orizon:notification-read", { bubbles:true })); }
    const readAll = event.target.closest("[data-notifications-read-all]"); if (readAll) readAll.closest("[data-orizon-notifications]").querySelectorAll(".is-unread").forEach(item => { item.classList.remove("is-unread"); item.dataset.unread = "false"; });
    const clearNotifications = event.target.closest("[data-notifications-clear]"); if (clearNotifications) clearNotifications.closest("[data-orizon-notifications]").querySelector(".orizon-notification-center__list").replaceChildren();
});

document.addEventListener("keydown", event => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "k") { const input = document.querySelector("[data-search-shortcut='true'] input:not(:disabled)"); if (input) { event.preventDefault(); input.focus(); } }
    if (event.key === "Escape") { const confirm = document.querySelector("[data-orizon-confirm]:not([hidden])"); const notifications = document.querySelector("[data-orizon-notifications]:not([hidden])"); if (confirm) closePanel(confirm, confirmReturnFocus); else if (notifications) closePanel(notifications, notificationReturnFocus); }
});

window.OrizonUI = Object.assign(window.OrizonUI || {}, { setLoading(target, active) { const root = typeof target === "string" ? document.querySelector(target) : target; if (!root?.matches("[data-orizon-loading-overlay]")) return; root.classList.toggle("is-loading", active); root.setAttribute("aria-busy", String(active)); root.querySelector(".orizon-loading-overlay").hidden = !active; } });
