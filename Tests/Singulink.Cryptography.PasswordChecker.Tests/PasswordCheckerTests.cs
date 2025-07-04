namespace Singulink.Cryptography.Tests;

[PrefixTestClass]
public sealed class PasswordCheckerTests
{
    [TestMethod]
    public void Subject_Verb_Subject_Suffix_IsMatch()
    {
        string password = "iloveyou!!";

        var result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["i", "love", "you", "!!"]);
    }

    [TestMethod]
    public void KeyboardSequence_L33tSubject_Year_IsMatch()
    {
        string password = "QWERTp@ssword1975";

        var result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["qwert", "password", "1975"]);
    }

    [TestMethod]
    public void L33tCompanySubject_PrefixedAdjective_IsMatch()
    {
        string password = "s1ngul1nkisthebest";

        var result = PasswordChecker.Default.CheckPassword(password, [new(ContextualSubjectType.Company, "Singulink Business Solutions")]);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["singulink", "is", "the", "best"]);
    }

    [TestMethod]
    public void TwoL33tEmailSubjectParts_RepeatDigitSuffix_RepeatCommonCharSuffix_IsMatch()
    {
        string password = "s1ngul1nkmikem111!!";

        var result = PasswordChecker.Default.CheckPassword(password, [new(ContextualSubjectType.Email, "mikem@singulink.com")]);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["singulink", "mikem", "111", "!!"]);
    }

    [TestMethod]
    public void Subject_RepeatSequence_IsMatch()
    {
        string password = "password 123123 123123";

        var result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["password", "123", "123", "123", "123"]);
    }

    public void RepeatSubject_IsMatch()
    {
        string password = "PasswordPassword PasswordPassword";

        var result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["password", "password", "password", "password"]);
    }

    [TestMethod]
    public void Subject_PrefixedAdjective_Subject_WithSeparators_IsMatch()
    {
        string password = "this is a bad password";

        var result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["this", "is", "a", "bad", "password"]);
    }

    [TestMethod]
    public void RepeatedNameSubject_SuffixedVerb_RepeatedWebsiteSubject_CommonSuffix_IsMatch()
    {
        string password = "MikeMikeLogInToSingulinkSingulink1!";

        var result = PasswordChecker.Default.CheckPassword(password, [
            new(ContextualSubjectType.Website, "https://www.singulink.com"),
            new(ContextualSubjectType.Name, "Mike Smith")
        ]);

        result.Matched.ShouldBeTrue();
        result.MatchedValues.ShouldBe(["mike", "mike", "login", "to", "singulink", "singulink", "1", "!"]);
    }

    public void NoMatch()
    {
        string password = "thisisnotamatch";
        var result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeFalse();
        result.MatchedValues.ShouldBeEmpty();

        password = "passwordk4efjawei";

        result = PasswordChecker.Default.CheckPassword(password);

        result.Matched.ShouldBeFalse();
        result.MatchedValues.ShouldBeEmpty();

        password = "9092394850923029384";

        result = PasswordChecker.Default.CheckPassword(password);
        result.Matched.ShouldBeFalse();

        password = "hello world!!";

        result = PasswordChecker.Default.CheckPassword(password);
        result.Matched.ShouldBeFalse();
    }
}
