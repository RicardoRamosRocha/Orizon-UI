namespace Orizon.UI.Icons;

internal static class SystemIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["gear"] = """
                <circle cx="12" cy="12" r="3"/>
                <path d="M19.4 15a1.6 1.6 0 0 0 .3 1.8l.1.1-2.8 2.8-.1-.1a1.6 1.6 0 0 0-1.8-.3 1.6 1.6 0 0 0-1 .6 1.6 1.6 0 0 0-.4 1.1V21h-4v-.1a1.6 1.6 0 0 0-.4-1.1 1.6 1.6 0 0 0-1-.6 1.6 1.6 0 0 0-1.8.3l-.1.1-2.8-2.8.1-.1a1.6 1.6 0 0 0 .3-1.8 1.6 1.6 0 0 0-.6-1A1.6 1.6 0 0 0 3 13.6V9.6h.1A1.6 1.6 0 0 0 4.7 8a1.6 1.6 0 0 0-.3-1.8l-.1-.1 2.8-2.8.1.1a1.6 1.6 0 0 0 1.8.3 1.6 1.6 0 0 0 1-.6A1.6 1.6 0 0 0 10.4 2H14v.1a1.6 1.6 0 0 0 .4 1.1 1.6 1.6 0 0 0 1 .6 1.6 1.6 0 0 0 1.8-.3l.1-.1 2.8 2.8-.1.1a1.6 1.6 0 0 0-.3 1.8 1.6 1.6 0 0 0 .6 1 1.6 1.6 0 0 0 1.1.4H21v4h-.1a1.6 1.6 0 0 0-1.5.6Z"/>
                """,

            ["settings"] = """
                <circle cx="12" cy="12" r="3"/>
                <path d="M19.4 15a1.6 1.6 0 0 0 .3 1.8l.1.1-2.8 2.8-.1-.1a1.6 1.6 0 0 0-1.8-.3M4.6 15A1.6 1.6 0 0 0 3 13.6V9.6A1.6 1.6 0 0 0 4.6 8"/>
                """,

            ["server"] = """
                <rect x="4" y="4" width="16" height="6" rx="1"/>
                <rect x="4" y="14" width="16" height="6" rx="1"/>
                <circle cx="7" cy="7" r="1"/>
                <circle cx="7" cy="17" r="1"/>
                <path d="M11 7h7"/>
                <path d="M11 17h7"/>
                """,

            ["database"] = """
                <ellipse cx="12" cy="5" rx="8" ry="3"/>
                <path d="M4 5v10c0 1.7 3.6 3 8 3s8-1.3 8-3V5"/>
                <path d="M4 10c0 1.7 3.6 3 8 3s8-1.3 8-3"/>
                """,

            ["cloud"] = """
                <path d="M7 18h10a4 4 0 0 0 0-8 6 6 0 0 0-11-2A4 4 0 0 0 7 18"/>
                """,

            ["cloud-upload"] = """
                <path d="M7 18h10a4 4 0 0 0 0-8 6 6 0 0 0-11-2A4 4 0 0 0 7 18"/>
                <path d="M12 15V8"/>
                <path d="m9 11 3-3 3 3"/>
                """,

            ["cloud-download"] = """
                <path d="M7 18h10a4 4 0 0 0 0-8 6 6 0 0 0-11-2A4 4 0 0 0 7 18"/>
                <path d="M12 8v7"/>
                <path d="m9 12 3 3 3-3"/>
                """,

            ["hard-drive"] = """
                <rect x="3" y="6" width="18" height="12" rx="2"/>
                <circle cx="17" cy="12" r="1"/>
                <path d="M7 15h5"/>
                """,

            ["cpu"] = """
                <rect x="7" y="7" width="10" height="10" rx="1"/>
                <path d="M9 1v4"/>
                <path d="M15 1v4"/>
                <path d="M9 19v4"/>
                <path d="M15 19v4"/>
                <path d="M1 9h4"/>
                <path d="M1 15h4"/>
                <path d="M19 9h4"/>
                <path d="M19 15h4"/>
                """,

            ["monitor"] = """
                <rect x="3" y="4" width="18" height="12" rx="2"/>
                <path d="M8 20h8"/>
                <path d="M12 16v4"/>
                """,

            ["smartphone"] = """
                <rect x="7" y="2" width="10" height="20" rx="2"/>
                <circle cx="12" cy="18" r="1"/>
                """,

            ["tablet"] = """
                <rect x="5" y="2" width="14" height="20" rx="2"/>
                <circle cx="12" cy="18" r="1"/>
                """,

            ["printer"] = """
                <rect x="6" y="3" width="12" height="6"/>
                <rect x="4" y="9" width="16" height="8"/>
                <rect x="6" y="15" width="12" height="6"/>
                """,

            ["wifi"] = """
                <path d="M2 8a16 16 0 0 1 20 0"/>
                <path d="M5 12a11 11 0 0 1 14 0"/>
                <path d="M8 16a6 6 0 0 1 8 0"/>
                <circle cx="12" cy="20" r="1"/>
                """,

            ["globe"] = """
                <circle cx="12" cy="12" r="9"/>
                <path d="M3 12h18"/>
                <path d="M12 3a15 15 0 0 1 0 18"/>
                <path d="M12 3a15 15 0 0 0 0 18"/>
                """,

            ["terminal"] = """
                <path d="m5 7 5 5-5 5"/>
                <path d="M12 17h7"/>
                """,

            ["code"] = """
                <path d="m9 18-5-6 5-6"/>
                <path d="m15 6 5 6-5 6"/>
                """,

            ["folder"] = """
                <path d="M3 6h6l2 2h10v10a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/>
                """,

            ["folder-open"] = """
                <path d="M3 8h6l2 2h10l-2 8H5z"/>
                """,

            ["folder-plus"] = """
                <path d="M3 6h6l2 2h10v10a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/>
                <path d="M12 11v6"/>
                <path d="M9 14h6"/>
                """
        };
}