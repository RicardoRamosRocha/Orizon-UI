document.addEventListener("click", event => {
    const collapse = event.target.closest("[data-widget-collapse]");
    if (collapse) {
        const widget = collapse.closest("[data-dashboard-widget]"); const body = widget.querySelector(".orizon-dashboard-widget__body"); const expanded = collapse.getAttribute("aria-expanded") === "true";
        collapse.setAttribute("aria-expanded", String(!expanded)); collapse.setAttribute("aria-label", expanded ? "Expandir widget" : "Recolher widget"); body.hidden = expanded; widget.classList.toggle("is-collapsed", expanded);
    }
    const fullscreen = event.target.closest("[data-widget-fullscreen]");
    if (fullscreen) { const widget=fullscreen.closest("[data-dashboard-widget]"); const active=widget.classList.toggle("is-fullscreen"); fullscreen.setAttribute("aria-label",active?"Sair da tela cheia":"Exibir widget em tela cheia"); document.body.style.overflow=active?"hidden":""; }
    const refresh = event.target.closest("[data-widget-refresh]");
    if (refresh) { const widget=refresh.closest("[data-dashboard-widget]"); widget.classList.add("is-refreshing"); widget.setAttribute("aria-busy","true"); widget.dispatchEvent(new CustomEvent("orizon:widget-refresh",{bubbles:true,detail:{complete(){widget.classList.remove("is-refreshing");widget.removeAttribute("aria-busy")}}})); }
});
document.addEventListener("keydown", event => {
    if (event.key !== "Escape") return;
    const widget = document.querySelector(".orizon-dashboard-widget.is-fullscreen");
    if (!widget) return;
    widget.classList.remove("is-fullscreen");
    document.body.style.overflow = "";
    const button = widget.querySelector("[data-widget-fullscreen]");
    button?.setAttribute("aria-label", "Exibir widget em tela cheia");
    button?.focus();
});
