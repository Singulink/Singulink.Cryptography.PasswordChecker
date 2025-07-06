using static Singulink.Cryptography.PasswordMatcher;

namespace Singulink.Cryptography.PasswordMatchers;

/// <summary>
/// Provides predefined matchers for the most common password text.
/// </summary>
public static class CommonMatchers
{
    /// <summary>
    /// Gets a matcher that matches common years (1900-2099).
    /// </summary>
    public static PasswordMatcher YearMatcher { get; } = FixedDigits(4, 1900, 2099);

    public static PasswordMatcher AdjectiveMatcher { get; } = Any([
        Segment("best"),
        Segment("worst"),
        Segment("good"),
        Segment("bad"),
        Segment("coolest"),
        Segment("cool"),
        Segment("great"),
        Segment("awesome"),
        Segment("amazing"),
        Segment("lovely"),
        Segment("funny"),
        Segment("cute"),
        Segment("sexy"),
        Segment("hot"),
    ]);

    public static PasswordMatcher AdjectiveToSubjectConjunctionMatcher { get; } = Any([
        Segment("as"),
        Segment("with"),
        Segment("like"),
    ]);

    public static PasswordMatcher SubjectToAdjectiveDeterminerMatcher { get; } = Any([
        Segment("the"),
        Segment("a"),
        Segment("an"),
    ]);

    public static PasswordMatcher SubjectToAdjectiveModifierMatcher { get; } = Any([
        Segment("so"),
        Segment("very"),
        Segment("too"),
    ]);

    public static PasswordMatcher SubjectToAdjectiveCopularVerbMatcher { get; } = Any([
        Sequence([Segment("is"), Optional(Segment("not")), Optional(SubjectToAdjectiveModifierMatcher), Optional(SubjectToAdjectiveDeterminerMatcher)]),
        Sequence([Segment("are"), Optional(Segment("not")), Optional(SubjectToAdjectiveModifierMatcher), Optional(SubjectToAdjectiveDeterminerMatcher)]),
        Sequence([Segment("am"), Optional(Segment("not")), Optional(SubjectToAdjectiveModifierMatcher), Optional(SubjectToAdjectiveDeterminerMatcher)]),
    ]);

    public static PasswordMatcher SubjectToSubjectCopularVerbMatcher { get; } = Any([
        Sequence([Segment("is"), Optional(Segment("not"))]),
        Sequence([Segment("are"), Optional(Segment("not"))]),
        Sequence([Segment("am"), Optional(Segment("not"))]),
    ]);

    public static PasswordMatcher SubjectConjunctionMatcher { get; } = Any([
        Segment("and"),
        Segment("or"),
        Segment("to"),
        Segment("in"),
        Segment("into"),
        SegmentSequence(["in", "to"]),
    ]);

    public static PasswordMatcher SubjectDeterminerMatcher { get; } = Any([
        Segment("the"),
        Segment("a"),
        Segment("an"),
        Segment("this"),
        Segment("that"),
        Segment("my"),
        Segment("your"),
        Segment("our"),
        Segment("my"),
        Segment("his"),
        Segment("her"),
        Segment("their"),
        Segment("trusted"),
        Segment("into"),
        Segment("let"),
    ]);

    public static PasswordMatcher GeneralSubjectMatcher { get; } = Any([
        YearMatcher,
        Segment("i", checkSubstitutions: false),
        Segment("me"),
        Segment("it"),
        Segment("you"),
        Segment("this"),
        Segment("he"),
        Segment("she"),
        Segment("they"),
        Segment("we"),
        Segment("him"),
        Segment("her"),
        Segment("them"),
        Segment("us"),

        Segment("password"),
        SegmentSequence(["pass", "word"]),
        Segment("pass"),
        Segment("login"),
        SegmentSequence(["log", "in"]),
        Segment("secret"),

        Segment("welcome"),
        Segment("hello"),
        Segment("hi"),

        Segment("master"),
        Segment("admin"),
        Segment("user"),
        Segment("nobody"),
        Segment("no1"),
        SegmentSequence(["no", "1"]),
        Segment("noone"),
        SegmentSequence(["no", "one"]),

        Segment("football"),
        Segment("baseball"),
        Segment("hockey"),
        Segment("soccer"),

        Segment("noobs"),
        Segment("noob"),
        Segment("google"),
        Segment("monkeys"),
        Segment("monkey"),
        Segment("dragons"),
        Segment("dragon"),
        Segment("ninjas"),
        Segment("ninja"),
        Segment("batman"),
        Segment("hotties"),
        Segment("hottie"),
        Segment("shadow"),
        Segment("sunshine"),
        Segment("princess"),
        Segment("freedom"),
        Segment("jesus"),
        Segment("god"),
        Segment("whatever"),
        Segment("love"),
        Segment("sex"),
        Segment("fuck"),
        Segment("shit"),
        SegmentSequence(["donald", "trump?"], parseOptionalSegments: true),
        SegmentSequence(["joe", "biden?"], parseOptionalSegments: true),
        Segment("starwars"),
        SegmentSequence(["star", "wars"]),

        Segment("69", checkSubstitutions: false),
        Segment("420", checkSubstitutions: false),
    ]);

    public static PasswordMatcher VerbMatcher { get; } = Any([
        Segment("loves"),
        Segment("love"),
        Segment("likes"),
        Segment("like"),
        Segment("hates"),
        Segment("hate"),
        Segment("am"),
        Segment("wants"),
        Segment("want"),
        Segment("trusts"),
        Segment("trust"),

        Segment("login"),
        SegmentSequence(["log", "in"]),

        Segment("let"),
    ]);

    public static PasswordMatcher VerbSuffixMatcher { get; } = Segment("to");

    public static readonly PasswordMatcher PasswordPrefixMatcher = Permutations([
        YearMatcher,
        Sequence([
            KeyboardSequence(KeyboardSequenceTypes.Row),
            Optional(KeyboardSequence(KeyboardSequenceTypes.Row)),
        ]),
        Sequence([
            KeyboardSequence(KeyboardSequenceTypes.Row),
            Optional(KeyboardSequence(KeyboardSequenceTypes.Row)),
        ]),
    ]);

    public static readonly PasswordMatcher PasswordSuffixMatcher = Permutations([
        YearMatcher,
        Sequence([
            KeyboardSequence(KeyboardSequenceTypes.Row),
            Optional(KeyboardSequence(KeyboardSequenceTypes.Row)),
        ]),
        Sequence([
            KeyboardSequence(KeyboardSequenceTypes.Row),
            Optional(KeyboardSequence(KeyboardSequenceTypes.Row)),
        ]),
        Sequence([
            RepeatedChars(1, 16, c => c is >= '0' and <= '9'),
            RepeatedChars(1, 16, c => c is >= '0' and <= '9'),
        ]),
        RepeatedChars(1, 16, c => c is '!' or '$' or '?'),
    ]);
}
