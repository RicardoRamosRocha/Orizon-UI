using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orizon.UI.TagHelpers;

[HtmlTargetElement("orizon-text-box")] public sealed class OrizonTextBoxTagHelper : AdvancedFormFieldTagHelper { protected override string InputType => "text"; }
[HtmlTargetElement("orizon-email")] public sealed class OrizonEmailTagHelper : AdvancedFormFieldTagHelper { protected override string InputType => "email"; protected override string AdditionalAttributes() => " autocomplete=\"email\""; }

[HtmlTargetElement("orizon-password")]
public sealed class OrizonPasswordTagHelper : AdvancedFormFieldTagHelper
{
    [HtmlAttributeName("show-toggle")] public bool ShowToggle { get; set; } = true;
    [HtmlAttributeName("strength")] public bool Strength { get; set; }
    [HtmlAttributeName("confirmation-for")] public string? ConfirmationFor { get; set; }
    protected override string InputType => "password";
    protected override string AdditionalAttributes() => $" autocomplete=\"new-password\" data-password-strength=\"{Strength.ToString().ToLowerInvariant()}\"{Attr("data-confirmation-for", ConfirmationFor)}";
    public override void Process(TagHelperContext context, TagHelperOutput output) { Clearable = ShowToggle; base.Process(context, output); if (ShowToggle) output.Content.SetHtmlContent(output.Content.GetContent().Replace("data-field-clear aria-label=\"Limpar campo\">&times;", "data-password-toggle aria-label=\"Mostrar senha\">◉")); if (Strength) output.PostContent.SetHtmlContent("<div class=\"orizon-password-strength\" data-password-meter><span></span></div>"); }
}

[HtmlTargetElement("orizon-number")]
public class OrizonNumberTagHelper : AdvancedFormFieldTagHelper
{
    [HtmlAttributeName("min")] public decimal? Min { get; set; }
    [HtmlAttributeName("max")] public decimal? Max { get; set; }
    [HtmlAttributeName("step")] public decimal? Step { get; set; }
    protected override string InputType => "number";
    protected override string AdditionalAttributes() => $"{Attr("min", Min?.ToString(System.Globalization.CultureInfo.InvariantCulture))}{Attr("max", Max?.ToString(System.Globalization.CultureInfo.InvariantCulture))}{Attr("step", Step?.ToString(System.Globalization.CultureInfo.InvariantCulture))}";
}

[HtmlTargetElement("orizon-currency")]
public sealed class OrizonCurrencyTagHelper : AdvancedFormFieldTagHelper
{
    [HtmlAttributeName("symbol")] public string Symbol { get; set; } = "R$";
    [HtmlAttributeName("decimal-separator")] public string DecimalSeparator { get; set; } = ",";
    [HtmlAttributeName("thousand-separator")] public string ThousandSeparator { get; set; } = ".";
    protected override string InputType => "text"; protected override string Kind => "currency";
    public override void Process(TagHelperContext context, TagHelperOutput output) { Prefix ??= Symbol; base.Process(context, output); }
    protected override string AdditionalAttributes() => $" inputmode=\"decimal\" data-mask=\"currency\" data-decimal=\"{DecimalSeparator}\" data-thousand=\"{ThousandSeparator}\"";
}

public abstract class MaskedFieldTagHelper : AdvancedFormFieldTagHelper { protected override string InputType => "tel"; protected abstract string Mask { get; } protected override string Kind => Mask; protected override string AdditionalAttributes() => $" inputmode=\"numeric\" data-mask=\"{Mask}\" autocomplete=\"off\""; }
[HtmlTargetElement("orizon-phone")] public sealed class OrizonPhoneTagHelper : MaskedFieldTagHelper { protected override string Mask => "phone"; }
[HtmlTargetElement("orizon-cpf")] public sealed class OrizonCpfTagHelper : MaskedFieldTagHelper { protected override string Mask => "cpf"; protected override string InputType => "text"; }
[HtmlTargetElement("orizon-cnpj")] public sealed class OrizonCnpjTagHelper : MaskedFieldTagHelper { protected override string Mask => "cnpj"; protected override string InputType => "text"; }
[HtmlTargetElement("orizon-cep")] public sealed class OrizonCepTagHelper : MaskedFieldTagHelper { protected override string Mask => "cep"; protected override string InputType => "text"; protected override string AdditionalAttributes() => base.AdditionalAttributes() + " data-viacep-ready=\"true\""; }

public abstract class DateFieldTagHelper : AdvancedFormFieldTagHelper
{
    [HtmlAttributeName("min")] public string? Min { get; set; }
    [HtmlAttributeName("max")] public string? Max { get; set; }
    [HtmlAttributeName("clear")] public bool Clear { get; set; } = true;
    protected override string AdditionalAttributes() => $"{Attr("min", Min)}{Attr("max", Max)} data-date-picker=\"{Kind}\"";
    public override void Process(TagHelperContext context, TagHelperOutput output) { Clearable = Clear; base.Process(context, output); }
}
[HtmlTargetElement("orizon-date-picker")] public sealed class OrizonDatePickerTagHelper : DateFieldTagHelper { protected override string InputType => "date"; protected override string Kind => "date"; }
[HtmlTargetElement("orizon-time-picker")] public sealed class OrizonTimePickerTagHelper : DateFieldTagHelper { [HtmlAttributeName("step")] public int Step { get; set; } = 60; protected override string InputType => "time"; protected override string Kind => "time"; protected override string AdditionalAttributes() => base.AdditionalAttributes() + $" step=\"{Step}\""; }
[HtmlTargetElement("orizon-date-time-picker")] public sealed class OrizonDateTimePickerTagHelper : DateFieldTagHelper { protected override string InputType => "datetime-local"; protected override string Kind => "datetime"; }
