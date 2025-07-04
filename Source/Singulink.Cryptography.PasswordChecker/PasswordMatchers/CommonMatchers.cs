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
        Segment("cool"),
        Segment("coolest"),
        Segment("great"),
        Segment("awesome"),
        Segment("amazing"),
        Segment("lovely"),
        Segment("funny"),
        Segment("cute"),
        Segment("sexy"),
        Segment("hot"),
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
        Sequence([Segment("is"), OptionalSegment("not"), Optional(SubjectToAdjectiveModifierMatcher), Optional(SubjectToAdjectiveDeterminerMatcher)]),
        Sequence([Segment("are"), OptionalSegment("not"), Optional(SubjectToAdjectiveModifierMatcher), Optional(SubjectToAdjectiveDeterminerMatcher)]),
        Sequence([Segment("am"), OptionalSegment("not"), Optional(SubjectToAdjectiveModifierMatcher), Optional(SubjectToAdjectiveDeterminerMatcher)]),
    ]);

    public static PasswordMatcher SubjectToSubjectCopularVerbMatcher { get; } = Any([
        Sequence([Segment("is"), OptionalSegment("not")]),
        Sequence([Segment("are"), OptionalSegment("not")]),
        Sequence([Segment("am"), OptionalSegment("not")]),
    ]);

    public static PasswordMatcher ConjunctionMatcher { get; } = Any([
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
        SegmentSequence(["pass", "word?"], parseOptionalSegments: true),
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

        Segment("noob"),
        Segment("noobs"),
        Segment("google"),
        Segment("monkey"),
        Segment("monkeys"),
        Segment("dragon"),
        Segment("dragons"),
        Segment("ninja"),
        Segment("ninjas"),
        Segment("batman"),
        Segment("hottie"),
        Segment("hotties"),
        Segment("shadow"),
        Segment("sunshine"),
        Segment("princess"),
        Segment("freedom"),
        Segment("jesus"),
        Segment("god"),
        Segment("whatever"),
        Segment("love"),
        Segment("sex"),
        SegmentPermutations(["donald", "trump"]),
        SegmentPermutations(["joe", "biden"]),
        Segment("starwars"),
        SegmentSequence(["star", "wars"]),

        Segment("69", checkSubstitutions: false),
        Segment("420", checkSubstitutions: false),
    ]);

    public static PasswordMatcher VerbMatcher { get; } = Any([
        Segment("love"),
        Segment("loves"),
        Segment("like"),
        Segment("likes"),
        Segment("hate"),
        Segment("hates"),
        Segment("am"),
        Segment("want"),
        Segment("wants"),
        Segment("trust"),
        Segment("trusts"),

        Segment("login"),
        SegmentSequence(["log", "in"]),

        Segment("let"),
    ]);

    public static PasswordMatcher VerbSuffixMatcher { get; } = Segment("to");

    public static readonly PasswordMatcher PasswordPrefixMatcher = Permutations([
        YearMatcher,
        KeyboardSequence(KeyboardSequenceTypes.Row),
        KeyboardSequence(KeyboardSequenceTypes.Row),
        KeyboardSequence(KeyboardSequenceTypes.Row),
        KeyboardSequence(KeyboardSequenceTypes.Row),
    ]);

    public static readonly PasswordMatcher PasswordSuffixMatcher = Permutations([
        YearMatcher,
        KeyboardSequence(KeyboardSequenceTypes.Row),
        KeyboardSequence(KeyboardSequenceTypes.Row),
        KeyboardSequence(KeyboardSequenceTypes.Row),
        KeyboardSequence(KeyboardSequenceTypes.Row),
        RepeatedChars(1, 10, c => c is '!' or '$' or '?'),
        RepeatedChars(1, 10, c => c is >= '0' and <= '9'),
    ]);
}
