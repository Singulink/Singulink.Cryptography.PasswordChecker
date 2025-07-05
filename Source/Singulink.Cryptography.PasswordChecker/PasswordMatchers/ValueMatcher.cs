namespace Singulink.Cryptography.PasswordMatchers;

public abstract class ValueMatcher : PasswordMatcher
{
    private static readonly ImmutableArray<char> Separators = [' ', '-', '_', '.'];

    public bool MatchTrailingSeparator { get; }

    /// <summary>
    /// Gets a value indicating whether repeated values are matched.
    /// </summary>
    public bool MatchRepeats { get; }

    protected ValueMatcher(bool matchRepeats, bool matchTrailingSeparator)
    {
        MatchRepeats = matchRepeats;
        MatchTrailingSeparator = matchTrailingSeparator;
    }

    protected internal sealed override IEnumerable<PasswordMatchContext> GetMatches(PasswordMatchContext context)
    {
        foreach (var childContext in GetValueMatches(context))
        {
            yield return childContext;

            SegmentMatcher repeatMatcher = null;

            if (MatchRepeats)
            {
                int repeatValueLength = childContext.TotalMatchedLength - context.TotalMatchedLength;

                if (repeatValueLength > 0)
                {
                    string repeatValue = context.CheckedPassword.Substring(context.TotalMatchedLength, repeatValueLength);

                    repeatMatcher = new SegmentMatcher(
                        segment: repeatValue,
                        checkSubstitutions: false,
                        matchRepeats: true,
                        matchTrailingSeparator: MatchTrailingSeparator);

                    foreach (var repeatContext in repeatMatcher.GetMatches(childContext))
                        yield return repeatContext;
                }
            }

            if (MatchTrailingSeparator && childContext.RemainingChars is [char s, ..] && Separators.Contains(s))
            {
                var childContextWithSeparator = childContext.CreateChild(1, null);
                yield return childContextWithSeparator;

                if (repeatMatcher is not null)
                {
                    foreach (var repeatContext in repeatMatcher.GetValueMatches(childContextWithSeparator))
                        yield return repeatContext;
                }
            }
        }
    }

    protected abstract IEnumerable<PasswordMatchContext> GetValueMatches(PasswordMatchContext context);
}
