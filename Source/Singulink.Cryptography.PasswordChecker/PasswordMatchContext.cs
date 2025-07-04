namespace Singulink.Cryptography;

public record PasswordMatchContext
{
    public string OriginalPassword { get; }

    public string CheckedPassword { get; }

    public ReadOnlySpan<char> RemainingChars => CheckedPassword.AsSpan(TotalMatchedLength);

    /// <summary>
    /// Gets the last text that matched to the password (without separators).
    /// </summary>
    public string? LastMatchedText { get; private init; }

    /// <summary>
    /// Gets the total length of characters matched against <see cref="CheckedPassword"/> so far.
    /// </summary>
    public int TotalMatchedLength { get; private init; }

    public PasswordMatchContext? ParentContext { get; private init; }

    internal PasswordMatchContext(string password)
    {
        OriginalPassword = password;
        CheckedPassword = OriginalPassword.Trim().ToLowerInvariant();
    }

    public PasswordMatchContext CreateChild(int length, string? matchedText = null)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.");

        int newTotalLength = TotalMatchedLength + length;

        if (newTotalLength > OriginalPassword.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "Length exceeds the original password length.");

        return this with {
            LastMatchedText = matchedText,
            TotalMatchedLength = TotalMatchedLength + length,
            ParentContext = this,
        };
    }
}