namespace Singulink.Cryptography.PasswordMatchers;

#pragma warning disable SA1515 // Single-line comment should be preceded by blank line

/// <summary>
/// Provides predefined keyboard layout data.
/// </summary>
public static class KeyboardData
{
    /// <summary>
    /// Gets the rows for the English (US) keyboard layout.
    /// </summary>
    public static ImmutableArray<string> EnglishUSRows { get; } = [
        // Normal
        @"`1234567890-=\",
        @"qwertyuiop[]\",
        @"asdfghjkl;'",
        @"zxcvbnm,./",

        // Shifted
        @"~!@#$%^&*()_+|",
        @"QWERTYUIOP{}|",
        @"ASDFGHJKL:""",
        @"ZXCVBNM<>?",
    ];

    public static ImmutableArray<string> EnglishUSForwardDiagonals { get; } = [
        // Normal
        @"1qaz",
        @"2wsx",
        @"3edc",
        @"4rfv",
        @"5tgb",
        @"6yhn",
        @"7ujm",
        @"8ik,",
        @"9ol.",
        @"0p;/",
        @"-['",
        @"=]",

        // Shifted
        @"!QAZ",
        @"@WSX",
        @"#EDC",
        @"$RFV",
        @"%TGB",
        @"^YHN",
        @"&UJM",
        @"*IK<",
        @"(OL>",
        @")P:?",
        @"_{""",
        @"+}",
    ];

    public static ImmutableArray<string> EnglishUSBackwardDiagonals { get; } = [
        // Normal
        @"2q",
        @"3wa",
        @"4esz",
        @"5rdx",
        @"6tfc",
        @"7ygv",
        @"8uhb",
        @"9ijn",
        @"0okm",
        @"-pl,",
        @"=[;.",
        @"\]'/",

        // Shifted
        @"@Q",
        @"#WA",
        @"$ESZ",
        @"%RDX",
        @"^TFC",
        @"&YGV",
        @"*UHB",
        @"(IJN",
        @")OKM",
        @"_PL<",
        @"+{:>",
        @"|}""?"
    ];

    public static ImmutableArray<string> EnglishUSKeyAndShiftedKey { get; } = [
        @"`~",
        @"1!",
        @"2@",
        @"3#",
        @"4$",
        @"5%",
        @"6^",
        @"7&",
        @"8*",
        @"9(",
        @"0)",
        @"-_",
        @"=+",
        @"[{",
        @"]}",
        @"\|",
        @";:",
        @"'""",
        @",<",
        @".>",
        @"/?",
    ];
}
