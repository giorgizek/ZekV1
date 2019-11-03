using System.Collections.Generic;
using Zek.Extensions;

namespace Zek.Core
{

    public static class StringHelper
    {
        #region Unicode & Acadnusx
        //private const string _GeorgianUTF8 = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
        //private const string _GeorgianASCII = "abgdevzTiklmnopJrstufqRySCcZwWxjh";
        public static string GeorgianASCIIToUTF8(string str)
        {
            return str.ReplaceAll("abgdevzTiklmnopJrstufqRySCcZwWxjh", "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ");
        }
        public static string GeorgianUTF8ToASCII(string str)
        {
            return str.ReplaceAll("აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ", "abgdevzTiklmnopJrstufqRySCcZwWxjh");
        }
        public static string GeorgianStandardToUTF8(string str)
        {
            return str.ReplaceAll("ÀÁÂÃÄÅÆÈÉÊËÌÍÏÐÑÒÓÔÖ×ØÙÚÛÜÝÞßàáãä¹", "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ№");
        }
        public static string GeorgianUTF8ToStandard(string str)
        {
            return str.ReplaceAll("აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ№", "ÀÁÂÃÄÅÆÈÉÊËÌÍÏÐÑÒÓÔÖ×ØÙÚÛÜÝÞßàáãä¹");
        }
        public static string GeorgianUTF8ToPhonetic(string str)
        {
            string[] from = { "ა", "ბ", "გ", "დ", "ე", "ვ", "ზ", "თ", "ი", "კ", "ლ", "მ", "ნ", "ო", "პ", "ჟ", "რ", "ს", "ტ", "უ", "ფ", "ქ", "ღ", "ყ", "შ", "ჩ", "ც", "ძ", "წ", "ჭ", "ხ", "ჯ", "ჰ" };
            string[] to = { "a", "b", "g", "d", "e", "v", "z", "th", "i", "k", "l", "m", "n", "o", "p", "zh", "r", "s", "t", "u", "ph", "q", "gh", "kh", "sh", "ch", "ts", "dz", "ts", "ch", "kh", "j", "h" };

            return str.ReplaceAll(from, to);
        }
        public static string GeorgianUTF8ToPassport(string str)
        {
            string[] from = { "ა", "ბ", "გ", "დ", "ე", "ვ", "ზ", "თ", "ი", "კ", "ლ", "მ", "ნ", "ო", "პ", "ჟ", "რ", "ს", "ტ", "უ", "ფ", "ქ", "ღ", "ყ", "შ", "ჩ", "ც", "ძ", "წ", "ჭ", "ხ", "ჯ", "ჰ" };
            string[] to = { "a", "b", "g", "d", "e", "v", "z", "t", "i", "k", "l", "m", "n", "o", "p", "zh", "r", "s", "t", "u", "p", "k", "gh", "kh", "sh", "ch", "ts", "dz", "ts", "ch", "kh", "j", "h" };

            return str.ReplaceAll(from, to);
        }

        //private static readonly string[] _GeorgianTranscript = new string[]
        //{
        //    "a", "b", "g", "d", "e", "v", "z", "th", "i", "k", "l",
        //    "m", "n", "o", "p", "zh", "r", "s", "t", "u", "ph", "q",
        //    "gh", "kh", "sh", "ch", "ts", "dz", "ts", "ch", "kh", "j", "h"
        //};
        //public static string TranscriptFromGeorgian(string value)
        //{
        //    return Replace(_GeorgianUTF8, _GeorgianTranscript, value);
        //}

        //private const string _russian = "абвгдезиклмнопрстуфцыі";
        //private const string _cyrillic = "abvgdeziklmnoprstufcyi";
        //private const string _russianCaps = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЪЫЭЮЯЇЄІ";
        //private const string _russianSmall = "абвгдеёжзийклмнопрстуфхцчшщьъыэюяїєі";
        //private static readonly string[] _ussians = new string[]
        //{
        //    "й", "ё", "ж", "х", "ч", 
        //    "ш", "щ", "э", "ю", "я",
        //    "ъ", "ь", "ї", "є",
        //};
        //private static readonly string[] _cyrillics = new string[]
        //{
        //    "jj", "jo", "zh", "kh", "ch",
        //    "sh", "shh", "je", "ju", "ja",
        //    "", "", "yi", "ye"
        //};
        //private static readonly string[] _GeorgianPhonetics = new string[]
        //{
        //    "a", "b", "g", "d", "e", "v", "z", "th", "i", "k",
        //    "l", "m", "n", "o", "p", "jh", "r", "s", "t", "u",
        //    "ph", "q", "gh", "y", "sh", "ch", "c", "dz", "w", "ch",
        //    "x", "j", "h"
        //};
        public static string RussianToCyrillic(string value)
        {
            var russians = new[]
            {
                "й", "ё", "ж", "х", "ч", 
                "ш", "щ", "э", "ю", "я",
                "ъ", "ь", "ї", "є"
            };
            var cyrillics = new[]
            {
                "jj", "jo", "zh", "kh", "ch",
                "sh", "shh", "je", "ju", "ja",
                "", "", "yi", "ye"
            };
            return value.ReplaceAll("абвгдезиклмнопрстуфцыі", "abvgdeziklmnoprstufcyi").ReplaceAll(russians, cyrillics).ReplaceAll("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЪЫЭЮЯЇЄІ", "абвгдеёжзийклмнопрстуфхцчшщьъыэюяїєі");
            // Replace(Replace(Replace(value, _Russian, _Cyrillic), _Russians, _Cyrillics), _RussianCaps, _RussianSmall);
        }
        #endregion



       
        public static string Join(string separator, params object[] values)
        {
            return values != null ? string.Join(separator, values) : null;
        }
        public static string Join(string separator, params string[] values)
        {
            return values != null ? string.Join(separator, values) : null;
        }
        public static string Join(string separator, IEnumerable<string> values)
        {
            return values != null ? string.Join(separator, values) : null;
        }
        public static string Join<T>(string separator, IEnumerable<T> values)
        {
            return values != null ? string.Join(separator, values) : null;
        }
        public static string Join<T>(string separator, params T[] values)
        {
            return values != null ? string.Join(separator, values) : null;
            //return Join(separator, Array.ConvertAll(args, Convert.ToString));
        }

        //public static string Join(string separator, List<object> args)
        //{
        //    return args != null ? string.Join(separator, args) : null;
        //}
        public static string Join(string seperator, string[] value, int startIndex)
        {
            return value != null ? string.Join(seperator, value, startIndex, value.Length == 0 ? 0 : value.Length - startIndex) : null;
        }


        /// <summary>
        /// აბრუნებს პირველ არა null და არა ცარიელ არგუმენტს
        /// (შემოწმება ხდება IsNullOrEmpty-ით).
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string CoalesceEmpty(params string[] args)
        {
            if (args == null) return null;
            foreach (var val in args)
            {
                if (!string.IsNullOrEmpty(val))
                    return val;
            }

            return null;
        }

        /// <summary>
        /// აბრუნებს პირველ არა null და არა ცარიელ არგუმენტს
        /// (შემოწმება ხდება IsNullOrEmpty-ით).
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string CoalesceWhiteSpace(params string[] args)
        {
            if (args == null) return null;
            foreach (var val in args)
            {
                if (!string.IsNullOrWhiteSpace(val))
                    return val;
            }

            return null;
        }

        /// <summary>
        /// აბრუნებს პირველ არა null და არა ცარიელ არგუმენტს
        /// (შემოწმება ხდება IsNullOrEmpty-ით).
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Coalesce(params string[] args)
        {
            if (args == null) return null;
            foreach (var val in args)
            {
                if (val != null)
                    return val;
            }

            return null;
        }
    }
}
