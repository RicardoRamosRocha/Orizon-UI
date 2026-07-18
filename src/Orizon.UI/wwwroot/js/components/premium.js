const selector = "[data-orizon-accordion], [data-orizon-chip]";

function toggleChip(chip) {
    if (chip.getAttribute("aria-disabled") === "true") return;
    const selected = chip.getAttribute("aria-checked") !== "true";
    chip.setAttribute("aria-checked", String(selected));
    chip.classList.toggle("is-selected", selected);
    const input = chip.querySelector("[data-chip-input]");
    if (input) input.disabled = !selected;
    chip.dispatchEvent(new CustomEvent("orizon:chip-change", { bubbles: true, detail: { selected, value: chip.dataset.value } }));
}

document.addEventListener("click", event => {
    const remove = event.target.closest("[data-chip-remove]");
    if (remove) { const chip = remove.closest("[data-orizon-chip]"); chip.dispatchEvent(new CustomEvent("orizon:chip-remove", { bubbles: true, detail: { value: chip.dataset.value } })); chip.remove(); return; }
    const chip = event.target.closest("[data-chip-selectable]"); if (chip) toggleChip(chip);
    const trigger = event.target.closest("[data-orizon-accordion] .orizon-accordion__trigger");
    if (trigger) {
        const accordion = trigger.closest("[data-orizon-accordion]"); const item = trigger.closest("[data-accordion-item]"); const open = trigger.getAttribute("aria-expanded") === "true";
        if (accordion.dataset.orizonAccordion === "single" && !open) accordion.querySelectorAll(".orizon-accordion__trigger[aria-expanded=true]").forEach(other => { if (other !== trigger) { other.setAttribute("aria-expanded", "false"); document.getElementById(other.getAttribute("aria-controls"))?.setAttribute("hidden", ""); other.closest("[data-accordion-item]")?.classList.remove("is-expanded"); } });
        trigger.setAttribute("aria-expanded", String(!open)); document.getElementById(trigger.getAttribute("aria-controls"))?.toggleAttribute("hidden", open); item.classList.toggle("is-expanded", !open);
    }
});

document.addEventListener("keydown", event => {
    const chip = event.target.closest("[data-chip-selectable]"); if (chip && (event.key === " " || event.key === "Enter")) { event.preventDefault(); toggleChip(chip); }
    const trigger = event.target.closest("[data-orizon-accordion] .orizon-accordion__trigger"); if (!trigger || !["ArrowDown", "ArrowUp", "Home", "End"].includes(event.key)) return;
    event.preventDefault(); const triggers = [...trigger.closest("[data-orizon-accordion]").querySelectorAll(".orizon-accordion__trigger:not(:disabled)")]; const index = triggers.indexOf(trigger); const next = event.key === "Home" ? 0 : event.key === "End" ? triggers.length - 1 : (index + (event.key === "ArrowDown" ? 1 : -1) + triggers.length) % triggers.length; triggers[next]?.focus();
});

document.documentElement.classList.add("orizon-premium-ready");
