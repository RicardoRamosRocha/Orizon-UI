namespace Orizon.UI.Icons;

internal static class NavigationIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["house"] = """
                <path d="M3 11.5L12 4l9 7.5"/>
                <path d="M5 10v10h14V10"/>
                <path d="M9 20v-6h6v6"/>
                """,

            ["home"] = """
                <path d="M3 11.5L12 4l9 7.5"/>
                <path d="M5 10v10h14V10"/>
                <path d="M9 20v-6h6v6"/>
                """,

            ["dashboard"] = """
                <rect x="3" y="3" width="8" height="8" rx="2"/>
                <rect x="13" y="3" width="8" height="5" rx="2"/>
                <rect x="13" y="10" width="8" height="11" rx="2"/>
                <rect x="3" y="13" width="8" height="8" rx="2"/>
                """,

            ["menu"] = """
                <path d="M4 7h16"/>
                <path d="M4 12h16"/>
                <path d="M4 17h16"/>
                """,

            ["search"] = """
                <circle cx="11" cy="11" r="7"/>
                <path d="m20 20-3.5-3.5"/>
                """,

            ["filter"] = """
                <path d="M4 5h16"/>
                <path d="M7 12h10"/>
                <path d="M10 19h4"/>
                """,

            ["download"] = """
                <path d="M12 3v12"/>
                <path d="m7 10 5 5 5-5"/>
                <path d="M4 20h16"/>
                """,

            ["upload"] = """
                <path d="M12 21V9"/>
                <path d="m17 14-5-5-5 5"/>
                <path d="M4 4h16"/>
                """,

            ["refresh"] = """
                <path d="M20 4v6h-6"/>
                <path d="M4 20v-6h6"/>
                <path d="M20 10a8 8 0 0 0-14-4"/>
                <path d="M4 14a8 8 0 0 0 14 4"/>
                """,

            ["sync"] = """
                <path d="M3 12a9 9 0 0 1 15-6"/>
                <path d="M18 3v6h-6"/>
                <path d="M21 12a9 9 0 0 1-15 6"/>
                <path d="M6 21v-6h6"/>
                """,

            ["chevron-up"] = """
                <path d="m6 15 6-6 6 6"/>
                """,

            ["chevron-down"] = """
                <path d="m6 9 6 6 6-6"/>
                """,

            ["chevron-left"] = """
                <path d="m15 18-6-6 6-6"/>
                """,

            ["chevron-right"] = """
                <path d="m9 18 6-6-6-6"/>
                """,

            ["arrow-left"] = """
                <path d="M19 12H5"/>
                <path d="m12 19-7-7 7-7"/>
                """,

            ["arrow-right"] = """
                <path d="M5 12h14"/>
                <path d="m12 5 7 7-7 7"/>
                """,

            ["arrow-up"] = """
                <path d="M12 19V5"/>
                <path d="m5 12 7-7 7 7"/>
                """,

            ["arrow-down"] = """
                <path d="M12 5v14"/>
                <path d="m19 12-7 7-7-7"/>
                """,

            ["more-horizontal"] = """
                <circle cx="6" cy="12" r="1.5"/>
                <circle cx="12" cy="12" r="1.5"/>
                <circle cx="18" cy="12" r="1.5"/>
                """,

            ["more-vertical"] = """
                <circle cx="12" cy="6" r="1.5"/>
                <circle cx="12" cy="12" r="1.5"/>
                <circle cx="12" cy="18" r="1.5"/>
                """
        };
}