using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zek.Utils
{
    public enum IbanValidationResult
    {
        IsValid,
        ValueMissing,
        ValueTooSmall,
        ValueTooBig,
        ValueFailsModule97Check,
        CountryCodeNotKnown
    }

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



        private static readonly IDictionary<string, int> Lengths = new Dictionary<string, int>
        {
            {"AL", 28},
            {"AD", 24},
            {"AT", 20},
            {"AZ", 28},
            {"BE", 16},
            {"BH", 22},
            {"BA", 20},
            {"BR", 29},
            {"BG", 22},
            {"CR", 21},
            {"HR", 21},
            {"CY", 28},
            {"CZ", 24},
            {"DK", 18},
            {"DO", 28},
            {"EE", 20},
            {"FO", 18},
            {"FI", 18},
            {"FR", 27},
            {"GE", 22},
            {"DE", 22},
            {"GI", 23},
            {"GR", 27},
            {"GL", 18},
            {"GT", 28},
            {"HU", 28},
            {"IS", 26},
            {"IE", 22},
            {"IL", 23},
            {"IT", 27},
            {"KZ", 20},
            {"KW", 30},
            {"LV", 21},
            {"LB", 28},
            {"LI", 21},
            {"LT", 20},
            {"LU", 20},
            {"MK", 19},
            {"MT", 31},
            {"MR", 27},
            {"MU", 30},
            {"MC", 27},
            {"MD", 24},
            {"ME", 22},
            {"NL", 18},
            {"NO", 15},
            {"PK", 24},
            {"PS", 29},
            {"PL", 28},
            {"PT", 25},
            {"RO", 24},
            {"SM", 27},
            {"SA", 24},
            {"RS", 22},
            {"SK", 24},
            {"SI", 19},
            {"ES", 24},
            {"SE", 24},
            {"CH", 21},
            {"TN", 24},
            {"TR", 26},
            {"AE", 23},
            {"GB", 22},
            {"VG", 24}
        };

        public IbanValidationResult IsValidIban(string value)
        {
            // Check if value is missing
            if (string.IsNullOrEmpty(value))
                return IbanValidationResult.ValueMissing;

            if (value.Length < 2)
                return IbanValidationResult.ValueTooSmall;

            var countryCode = value.Substring(0, 2).ToUpper();

            int lengthForCountryCode;

            var countryCodeKnown = Lengths.TryGetValue(countryCode, out lengthForCountryCode);
            if (!countryCodeKnown)
            {
                return IbanValidationResult.CountryCodeNotKnown;
            }

            // Check length.
            if (value.Length < lengthForCountryCode)
                return IbanValidationResult.ValueTooSmall;

            if (value.Length > lengthForCountryCode)
                return IbanValidationResult.ValueTooBig;

            value = value.ToUpper();
            var newIban = value.Substring(4) + value.Substring(0, 4);

            newIban = Regex.Replace(newIban, @"\D", match => (match.Value[0] - 55).ToString());

            var remainder = long.Parse(newIban) % 97;

            if (remainder != 1)
                return IbanValidationResult.ValueFailsModule97Check;

            return IbanValidationResult.IsValid;
        }
    }
}
