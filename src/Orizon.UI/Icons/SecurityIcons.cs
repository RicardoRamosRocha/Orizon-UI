namespace Orizon.UI.Icons;

internal static class SecurityIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["shield"] = """
                <path d="M12 3 5 6v6c0 5 3.5 8.5 7 9 3.5-.5 7-4 7-9V6l-7-3z"/>
                """,

            ["shield-check"] = """
                <path d="M12 3 5 6v6c0 5 3.5 8.5 7 9 3.5-.5 7-4 7-9V6l-7-3z"/>
                <path d="m9 12 2 2 4-4"/>
                """,

            ["shield-x"] = """
                <path d="M12 3 5 6v6c0 5 3.5 8.5 7 9 3.5-.5 7-4 7-9V6l-7-3z"/>
                <path d="m10 10 4 4"/>
                <path d="m14 10-4 4"/>
                """,

            ["lock"] = """
                <rect x="5" y="11" width="14" height="10" rx="2"/>
                <path d="M8 11V8a4 4 0 1 1 8 0v3"/>
                """,

            ["unlock"] = """
                <rect x="5" y="11" width="14" height="10" rx="2"/>
                <path d="M8 11V8a4 4 0 0 1 8 0"/>
                """,

            ["key"] = """
                <circle cx="8" cy="12" r="3"/>
                <path d="M11 12h10"/>
                <path d="M18 12v3"/>
                <path d="M15 12v2"/>
                """,

            ["fingerprint"] = """
                <path d="M12 4a5 5 0 0 1 5 5"/>
                <path d="M7 9a5 5 0 0 1 10 0"/>
                <path d="M12 4a8 8 0 0 0-8 8"/>
                <path d="M17 12a5 5 0 0 1-10 0"/>
                <path d="M12 20a8 8 0 0 0 8-8"/>
                """,

            ["user-lock"] = """
                <circle cx="10" cy="8" r="4"/>
                <path d="M2 20a8 8 0 0 1 16 0"/>
                <rect x="17" y="10" width="5" height="4" rx="1"/>
                <path d="M18 10V8a2 2 0 0 1 4 0v2"/>
                """,

            ["user-shield"] = """
                <circle cx="8" cy="8" r="3"/>
                <path d="M2 20a6 6 0 0 1 12 0"/>
                <path d="M18 6l4 2v4c0 3-2 5-4 6-2-1-4-3-4-6V8z"/>
                """,

            ["password"] = """
                <circle cx="8" cy="10" r="1"/>
                <circle cx="12" cy="10" r="1"/>
                <circle cx="16" cy="10" r="1"/>
                <path d="M5 18h14"/>
                """,

            ["login"] = """
                <path d="M10 17l5-5-5-5"/>
                <path d="M15 12H3"/>
                <path d="M18 4h3v16h-3"/>
                """,

            ["logout"] = """
                <path d="M14 17l-5-5 5-5"/>
                <path d="M9 12h12"/>
                <path d="M3 4h3v16H3"/>
                """,

            ["permission"] = """
                <path d="M12 3 5 6v6c0 5 3.5 8.5 7 9 3.5-.5 7-4 7-9V6l-7-3z"/>
                <path d="M12 9v6"/>
                <path d="M9 12h6"/>
                """,

            ["audit"] = """
                <path d="M6 2h10l4 4v16H6z"/>
                <path d="M16 2v4h4"/>
                <circle cx="12" cy="14" r="3"/>
                <path d="m15 17 3 3"/>
                """,

            ["verified"] = """
                <circle cx="12" cy="12" r="9"/>
                <path d="m8 12 3 3 5-6"/>
                """,

            ["warning"] = """
                <path d="M12 3 2 21h20L12 3z"/>
                <path d="M12 9v5"/>
                <circle cx="12" cy="17" r="1"/>
                """,

            ["error"] = """
                <circle cx="12" cy="12" r="9"/>
                <path d="m9 9 6 6"/>
                <path d="m15 9-6 6"/>
                """,

            ["info"] = """
                <circle cx="12" cy="12" r="9"/>
                <path d="M12 10v6"/>
                <circle cx="12" cy="7" r="1"/>
                """,

            ["privacy"] = """
                <path d="M12 3 5 6v6c0 5 3.5 8.5 7 9 3.5-.5 7-4 7-9V6l-7-3z"/>
                <path d="M12 9v4"/>
                <circle cx="12" cy="16" r="1"/>
                """,

            ["bug"] = """
                <path d="M8 8h8"/>
                <rect x="8" y="8" width="8" height="10" rx="4"/>
                <path d="M4 10h4"/>
                <path d="M16 10h4"/>
                <path d="M5 16h4"/>
                <path d="M15 16h4"/>
                <path d="M10 4l2 4 2-4"/>
                """
        };
}