namespace Orizon.UI.Icons;

internal static class CommerceIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["package"] = """
                <path d="M12 2 3 7l9 5 9-5-9-5z"/>
                <path d="M3 7v10l9 5 9-5V7"/>
                <path d="M12 12v10"/>
                """,

            ["packages"] = """
                <rect x="3" y="4" width="8" height="8" rx="1"/>
                <rect x="13" y="4" width="8" height="8" rx="1"/>
                <rect x="8" y="14" width="8" height="7" rx="1"/>
                """,

            ["box"] = """
                <path d="M3 7 12 2l9 5v10l-9 5-9-5z"/>
                <path d="M12 2v20"/>
                """,

            ["boxes"] = """
                <rect x="3" y="3" width="8" height="8"/>
                <rect x="13" y="3" width="8" height="8"/>
                <rect x="8" y="13" width="8" height="8"/>
                """,

            ["warehouse"] = """
                <path d="M3 10 12 3l9 7"/>
                <path d="M5 10v11h14V10"/>
                <path d="M9 21v-6h6v6"/>
                """,

            ["truck"] = """
                <path d="M1 6h13v9H1z"/>
                <path d="M14 9h5l3 3v3h-8"/>
                <circle cx="6" cy="18" r="2"/>
                <circle cx="18" cy="18" r="2"/>
                """,

            ["delivery"] = """
                <path d="M2 7h12v8H2z"/>
                <path d="M14 9h5l3 3v3h-8"/>
                <circle cx="6" cy="18" r="2"/>
                <circle cx="18" cy="18" r="2"/>
                <path d="M6 18h12"/>
                """,

            ["shopping-cart"] = """
                <circle cx="9" cy="20" r="1.5"/>
                <circle cx="18" cy="20" r="1.5"/>
                <path d="M2 3h3l2.5 11h11l2-8H6"/>
                """,

            ["basket"] = """
                <path d="M5 10h14l-2 10H7z"/>
                <path d="M9 10 12 4l3 6"/>
                """,

            ["barcode"] = """
                <path d="M4 5v14"/>
                <path d="M7 5v14"/>
                <path d="M10 5v14"/>
                <path d="M13 5v14"/>
                <path d="M16 5v14"/>
                <path d="M19 5v14"/>
                """,

            ["qr-code"] = """
                <rect x="3" y="3" width="6" height="6"/>
                <rect x="15" y="3" width="6" height="6"/>
                <rect x="3" y="15" width="6" height="6"/>
                <path d="M15 15h2v2h-2z"/>
                <path d="M19 15h2v6h-6"/>
                """,

            ["tag"] = """
                <path d="M20 10 10 20 3 13V4h9z"/>
                <circle cx="8" cy="8" r="1"/>
                """,

            ["tags"] = """
                <path d="M18 8 10 16 4 10V4h6z"/>
                <path d="M21 11 13 19"/>
                """,

            ["receipt"] = """
                <path d="M6 2h12v20l-2-2-2 2-2-2-2 2-2-2-2 2z"/>
                <path d="M9 7h6"/>
                <path d="M9 11h6"/>
                <path d="M9 15h4"/>
                """,

            ["clipboard"] = """
                <rect x="6" y="4" width="12" height="18" rx="2"/>
                <rect x="9" y="2" width="6" height="4" rx="1"/>
                """,

            ["inventory"] = """
                <path d="M4 6h16"/>
                <path d="M4 12h16"/>
                <path d="M4 18h16"/>
                <circle cx="7" cy="6" r="1"/>
                <circle cx="7" cy="12" r="1"/>
                <circle cx="7" cy="18" r="1"/>
                """,

            ["pallet"] = """
                <rect x="5" y="5" width="14" height="10"/>
                <path d="M5 17h14"/>
                <path d="M7 20h2"/>
                <path d="M15 20h2"/>
                """,

            ["factory"] = """
                <path d="M3 21V9l6 3V9l6 3V3l6 4v14"/>
                <path d="M3 21h18"/>
                """,

            ["store"] = """
                <path d="M4 9h16"/>
                <path d="M5 9V5h14v4"/>
                <path d="M6 9v12"/>
                <path d="M18 9v12"/>
                <path d="M10 21v-6h4v6"/>
                """,

            ["scale"] = """
                <path d="M12 3v18"/>
                <path d="M5 7h14"/>
                <path d="m5 7-3 5h6z"/>
                <path d="m19 7-3 5h6z"/>
                """
        };
}