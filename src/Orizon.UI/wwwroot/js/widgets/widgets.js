(() => {
    "use strict";

    const initialize = () => {};

    // Orizon.Widgets is the widget framework's canonical public namespace.
    // Existing namespace objects and implementations always take precedence.
    window.Orizon = window.Orizon || {};
    window.Orizon.Widgets = window.Orizon.Widgets || {};

    if (typeof window.Orizon.Widgets.initialize !== "function") {
        window.Orizon.Widgets.initialize = initialize;
    }

    // OrizonUI is the established library namespace. Expose the widget contract
    // there when available without replacing an existing Widgets object or method.
    window.OrizonUI = window.OrizonUI || {};
    window.OrizonUI.Widgets = window.OrizonUI.Widgets || window.Orizon.Widgets;

    if (typeof window.OrizonUI.Widgets.initialize !== "function") {
        window.OrizonUI.Widgets.initialize = window.Orizon.Widgets.initialize;
    }
})();
