namespace Orizon.UI.Icons;

public static class OrizonIconRegistry
{
    private static readonly IReadOnlyDictionary<string, string> Icons =
        NavigationIcons.All
            .Concat(ActionIcons.All)
            .Concat(BusinessIcons.All)
            .Concat(CommerceIcons.All)
            .Concat(FinanceIcons.All)
            .Concat(ChartIcons.All)
            .Concat(SecurityIcons.All)
            .Concat(SystemIcons.All)
            .ToDictionary(
                x => x.Key,
                x => x.Value,
                StringComparer.OrdinalIgnoreCase);

    public static bool TryGet(string name, out string svgContent)
    {
        return Icons.TryGetValue(name, out svgContent!);
    }
}