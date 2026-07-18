using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Orizon.UI.Charts;

internal sealed record ChartSeries(string Name, IReadOnlyList<double> Data);

internal static class ChartSvgEngine
{
    private static readonly string[] DefaultColors = ["var(--orizon-chart-1)", "var(--orizon-chart-2)", "var(--orizon-chart-3)", "var(--orizon-chart-4)", "var(--orizon-chart-5)", "var(--orizon-chart-6)"];
    private static string N(double value) => value.ToString("0.##", CultureInfo.InvariantCulture);
    private static string E(string value) => HtmlEncoder.Default.Encode(value);

    public static IReadOnlyList<string> ParseLabels(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return [];
        try { return JsonSerializer.Deserialize<string[]>(json) ?? []; } catch (JsonException) { return []; }
    }

    public static IReadOnlyList<ChartSeries> ParseSeries(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return [];
        try
        {
            using var document = JsonDocument.Parse(json);
            return document.RootElement.EnumerateArray().Select((item, index) => new ChartSeries(
                item.TryGetProperty("name", out var name) ? name.GetString() ?? $"Série {index + 1}" : $"Série {index + 1}",
                item.TryGetProperty("data", out var data) ? data.EnumerateArray().Select(value => value.TryGetDouble(out var number) ? number : 0).ToArray() : [])).ToArray();
        }
        catch (JsonException) { return []; }
    }

    public static IReadOnlyList<string> ParseColors(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return DefaultColors;
        try { var values = JsonSerializer.Deserialize<string[]>(json); return values?.Length > 0 ? values : DefaultColors; } catch (JsonException) { return DefaultColors; }
    }

    public static string Render(string type, IReadOnlyList<string> labels, IReadOnlyList<ChartSeries> series, IReadOnlyList<string> colors, bool grid, bool axis)
    {
        const double width = 640, height = 280, left = 52, top = 18, right = 18, bottom = 42;
        var plotWidth = width - left - right; var plotHeight = height - top - bottom;
        if (type is "pie" or "doughnut") return RenderPie(type, labels, series, colors, width, height);
        var all = series.SelectMany(item => item.Data).ToArray(); var maximum = Math.Max(1, all.DefaultIfEmpty(1).Max()); var minimum = Math.Min(0, all.DefaultIfEmpty(0).Min()); var range = Math.Max(1, maximum - minimum);
        double X(int index, int count) => left + (count <= 1 ? plotWidth / 2 : index * plotWidth / (count - 1));
        double Y(double value) => top + (maximum - value) / range * plotHeight;
        var svg = new StringBuilder($"<svg class=\"orizon-chart__svg\" viewBox=\"0 0 {width} {height}\" role=\"img\">");
        if (grid) for (var tick = 0; tick <= 4; tick++) { var y = top + tick * plotHeight / 4; svg.Append($"<line class=\"orizon-chart__grid\" x1=\"{left}\" y1=\"{N(y)}\" x2=\"{width - right}\" y2=\"{N(y)}\" />"); }
        if (axis) { svg.Append($"<line class=\"orizon-chart__axis\" x1=\"{left}\" y1=\"{top}\" x2=\"{left}\" y2=\"{height - bottom}\"/><line class=\"orizon-chart__axis\" x1=\"{left}\" y1=\"{height - bottom}\" x2=\"{width-right}\" y2=\"{height-bottom}\"/>"); for (var i = 0; i < labels.Count; i++) svg.Append($"<text class=\"orizon-chart__label\" x=\"{N(X(i, labels.Count))}\" y=\"{height - 14}\" text-anchor=\"middle\">{E(labels[i])}</text>"); }
        if (type is "bar" or "horizontal-bar") RenderBars(svg, type, labels, series, colors, left, top, plotWidth, plotHeight, maximum); else RenderLines(svg, type, labels, series, colors, X, Y, height - bottom);
        svg.Append("</svg>"); return svg.ToString();
    }

    private static void RenderLines(StringBuilder svg, string type, IReadOnlyList<string> labels, IReadOnlyList<ChartSeries> series, IReadOnlyList<string> colors, Func<int,int,double> x, Func<double,double> y, double baseline)
    {
        for (var s = 0; s < series.Count; s++)
        {
            var item = series[s]; if (item.Data.Count == 0) continue; var color = colors[s % colors.Count]; var points = string.Join(" ", item.Data.Select((value, index) => $"{N(x(index, Math.Max(labels.Count, item.Data.Count)))},{N(y(value))}"));
            if (type == "area") svg.Append($"<polygon class=\"orizon-chart__area\" points=\"{N(x(0,item.Data.Count))},{baseline} {points} {N(x(item.Data.Count-1,item.Data.Count))},{baseline}\" fill=\"{E(color)}\" data-series=\"{s}\"/>");
            svg.Append($"<polyline class=\"orizon-chart__line\" points=\"{points}\" stroke=\"{E(color)}\" data-series=\"{s}\"/>");
            foreach (var (value,index) in item.Data.Select((value,index)=>(value,index))) svg.Append($"<circle class=\"orizon-chart__point\" cx=\"{N(x(index,Math.Max(labels.Count,item.Data.Count)))}\" cy=\"{N(y(value))}\" r=\"4\" fill=\"{E(color)}\" tabindex=\"0\" data-series=\"{s}\" data-index=\"{index}\" data-value=\"{N(value)}\"/>");
        }
    }

    private static void RenderBars(StringBuilder svg, string type, IReadOnlyList<string> labels, IReadOnlyList<ChartSeries> series, IReadOnlyList<string> colors, double left, double top, double plotWidth, double plotHeight, double maximum)
    {
        var count = Math.Max(1, labels.Count); var group = (type == "horizontal-bar" ? plotHeight : plotWidth) / count; var bar = group * .72 / Math.Max(1, series.Count);
        for (var s=0;s<series.Count;s++) for(var i=0;i<series[s].Data.Count;i++){var value=series[s].Data[i];var ratio=Math.Max(0,value/maximum);var color=E(colors[s%colors.Count]);double x,y,w,h;if(type=="horizontal-bar"){x=left;y=top+i*group+s*bar+group*.14;w=plotWidth*ratio;h=bar;}else{x=left+i*group+s*bar+group*.14;y=top+plotHeight*(1-ratio);w=bar;h=plotHeight*ratio;}svg.Append($"<rect class=\"orizon-chart__bar\" x=\"{N(x)}\" y=\"{N(y)}\" width=\"{N(w)}\" height=\"{N(h)}\" rx=\"4\" fill=\"{color}\" tabindex=\"0\" data-series=\"{s}\" data-index=\"{i}\" data-value=\"{N(value)}\"/>");}
    }

    private static string RenderPie(string type, IReadOnlyList<string> labels, IReadOnlyList<ChartSeries> series, IReadOnlyList<string> colors, double width, double height)
    {
        var values = series.FirstOrDefault()?.Data ?? []; var total = Math.Max(1, values.Sum()); const double cx=320,cy=140,r=102; var start=-Math.PI/2; var svg=new StringBuilder($"<svg class=\"orizon-chart__svg\" viewBox=\"0 0 {width} {height}\" role=\"img\">"); for(var i=0;i<values.Count;i++){var angle=values[i]/total*Math.PI*2;var end=start+angle;var x1=cx+r*Math.Cos(start);var y1=cy+r*Math.Sin(start);var x2=cx+r*Math.Cos(end);var y2=cy+r*Math.Sin(end);var large=angle>Math.PI?1:0;var path=$"M {cx} {cy} L {N(x1)} {N(y1)} A {r} {r} 0 {large} 1 {N(x2)} {N(y2)} Z";svg.Append($"<path class=\"orizon-chart__slice\" d=\"{path}\" fill=\"{E(colors[i%colors.Count])}\" tabindex=\"0\" data-series=\"0\" data-index=\"{i}\" data-value=\"{N(values[i])}\" data-label=\"{E(i<labels.Count?labels[i]:$"Item {i+1}")}\"/>");start=end;}if(type=="doughnut")svg.Append($"<circle cx=\"{cx}\" cy=\"{cy}\" r=\"58\" class=\"orizon-chart__doughnut-hole\"/>");svg.Append("</svg>");return svg.ToString();
    }

    public static string Sparkline(IReadOnlyList<double> values, string type, string color)
    {
        if(values.Count==0)return "";var max=Math.Max(1,values.Max());double x(int i)=>i*100d/Math.Max(1,values.Count-1);double y(double v)=>36-v/max*32;var points=string.Join(" ",values.Select((v,i)=>$"{N(x(i))},{N(y(v))}"));if(type=="bar")return $"<svg viewBox=\"0 0 100 40\" aria-hidden=\"true\">{string.Join("",values.Select((v,i)=>$"<rect x=\"{N(i*100d/values.Count+1)}\" y=\"{N(y(v))}\" width=\"{N(100d/values.Count-2)}\" height=\"{N(36-y(v))}\" rx=\"1\" fill=\"{E(color)}\"/>"))}</svg>";var area=type=="area"?$"<polygon points=\"0,36 {points} 100,36\" fill=\"{E(color)}\" opacity=\".16\"/>":"";return $"<svg viewBox=\"0 0 100 40\" aria-hidden=\"true\">{area}<polyline points=\"{points}\" fill=\"none\" stroke=\"{E(color)}\" stroke-width=\"2.5\" vector-effect=\"non-scaling-stroke\"/></svg>";
    }
}
