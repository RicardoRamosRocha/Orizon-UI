namespace Orizon.UI.Icons;

internal static class ChartIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["chart-bar"] = """
                <path d="M4 20h16"/>
                <rect x="6" y="11" width="2" height="7"/>
                <rect x="11" y="8" width="2" height="10"/>
                <rect x="16" y="5" width="2" height="13"/>
                """,

            ["chart-column"] = """
                <path d="M4 20h16"/>
                <rect x="5" y="12" width="3" height="6"/>
                <rect x="10" y="8" width="3" height="10"/>
                <rect x="15" y="4" width="3" height="14"/>
                """,

            ["chart-line"] = """
                <path d="M4 20h16"/>
                <path d="m5 15 4-4 4 2 6-7"/>
                <circle cx="5" cy="15" r="1"/>
                <circle cx="9" cy="11" r="1"/>
                <circle cx="13" cy="13" r="1"/>
                <circle cx="19" cy="6" r="1"/>
                """,

            ["chart-area"] = """
                <path d="M4 20h16"/>
                <path d="M5 15 9 10 13 13 19 6V20H5Z"/>
                """,

            ["chart-pie"] = """
                <path d="M12 3v9h9"/>
                <path d="M21 12A9 9 0 1 1 12 3"/>
                """,

            ["chart-donut"] = """
                <circle cx="12" cy="12" r="8"/>
                <circle cx="12" cy="12" r="3"/>
                <path d="M12 4a8 8 0 0 1 8 8"/>
                """,

            ["chart-scatter"] = """
                <path d="M4 20h16"/>
                <circle cx="7" cy="15" r="1.2"/>
                <circle cx="11" cy="11" r="1.2"/>
                <circle cx="15" cy="13" r="1.2"/>
                <circle cx="18" cy="7" r="1.2"/>
                """,

            ["chart-radar"] = """
                <polygon points="12,4 18,8 16,18 8,18 6,8"/>
                <polygon points="12,7 15,9 14,15 10,15 9,9"/>
                """,

            ["analytics"] = """
                <path d="M4 20h16"/>
                <path d="m6 16 3-5 4 3 5-8"/>
                <circle cx="6" cy="16" r="1"/>
                <circle cx="9" cy="11" r="1"/>
                <circle cx="13" cy="14" r="1"/>
                <circle cx="18" cy="6" r="1"/>
                """,

            ["dashboard"] = """
                <rect x="3" y="3" width="8" height="8" rx="2"/>
                <rect x="13" y="3" width="8" height="5" rx="2"/>
                <rect x="13" y="10" width="8" height="11" rx="2"/>
                <rect x="3" y="13" width="8" height="8" rx="2"/>
                """,

            ["kpi"] = """
                <circle cx="12" cy="12" r="8"/>
                <path d="M12 12 17 8"/>
                <circle cx="12" cy="12" r="1"/>
                """,

            ["target"] = """
                <circle cx="12" cy="12" r="8"/>
                <circle cx="12" cy="12" r="5"/>
                <circle cx="12" cy="12" r="2"/>
                """,

            ["trending-up"] = """
                <path d="M4 20h16"/>
                <path d="m5 15 5-5 4 3 5-8"/>
                <path d="M19 5v5h-5"/>
                """,

            ["trending-down"] = """
                <path d="M4 20h16"/>
                <path d="m5 7 5 5 4-3 5 8"/>
                <path d="M19 19v-5h-5"/>
                """,

            ["activity"] = """
                <path d="M3 12h4l2-5 4 10 2-5h6"/>
                """,

            ["growth"] = """
                <path d="M4 20h16"/>
                <path d="m5 16 3-3 3 2 4-6 4-2"/>
                """,

            ["report"] = """
                <path d="M6 2h10l4 4v16H6z"/>
                <path d="M16 2v4h4"/>
                <path d="M9 11h6"/>
                <path d="M9 15h4"/>
                """,

            ["presentation"] = """
                <rect x="4" y="3" width="16" height="12" rx="2"/>
                <path d="M12 15v6"/>
                <path d="M8 21h8"/>
                """,

            ["timeline"] = """
                <path d="M5 12h14"/>
                <circle cx="7" cy="12" r="1.5"/>
                <circle cx="12" cy="12" r="1.5"/>
                <circle cx="17" cy="12" r="1.5"/>
                """,

            ["speedometer"] = """
                <path d="M5 17a7 7 0 1 1 14 0"/>
                <path d="M12 17l4-5"/>
                <circle cx="12" cy="17" r="1"/>
                """
        };
}