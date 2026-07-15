namespace Orizon.UI.Icons;

public static class OrizonIconRegistry
{
    private static readonly IReadOnlyDictionary<string, string> Icons =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["house"] = """
                <path d="M3 11.5 12 4l9 7.5" />
                <path d="M5 10v10h14V10" />
                <path d="M9 20v-6h6v6" />
                """,

            ["menu"] = """
                <path d="M4 7h16" />
                <path d="M4 12h16" />
                <path d="M4 17h16" />
                """,

            ["bell"] = """
                <path d="M18 8a6 6 0 0 0-12 0c0 7-3 7-3 9h18c0-2-3-2-3-9" />
                <path d="M10 21h4" />
                """,

            ["sun"] = """
                <circle cx="12" cy="12" r="4" />
                <path d="M12 2v2" />
                <path d="M12 20v2" />
                <path d="m4.93 4.93 1.42 1.42" />
                <path d="m17.66 17.66 1.41 1.41" />
                <path d="M2 12h2" />
                <path d="M20 12h2" />
                <path d="m6.35 17.66-1.42 1.41" />
                <path d="m19.07 4.93-1.41 1.42" />
                """,

            ["moon"] = """
                <path d="M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79Z" />
                """,

            ["users"] = """
                <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" />
                <circle cx="9" cy="7" r="4" />
                <path d="M22 21v-2a4 4 0 0 0-3-3.87" />
                <path d="M16 3.13a4 4 0 0 1 0 7.75" />
                """,

            ["building"] = """
                <path d="M3 21h18" />
                <path d="M6 21V4h12v17" />
                <path d="M9 8h2" />
                <path d="M13 8h2" />
                <path d="M9 12h2" />
                <path d="M13 12h2" />
                <path d="M9 16h2" />
                <path d="M13 16h2" />
                """,

            ["gear"] = """
                <circle cx="12" cy="12" r="3" />
                <path d="M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06-2.83 2.83-.06-.06A1.65 1.65 0 0 0 15 19.4a1.65 1.65 0 0 0-1 .6 1.65 1.65 0 0 0-.4 1.08V21h-4v-.08A1.65 1.65 0 0 0 8.6 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06-2.83-2.83.06-.06A1.65 1.65 0 0 0 4.6 15a1.65 1.65 0 0 0-.6-1 1.65 1.65 0 0 0-1.08-.4H3v-4h.08A1.65 1.65 0 0 0 4.6 8.6a1.65 1.65 0 0 0-.33-1.82l-.06-.06 2.83-2.83.06.06A1.65 1.65 0 0 0 9 4.6a1.65 1.65 0 0 0 1-.6 1.65 1.65 0 0 0 .4-1.08V3h4v.08A1.65 1.65 0 0 0 15.4 4a1.65 1.65 0 0 0 1.82-.33l.06-.06 2.83 2.83-.06.06A1.65 1.65 0 0 0 19.4 9c.15.36.37.69.66.96.29.27.67.42 1.06.44H21v4h-.08a1.65 1.65 0 0 0-1.52.6Z" />
                """,

            ["chevron-down"] = """
                <path d="m6 9 6 6 6-6" />
                """,

            ["chevron-left"] = """
                <path d="m15 18-6-6 6-6" />
                """
        };

    public static bool TryGet(string name, out string svgContent)
    {
        return Icons.TryGetValue(name, out svgContent!);
    }
}