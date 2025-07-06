using Singulink.Enums;

namespace Singulink.Cryptography;

public class ContextualSubject
{
    public ContextualSubjectType Type { get; }

    public string Value { get; }

    public ContextualSubject(string value, ContextualSubjectType type = ContextualSubjectType.General)
    {
        if (!type.IsDefined())
            throw new ArgumentOutOfRangeException(nameof(type), "Invalid contextual subject type.");

        Value = value?.Trim() ?? string.Empty;
        Type = type;
    }
}
