using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Orizon.UI.Charts;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-chart")]
public sealed class OrizonChartTagHelper : TagHelper
{
    [HtmlAttributeName("type")] public string Type { get; set; } = "line";
    [HtmlAttributeName("title")] public string? Title { get; set; }
    [HtmlAttributeName("subtitle")] public string? Subtitle { get; set; }
    [HtmlAttributeName("labels")] public string? Labels { get; set; }
    [HtmlAttributeName("series")] public string? Series { get; set; }
    [HtmlAttributeName("height")] public int Height { get; set; } = 320;
    [HtmlAttributeName("legend")] public bool Legend { get; set; } = true;
    [HtmlAttributeName("tooltip")] public bool Tooltip { get; set; } = true;
    [HtmlAttributeName("colors")] public string? Colors { get; set; }
    [HtmlAttributeName("grid")] public bool Grid { get; set; } = true;
    [HtmlAttributeName("axis")] public bool Axis { get; set; } = true;
    [HtmlAttributeName("loading")] public bool Loading { get; set; }
    [HtmlAttributeName("empty")] public bool Empty { get; set; }
    [HtmlAttributeName("error")] public string? Error { get; set; }
    [HtmlAttributeName("animate")] public bool Animate { get; set; } = true;
    [HtmlAttributeName("responsive")] public bool Responsive { get; set; } = true;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var labels=ChartSvgEngine.ParseLabels(Labels);var series=ChartSvgEngine.ParseSeries(Series);var colors=ChartSvgEngine.ParseColors(Colors);var id=output.Attributes["id"]?.Value?.ToString()??$"orizon-chart-{context.UniqueId.Replace("__","")}";var cls=output.Attributes["class"]?.Value?.ToString();output.Attributes.Clear();output.TagName="figure";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("id",id);output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-chart",$"orizon-chart--{Type}",Animate?"is-animated":null,Responsive?"is-responsive":null,Loading?"is-loading":null,cls));output.Attributes.SetAttribute("data-orizon-chart","");output.Attributes.SetAttribute("data-labels",Labels??"[]");output.Attributes.SetAttribute("data-series",Series??"[]");output.Attributes.SetAttribute("style",$"--orizon-chart-height:{Math.Clamp(Height,160,800)}px");if(Loading)output.Attributes.SetAttribute("aria-busy","true");
        var heading=string.IsNullOrWhiteSpace(Title)?"":$"<figcaption><strong>{HtmlEncoder.Default.Encode(Title)}</strong>{(Subtitle is null?"":$"<span>{HtmlEncoder.Default.Encode(Subtitle)}</span>")}</figcaption>";string body;if(Loading)body="<div class=\"orizon-chart__state\"><span class=\"orizon-skeleton orizon-skeleton--card is-animated\" aria-hidden=\"true\"></span><span class=\"orizon-visually-hidden\">Carregando gráfico</span></div>";else if(Error is not null)body=$"<div class=\"orizon-chart__state is-error\" role=\"alert\"><strong>Não foi possível carregar o gráfico</strong><span>{HtmlEncoder.Default.Encode(Error)}</span></div>";else if(Empty||series.Count==0||series.All(x=>x.Data.Count==0))body="<div class=\"orizon-chart__state\"><strong>Sem dados para exibir</strong><span>Os dados aparecerão aqui quando estiverem disponíveis.</span></div>";else body=$"<div class=\"orizon-chart__canvas\" style=\"height:var(--orizon-chart-height)\">{ChartSvgEngine.Render(Type,labels,series,colors,Grid,Axis)}{(Tooltip?"<div class=\"orizon-chart-tooltip\" role=\"tooltip\" aria-live=\"polite\" hidden></div>":"")}</div>{(Legend?LegendMarkup(series,colors):"")}";output.Content.SetHtmlContent($"{heading}{body}");
    }
    private static string LegendMarkup(IReadOnlyList<ChartSeries> series,IReadOnlyList<string> colors)=>$"<div class=\"orizon-chart-legend\" aria-label=\"Legenda do gráfico\">{string.Join("",series.Select((item,index)=>$"<button type=\"button\" data-legend-series=\"{index}\" aria-pressed=\"true\"><span style=\"--legend-color:{HtmlEncoder.Default.Encode(colors[index%colors.Count])}\"></span>{HtmlEncoder.Default.Encode(item.Name)}</button>"))}</div>";
}

[HtmlTargetElement("orizon-sparkline")]
public sealed class OrizonSparklineTagHelper:TagHelper
{
    [HtmlAttributeName("type")]public string Type{get;set;}="line";[HtmlAttributeName("values")]public string? Values{get;set;}[HtmlAttributeName("color")]public string Color{get;set;}="var(--orizon-color-primary)";[HtmlAttributeName("label")]public string? Label{get;set;}
    public override void Process(TagHelperContext context,TagHelperOutput output){IReadOnlyList<double> values;try{values=System.Text.Json.JsonSerializer.Deserialize<double[]>(Values??"[]")??[];}catch(System.Text.Json.JsonException){values=[];}output.TagName="span";output.TagMode=TagMode.StartTagAndEndTag;output.Attributes.SetAttribute("class",TagHelperClassBuilder.Build("orizon-sparkline",$"orizon-sparkline--{Type}",output.Attributes["class"]?.Value?.ToString()));if(Label is null)output.Attributes.SetAttribute("aria-hidden","true");else{output.Attributes.SetAttribute("role","img");output.Attributes.SetAttribute("aria-label",Label);}output.Content.SetHtmlContent(ChartSvgEngine.Sparkline(values,Type,Color));}
}
