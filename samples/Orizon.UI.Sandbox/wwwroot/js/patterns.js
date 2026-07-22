(() => {
    "use strict";

    const page = document.querySelector("[data-pattern-page]");
    if (!page) return;

    const announce = message => {
        const region = page.querySelector("[data-pattern-status]");
        if (region) region.textContent = message;
    };
    const normalize = value => (value || "").normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase();
    const wireTabs = (tabs, panels, key) => {
        const activate = tab => {
            tabs.forEach(item => {
                const active = item === tab;
                item.setAttribute("aria-selected", String(active));
                item.tabIndex = active ? 0 : -1;
            });
            panels.forEach(panel => panel.hidden = panel.dataset[key] !== tab.dataset[key.replace("Panel", "Tab")]);
        };
        tabs.forEach((tab, index) => {
            tab.addEventListener("click", () => activate(tab));
            tab.addEventListener("keydown", event => {
                if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return;
                event.preventDefault();
                const next = event.key === 'Home' ? 0 : event.key === 'End' ? tabs.length - 1 : (index + (event.key === 'ArrowRight' ? 1 : -1) + tabs.length) % tabs.length;
                tabs[next].focus(); activate(tabs[next]);
            });
        });
    };

    const code = page.querySelector("[data-pattern-code]");
    if (code) {
        const tabs = [...code.querySelectorAll("[data-code-tab]")];
        const blocks = [...code.querySelectorAll("[data-code-block]")];
        const copy = code.querySelector("[data-copy-code]");
        const status = code.querySelector("[data-copy-status]");
        let active = "razor";
        const activate = tab => {
            active = tab.dataset.codeTab;
            tabs.forEach(item => { const selected = item === tab; item.setAttribute("aria-selected", String(selected)); item.tabIndex = selected ? 0 : -1; });
            blocks.forEach(block => block.hidden = block.dataset.codeBlock !== active);
            copy.querySelector("span").textContent = `Copy ${active === "csharp" ? "C#" : active.toUpperCase()}`;
        };
        tabs.forEach((tab, index) => {
            tab.addEventListener("click", () => activate(tab));
            tab.addEventListener("keydown", event => {
                if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return;
                event.preventDefault();
                const next = event.key === 'Home' ? 0 : event.key === 'End' ? tabs.length - 1 : (index + (event.key === 'ArrowRight' ? 1 : -1) + tabs.length) % tabs.length;
                tabs[next].focus(); activate(tabs[next]);
            });
        });
        copy.addEventListener("click", async () => {
            try {
                await navigator.clipboard.writeText(code.querySelector(`[data-code-block="${active}"] code`).textContent);
                status.textContent = "Código copiado para a área de transferência.";
                window.setTimeout(() => status.textContent = "", 2400);
            } catch { status.textContent = "Não foi possível copiar automaticamente. Selecione o código e copie manualmente."; }
        });
    }

    page.querySelectorAll("form").forEach(form => form.addEventListener("submit", event => event.preventDefault()));
    page.querySelectorAll("[data-demo-action]").forEach(button => button.addEventListener("click", () => announce(button.dataset.demoAction)));

    const crud = page.querySelector('[data-demo="crud"]');
    if (crud) {
        const rows = [...crud.querySelectorAll("[data-demo-row]")];
        const search = crud.querySelector("orizon-search-box input, .orizon-search-box input, input[type=search]");
        const filter = crud.querySelector("[data-demo-filter]");
        const apply = () => rows.forEach(row => row.hidden = !normalize(row.dataset.demoRow).includes(normalize(search?.value)) || (filter.value && !row.dataset.demoRow.includes(filter.value)));
        search?.addEventListener("input", apply); filter.addEventListener("change", apply);
        const dialog = crud.querySelector("[data-demo-dialog]");
        let returnFocus;
        crud.querySelector("[data-demo-edit]").addEventListener("click", event => { returnFocus = event.currentTarget; dialog.hidden = false; dialog.querySelector("input")?.focus(); });
        const close = message => { dialog.hidden = true; returnFocus?.focus(); if (message) announce(message); };
        dialog.querySelector("[data-demo-close]").addEventListener("click", () => close());
        dialog.querySelector("[data-demo-save]").addEventListener("click", () => close("Cliente salvo. Notificação de sucesso exibida."));
        dialog.addEventListener("keydown", event => { if (event.key === "Escape") close(); });
        crud.querySelector("[data-demo-confirm]").addEventListener("click", () => announce("Confirmação de exclusão aberta."));
    }

    const login = page.querySelector('[data-demo="login"]');
    if (login) {
        wireTabs([...login.querySelectorAll("[data-auth-tab]")], [...login.querySelectorAll("[data-auth-panel]")], "authPanel");
        login.querySelectorAll("form").forEach(form => form.addEventListener("submit", () => announce("Fluxo validado. Próxima etapa preparada.")));
    }

    const wizard = page.querySelector('[data-demo="wizard"]');
    if (wizard) {
        const steps = [...wizard.querySelectorAll("[data-wizard-step]")];
        const markers = [...wizard.querySelectorAll(".patterns-stepper span")];
        const back = wizard.querySelector("[data-wizard-back]");
        const next = wizard.querySelector("[data-wizard-next]");
        let index = 0;
        const render = () => {
            steps.forEach((step, stepIndex) => step.hidden = stepIndex !== index);
            markers.forEach((marker, stepIndex) => marker.classList.toggle("is-current", stepIndex === index));
            back.disabled = index === 0; next.hidden = index === steps.length - 1;
            const progress = wizard.querySelector('[role="progressbar"]');
            progress?.setAttribute("aria-valuenow", String((index + 1) * 25));
            announce(`Etapa ${index + 1} de ${steps.length}.`);
        };
        next.addEventListener("click", () => { const required = steps[index].querySelector("input[required]"); if (required && !required.value) { required.focus(); announce("Preencha o campo obrigatório antes de avançar."); return; } index = Math.min(index + 1, steps.length - 1); render(); });
        back.addEventListener("click", () => { index = Math.max(index - 1, 0); render(); });
    }

    const searchDemo = page.querySelector('[data-demo="search"]');
    if (searchDemo) {
        const input = searchDemo.querySelector("orizon-search-box input, .orizon-search-box input, input[type=search]");
        const filters = [...searchDemo.querySelectorAll("[data-quick-filter]")];
        const results = searchDemo.querySelector("[data-search-results]");
        const empty = searchDemo.querySelector("[data-search-empty]");
        const sort = searchDemo.querySelector("[data-search-sort]");
        let filter = "";
        const apply = () => {
            const items = [...results.querySelectorAll("[data-search-item]")]; let count = 0;
            items.forEach(item => { const visible = normalize(item.dataset.searchItem).includes(normalize(input?.value)) && (!filter || item.dataset.searchItem.includes(filter)); item.hidden = !visible; if (visible) count++; });
            results.hidden = count === 0; empty.hidden = count !== 0; announce(`${count} resultado${count === 1 ? "" : "s"} encontrado${count === 1 ? "" : "s"}.`);
        };
        input?.addEventListener("input", apply);
        filters.forEach(button => button.addEventListener("click", () => { filter = button.dataset.quickFilter; filters.forEach(item => item.classList.toggle("is-active", item === button)); apply(); }));
        sort.addEventListener("change", () => { const items = [...results.children]; items.sort((a, b) => sort.value === "value" ? Number(b.dataset.value) - Number(a.dataset.value) : a.dataset.searchItem.localeCompare(b.dataset.searchItem)); items.forEach(item => results.append(item)); announce("Resultados reordenados."); });
    }

    const master = page.querySelector('[data-demo="master-detail"]');
    if (master) {
        const items = [...master.querySelectorAll("[data-master-item]")];
        items.forEach(item => item.addEventListener("click", () => { items.forEach(current => current.classList.toggle("is-active", current === item)); master.querySelector("[data-detail-name]").textContent = item.dataset.name; master.querySelector("[data-detail-segment]").textContent = item.dataset.segment; announce(`${item.dataset.name} selecionado.`); }));
        wireTabs([...master.querySelectorAll("[data-detail-tab]")], [...master.querySelectorAll("[data-detail-panel]")], "detailPanel");
    }

    const entry = page.querySelector('[data-demo="data-entry"]');
    if (entry) {
        let timer;
        entry.addEventListener("input", () => { const status = entry.querySelector("[data-autosave-status]"); status.textContent = "Salvando…"; clearTimeout(timer); timer = window.setTimeout(() => { status.textContent = "Rascunho salvo"; announce("Rascunho salvo automaticamente."); }, 700); });
        entry.addEventListener("submit", () => { const invalid = entry.querySelector("input[required]:invalid"); const error = entry.querySelector("[data-entry-error]"); error.hidden = !invalid; if (invalid) { invalid.focus(); announce("Há campos obrigatórios pendentes."); } else announce("Cadastro enviado com sucesso."); });
    }

    const approval = page.querySelector('[data-demo="approval"]');
    if (approval) approval.querySelectorAll("[data-approval]").forEach(button => button.addEventListener("click", () => { const comment = approval.querySelector("textarea")?.value.trim(); if (!comment) { approval.querySelector("textarea")?.focus(); announce("Adicione um comentário para registrar a decisão."); return; } announce(`Solicitação ${button.dataset.approval}. Auditoria atualizada.`); }));

    const kanban = page.querySelector('[data-demo="kanban"]');
    if (kanban) kanban.querySelectorAll("[data-kanban-filter]").forEach(button => button.addEventListener("click", () => { const value = button.dataset.kanbanFilter; kanban.querySelectorAll("[data-kanban-filter]").forEach(item => item.classList.toggle("is-active", item === button)); kanban.querySelectorAll("[data-kanban-card]").forEach(card => card.hidden = value && card.dataset.priority !== value); announce(value ? "Filtro de alta prioridade aplicado." : "Todos os cartões exibidos."); }));

    const analytics = page.querySelector('[data-demo="analytics"]');
    if (analytics) {
        analytics.querySelector("[data-analytics-period]").addEventListener("change", event => announce(`Período alterado para ${event.target.selectedOptions[0].text}.`));
        analytics.querySelector("[data-analytics-drill]").addEventListener("click", event => { const detail = analytics.querySelector("[data-analytics-detail]"); detail.hidden = !detail.hidden; event.currentTarget.setAttribute("aria-expanded", String(!detail.hidden)); announce(detail.hidden ? "Detalhamento fechado." : "Detalhamento por segmento aberto."); });
        analytics.querySelector(".orizon-command-bar button")?.addEventListener("click", () => announce("Exportação preparada."));
    }
})();
