using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-chart-legend")]
public sealed class OrizonChartLegendTagHelper:TagHelper
{
 [HtmlAttributeName("items")]public string? Items{get;set;}[HtmlAttributeName("toggleable")]public bool Toggleable{get;set;}=true;[HtmlAttributeName("label")]public string Label{get;set;}="Legenda do gráfico";
 public override void Process(TagHelperContext context,TagHelperOutput output){var e=HtmlEncoder.Default;List<(string Name,string Color)> items=[];try{using var doc=JsonDocument.Parse(Items??"[]");items=doc.RootElement.EnumerateArray().Select(x=>(x.GetProperty("name").GetString()??"Série",x.GetProperty("color").GetString()??"var(--orizon-color-primary)")).ToList();}catch(JsonException){}output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class","orizon-chart-legend");output.Attributes.SetAttribute("aria-label",Label);output.Content.SetHtmlContent(string.Join("",items.Select((x,i)=>$"<{(Toggleable?"button":"span")}{(Toggleable?" type=\"button\" aria-pressed=\"true\"":"")} data-legend-series=\"{i}\"><span style=\"--legend-color:{e.Encode(x.Color)}\"></span>{e.Encode(x.Name)}</{(Toggleable?"button":"span")}>")));}
}

[HtmlTargetElement("orizon-chart-tooltip")]
public sealed class OrizonChartTooltipTagHelper:TagHelper
{
 [HtmlAttributeName("id")]public string? Id{get;set;}[HtmlAttributeName("text")]public string? Text{get;set;}[HtmlAttributeName("visible")]public bool Visible{get;set;}
 public override void Process(TagHelperContext context,TagHelperOutput output){output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("id",Id??$"orizon-chart-tooltip-{context.UniqueId.Replace("__","")}");output.Attributes.SetAttribute("class","orizon-chart-tooltip");output.Attributes.SetAttribute("role","tooltip");output.Attributes.SetAttribute("aria-live","polite");if(!Visible)output.Attributes.SetAttribute("hidden","hidden");output.Content.SetContent(Text??"");}
}

[HtmlTargetElement("orizon-chart-empty-state")]
public sealed class OrizonChartEmptyStateTagHelper:TagHelper
{
 [HtmlAttributeName("icon")]public string? Icon{get;set;}[HtmlAttributeName("title")]public string Title{get;set;}="Sem dados para exibir";[HtmlAttributeName("description")]public string? Description{get;set;}[HtmlAttributeName("action-label")]public string? ActionLabel{get;set;}[HtmlAttributeName("action-href")]public string? ActionHref{get;set;}
 public override void Process(TagHelperContext context,TagHelperOutput output){var e=HtmlEncoder.Default;output.TagName="div";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class","orizon-chart-empty-state");output.Content.SetHtmlContent($"{(Icon is null?"":$"<span aria-hidden=\"true\"><i class=\"ph ph-{e.Encode(Icon)}\"></i></span>")}<strong>{e.Encode(Title)}</strong>{(Description is null?"":$"<p>{e.Encode(Description)}</p>")}{(ActionLabel is null||ActionHref is null?"":$"<a class=\"orizon-button orizon-button--soft orizon-button--primary\" href=\"{e.Encode(ActionHref)}\">{e.Encode(ActionLabel)}</a>")}");}
}
