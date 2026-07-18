using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-tree-view")]
public sealed class OrizonTreeViewTagHelper:TagHelper
{
 [HtmlAttributeName("label")]public string Label{get;set;}="Árvore";[HtmlAttributeName("selection")]public string Selection{get;set;}="single";[HtmlAttributeName("lazy")]public bool Lazy{get;set;}
 public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output){var content=await output.GetChildContentAsync();output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-tree-view",output.Attributes["class"]?.Value?.ToString()));output.Attributes.SetAttribute("data-orizon-tree","");output.Attributes.SetAttribute("data-selection",Selection);output.Attributes.SetAttribute("data-lazy",Lazy.ToString().ToLowerInvariant());output.Attributes.SetAttribute("role","tree");output.Attributes.SetAttribute("aria-label",Label);output.Content.SetHtmlContent(content);}
}

[HtmlTargetElement("orizon-tree-grid")]
public sealed class OrizonTreeGridTagHelper:TagHelper
{
 [HtmlAttributeName("label")]public string Label{get;set;}="Grade hierárquica";[HtmlAttributeName("selectable")]public bool Selectable{get;set;}[HtmlAttributeName("striped")]public bool Striped{get;set;}[HtmlAttributeName("compact")]public bool Compact{get;set;}
 public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output){var content=await output.GetChildContentAsync();output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-tree-grid",Striped?"is-striped":null,Compact?"is-compact":null));output.Attributes.SetAttribute("data-orizon-tree-grid","");output.Attributes.SetAttribute("data-selectable",Selectable.ToString().ToLowerInvariant());output.Attributes.SetAttribute("role","treegrid");output.Attributes.SetAttribute("aria-label",Label);output.Content.SetHtmlContent($"<div class=\"orizon-tree-grid__scroll\">{content.GetContent()}</div>");}
}
