namespace Orizon.UI.Icons;

internal static class FinanceIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["wallet"] = """
                <path d="M3 7a2 2 0 0 1 2-2h14v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/>
                <path d="M16 11h5v4h-5a2 2 0 0 1 0-4z"/>
                """,

            ["bank"] = """
                <path d="M3 9h18"/>
                <path d="M5 9v9"/>
                <path d="M9 9v9"/>
                <path d="M15 9v9"/>
                <path d="M19 9v9"/>
                <path d="M2 21h20"/>
                <path d="M12 3 2 8h20z"/>
                """,

            ["credit-card"] = """
                <rect x="2" y="5" width="20" height="14" rx="2"/>
                <path d="M2 10h20"/>
                <path d="M6 15h4"/>
                """,

            ["cash"] = """
                <rect x="3" y="6" width="18" height="12" rx="2"/>
                <circle cx="12" cy="12" r="3"/>
                <path d="M6 9h.01"/>
                <path d="M18 15h.01"/>
                """,

            ["coin"] = """
                <circle cx="12" cy="12" r="8"/>
                <path d="M12 8v8"/>
                <path d="M9 10c0-1.1 1.3-2 3-2s3 .9 3 2-1.3 2-3 2-3 .9-3 2 1.3 2 3 2 3-.9 3-2"/>
                """,

            ["dollar"] = """
                <path d="M12 3v18"/>
                <path d="M16 7c0-1.7-1.8-3-4-3S8 5.3 8 7s1.8 3 4 3 4 1.3 4 3-1.8 3-4 3-4-1.3-4-3"/>
                """,

            ["percent"] = """
                <path d="M19 5 5 19"/>
                <circle cx="7" cy="7" r="2"/>
                <circle cx="17" cy="17" r="2"/>
                """,

            ["calculator"] = """
                <rect x="6" y="2" width="12" height="20" rx="2"/>
                <rect x="8" y="5" width="8" height="3"/>
                <path d="M9 11h2"/>
                <path d="M13 11h2"/>
                <path d="M9 15h2"/>
                <path d="M13 15h2"/>
                <path d="M9 19h6"/>
                """,

            ["invoice"] = """
                <path d="M6 2h12v20l-2-2-2 2-2-2-2 2-2-2-2 2z"/>
                <path d="M9 7h6"/>
                <path d="M9 11h6"/>
                <path d="M9 15h4"/>
                """,

            ["payment"] = """
                <rect x="3" y="6" width="18" height="12" rx="2"/>
                <path d="M7 12h10"/>
                <path d="M12 9v6"/>
                """,

            ["piggy-bank"] = """
                <path d="M6 12a6 6 0 1 1 12 0"/>
                <circle cx="16" cy="11" r="1"/>
                <path d="M12 6v2"/>
                <path d="M8 18v2"/>
                <path d="M16 18v2"/>
                """,

            ["chart-profit"] = """
                <path d="M4 20h16"/>
                <path d="m6 16 4-4 3 3 5-7"/>
                <path d="M18 8h2v2"/>
                """,

            ["chart-loss"] = """
                <path d="M4 20h16"/>
                <path d="m6 8 4 4 3-3 5 7"/>
                <path d="M18 16h2v2"/>
                """,

            ["wallet-open"] = """
                <path d="M3 8h18v10H3z"/>
                <path d="M3 8V6a2 2 0 0 1 2-2h11"/>
                <path d="M17 12h4"/>
                """,

            ["safe"] = """
                <rect x="4" y="4" width="16" height="16" rx="2"/>
                <circle cx="12" cy="12" r="2"/>
                <path d="M12 8v2"/>
                <path d="M12 14v2"/>
                <path d="M8 12h2"/>
                <path d="M14 12h2"/>
                """,

            ["receipt-check"] = """
                <path d="M6 2h12v20l-2-2-2 2-2-2-2 2-2-2-2 2z"/>
                <path d="m9 12 2 2 4-4"/>
                """,

            ["receipt-x"] = """
                <path d="M6 2h12v20l-2-2-2 2-2-2-2 2-2-2-2 2z"/>
                <path d="m10 10 4 4"/>
                <path d="m14 10-4 4"/>
                """,

            ["wallet-check"] = """
                <path d="M3 7a2 2 0 0 1 2-2h14v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/>
                <path d="m9 13 2 2 4-4"/>
                """,

            ["wallet-x"] = """
                <path d="M3 7a2 2 0 0 1 2-2h14v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/>
                <path d="m10 10 4 4"/>
                <path d="m14 10-4 4"/>
                """,

            ["hand-coins"] = """
                <path d="M5 14h5l3-3a2 2 0 0 1 3 3l-3 3H8l-3-3z"/>
                <circle cx="17" cy="7" r="3"/>
                """
        };
}