using System;

namespace Zek.Security
{
    [Serializable]
    public enum PasswordStatus
    {
        Success,
        TooShort,
        TooLong,
        NeedMoreLowerChars,
        NeedMoreUpperChars,
        NeedMoreDigitChars,
        NeedMoreSpecialChars,
    }
}
