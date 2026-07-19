(() => {
    "use strict";

    const root = document.querySelector("[data-playground-kind]");
    if (!root) return;

    const kind = root.dataset.playgroundKind;
    const preview = root.querySelector("[data-preview]");
    const code = root.querySelector("[data-generated-code]");
    const controls = Object.fromEntries(
        [...root.querySelectorAll("[data-control]")].map(control => [control.dataset.control, control])
    );
    const value = name => controls[name]?.value ?? "";
    const checked = name => controls[name]?.checked === true;
    const escapeAttribute = input => input.replaceAll("&", "&amp;").replaceAll('"', "&quot;");

    const updateButton = () => {
        const button = preview.querySelector(".orizon-button");
        const text = value("text") || "Button";
        const variant = value("variant");
        const size = value("size");
        const icon = value("icon");
        const loading = checked("loading");
        button.className = `orizon-button orizon-button--${variant} orizon-button--primary orizon-button--${size}${checked("fullWidth") ? " orizon-button--block" : ""}${loading ? " is-loading" : ""}`;
        button.disabled = checked("disabled") || loading;
        button.toggleAttribute("aria-busy", loading);
        button.innerHTML = `${loading ? '<span class="orizon-button__spinner" aria-hidden="true"></span>' : ""}${icon ? `<i class="orizon-button__icon ph ph-${icon}" aria-hidden="true"></i>` : ""}<span class="orizon-button__content"></span>`;
        button.querySelector(".orizon-button__content").textContent = text;
        const attrs = [`variant="${variant}"`, 'color="primary"', `size="${size}"`, icon ? `icon="${icon}"` : "", checked("disabled") ? "disabled" : "", loading ? "loading" : "", checked("fullWidth") ? "full-width" : ""].filter(Boolean).join("\n    ");
        code.textContent = `<orizon-button\n    ${attrs}>\n    ${text}\n</orizon-button>`;
    };

    const updateCard = () => {
        const card = preview.querySelector(".orizon-card");
        const title = value("title") || "Card title";
        const subtitle = value("subtitle");
        const variant = checked("bordered") ? "outlined" : value("elevation");
        const padding = value("padding");
        card.className = `orizon-card orizon-card--${variant} orizon-card--padding-${padding}`;
        card.querySelector(".orizon-card__title").textContent = title;
        card.querySelector(".orizon-card__subtitle").textContent = subtitle;
        code.textContent = `<orizon-card\n    title="${title}"\n    subtitle="${subtitle}"\n    variant="${variant}"\n    padding="${padding}">\n    Conteúdo\n</orizon-card>`;
    };

    const updateBadge = () => {
        const badge = preview.querySelector(".orizon-badge");
        const text = value("text") || "Badge";
        const color = value("color");
        const shape = value("shape");
        const size = value("size");
        badge.className = `orizon-badge orizon-badge--${color} orizon-badge--${size}${shape === "pill" ? " orizon-badge--pill" : ""}`;
        badge.innerHTML = shape === "dot" ? '<span class="orizon-badge__dot" aria-hidden="true"></span><span></span>' : "";
        if (shape === "dot") badge.lastElementChild.textContent = text; else badge.textContent = text;
        const shapeAttribute = shape === "default" ? "" : ` ${shape}`;
        code.textContent = `<orizon-badge variant="${color}" size="${size}"${shapeAttribute}>${text}</orizon-badge>`;
    };

    const updateInput = () => {
        const field = preview.querySelector(".orizon-field");
        const input = field.querySelector(".orizon-input");
        const label = field.querySelector("label");
        const labelText = value("label") || "Label";
        const placeholder = value("placeholder");
        input.placeholder = placeholder;
        input.readOnly = checked("readonly");
        input.disabled = checked("disabled");
        input.required = checked("required");
        input.setAttribute("aria-required", String(checked("required")));
        field.classList.toggle("is-disabled", checked("disabled"));
        label.textContent = `${labelText}${checked("required") ? " *" : ""}`;
        const attrs = [`label="${escapeAttribute(labelText)}"`, `placeholder="${escapeAttribute(placeholder)}"`, checked("readonly") ? "readonly" : "", checked("disabled") ? "disabled" : "", checked("required") ? "required" : ""].filter(Boolean).join("\n    ");
        code.textContent = `<orizon-input\n    name="Email"\n    type="email"\n    ${attrs} />`;
    };

    const update = { button: updateButton, card: updateCard, badge: updateBadge, input: updateInput }[kind];
    root.querySelectorAll("[data-control]").forEach(control => {
        control.addEventListener(control.matches("select,input[type='checkbox']") ? "change" : "input", update);
    });

    root.querySelector("[data-copy-code]")?.addEventListener("click", async event => {
        const button = event.currentTarget;
        const status = root.querySelector("[data-copy-status]");
        try {
            await navigator.clipboard.writeText(code.textContent);
            button.classList.add("is-copied");
            button.querySelector("span").textContent = "Código copiado";
            status.textContent = "Pronto para colar no seu arquivo Razor.";
            window.setTimeout(() => { button.classList.remove("is-copied"); button.querySelector("span").textContent = "Copiar Código"; status.textContent = ""; }, 2200);
        } catch {
            status.textContent = "Não foi possível copiar automaticamente. Selecione o código acima.";
        }
    });

    update();
})();
