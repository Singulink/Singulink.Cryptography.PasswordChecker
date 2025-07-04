namespace Singulink.Cryptography;

public class ContextualSubject
{
    public ContextualSubjectType Type { get; }

    public string Value { get; }

    public ContextualSubject(ContextualSubjectType type, string? value)
    {
        Type = type;
        Value = value?.Trim() ?? string.Empty;
    }
}
