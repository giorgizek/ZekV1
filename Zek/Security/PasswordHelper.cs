using System.Text.RegularExpressions;
using Zek.Extensions;

namespace Zek.Security
{
    public class PasswordHelper
    {
        public static string Generate()
        {
            return Generate(4, 8);
        }
        public static string Generate(int minLength, int maxLength)
        {
            var passwordLength = maxLength;

            if (minLength != maxLength)
                passwordLength = RandomHelper.GetRandom().Next(minLength, maxLength);

            return Generate(passwordLength, true, true, true, true, true);
        }
        public static string Generate(int passwordLength)
        {
            return Generate(passwordLength, true, true, true, true, true);
        }
        /// <summary>
        /// Generate Random Password
        /// </summary>
        /// <param name="minLength">Min Password Length</param>
        /// <param name="maxLength">Max Password Length</param>
        /// <param name="includeLetters">(e.g. abcdef)</param>
        /// <param name="includeMixedCase">(e.g. AbcDEf)</param>
        /// <param name="includeNumbers">(e.g. a9b8c7d)</param>
        /// <param name="includePunctuation">(e.g. a!b*c_d)</param>
        /// <param name="noSimilarCharacters">(e.g. i, l, o, 1, 0, I)</param>
        /// <returns>Random Password</returns>
        public static string Generate(int minLength, int maxLength, bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includePunctuation, bool noSimilarCharacters)
        {
            var passwordLength = maxLength;

            if (minLength != maxLength)
                passwordLength = RandomHelper.GetRandom().Next(minLength, maxLength);
            return Generate(passwordLength, includeLetters, includeMixedCase, includeNumbers, includePunctuation, noSimilarCharacters);
        }
        /// <summary>
        /// Generate Random Password
        /// </summary>
        /// <param name="passwordLength">Password Length</param>
        /// <param name="includeLetters">(e.g. abcdef)</param>
        /// <param name="includeMixedCase">(e.g. AbcDEf)</param>
        /// <param name="includeNumbers">(e.g. a9b8c7d)</param>
        /// <param name="includePunctuation">(e.g. a!b*c_d)</param>
        /// <param name="noSimilarCharacters">(e.g. i, l, o, 1, 0, I)</param>
        /// <returns>Random Password</returns>
        public static string Generate(int passwordLength, bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includePunctuation, bool noSimilarCharacters)
        {
            return Generate(passwordLength, GetAllowedChars(includeLetters, includeMixedCase, includeNumbers, includePunctuation, noSimilarCharacters));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        public static string Generate(int minLength, int maxLength, string allowedChars)
        {
            var passwordLength = maxLength;

            if (minLength != maxLength)
                passwordLength = RandomHelper.GetRandom().Next(minLength, maxLength);

            return Generate(passwordLength, allowedChars);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordLength"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        public static string Generate(int passwordLength, string allowedChars)
        {
            var randomBytes = new byte[passwordLength];
            RandomHelper.GetRandom().NextBytes(randomBytes);
            var chars = new char[passwordLength];
            var allowedCharCount = allowedChars.Length;

            for (var i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeLetters"></param>
        /// <param name="includeMixedCase"></param>
        /// <param name="includeNumbers"></param>
        /// <param name="includePunctuation"></param>
        /// <param name="noSimilarCharacters"></param>
        /// <returns></returns>
        public static string GetAllowedChars(bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includePunctuation, bool noSimilarCharacters)
        {
            var allowedChars = includeLetters ? (noSimilarCharacters ? "abcdefghijkmnpqrstuvwxyz" : "abcdefghijklmnopqrstuvwxyz") : string.Empty;
            allowedChars += includeMixedCase ? (noSimilarCharacters ? "ABCDEFGHJKLMNPQRSTUVWXYZ" : "ABCDEFGHIJKLMNOPQRSTUVWXYZ") : string.Empty;
            allowedChars += includeNumbers ? (noSimilarCharacters ? "23456789" : "0123456789") : string.Empty;
            allowedChars += includePunctuation ? @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~" : string.Empty;

            return allowedChars;
        }








        public static bool IsValidUserName(string userName)
        {
            //if( ! preg_match( "/[\||\'|\<|\>|\"|\!|\?|\$|\@|\/|\\\|\&\~\*\+]/", $_POST['login_name'] ) ) {
            if (userName == null || userName.Length < 3) return false;
            return Regex.IsMatch(userName, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?$", RegexOptions.IgnoreCase);
        }

        public static bool IsValidPassword(string password, int minRequiredPasswordLength, int minRequiredLowerChars, int minRequiredUpperChars, int minRequiredDigits, int minRequiredSpecialChars)
        {
            return GetPasswordStatus(password, minRequiredPasswordLength, minRequiredLowerChars, minRequiredUpperChars, minRequiredDigits, minRequiredSpecialChars) == PasswordStatus.Success;
        }
        public static PasswordStatus GetPasswordStatus(string password, int minRequiredPasswordLength, int minRequiredLowerChars, int minRequiredUpperChars, int minRequiredDigits, int minRequiredSpecialChars)
        {
            password = password ?? string.Empty;

            if (password.Length < minRequiredPasswordLength)
                return PasswordStatus.TooShort;
            if (password.Length > 128)
                return PasswordStatus.TooLong;

            if (password.FindCount("abcdefghijklmnopqrstuvwxyz") < minRequiredLowerChars)
                return PasswordStatus.NeedMoreLowerChars;

            if (password.FindCount("ABCDEFGHIJKLMNOPQRSTUVWXYZ") < minRequiredUpperChars)
                return PasswordStatus.NeedMoreUpperChars;

            if (password.FindCount("0123456789") < minRequiredDigits)
                return PasswordStatus.NeedMoreDigitChars;

            if (password.FindCount(@"`-=\~!@#$%^&*()_+|[]{};':"",./<>?") < minRequiredSpecialChars)
                return PasswordStatus.NeedMoreDigitChars;

            return PasswordStatus.Success;
        }
    }


}
