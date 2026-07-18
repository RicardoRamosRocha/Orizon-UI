using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-confirm-dialog")]
public sealed class OrizonConfirmDialogTagHelper : TagHelper
{
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("title")] public string Title { get; set; } = "Confirmar ação";
    [HtmlAttributeName("message")] public string Message { get; set; } = "Deseja continuar?";
    [HtmlAttributeName("type")] public string Type { get; set; } = "warning";
    [HtmlAttributeName("confirm-text")] public string ConfirmText { get; set; } = "Confirmar";
    [HtmlAttributeName("cancel-text")] public string CancelText { get; set; } = "Cancelar";
    [HtmlAttributeName("open")] public bool Open { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var id = Id ?? $"orizon-confirm-{context.UniqueId.Replace("__", "")}"; var titleId = $"{id}-title"; output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("id", id); output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-confirm-dialog", $"orizon-confirm-dialog--{Type}", Open ? "is-open" : null)); output.Attributes.SetAttribute("data-orizon-confirm", ""); output.Attributes.SetAttribute("role", "alertdialog"); output.Attributes.SetAttribute("aria-modal", "true"); output.Attributes.SetAttribute("aria-labelledby", titleId); if (!Open) output.Attributes.SetAttribute("hidden", "hidden");
        output.Content.SetHtmlContent($"<div class=\"orizon-confirm-dialog__backdrop\" data-confirm-cancel></div><div class=\"orizon-confirm-dialog__panel\" tabindex=\"-1\"><span class=\"orizon-confirm-dialog__icon\" aria-hidden=\"true\">{(Type == "danger" ? "!" : Type == "success" ? "✓" : "i")}</span><h2 id=\"{e.Encode(titleId)}\">{e.Encode(Title)}</h2><p>{e.Encode(Message)}</p><div class=\"orizon-confirm-dialog__actions\"><button class=\"orizon-button orizon-button--outline orizon-button--neutral\" type=\"button\" data-confirm-cancel>{e.Encode(CancelText)}</button><button class=\"orizon-button orizon-button--solid orizon-button--{e.Encode(Type == "danger" ? "danger" : Type == "success" ? "success" : "primary")}\" type=\"button\" data-confirm-accept>{e.Encode(ConfirmText)}</button></div></div>");
    }
}
