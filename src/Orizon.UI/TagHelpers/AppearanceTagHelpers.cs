using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-theme-card")]
public sealed class ThemeCardTagHelper : TagHelper
{
    public string Theme { get; set; } = "light";
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("type", "button");
        output.Attributes.SetAttribute("class", "orizon-theme-card");
        output.Attributes.SetAttribute("data-appearance-key", "theme");
        output.Attributes.SetAttribute("data-appearance-value", Theme);
        output.Attributes.SetAttribute("aria-pressed", "false");
        output.Content.SetHtmlContent($"""
            <span class="orizon-theme-card__preview" data-theme="{Theme}" aria-hidden="true">
                <span class="orizon-theme-card__sidebar"><i></i><i></i><i></i></span>
                <span class="orizon-theme-card__workspace">
                    <span class="orizon-theme-card__topbar"></span>
                    <span class="orizon-theme-card__card"></span>
                    <span class="orizon-theme-card__button"></span>
                    <span class="orizon-theme-card__table"><i></i><i></i></span>
                </span>
            </span>
            <span class="orizon-theme-card__content"><strong>{Title}</strong><small>{Description}</small></span>
            <span class="orizon-theme-card__check" aria-hidden="true">&#10003;</span>
            """);
    }
}

[HtmlTargetElement("orizon-appearance-option")]
public sealed class AppearanceOptionTagHelper : TagHelper
{
    public string Preference { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("type", "button");
        output.Attributes.SetAttribute("class", "orizon-appearance-option");
        output.Attributes.SetAttribute("data-appearance-key", Preference);
        output.Attributes.SetAttribute("data-appearance-value", Value);
        output.Attributes.SetAttribute("aria-pressed", "false");
        output.Content.SetHtmlContent($"""
            <span class="orizon-appearance-option__icon" aria-hidden="true"><i class="{Icon}"></i></span>
            <span><strong>{Label}</strong><small>{Description}</small></span>
            """);
    }
}

[HtmlTargetElement("orizon-preview-panel")]
public sealed class PreviewPanelTagHelper : TagHelper
{
    public string Title { get; set; } = "Preview em tempo real";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "section";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", "orizon-preview-panel");
        output.Attributes.SetAttribute("aria-label", Title);
        output.Content.SetHtmlContent($"""
            <header><strong>{Title}</strong><span class="orizon-badge orizon-badge--success">Ao vivo</span></header>
            <div class="orizon-preview-panel__frame">
                <aside><b>O</b><i></i><i></i><i></i></aside>
                <main>
                    <nav><span></span><span></span></nav>
                    <div class="orizon-preview-panel__body">
                        <article><strong>Resumo</strong><button type="button">Acao</button></article>
                        <div class="orizon-preview-panel__table"><b>Produto</b><b>Status</b><span>Item demonstrativo</span><em>Ativo</em></div>
                    </div>
                </main>
            </div>
        """);
    }
}
