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

    if (!input || cards.length === 0 || !empty) {
        return;
    }

    const filter = () => {
        const query = normalizeTemplateSearch(input.value);
        let visibleCount = 0;

        cards.forEach((card) => {
            const value = normalizeTemplateSearch(card.dataset.templateSearch ?? "");
            const visible = query.length === 0 || value.includes(query);
            card.hidden = !visible;
            visibleCount += visible ? 1 : 0;
        });

        empty.hidden = visibleCount > 0;
    };

    input.addEventListener("input", filter);
};

document
    .querySelectorAll("[data-template-gallery]")
    .forEach(initializeTemplateGallery);
