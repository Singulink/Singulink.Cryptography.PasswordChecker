namespace Singulink.Cryptography;

public interface IPasswordChecker
{
    PasswordCheckResult CheckPassword(string password, IEnumerable<ContextualSubject>? subjects);
}