namespace Orizon.UI.Icons;

internal static class BusinessIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["user"] = """
                <circle cx="12" cy="8" r="4"/>
                <path d="M4 20a8 8 0 0 1 16 0"/>
                """,

            ["user-plus"] = """
                <circle cx="10" cy="8" r="4"/>
                <path d="M2 20a8 8 0 0 1 16 0"/>
                <path d="M20 8v6"/>
                <path d="M17 11h6"/>
                """,

            ["user-minus"] = """
                <circle cx="10" cy="8" r="4"/>
                <path d="M2 20a8 8 0 0 1 16 0"/>
                <path d="M17 11h6"/>
                """,

            ["user-check"] = """
                <circle cx="10" cy="8" r="4"/>
                <path d="M2 20a8 8 0 0 1 16 0"/>
                <path d="m18 10 2 2 4-4"/>
                """,

            ["users"] = """
                <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"/>
                <circle cx="9" cy="7" r="4"/>
                <path d="M22 21v-2a4 4 0 0 0-3-3.87"/>
                <path d="M16 3.13a4 4 0 0 1 0 7.75"/>
                """,

            ["team"] = """
                <circle cx="8" cy="8" r="3"/>
                <circle cx="16" cy="8" r="3"/>
                <path d="M2 20a6 6 0 0 1 12 0"/>
                <path d="M10 20a6 6 0 0 1 12 0"/>
                """,

            ["building"] = """
                <path d="M3 21h18"/>
                <path d="M6 21V4h12v17"/>
                <path d="M9 8h2"/>
                <path d="M13 8h2"/>
                <path d="M9 12h2"/>
                <path d="M13 12h2"/>
                <path d="M9 16h2"/>
                <path d="M13 16h2"/>
                """,

            ["office"] = """
                <rect x="4" y="3" width="16" height="18" rx="2"/>
                <path d="M8 7h2"/>
                <path d="M14 7h2"/>
                <path d="M8 11h2"/>
                <path d="M14 11h2"/>
                <path d="M8 15h2"/>
                <path d="M14 15h2"/>
                """,

            ["company"] = """
                <path d="M5 21V5h14v16"/>
                <path d="M9 9h2"/>
                <path d="M13 9h2"/>
                <path d="M9 13h2"/>
                <path d="M13 13h2"/>
                <path d="M10 21v-4h4v4"/>
                """,

            ["contact"] = """
                <rect x="4" y="4" width="16" height="16" rx="2"/>
                <circle cx="12" cy="10" r="3"/>
                <path d="M7 17a5 5 0 0 1 10 0"/>
                """,

            ["id-card"] = """
                <rect x="3" y="6" width="18" height="12" rx="2"/>
                <circle cx="8" cy="12" r="2"/>
                <path d="M6 16a3 3 0 0 1 4 0"/>
                <path d="M13 10h5"/>
                <path d="M13 14h5"/>
                """,

            ["badge"] = """
                <circle cx="12" cy="8" r="4"/>
                <path d="M9 12l-2 8 5-3 5 3-2-8"/>
                """,

            ["phone"] = """
                <path d="M6 2h4l2 5-2 2a16 16 0 0 0 5 5l2-2 5 2v4a2 2 0 0 1-2 2C10 20 4 14 4 6a2 2 0 0 1 2-2"/>
                """,

            ["mail"] = """
                <rect x="3" y="5" width="18" height="14" rx="2"/>
                <path d="m3 7 9 6 9-6"/>
                """,

            ["calendar"] = """
                <rect x="3" y="5" width="18" height="16" rx="2"/>
                <path d="M8 3v4"/>
                <path d="M16 3v4"/>
                <path d="M3 10h18"/>
                """,

            ["clock"] = """
                <circle cx="12" cy="12" r="9"/>
                <path d="M12 7v6"/>
                <path d="M12 13l4 2"/>
                """,

            ["history"] = """
                <path d="M3 12a9 9 0 1 0 3-6"/>
                <path d="M3 4v5h5"/>
                <path d="M12 8v5l3 2"/>
                """,

            ["map-pin"] = """
                <path d="M12 21s6-5.5 6-11a6 6 0 1 0-12 0c0 5.5 6 11 6 11z"/>
                <circle cx="12" cy="10" r="2"/>
                """
        };
}