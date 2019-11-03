using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Zek.Validation
{
    /// <summary>
    /// ვალიდაციის კლასი (ნომრიანი ტექსტის, ელ.ფოსტის).
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// ციფრების ვალიდაცია.
        /// </summary>
        /// <param name="value">შეყვანილი ტექსტი.</param>
        /// <returns>აბრუნებს trues, როცა ცარიელი ტექსტია ან შეიცავს მხოლოდ ასოებს, სხვა შემთხვევაში false.</returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            foreach (var c in value)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// ამოწმებს მოცემული ტიპი არის თუ არა ნუმერული.
        /// </summary>
        /// <param name="type">ტიპი რომლის შემოწმებაც გვინდა.</param>
        /// <returns>თუ ნუმერულია აბრუნებს true-ს (int, decimal, double...)</returns>
        public static bool IsNumericType(Type type)
        {
            if (!type.IsEnum)
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ელ. ფოსტის ვალიდაცია.
        /// </summary>
        /// <param name="value">ელ. ფოსტის მისამართი.</param>
        /// <returns>აბრუნებს თუ სწორია trues, სხვა შემთხვევაში false.</returns>
        public static bool IsValidEmail(string value)
        {
            var regex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            return (value != null) && (regex.Match(value).Length > 0);
        }

        /// <summary>
        /// ტელეფონის ნომრის ვალიდაცია
        /// </summary>
        /// <param name="value">ტელეფონის ნომერი.</param>
        /// <returns>აბრუნებს თუ სწორია trues, სხვა შემთხვევაში false.</returns>
        public static bool IsValidPhone(string value)
        {
            var regex = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            return (value != null) && (regex.Match(value).Length > 0);
        }


        /// <summary>
        /// პლასტიკური ბარათის ნომრის ვალიდაცია Luhn/mod10 ალგორითმის მიხედვით.
        /// </summary>
        /// <param name="value">ბარათის ნომერი, with or without punctuation.</param>
        /// <returns>აბრუნებს true-ს თუ ბარათის ნომერი სწორია, სხვა შემთხვევაში false.</returns>
        public static bool IsCreditCardValid(string value)
        {
            const string allowed = "0123456789";
            int i;

            var cleanNumber = new StringBuilder();
            for (i = 0; i < value.Length; i++)
            {
                if (allowed.IndexOf(value.Substring(i, 1), StringComparison.Ordinal) >= 0)
                    cleanNumber.Append(value.Substring(i, 1));
            }

            if (cleanNumber.Length < 13 || cleanNumber.Length > 16)
                return false;

            for (i = cleanNumber.Length + 1; i <= 16; i++)
                cleanNumber.Insert(0, "0");

            var total = 0;
            var number = cleanNumber.ToString();

            for (i = 1; i <= 16; i++)
            {
                var multiplier = 1 + i % 2;
                var digit = int.Parse(number.Substring(i - 1, 1));
                var sum = digit * multiplier;
                if (sum > 9)
                    sum -= 9;
                total += sum;
            }
            return total % 10 == 0;
        }

        public static bool IsValidUserName(string value)
        {
            return Regex.IsMatch(value, @"^[A-Z0-9._-]+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Function to test for Positive Integers.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNaturalNumber(string value)
        {
            var notNaturalPattern = new Regex("[^0-9]");
            var naturalPattern = new Regex("0*[1-9][0-9]*");

            return !notNaturalPattern.IsMatch(value) && naturalPattern.IsMatch(value);
        }

        /// <summary>
        /// Function to test for Positive Integers with zero inclusive
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(string value)
        {
            var notWholePattern = new Regex("[^0-9]");
            return !notWholePattern.IsMatch(value);
        }

        /// <summary>
        /// Function to Test for Integers both Positive & Negative
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(string value)
        {
            var notIntPattern = new Regex("[^0-9-]");
            var intPattern = new Regex("^-[0-9]+$|^[0-9]+$");

            return !notIntPattern.IsMatch(value) && intPattern.IsMatch(value);
        }

        /// <summary>
        /// Function to Test for Positive Number both Integer & Real
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositiveNumber(string value)
        {
            var notPositivePattern = new Regex("[^0-9.]");
            var positivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            var twoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");

            return !notPositivePattern.IsMatch(value) && positivePattern.IsMatch(value) && !twoDotPattern.IsMatch(value);
        }

        /// <summary>
        /// Function to test whether the string is valid number or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            var notNumberPattern = new Regex("[^0-9.-]");
            var twoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            var twoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            const String validIntegerPattern = "^([-]|[0-9])[0-9]*$";
            var numberPattern = new Regex($"(^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$)|({validIntegerPattern})");

            return !notNumberPattern.IsMatch(value) && !twoDotPattern.IsMatch(value) && !twoMinusPattern.IsMatch(value) && numberPattern.IsMatch(value);
        }

        /// <summary>
        /// ამოწმებს გადმოცემული ტექსტი არის თუ არა ლათინური ალფაბეტური (a-Z).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAlpha(string value)
        {
            var alphaPattern = new Regex("[^a-zA-Z]");
            return !alphaPattern.IsMatch(value);
        }

        /// <summary>
        /// ამოწმებს გადმოცემული ტექსტი არის თუ არა ლათინური ალფაბეტური ან ნომერული (a-Z,0-9).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(string value)
        {
            var alphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !alphaNumericPattern.IsMatch(value);
        }

        /// <summary>
        /// ლინკის ვალიდაცია (http, ftp)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUrl(string value)
        {
            var regex = new Regex(@"^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            return (value != null) && (regex.Match(value).Length > 0);
        }

        /// <summary>
        /// პირადი ნომრის შემოწმება.
        /// </summary>
        /// <param name="personNumber">პირადი ნომერი.</param>
        /// <returns>აბრუნებს true-ს როცა სწორი პირადი ნომერი გადაეცა.</returns>
        public static bool IsValidPersonalNumber(string personNumber)
        {
            return string.IsNullOrWhiteSpace(personNumber) || Regex.IsMatch(personNumber, "^[0-9]{11}$");
        }

        //public static bool IsUrl2(string value)
        //{
        //    return Regex.IsMatch(value, @"\A(?:\b(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|$!:,.;]*[A-Z0-9+&@#/%=~_|$])\Z", RegexOptions.IgnoreCase);
        //}
    }
}
