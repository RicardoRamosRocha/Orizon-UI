(() => {
    "use strict";

    const root = document.querySelector(".designer-shell");
    if (!root) return;

    const canvas = root.querySelector("[data-designer-canvas]");
    const zones = root.querySelector("[data-designer-zones]");
    const undoButton = root.querySelector("[data-designer-undo]");
    const redoButton = root.querySelector("[data-designer-redo]");
    const selectionStatus = root.querySelector("[data-designer-selection]");
    const historyStatus = root.querySelector("[data-designer-history]");
    const properties = name => root.querySelector(`[data-designer-property="${name}"]`);
    const viewportButtons = [...root.querySelectorAll("[data-designer-viewport]")];
    const undo = [];
    const redo = [];
    let drag = null;
    let selectedId = null;
    let indicator = null;

    const snapshot = () => ({
        html: zones.innerHTML,
        selectedId,
        scrollLeft: canvas.scrollLeft,
        scrollTop: canvas.scrollTop
    });

    const restore = state => {
        zones.innerHTML = state.html;
        selectedId = state.selectedId;
        bindCanvas();
        select(selectedId);
        canvas.scrollLeft = state.scrollLeft;
        canvas.scrollTop = state.scrollTop;
    };

    const updateHistory = () => {
        undoButton.disabled = undo.length === 0;
        redoButton.disabled = redo.length === 0;
        historyStatus.textContent = `${undo.length} change${undo.length === 1 ? "" : "s"}`;
    };

    const command = mutate => {
        undo.push(snapshot());
        redo.length = 0;
        mutate();
        normalize();
        updateHistory();
    };

    const normalize = () => {
        zones.querySelectorAll("[data-designer-zone]").forEach(zone => {
            [...zone.querySelectorAll("[data-designer-widget]")].forEach((widget, order) => {
                widget.dataset.zone = zone.dataset.designerZone;
                widget.dataset.zoneKind = zone.dataset.zoneKind;
                widget.dataset.order = String(order);
                const orderLabel = widget.querySelector("small");
                if (orderLabel) orderLabel.textContent = `${widget.dataset.widgetName} · #${order}`;
            });
        });
        select(selectedId);
    };

    const select = id => {
        selectedId = id && zones.querySelector(`[data-designer-widget="${CSS.escape(id)}"]`) ? id : null;
        zones.querySelectorAll("[data-designer-widget]").forEach(widget =>
            widget.classList.toggle("is-selected", widget.dataset.designerWidget === selectedId));
        const widget = selectedId ? zones.querySelector(`[data-designer-widget="${CSS.escape(selectedId)}"]`) : null;
        selectionStatus.textContent = `Selection: ${widget ? "Widget" : "Template"}`;
        properties("widget").textContent = widget?.dataset.widgetName || "Nenhum widget selecionado";
        ["zone", "order", "priority", "visibleOn", "hiddenOn"].forEach(name => {
            properties(name).textContent = widget?.dataset[name] || "—";
        });
    };

    const insertAt = (container, node, index) => {
        const widgets = [...container.querySelectorAll(":scope > [data-designer-widget]")];
        container.insertBefore(node, widgets[index] || container.querySelector(".designer-zone-placeholder"));
    };

    const createWidget = (name, type) => {
        const id = `designer-${name.toLowerCase().replace(/[^a-z0-9]+/g, "-")}-${crypto.randomUUID().slice(0, 8)}`;
        const node = document.createElement("article");
        node.className = "designer-widget is-preview";
        node.draggable = true;
        Object.assign(node.dataset, {
            designerWidget: id, widgetName: name, widgetType: type,
            priority: "0", visibleOn: "", hiddenOn: ""
        });
        node.innerHTML = `<span class="designer-widget-handle">⋮⋮</span><div><strong>${name}</strong><small>${name}</small></div><div class="designer-widget-actions"><button type="button" data-designer-duplicate aria-label="Duplicar widget">＋</button><button type="button" data-designer-delete aria-label="Remover widget">×</button></div>`;
        return node;
    };

    const clearIndicator = () => { indicator?.remove(); indicator = null; root.classList.remove("is-dragging"); };

    const BeginDrag = payload => { drag = payload; root.classList.add("is-dragging"); };
    const EndDrag = () => { drag = null; clearIndicator(); };
    const DropWidget = (zone, index) => command(() => {
        let widget;
        if (drag.source === "toolbox") widget = createWidget(drag.name, drag.type);
        else widget = zones.querySelector(`[data-designer-widget="${CSS.escape(drag.id)}"]`);
        if (!widget) return;
        insertAt(zone.querySelector(".designer-zone-widgets"), widget, index);
        selectedId = widget.dataset.designerWidget;
        widget.classList.remove("is-preview");
    });
    const MoveWidget = (id, zone, index) => { BeginDrag({ source: "canvas", id }); DropWidget(zone, index); EndDrag(); };
    const InsertWidget = (name, type, zone, index) => { BeginDrag({ source: "toolbox", name, type }); DropWidget(zone, index); EndDrag(); };
    const DeleteWidget = id => command(() => zones.querySelector(`[data-designer-widget="${CSS.escape(id)}"]`)?.remove());
    const Undo = () => { if (!undo.length) return; redo.push(snapshot()); restore(undo.pop()); updateHistory(); };
    const Redo = () => { if (!redo.length) return; undo.push(snapshot()); restore(redo.pop()); updateHistory(); };
    const CanUndo = () => undo.length > 0;
    const CanRedo = () => redo.length > 0;

    window.OrizonDesigner = { BeginDrag, EndDrag, DropWidget, MoveWidget, InsertWidget, DeleteWidget, Undo, Redo, CanUndo, CanRedo };

    const bindCanvas = () => {
        zones.querySelectorAll("[data-designer-widget]").forEach(widget => {
            if (widget.dataset.designerBound) return;
            widget.dataset.designerBound = "true";
            widget.addEventListener("click", () => select(widget.dataset.designerWidget));
            widget.addEventListener("dragstart", event => {
                BeginDrag({ source: "canvas", id: widget.dataset.designerWidget });
                event.dataTransfer.effectAllowed = "move";
            });
            widget.addEventListener("dragend", EndDrag);
            widget.querySelector("[data-designer-delete]")?.addEventListener("click", event => {
                event.stopPropagation(); DeleteWidget(widget.dataset.designerWidget);
            });
            widget.querySelector("[data-designer-duplicate]")?.addEventListener("click", event => {
                event.stopPropagation();
                const clone = createWidget(widget.dataset.widgetName, widget.dataset.widgetType || widget.dataset.widgetName);
                command(() => { widget.after(clone); selectedId = clone.dataset.designerWidget; });
            });
        });
        zones.querySelectorAll("[data-designer-zone]").forEach(zone => {
            if (zone.dataset.designerBound) return;
            zone.dataset.designerBound = "true";
            zone.addEventListener("dragover", event => {
                if (!drag || drag.source === "zone") return;
                event.preventDefault();
                const list = zone.querySelector(".designer-zone-widgets");
                const widgets = [...list.querySelectorAll(":scope > [data-designer-widget]")];
                const next = widgets.find(widget => event.clientY < widget.getBoundingClientRect().top + widget.offsetHeight / 2);
                indicator ||= Object.assign(document.createElement("div"), { className: "designer-drop-indicator" });
                list.insertBefore(indicator, next || list.querySelector(".designer-zone-placeholder"));
            });
            zone.addEventListener("drop", event => {
                if (!drag || drag.source === "zone") return;
                event.preventDefault();
                const index = [...zone.querySelectorAll("[data-designer-widget], .designer-drop-indicator")].indexOf(indicator);
                indicator?.remove(); indicator = null;
                DropWidget(zone, Math.max(0, index)); EndDrag(); bindCanvas();
            });
            zone.addEventListener("dragstart", event => {
                if (event.target !== zone) return;
                BeginDrag({ source: "zone", zone });
            });
            zone.addEventListener("dragend", EndDrag);
        });
    };

    root.querySelectorAll("[data-designer-toolbox-widget]").forEach(item => {
        item.addEventListener("dragstart", event => {
            BeginDrag({ source: "toolbox", type: item.dataset.designerToolboxWidget, name: item.dataset.designerWidgetName });
            event.dataTransfer.effectAllowed = "copy";
        });
        item.addEventListener("dragend", EndDrag);
    });
    viewportButtons.forEach(button => button.addEventListener("click", () => {
        const left = canvas.scrollLeft;
        const top = canvas.scrollTop;
        const active = selectedId;
        canvas.closest(".designer-canvas").className = `designer-canvas designer-viewport--${button.dataset.designerViewport.toLowerCase()}`;
        viewportButtons.forEach(item => item.classList.toggle("is-active", item === button));
        properties("viewport").textContent = button.dataset.designerViewport;
        select(active);
        canvas.scrollLeft = left;
        canvas.scrollTop = top;
    }));
    zones.addEventListener("dragover", event => {
        if (drag?.source !== "zone") return;
        event.preventDefault();
        const candidates = [...zones.querySelectorAll(":scope > [data-designer-zone]")].filter(zone => zone !== drag.zone);
        const next = candidates.find(zone => event.clientY < zone.getBoundingClientRect().top + zone.offsetHeight / 2);
        indicator ||= Object.assign(document.createElement("div"), { className: "designer-zone-drop-indicator" });
        zones.insertBefore(indicator, next || null);
    });
    zones.addEventListener("drop", event => {
        if (drag?.source !== "zone") return;
        event.preventDefault();
        command(() => zones.insertBefore(drag.zone, indicator));
        EndDrag();
    });
    undoButton.addEventListener("click", Undo);
    redoButton.addEventListener("click", Redo);
    bindCanvas();
    normalize();
    updateHistory();
})();
