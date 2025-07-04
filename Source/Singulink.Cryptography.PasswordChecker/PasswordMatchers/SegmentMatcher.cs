namespace Singulink.Cryptography.PasswordMatchers;

public class SegmentMatcher : ValueMatcher
{
    private static readonly FrozenDictionary<char, ImmutableArray<char>> CharacterSubstitutions = new Dictionary<char, ImmutableArray<char>>() {
                { 'a', ['4', '@', 'Д'] },
                { 'b', ['8', 'ß'] },
                { 'c', ['(', '<', '{', '[', '¢'] },
                { 'e', ['3', '€', '£'] },
                { 'f', ['ƒ'] },
                { 'g', ['6', '9'] },
                { 'i', ['1', '!', '|'] },
                { 'l', ['1', '|', '7'] },
                { 'n', ['И', 'ท'] },
                { 'o', ['0', 'Ø'] },
                { 'r', ['®', 'Я'] },
                { 's', ['5', '$'] },
                { 't', ['7', '+'] },
                { 'u', ['µ', 'บ'] },
                { 'w', ['พ', '₩', 'ω'] },
                { 'x', ['×'] },
                { 'y', ['¥'] },
                { 'z', ['2', '%'] },
            }.ToFrozenDictionary();

    public new string Segment { get; }

    public bool CheckSubstitutions { get; }

    public SegmentMatcher(string segment, bool checkSubstitutions, bool matchRepeats, bool matchTrailingSeparator)
        : base(matchRepeats, matchTrailingSeparator)
    {
        segment = segment.Trim().ToLowerInvariant();

        if (segment.Length is 0)
            throw new ArgumentException("Segment cannot be empty.", nameof(segment));

        Segment = segment;
        CheckSubstitutions = checkSubstitutions;
    }

    protected override IEnumerable<PasswordMatchContext> GetValueMatches(PasswordMatchContext context)
    {
        var p = context.RemainingChars;

        if (p.Length < Segment.Length)
            return [];

        for (int i = 0; i < Segment.Length; i++)
        {
            if (p[i] != Segment[i])
            {
                if (!CheckSubstitutions || !CharacterSubstitutions.TryGetValue(Segment[i], out var substitutions) || !substitutions.Contains(p[i]))
                    return [];
            }
        }

        return [context.CreateChild(Segment.Length, Segment)];
    }
}
