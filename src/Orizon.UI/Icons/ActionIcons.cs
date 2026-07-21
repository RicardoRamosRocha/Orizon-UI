namespace Orizon.UI.Icons;

internal static class ActionIcons
{
    public static IReadOnlyDictionary<string, string> All =>
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["plus"] = """
                <path d="M12 5v14"/>
                <path d="M5 12h14"/>
                """,

            ["minus"] = """
                <path d="M5 12h14"/>
                """,

            ["x"] = """
                <path d="m6 6 12 12"/>
                <path d="m18 6-12 12"/>
                """,

            ["check"] = """
                <path d="m5 13 4 4L19 7"/>
                """,

            ["edit"] = """
                <path d="M4 20h4l10-10-4-4L4 16v4z"/>
                <path d="m12 6 4 4"/>
                """,

            ["trash"] = """
                <path d="M4 7h16"/>
                <path d="M10 11v6"/>
                <path d="M14 11v6"/>
                <path d="M6 7l1 13h10l1-13"/>
                <path d="M9 7V4h6v3"/>
                """,

            ["save"] = """
                <path d="M5 3h12l2 2v16H5z"/>
                <path d="M8 3v6h8V3"/>
                <path d="M8 21v-7h8v7"/>
                """,

            ["copy"] = """
                <rect x="8" y="8" width="11" height="11" rx="2"/>
                <rect x="5" y="5" width="11" height="11" rx="2"/>
                """,

            ["paste"] = """
                <path d="M9 3h6v3H9z"/>
                <path d="M7 6h10v15H7z"/>
                """,

            ["undo"] = """
                <path d="M9 7 4 12l5 5"/>
                <path d="M20 17a8 8 0 0 0-8-8H4"/>
                """,

            ["redo"] = """
                <path d="m15 7 5 5-5 5"/>
                <path d="M4 17a8 8 0 0 1 8-8h8"/>
                """,

            ["eye"] = """
                <path d="M2 12s4-7 10-7 10 7 10 7-4 7-10 7-10-7-10-7z"/>
                <circle cx="12" cy="12" r="3"/>
                """,

            ["eye-off"] = """
                <path d="M3 3l18 18"/>
                <path d="M10.5 6.3A10.7 10.7 0 0 1 12 6c6 0 10 6 10 6a18 18 0 0 1-4.1 4.8"/>
                <path d="M6.2 6.2A18 18 0 0 0 2 12s4 6 10 6c1.2 0 2.3-.2 3.3-.6"/>
                """,

            ["play"] = """
                <path d="M8 5v14l11-7z"/>
                """,

            ["pause"] = """
                <path d="M8 5v14"/>
                <path d="M16 5v14"/>
                """,

            ["stop"] = """
                <rect x="6" y="6" width="12" height="12"/>
                """,

            ["bookmark"] = """
                <path d="M7 3h10v18l-5-3-5 3z"/>
                """,

            ["star"] = """
                <path d="m12 3 2.9 6 6.6.9-4.8 4.7 1.2 6.4L12 18l-5.9 3 1.2-6.4L2.5 9.9l6.6-.9z"/>
                """,

            ["flag"] = """
                <path d="M5 21V4"/>
                <path d="M5 4h11l-2 4 2 4H5"/>
                """,

            ["link"] = """
                <path d="M10 14l4-4"/>
                <path d="M7 17a4 4 0 0 1 0-6l2-2a4 4 0 0 1 6 6l-1 1"/>
                <path d="M17 7a4 4 0 0 1 0 6l-2 2a4 4 0 0 1-6-6l1-1"/>
                """,

            ["unlink"] = """
                <path d="M3 3l18 18"/>
                <path d="M10 14l4-4"/>
                <path d="M8 8l2-2"/>
                <path d="M14 14l2 2"/>
                """,

            ["bell"] = """
              <path d="M12 3a5 5 0 0 0-5 5v3.5L5.5 15A1 1 0 0 0 6.3 17h11.4a1 1 0 0 0 .8-2L17 11.5V8a5 5 0 0 0-5-5"/>
              <path d="M10 20a2 2 0 0 0 4 0"/>
             """,

            ["moon"] = """
              <path d="M21 12.79A9 9 0 1 1 11.21 3a7 7 0 0 0 9.79 9.79z"/>
            """,

            ["sun"] = """
            <circle cx="12" cy="12" r="4"/>
           <path d="M12 2v2"/>
           <path d="M12 20v2"/>
           <path d="M2 12h2"/>
           <path d="M20 12h2"/>
           <path d="M4.93 4.93l1.41 1.41"/>
           <path d="M17.66 17.66l1.41 1.41"/>
           <path d="M19.07 4.93l-1.41 1.41"/>
           <path d="M6.34 17.66l-1.41 1.41"/>
      """,

            ["share"] = """
    <circle cx="18" cy="5" r="2"/>
    <circle cx="6" cy="12" r="2"/>
    <circle cx="18" cy="19" r="2"/>
    <path d="M8 12l8-6"/>
    <path d="M8 12l8 6"/>
    """,

            ["bell"] = """
    <path d="M12 3a5 5 0 0 0-5 5v3.5L5.5 15A1 1 0 0 0 6.3 17h11.4a1 1 0 0 0 .8-2L17 11.5V8a5 5 0 0 0-5-5"/>
    <path d="M10 20a2 2 0 0 0 4 0"/>
    """,

            ["bell-off"] = """
    <path d="M3 3l18 18"/>
    <path d="M12 3a5 5 0 0 0-5 5v3.5L5.5 15A1 1 0 0 0 6.3 17h9.5"/>
    <path d="M10 20a2 2 0 0 0 4 0"/>
    """,

            ["sun"] = """
    <circle cx="12" cy="12" r="4"/>
    <path d="M12 2v2"/>
    <path d="M12 20v2"/>
    <path d="M2 12h2"/>
    <path d="M20 12h2"/>
    <path d="M4.93 4.93l1.41 1.41"/>
    <path d="M17.66 17.66l1.41 1.41"/>
    <path d="M19.07 4.93l-1.41 1.41"/>
    <path d="M6.34 17.66l-1.41 1.41"/>
    """,

            ["moon"] = """
    <path d="M21 12.79A9 9 0 1 1 11.21 3a7 7 0 0 0 9.79 9.79z"/>
    """

         
        };
}