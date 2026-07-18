using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-notification-center")]
public sealed class OrizonNotificationCenterTagHelper : TagHelper
{
    [HtmlAttributeName("id")] public string? Id { get; set; }
    [HtmlAttributeName("title")] public string Title { get; set; } = "Notificações";
    [HtmlAttributeName("unread-count")] public int UnreadCount { get; set; }
    [HtmlAttributeName("open")] public bool Open { get; set; }
    [HtmlAttributeName("clear-label")] public string ClearLabel { get; set; } = "Limpar";
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var content = await output.GetChildContentAsync(); var id = Id ?? $"orizon-notifications-{context.UniqueId.Replace("__", "")}"; output.TagName = "aside"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("id", id); output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-notification-center", Open ? "is-open" : null)); output.Attributes.SetAttribute("data-orizon-notifications", ""); output.Attributes.SetAttribute("aria-label", Title); if (!Open) output.Attributes.SetAttribute("hidden", "hidden");
        output.Content.SetHtmlContent($"<div class=\"orizon-notification-center__backdrop\" data-notifications-close></div><div class=\"orizon-notification-center__panel\"><header><div><h2>{e.Encode(Title)}</h2><span class=\"orizon-notification-center__count\" aria-label=\"{UnreadCount} não lidas\">{UnreadCount}</span></div><button type=\"button\" data-notifications-close aria-label=\"Fechar notificações\">&times;</button></header><div class=\"orizon-notification-center__list\" role=\"list\">{content.GetContent()}</div><footer><button type=\"button\" data-notifications-read-all>Marcar todas como lidas</button><button type=\"button\" data-notifications-clear>{e.Encode(ClearLabel)}</button></footer></div>");
    }
}

[HtmlTargetElement("orizon-notification", ParentTag = "orizon-notification-center")]
public sealed class OrizonNotificationTagHelper : TagHelper
{
    [HtmlAttributeName("title")] public string Title { get; set; } = string.Empty;
    [HtmlAttributeName("time")] public string? Time { get; set; }
    [HtmlAttributeName("unread")] public bool Unread { get; set; }
    [HtmlAttributeName("href")] public string? Href { get; set; }
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var e = HtmlEncoder.Default; var content = await output.GetChildContentAsync(); output.TagName = "article"; output.TagMode = TagMode.StartTagAndEndTag; output.Attributes.SetAttribute("class", TagHelperClassBuilder.Build("orizon-notification", Unread ? "is-unread" : null)); output.Attributes.SetAttribute("role", "listitem"); output.Attributes.SetAttribute("data-notification", ""); output.Attributes.SetAttribute("data-unread", Unread.ToString().ToLowerInvariant()); var body = $"<strong>{e.Encode(Title)}</strong><p>{content.GetContent()}</p>{(Time is null ? "" : $"<time>{e.Encode(Time)}</time>")}"; output.Content.SetHtmlContent(Href is null ? body : $"<a href=\"{e.Encode(Href)}\">{body}</a><button type=\"button\" data-notification-read aria-label=\"Marcar {e.Encode(Title)} como lida\">✓</button>");
    }
}
