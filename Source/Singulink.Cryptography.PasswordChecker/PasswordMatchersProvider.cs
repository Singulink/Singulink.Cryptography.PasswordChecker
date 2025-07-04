using System.Text.RegularExpressions;
using static Singulink.Cryptography.PasswordMatcher;
using static Singulink.Cryptography.PasswordMatchers.CommonMatchers;

namespace Singulink.Cryptography;

public abstract class PasswordMatchersProvider : IPasswordMatchersProvider
{
    public static PasswordMatchersProvider Default { get; } = new DefaultPasswordMatchersProvider(ContextualSubjectMatcherProvider.Default);

    public abstract IEnumerable<PasswordMatcher> GetMatchers(IEnumerable<ContextualSubject>? subjects = null);
}

public class DefaultPasswordMatchersProvider : PasswordMatchersProvider
{
    private readonly IContextualSubjectMatcherProvider _subjectMatcherProvider;

    public DefaultPasswordMatchersProvider(IContextualSubjectMatcherProvider subjectMatcherProvider)
    {
        _subjectMatcherProvider = subjectMatcherProvider;
    }

    public override IEnumerable<PasswordMatcher> GetMatchers(IEnumerable<ContextualSubject>? subjects = null)
    {
        // Subject matchers

        var contextualSubjectMatchers = subjects?
            .Select(_subjectMatcherProvider.GetMatcher)
            .OfType<PasswordMatcher>()
            .ToList() ?? [];

        var subjectMatcher = Any([
            ..contextualSubjectMatchers,
            GeneralSubjectMatcher,
        ]);

        var subjectWithOptionalDeterminerMatcher = Sequence([
                Optional(DeterminerMatcher),
                subjectMatcher,
            ]);

        // Adjective matchers

        // Pattern: [prefix] adjective
        // Example: "[is] great"
        var adjectiveAfterSubjectMatcher = Sequence([
            Optional(AdjectiveAfterSubjectPrefixMatcher),
            AdjectiveMatcher,
        ]);

        // Pattern: [determiner] adjective
        var adjectiveBeforeSubjectMatcher = Sequence([
            Optional(DeterminerMatcher),
            AdjectiveMatcher,
        ]);

        // Verb matchers

        // Pattern: verb [suffix]
        // Example: "loves [to]"
        var verbWithOptionalSuffixMatcher = Sequence([
            VerbMatcher,
            Optional(VerbSuffixMatcher),
        ]);

        // -------------------------------
        // Result common sequence matchers
        // -------------------------------

        // Pattern: pwd prefix [pwd suffix]
        // Example: qwerty [!!!]
        yield return Sequence([
            PasswordPrefixMatcher,
            Optional(PasswordSuffixMatcher),
        ]);

        // Pattern: [pwd prefix] subject [subject] [subject] [pwd suffix]
        // Example: [qwerty] password [password] [password] [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            subjectMatcher,
            Optional(subjectMatcher),
            Optional(subjectMatcher),
            Optional(PasswordSuffixMatcher)
        ]);

        // Pattern: [pwd prefix] [determiner]subject [[prefix]adjective] [pwd suffix]
        // Example: [qwerty] [this] password [[is] great] [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            subjectWithOptionalDeterminerMatcher,
            Optional(adjectiveAfterSubjectMatcher),
            Optional(PasswordSuffixMatcher)
        ]);

        // Pattern: [pwd prefix] [determiner]adjective subject [pwd suffix]
        // Example: [qwerty] [the] great password [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            adjectiveBeforeSubjectMatcher,
            subjectMatcher,
            Optional(PasswordSuffixMatcher)
        ]);

        // Pattern: [pwd prefix] [determiner]subject [prefix]adjective [determiner]subject [pwd suffix]
        // Example: [qwerty] this [is a] bad password [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            subjectWithOptionalDeterminerMatcher,
            adjectiveAfterSubjectMatcher,
            subjectWithOptionalDeterminerMatcher,
            Optional(PasswordSuffixMatcher)
        ]);

        // Pattern: [pwd prefix] [determiner]subject [conjunction] [determiner]subject [[prefix]adjective] [pwd suffix]
        // Example: [qwerty] [this] password [and] [the] appname [[is] great] [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            subjectWithOptionalDeterminerMatcher,
            Optional(SubjectConjunctionMatcher),
            subjectWithOptionalDeterminerMatcher,
            Optional(adjectiveAfterSubjectMatcher),
            Optional(PasswordSuffixMatcher)
        ]);

        // Pattern: [pwd prefix] [determiner]subject verb[suffix] verb [pwd suffix]
        // Example: [qwerty] [this] admin loves [to] login [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            subjectWithOptionalDeterminerMatcher,
            verbWithOptionalSuffixMatcher,
            VerbMatcher,
            Optional(PasswordSuffixMatcher)
        ]);

        // Pattern: [pwd prefix] [determiner]subject verb[suffix] [verb[suffix]] [determiner]subject [pwd suffix]
        // Example: [qwerty] [this] admin loves [to] log in[to] appname [123]
        yield return Sequence([
            Optional(PasswordPrefixMatcher),
            subjectWithOptionalDeterminerMatcher,
            verbWithOptionalSuffixMatcher,
            Optional(verbWithOptionalSuffixMatcher),
            subjectWithOptionalDeterminerMatcher,
            Optional(PasswordSuffixMatcher)
        ]);
    }
}
