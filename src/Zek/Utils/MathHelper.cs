﻿using System;
using System.Globalization;

namespace Zek.Utils
{
    public static class MathHelper
    {
        public static DateTime? Min(DateTime? a, DateTime? b)
        {
            if (a == null) return b;
            if (b == null) return a;

            return Min(a.Value, b.Value);
        }
        public static DateTime? Max(DateTime? a, DateTime? b)
        {
            if (a == null) return b;
            if (b == null) return a;

            return Max(a.Value, b.Value);
        }
        public static DateTime Min(DateTime a, DateTime b)
        {
            return a <= b ? a : b;
        }
        public static DateTime Max(DateTime a, DateTime b)
        {
            return a >= b ? a : b;
        }






        /// <summary>
        /// Converts the specified value to its equivalent string representation.
        /// </summary>
        /// <param name="value">value - A value containing a number to convert</param>
        /// <param name="decimals">decimals - A value containing a decimals.</param>
        /// <returns>Returns or does not return the string representation of the integer value.</returns>
        public static string NumToStr(decimal value, int decimals = 0)
        {
            if (decimals > 6)
                throw new ArgumentOutOfRangeException(nameof(decimals), decimals, "Looks up a localized string similar to Maximum number is 6.");

            var str = NumToStrHelper.NumToStr((long)value);

            if (decimals > 0)
            {
                value = Math.Round(Math.Abs(value) % 1, decimals);//აქ უკვე value მხოლოდ წილადი რიცხვი ხდება 123.456 => 0.456

                //decimal fraction = Math.Abs(value - Math.Truncate(value));
                //int val = Convert.ToInt32((double)fraction * Math.Pow(10, fraction.ToString().Length - 2));

                var length = FractionLength(value);
                if (length > 0)
                {
                    string fraction;
                    switch (length)
                    {
                        case 1:
                            fraction = "მეათედი";
                            break;

                        case 2:
                            fraction = "მეასედი";
                            break;

                        case 3:
                            fraction = "მეათასედი";
                            break;

                        case 4:
                            fraction = "მეათიათასედი";
                            break;

                        case 5:
                            fraction = "მეასიათასედი";
                            break;

                        case 6:
                            fraction = "მემილიონედი";
                            break;

                        default:
                            throw new Exception("fraction არის 6-ზე მეტი.");
                    }
                    str += $" მთელი {FracToInt32(value)} {fraction}";
                }
            }
            return str;
        }

   
        /// <summary>
        /// რიცხვიდან წილადის ამოღება 123.456 => 456
        /// </summary>
        /// <param name="value">რიცხვი რომლის წილადის ამოღებაც გვინდა.</param>
        /// <returns>წილადის მნიშვნელობა.</returns>
        public static int FracToInt32(decimal value)
        {
            var frac = Math.Abs(value) % 1;
            var fraction = frac.ToString(CultureInfo.InvariantCulture);
            return frac != 0m ? Convert.ToInt32(fraction.Substring(2)) : 0;
        }

        /// <summary>
        /// რიცხვიდან წილადის სიგრძის ამოღება.
        /// </summary>
        /// <param name="value">რიცხვი რომლის წილადის სიგრძეც გვაინტერესებს.</param>
        /// <returns>აბრუნებს სიგრძეს.</returns>
        public static int FractionLength(decimal value)
        {
            var frac = Math.Abs(value) % 1;

            return frac != 0m
                ? frac.ToString(CultureInfo.InvariantCulture).Length - 2
                : 0;
        }
    }
}
