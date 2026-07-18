using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

public abstract class UploadTagHelperBase : TagHelper
{
    [HtmlAttributeName("asp-for")] public ModelExpression? For { get; set; }
    [HtmlAttributeName("name")] public string? Name { get; set; }
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("label")] public string? Label { get; set; }
    [HtmlAttributeName("helper-text")] public string? HelperText { get; set; }
    [HtmlAttributeName("required")] public bool Required { get; set; }
    [HtmlAttributeName("disabled")] public bool Disabled { get; set; }
    [HtmlAttributeName("multiple")] public bool Multiple { get; set; }
    [HtmlAttributeName("accept")] public string? Accept { get; set; }
    [HtmlAttributeName("max-size")] public long? MaxSize { get; set; }
    [HtmlAttributeName("preview")] public bool Preview { get; set; } = true; [ViewContext, HtmlAttributeNotBound] public ViewContext ViewContext { get; set; } = null!; protected abstract bool Drop { get; }
    public override void Process(TagHelperContext context, TagHelperOutput output) { var e = HtmlEncoder.Default; var name = For is null ? Name ?? $"upload-{context.UniqueId}" : ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(For.Name); var id = Id ?? TagBuilder.CreateSanitizedId(name, "_"); output.TagName = "div"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-advanced-field", Drop ? "orizon-drop-upload" : "orizon-file-upload")); output.Attributes.SetAttribute("data-orizon-upload", Drop ? "drop" : "file"); output.Attributes.SetAttribute("data-max-size", MaxSize?.ToString() ?? ""); output.Content.SetHtmlContent($"{FormFieldMarkup.Label(id, Label ?? For?.Metadata.DisplayName, Required)}<label class=\"orizon-upload__zone\" for=\"{e.Encode(id)}\"><span class=\"orizon-upload__icon\" aria-hidden=\"true\">⇧</span><strong>{(Drop ? "Arraste arquivos ou clique para selecionar" : "Selecionar arquivo")}</strong><span>{e.Encode(Accept ?? "Arquivos permitidos")}</span></label><input class=\"orizon-visually-hidden\" type=\"file\" id=\"{e.Encode(id)}\" name=\"{e.Encode(name)}\"{(Multiple ? " multiple" : "")}{(Required ? " required" : "")}{(Disabled ? " disabled" : "")}{(Accept is null ? "" : $" accept=\"{e.Encode(Accept)}\"")} data-upload-input><div class=\"orizon-upload__list\" data-upload-list data-preview=\"{Preview.ToString().ToLowerInvariant()}\" aria-live=\"polite\"></div>{FormFieldMarkup.Message(id, HelperText, false)}"); }
}
[HtmlTargetElement("orizon-file-upload")] public sealed class OrizonFileUploadTagHelper : UploadTagHelperBase { protected override bool Drop => false; }
[HtmlTargetElement("orizon-drag-drop-upload")] public sealed class OrizonDragDropUploadTagHelper : UploadTagHelperBase { protected override bool Drop => true; }
