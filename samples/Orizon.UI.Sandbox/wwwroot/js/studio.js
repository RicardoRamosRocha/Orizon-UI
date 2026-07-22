(() => {
    "use strict";

    const normalize = value => (value || "").normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase().trim();
    const escapeHtml = value => value.replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll('"', "&quot;");

    const copyText = async (value, button, status) => {
        try {
            await navigator.clipboard.writeText(value);
            button?.classList.add("is-copied");
            const label = button?.querySelector("span");
            const previous = label?.textContent;
            if (label) label.textContent = "Copiado";
            if (status) { status.hidden = false; status.textContent = "Código copiado para a área de transferência."; }
            window.setTimeout(() => {
                button?.classList.remove("is-copied");
                if (label && previous) label.textContent = previous;
                if (status) status.textContent = "";
            }, 2000);
        } catch {
            if (status) { status.hidden = false; status.textContent = "Não foi possível copiar automaticamente."; }
        }
    };

    const searchRoot = document.querySelector("[data-studio-search]");
    if (searchRoot) {
        const input = searchRoot.querySelector("input");
        const results = searchRoot.querySelector(".studio-search__results");
        const items = [...results.querySelectorAll("[data-search-value]")];
        const empty = results.querySelector("[data-search-empty]");
        let activeIndex = -1;

        const visibleItems = () => items.filter(item => !item.hidden);
        const setActive = index => {
            visibleItems().forEach(item => item.classList.remove("is-keyboard-active"));
            const visible = visibleItems();
            if (!visible.length) return;
            activeIndex = (index + visible.length) % visible.length;
            visible[activeIndex].classList.add("is-keyboard-active");
            visible[activeIndex].scrollIntoView({ block: "nearest" });
        };
        const updateSearch = () => {
            const query = normalize(input.value);
            let count = 0;
            items.forEach(item => {
                const matches = query.length > 0 && normalize(item.dataset.searchValue).includes(query);
                item.hidden = !matches;
                if (matches && count < 8) count += 1;
                else if (matches) item.hidden = true;
            });
            empty.hidden = count !== 0 || query.length === 0;
            results.hidden = query.length === 0;
            input.setAttribute("aria-expanded", String(query.length > 0));
            activeIndex = -1;
        };
        input.addEventListener("input", updateSearch);
        input.addEventListener("keydown", event => {
            if (event.key === "ArrowDown") { event.preventDefault(); setActive(activeIndex + 1); }
            if (event.key === "ArrowUp") { event.preventDefault(); setActive(activeIndex - 1); }
            if (event.key === "Enter" && activeIndex >= 0) visibleItems()[activeIndex]?.click();
            if (event.key === "Escape") { input.value = ""; updateSearch(); input.blur(); }
        });
        document.addEventListener("keydown", event => {
            if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "k") { event.preventDefault(); input.focus(); }
        });
        document.addEventListener("click", event => {
            if (!searchRoot.contains(event.target)) { results.hidden = true; input.setAttribute("aria-expanded", "false"); }
        });
    }

    document.querySelectorAll("[data-local-filter]").forEach(input => {
        const target = input.dataset.localFilter;
        const items = [...document.querySelectorAll('[data-filter-item="' + target + '"]')];
        input.addEventListener("input", () => items.forEach(item => { item.hidden = !normalize(item.dataset.filterValue).includes(normalize(input.value)); }));
    });

    document.querySelectorAll("[data-viewport-controls]").forEach(controls => {
        const scope = controls.closest("[data-studio-page]");
        const shells = [...scope.querySelectorAll("[data-preview-shell]")];
        controls.querySelectorAll("[data-viewport]").forEach(button => button.addEventListener("click", () => {
            controls.querySelectorAll("[data-viewport]").forEach(item => item.classList.toggle("is-active", item === button));
            shells.forEach(shell => shell.dataset.size = button.dataset.viewport);
        }));
    });

    document.querySelectorAll("[data-code-panel]").forEach(panel => {
        const tabs = [...panel.querySelectorAll("[data-code-tab]")];
        const blocks = [...panel.querySelectorAll("[data-code-content]")];
        const copy = panel.querySelector("[data-copy-active]");
        const status = panel.querySelector(".studio-copy-status");
        let active = "razor";
        tabs.forEach(tab => tab.addEventListener("click", () => {
            active = tab.dataset.codeTab;
            tabs.forEach(item => { item.classList.toggle("is-active", item === tab); item.setAttribute("aria-selected", String(item === tab)); });
            blocks.forEach(block => block.hidden = block.dataset.codeContent !== active);
            copy.querySelector("span").textContent = "Copy " + (active === "csharp" ? "C#" : active.toUpperCase());
        }));
        copy.addEventListener("click", () => copyText(panel.querySelector('[data-code-content="' + active + '"] code').textContent, copy, status));
    });

    const playground = document.querySelector("[data-button-playground]");
    if (playground) {
        const controls = Object.fromEntries([...playground.querySelectorAll("[data-button-control]")].map(control => [control.dataset.buttonControl, control]));
        const preview = playground.querySelector("[data-button-preview]");
        const output = playground.querySelector("[data-generated-output]");
        const panel = playground.querySelector("[data-generated-panel]");
        const tabs = [...panel.querySelectorAll("[data-generated-tab]")];
        const copy = panel.querySelector("[data-copy-generated]");
        const status = panel.querySelector(".studio-copy-status");
        let active = "razor";
        let snippets = {};
        const checked = key => controls[key].checked;
        const update = () => {
            const text = controls.text.value || "Button";
            const variant = controls.variant.value;
            const size = controls.size.value;
            const icon = controls.icon.value;
            const loading = checked("loading");
            const fullWidth = checked("fullWidth");
            preview.className = "orizon-button orizon-button--" + variant + " orizon-button--primary orizon-button--" + size + (fullWidth ? " orizon-button--block" : "") + (loading ? " is-loading" : "") + (checked("rounded") ? " is-rounded" : "");
            preview.disabled = checked("disabled") || loading;
            preview.toggleAttribute("aria-busy", loading);
            preview.innerHTML = (loading ? '<span class="orizon-button__spinner" aria-hidden="true"></span>' : "") + (icon ? '<i class="orizon-button__icon ph ph-' + icon + '" aria-hidden="true"></i>' : "") + '<span class="orizon-button__content">' + escapeHtml(text) + "</span>";
            const attrs = ['variant="' + variant + '"', 'color="primary"', 'size="' + size + '"', icon ? 'icon="' + icon + '"' : "", checked("disabled") ? "disabled" : "", loading ? "loading" : "", fullWidth ? "full-width" : ""].filter(Boolean).join(" ");
            snippets = {
                razor: "<orizon-button " + attrs + ">" + text + "</orizon-button>",
                html: preview.outerHTML,
                css: checked("rounded") ? ".my-button { border-radius: 999px; }" : ".my-button { /* use os design tokens Orizon */ }",
                csharp: 'ViewData["PrimaryAction"] = "' + text.replaceAll('"', '\\"') + '";'
            };
            output.textContent = snippets[active];
        };
        Object.values(controls).forEach(control => control.addEventListener(control.type === "text" ? "input" : "change", update));
        tabs.forEach(tab => tab.addEventListener("click", () => {
            active = tab.dataset.generatedTab;
            tabs.forEach(item => item.classList.toggle("is-active", item === tab));
            output.textContent = snippets[active];
            copy.querySelector("span").textContent = "Copy " + (active === "csharp" ? "C#" : active.toUpperCase());
        }));
        copy.addEventListener("click", () => copyText(snippets[active], copy, status));
        update();
    }

    const iconExplorer = document.querySelector("[data-icon-explorer]");
    if (iconExplorer) {
        const search = iconExplorer.querySelector("[data-icon-search]");
        const cards = [...iconExplorer.querySelectorAll("[data-icon-item]")];
        const categoryButtons = [...iconExplorer.querySelectorAll("[data-icon-category]")];
        const empty = iconExplorer.querySelector("[data-icon-empty]");
        const status = iconExplorer.querySelector(".studio-floating-status");
        let category = "all";
        const filter = () => {
            let count = 0;
            cards.forEach(card => {
                const visible = (category === "all" || card.dataset.iconCategoryValue === category) && normalize(card.dataset.iconName).includes(normalize(search.value));
                card.hidden = !visible;
                if (visible) count += 1;
            });
            empty.hidden = count > 0;
        };
        search.addEventListener("input", filter);
        categoryButtons.forEach(button => button.addEventListener("click", () => {
            category = button.dataset.iconCategory;
            categoryButtons.forEach(item => item.classList.toggle("is-active", item === button));
            filter();
        }));
        cards.forEach(card => card.querySelector("[data-copy-text]").addEventListener("click", event => copyText(event.currentTarget.dataset.copyText, event.currentTarget, status)));
        if (location.hash) document.getElementById(location.hash.slice(1))?.scrollIntoView({ block: "center" });
    }

    const themeBuilder = document.querySelector("[data-theme-builder]");
    if (themeBuilder) {
        const preview = themeBuilder.querySelector("[data-theme-preview]");
        const controls = [...themeBuilder.querySelectorAll("[data-theme-token]")];
        const defaults = Object.fromEntries(controls.map(control => [control.dataset.themeToken, control.value]));
        const apply = control => {
            const unit = control.dataset.unit || "";
            preview.style.setProperty(control.dataset.themeToken, control.value + unit);
            const name = control.dataset.themeToken.replace("--studio-", "");
            const output = themeBuilder.querySelector('[data-theme-output="' + name + '"]');
            if (output) output.textContent = control.value + unit;
        };
        controls.forEach(control => control.addEventListener("input", () => apply(control)));
        themeBuilder.querySelector("[data-theme-dark]").addEventListener("change", event => preview.classList.toggle("is-dark", event.currentTarget.checked));
        themeBuilder.querySelector("[data-theme-reset]").addEventListener("click", () => {
            controls.forEach(control => { control.value = defaults[control.dataset.themeToken]; apply(control); });
            themeBuilder.querySelector("[data-theme-dark]").checked = false;
            preview.classList.remove("is-dark");
        });
    }

    const layoutBuilder = document.querySelector("[data-layout-builder]");
    if (layoutBuilder) {
        const preview = layoutBuilder.querySelector("[data-layout-preview]");
        const control = name => layoutBuilder.querySelector('[data-layout-control="' + name + '"]');
        const update = () => {
            preview.classList.toggle("is-sidebar-hidden", !control("sidebar").checked);
            preview.classList.toggle("is-compact", control("sidebar").checked && control("compact").checked);
            preview.classList.toggle("is-topbar-hidden", !control("topbar").checked);
            preview.dataset.width = control("width").value;
            preview.dataset.density = control("density").value;
        };
        layoutBuilder.querySelectorAll("[data-layout-control]").forEach(item => item.addEventListener("change", update));
        control("sidebar").addEventListener("change", () => { if (!control("sidebar").checked) control("compact").checked = false; });
        control("compact").addEventListener("change", () => { if (control("compact").checked) control("sidebar").checked = true; });
        update();
    }
})();
