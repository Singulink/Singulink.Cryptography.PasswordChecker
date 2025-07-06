using System.Diagnostics.CodeAnalysis;
using Singulink.Enums;

namespace Singulink.Cryptography.Utilities;

public static class EnumExtensions
{
    public static void ThrowIfNotDefined<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum>(this TEnum value, string paramName)
        where TEnum : unmanaged, Enum
    {
        if (!value.IsDefined())
            throw new ArgumentOutOfRangeException(paramName, $"Invalid {typeof(TEnum).Name} value.");
    }
}
