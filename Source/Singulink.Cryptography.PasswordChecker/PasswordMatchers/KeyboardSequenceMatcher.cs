using Singulink.Enums;

namespace Singulink.Cryptography.PasswordMatchers;

public class KeyboardSequenceMatcher : ValueMatcher
{
    private static readonly ImmutableArray<string> Rows = GetRows();

    public KeyboardSequenceTypes SequenceTypes { get; }

    public KeyboardSequenceMatcher(KeyboardSequenceTypes sequenceTypes, bool matchTrailingSeparator)
        : base(true, matchTrailingSeparator)
    {
        if (!sequenceTypes.IsValid())
            throw new ArgumentOutOfRangeException(nameof(sequenceTypes), "Invalid sequence types specified.");

        if (sequenceTypes == KeyboardSequenceTypes.None)
            throw new ArgumentException("At least one sequence type must be specified.", nameof(sequenceTypes));

        SequenceTypes = sequenceTypes;
    }

    protected override IEnumerable<PasswordMatchContext> GetValueMatches(PasswordMatchContext context)
    {
        if ((SequenceTypes & KeyboardSequenceTypes.Row) is not 0)
        {
            foreach (string row in Rows)
            {
                // Check if remaining characters start with 3 consecutive letters anywhere in the row

                for (int i = 0; i <= row.Length - 3; i++)
                {
                    if (context.RemainingChars.StartsWith(row.AsSpan(i, 3), StringComparison.OrdinalIgnoreCase))
                    {
                        var matchContext = context.CreateChild(3, row.Substring(i, 3));
                        yield return matchContext;

                        // Check for each additional character match and yield each one

                        for (int j = i + 3; j < row.Length && j < context.RemainingChars.Length; j++)
                        {
                            if (context.RemainingChars[j] == row[j])
                            {
                                matchContext = context.CreateChild(j + 1, matchContext.LastMatchedText + row[j]);
                                yield return matchContext;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private static ImmutableArray<string> GetRows()
    {
        var forward = KeyboardData.EnglishUSRows.Select(r => r.ToLowerInvariant()).ToImmutableArray();
        var backward = forward.Select(r => new string(r.Reverse().ToArray())).ToImmutableArray();

        return [.. forward, .. backward];
    }
}
