using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-timeline")]
public sealed class OrizonTimelineTagHelper:TagHelper
{
 [HtmlAttributeName("orientation")]public string Orientation{get;set;}="vertical";[HtmlAttributeName("label")]public string Label{get;set;}="Linha do tempo";[HtmlAttributeName("compact")]public bool Compact{get;set;}
 public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output){var content=await output.GetChildContentAsync();output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-timeline",$"orizon-timeline--{Orientation}",Compact?"is-compact":null));output.Attributes.SetAttribute("role","list");output.Attributes.SetAttribute("aria-label",Label);output.Content.SetHtmlContent(content);}
}

[HtmlTargetElement("orizon-kanban")]
public sealed class OrizonKanbanTagHelper:TagHelper
{
 [HtmlAttributeName("label")]public string Label{get;set;}="Quadro Kanban";[HtmlAttributeName("draggable")]public bool Draggable{get;set;}=true;[HtmlAttributeName("persist-key")]public string? PersistKey{get;set;}[HtmlAttributeName("compact")]public bool Compact{get;set;}
 public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output){var content=await output.GetChildContentAsync();output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-kanban",Compact?"is-compact":null));output.Attributes.SetAttribute("data-orizon-kanban","");output.Attributes.SetAttribute("data-draggable",Draggable.ToString().ToLowerInvariant());if(PersistKey is not null)output.Attributes.SetAttribute("data-persist-key",PersistKey);output.Attributes.SetAttribute("aria-label",Label);output.Content.SetHtmlContent(content);}
}

[HtmlTargetElement("orizon-activity-feed")]
public sealed class OrizonActivityFeedTagHelper:TagHelper
{
 [HtmlAttributeName("label")]public string Label{get;set;}="Atividades recentes";[HtmlAttributeName("grouped")]public bool Grouped{get;set;}=true;[HtmlAttributeName("compact")]public bool Compact{get;set;}
 public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output){var content=await output.GetChildContentAsync();output.TagName="section";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-activity-feed",Grouped?"is-grouped":null,Compact?"is-compact":null));output.Attributes.SetAttribute("aria-label",Label);output.Attributes.SetAttribute("data-orizon-activity-feed","");output.Content.SetHtmlContent(content);}
}

[HtmlTargetElement("orizon-quick-actions")]
public sealed class OrizonQuickActionsTagHelper:TagHelper
{
 [HtmlAttributeName("label")]public string Label{get;set;}="Ações rápidas";[HtmlAttributeName("columns")]public int Columns{get;set;}=4;[HtmlAttributeName("favorites")]public bool Favorites{get;set;}=true;[HtmlAttributeName("persist-key")]public string? PersistKey{get;set;}
 public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output){var content=await output.GetChildContentAsync();output.TagName="nav";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class","orizon-quick-actions");output.Attributes.SetAttribute("aria-label",Label);output.Attributes.SetAttribute("data-orizon-quick-actions","");output.Attributes.SetAttribute("data-favorites",Favorites.ToString().ToLowerInvariant());if(PersistKey is not null)output.Attributes.SetAttribute("data-persist-key",PersistKey);output.Attributes.SetAttribute("style",$"--quick-columns:{Math.Clamp(Columns,1,8)}");output.Content.SetHtmlContent(content);}
}
