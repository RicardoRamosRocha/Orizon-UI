document.addEventListener("keydown", event => {
    if (event.key !== "Escape") return;
    document.querySelectorAll(".orizon-multi-select__trigger[aria-expanded='true']")
        .forEach(trigger => trigger.setAttribute("aria-expanded", "false"));
    document.querySelectorAll("[data-autocomplete-input][aria-expanded='true']")
        .forEach(input => input.setAttribute("aria-expanded", "false"));
});
