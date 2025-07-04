namespace Singulink.Cryptography;

public class PasswordChecker : IPasswordChecker
{
    public static PasswordChecker Default { get; } = new PasswordChecker(PasswordMatchersProvider.Default);

    private readonly IPasswordMatchersProvider _matchersProvider;

    public PasswordChecker(IPasswordMatchersProvider matchersProvider)
    {
        _matchersProvider = matchersProvider;
    }

    public PasswordCheckResult CheckPassword(string password, IEnumerable<ContextualSubject>? subjects = null)
    {
        return GetResult(password, _matchersProvider.GetMatchers(subjects));
    }

    public static PasswordCheckResult GetResult(string password, IEnumerable<PasswordMatcher> matchers)
    {
        var match = PasswordMatcher.GetFirstMatch(password, matchers);

        return new PasswordCheckResult {
            Matched = match is not null,
            MatchedValues = match?.Items.Select(i => i.MatchedText).ToList() ?? [],
        };
    }
}
