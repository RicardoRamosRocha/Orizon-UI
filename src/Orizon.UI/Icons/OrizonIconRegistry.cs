namespace Orizon.UI.Icons;

public static class OrizonIconRegistry
{
    private static readonly IReadOnlyDictionary<string, string> Icons = CreateRegistry();

    private static IReadOnlyDictionary<string, string> CreateRegistry()
    {
        var allIcons =
            NavigationIcons.All
                .Concat(ActionIcons.All)
                .Concat(BusinessIcons.All)
                .Concat(CommerceIcons.All)
                .Concat(FinanceIcons.All)
                .Concat(ChartIcons.All)
                .Concat(SecurityIcons.All)
                .Concat(SystemIcons.All)
                .ToList();

        var duplicates = allIcons
            .GroupBy(x => x.Key, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .OrderBy(x => x)
            .ToList();

        if (duplicates.Any())
        {
            throw new InvalidOperationException(
                $"Duplicate icon names found: {string.Join(", ", duplicates)}");
        }

        return allIcons.ToDictionary(
            x => x.Key,
            x => x.Value,
            StringComparer.OrdinalIgnoreCase);
    }

    public static bool TryGet(string name, out string svgContent)
    {
        return Icons.TryGetValue(name, out svgContent!);
    }
}