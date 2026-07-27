const normalizeTemplateSearch = (value) =>
    value
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .toLocaleLowerCase()
        .trim();

const initializeTemplateGallery = (gallery) => {
    const input = gallery.querySelector("[data-template-search-input]");
    const cards = [...gallery.querySelectorAll("[data-template-card]")];
    const empty = gallery.querySelector("[data-template-empty]");
    const categoryFilters = [...gallery.querySelectorAll("[data-template-category-filter]")];
    let category = "";

    if (!input || cards.length === 0 || !empty) {
        return;
    }

    const filter = () => {
        const query = normalizeTemplateSearch(input.value);
        let visibleCount = 0;

        cards.forEach((card) => {
            const value = normalizeTemplateSearch(card.dataset.templateSearch ?? "");
            const matchesQuery = query.length === 0 || value.includes(query);
            const matchesCategory = category.length === 0 ||
                normalizeTemplateSearch(card.dataset.templateCategory ?? "") === category;
            const visible = matchesQuery && matchesCategory;
            card.hidden = !visible;
            visibleCount += visible ? 1 : 0;
        });

        empty.hidden = visibleCount > 0;
    };

    input.addEventListener("input", filter);
    categoryFilters.forEach((button) => {
        button.addEventListener("click", () => {
            category = normalizeTemplateSearch(button.dataset.templateCategoryFilter ?? "");
            categoryFilters.forEach((item) =>
                item.setAttribute("aria-pressed", String(item === button)));
            filter();
        });
    });
};

document
    .querySelectorAll("[data-template-gallery]")
    .forEach(initializeTemplateGallery);
