namespace Singulink.Cryptography;

public class PasswordCheckResult
{
    public required bool Matched { get; init; }

    public IReadOnlyList<string> MatchedValues { get; init; } = [];
}
