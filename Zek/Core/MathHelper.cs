using System;
using System.Globalization;
using Zek.Properties;

namespace Zek.Core
{
    /// <summary>
    /// მათემატიკური და ფინანსური გაანგარიშების კლასი.
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// თანხიდან აკლებს დამატებულ პროცენტს (მაგ: RemoveAddedPct(118.00, 18.00)=100.00 )
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლიდანაც გვინდა %-ის ამოღება.</param>
        /// <param name="percent">ამოსაღები პროცენტი.</param>
        /// <returns>აბრუნებს გამოკლებულ პროცენტს.</returns>
        public static decimal RemoveAddedPct(decimal value, decimal percent)
        {
            return value / (1m + percent / 100m);
        }
        /// <summary>
        /// პროცენტის დამატება/გამოკლება.
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომელზეც გვინდა პროცენტის დამატება/გამოკლება.</param>
        /// <param name="percent">დასამატი/გამოსაკლები პროცენტი.</param>
        /// <returns>აბრუნებს დამატებულ/გამოკლებულ პროცენტს.</returns>
        public static decimal AddPct(decimal value, decimal percent)
        {
            return value * (1m + percent / 100m);
        }
        /// <summary>
        /// პროცენტის მოჭრა.
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომელზეც გვინდა პროცენტის დამატება/გამოკლება.</param>
        /// <param name="percent">დასამატი/გამოსაკლები პროცენტი.</param>
        /// <returns>აბრუნებს დამატებულ/გამოკლებულ პროცენტს.</returns>
        public static decimal RemovePct(decimal value, decimal percent)
        {
            return AddPct(value, -percent);
        }



        /// <summary>
        /// იღებს პროცენტს.
        /// მაგალითად  GetPct(200, 220) = 10;
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static decimal GetPct(decimal val1, decimal val2)
        {
            return val2 * 100m / val1;
        }

        #region Financial
        /// <summary>
        /// მნიშვნელობა გადაიტანს წილადის ნიშნის შემდეგ (მაგ: 123 -> 0.123).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal MoveAfterDecimalMark(decimal value)
        {
            return value / Pow(10m, ((int)Math.Abs(value)).ToString(CultureInfo.InvariantCulture).Length);
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
        /// რიცხვიდან წილადის ამოღება 123.456 => 456
        /// </summary>
        /// <param name="value">რიცხვი რომლის წილადის ამოღებაც გვინდა.</param>
        /// <returns>წილადის მნიშვნელობა.</returns>
        public static long FracToInt64(decimal value)
        {
            var frac = Math.Abs(value) % 1;
            var fraction = frac.ToString(CultureInfo.InvariantCulture);
            return frac != 0m ? Convert.ToInt64(fraction.Substring(2)) : 0;
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
        /// <summary>
        /// Converts the specified value to its equivalent string representation.
        /// </summary>
        /// <param name="value">value - A value containing a number to convert</param>
        /// <param name="decimals">decimals - A value containing a decimals.</param>
        /// <returns>Returns or does not return the string representation of the integer value.</returns>
        public static string NumToStr(decimal value, int decimals = 0)
        {
            if (decimals > 6)
                throw new ArgumentOutOfRangeException(nameof(decimals), decimals, Resources.NumToStrMaximumDecimalsErrorText);

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
        /// ფასის დამრგვალება.
        /// მაგალითი: FinancialRound1(123.234) = 123.23;
        /// </summary>
        /// <param name="unrounded">დასამრგვალებელი მნიშვნელობა.</param>
        /// <returns>აბრუნებს დამრგვალებულ მნიშვნელობა (0.01-ით).</returns>
        public static decimal FinancialRound1(decimal unrounded)
        {
            return FinancialRound(unrounded, 0.01m);
        }
        /// <summary>
        /// ფასის დამრგვალება.
        /// მაგალითი: FinancialRound5(123.234) = 123.25;
        /// </summary>
        /// <param name="unrounded">დასამრგვალებელი მნიშვნელობა.</param>
        /// <returns>აბრუნებს დამრგვალებულ მნიშვნელობა (0.05-ით).</returns>
        public static decimal FinancialRound5(decimal unrounded)
        {
            return FinancialRound(unrounded, 0.05m);
        }
        /// <summary>
        /// აბრუნებს დამრგვალებულ მნიშვნელობას.
        /// მაგალითი №1: PriceRound(123.2343, 0.01) = 123.23;
        /// მაგალითი №2: PriceRound(123.2343, 0.05) = 123.25;
        /// მაგალითი №3: PriceRound(123.2343, 0.10) = 123.20;
        /// მაგალითი №3: PriceRound(123.2343, 1.00) = 123.00;
        /// ის ასევე ცნობილია "საბანკო დამრგვალების" შახელწოდბით.
        /// </summary>
        /// <param name="unrounded">დასამრგვალებელი ფასი.</param>
        /// <param name="truncateValue">დასამრგვალებელი რაოდენობა (მაგალითად PriceRound(123.234m, 0.05m) = 123.25m)</param>
        /// <returns>აბრუნებს დამრგვალებულ ფასს (0.05-ით).</returns>
        public static decimal FinancialRound(decimal unrounded, decimal truncateValue)
        {
            var modulus = unrounded % truncateValue;
            return unrounded - modulus + (truncateValue - modulus <= modulus ? truncateValue : 0m);
        }


        /*/// <summary>
        /// Returns the number rounded to 5 Rappen (0.05) for showing to 
        /// the user and printing.
        /// </summary>
        /// <param name="unrounded">Decimal value to be rounded.</param>
        /// <returns>The value rounded to 5 Rappen.</returns>
        public static decimal FinancialRound5(decimal unrounded)
        {
            decimal amount = (unrounded * 20);
            if ((amount - decimal.Truncate(amount)) >= 0.5m)
                amount = Math.Ceiling(amount);
            else
                amount = Math.Floor(amount);
            return (amount / 20);
        }
        /// <summary>
        /// Returns the number with the specified precision nearest the 
        /// specified value also known as "bankers rounding".
        /// </summary>
        /// <remarks>
        /// For detailed information see <see cref="Math.Round"/> function.
        /// </remarks>
        /// <param name="unrounded">decimal value to be rounded.</param>
        /// <param name="digits">The precision to round.</param>
        /// <returns>The number nearest value with precision equal to digits.</returns>
        public static decimal FinancialRound(decimal unrounded, int digits)
        {
            return (decimal)(Math.Floor((double)unrounded * Math.Pow(10.0, digits) + 0.5) / Math.Pow(10.0, digits));
        }
        /// <summary>
        /// Returns the number with the precision of 2 digits (= 1 Rappen) 
        /// nearest the specified value also known as "bankers rounding".
        /// </summary>
        /// <remarks>
        /// For detailed information see <see cref="Math.Round"/> function.
        /// </remarks>
        /// <param name="unrounded">decimal value to be rounded.</param>
        /// <returns>The number nearest value with precision of 2 digits.</returns>
        public static decimal FinancialRound1(decimal unrounded)
        {
            return FinancialRound(unrounded, 2);
        }
        /// <summary>
        /// Rounds the value always up. If round to 2 digits a value 1.791 is rounded to 1.80 but
        /// just 1.790 is rounded to 1.79
        /// </summary>
        /// <param name="unrounded">the unrounded value</param>
        /// <param name="digits">the number of digits the value has to have after rounding</param>
        /// <returns>the rounded value</returns>
        public static decimal RoundUp(decimal unrounded, int digits)
        {
            decimal roundedValue;
            decimal truncatedDigits;

            if (digits < 0) throw new ArgumentOutOfRangeException("digits", digits, "cannot be less than 0");

            truncatedDigits = unrounded * (decimal)Math.Pow(10.0, digits);
            roundedValue = decimal.Truncate(truncatedDigits);
            truncatedDigits = truncatedDigits - roundedValue; // now we got the digits that are beeing truncated
            if (truncatedDigits > 0)
            {
                // round up
                roundedValue++;
            }
            else if (truncatedDigits < 0)
            {
                // round up for negativ values
                roundedValue--;
            }
            else
            {
                // round down (let it)
            }
            return (roundedValue * (decimal)Math.Pow(10.0, (-1.0) * digits)); // shift back to the original digits

        }*/
        #endregion

        #region Units
        /// <summary>
        /// მინიმალური რაოდენობის გამოანგარიშება.
        /// </summary>
        /// <param name="packQty">დაყოფა (ანუ რამდენი ცალი შედის პაკეტში).</param>
        /// <returns>აბრუნებს მინიმალურ რაოდენობას, რაც შეიძლება ჩაისვას მონაცემებში.</returns>
        public static decimal MinQuantity(int packQty)
        {
            return MinQuantity(packQty, 2);
        }
        /// <summary>
        /// მინიმალური რაოდენობის გამოანგარიშება.
        /// </summary>
        /// <param name="packQty">დაყოფა (ანუ რამდენი ცალი შედის პაკეტში).</param>
        /// <param name="scale">დამრგვალება.</param>
        /// <returns>აბრუნებს მინიმალურ რაოდენობას, რაც შეიძლება ჩაისვას მონაცემებში.</returns>
        public static decimal MinQuantity(int packQty, int scale)
        {
            return Math.Round(MinUnit(packQty, scale) / (decimal)packQty, scale);
        }

        /// <summary>
        /// მინიმალური ცალობითი რაოდენობის დააგარიშება.
        /// </summary>
        /// <param name="packQty">დაყოფა (ანუ რამდენი ცალი შედის პაკეტში).</param>
        /// <returns>აბრუნებს მინიმალურ ცალობით რაოდენობას.</returns>
        public static int MinUnit(int packQty)
        {
            return MinUnit(packQty, 2);
        }
        /// <summary>
        /// მინიმალური ცალობითი რაოდენობის დააგარიშება.
        /// </summary>
        /// <param name="packQty">დაყოფა (ანუ რამდენი ცალი შედის პაკეტში).</param>
        /// <param name="scale">დამრგვალება.</param>
        /// <returns>აბრუნებს მინიმალურ ცალობით რაოდენობას.</returns>
        public static int MinUnit(int packQty, int scale)
        {
            var value = (int)Math.Ceiling(packQty / (decimal)Math.Pow(10, scale));
            return value > 0 ? value : 1;
        }


        private static decimal GetMaxQuantityValue(int packQty)
        {
            return int.MaxValue / packQty;
        }

        public static int RoundUnit(decimal quantity, int packQty)
        {
            return RoundUnit(quantity, packQty, 2);
        }
        public static int RoundUnit(decimal quantity, int packQty, int scale)
        {
            return RoundUnit(quantity, packQty, scale, GetMaxQuantityValue(packQty));
        }
        public static int RoundUnit(decimal quantity, int packQty, int scale, decimal maxQuantity)
        {
            if (quantity == 0m) return 0;
            var value = (int)Math.Round(quantity * packQty);

            var minUnit = MinUnit(packQty, scale);
            if (value < minUnit) return minUnit;

            var maxUnit = (int)Math.Round(maxQuantity * packQty);
            if (value > maxUnit) return maxUnit;

            return value;
        }

        public static int CeilingUnit(decimal quantity, int packQty)
        {
            return CeilingUnit(quantity, packQty, 2);
        }
        public static int CeilingUnit(decimal quantity, int packQty, int scale)
        {
            return CeilingUnit(quantity, packQty, scale, GetMaxQuantityValue(packQty));
        }
        public static int CeilingUnit(decimal quantity, int packQty, int scale, decimal maxQuantity)
        {
            if (quantity == 0m) return 0;
            var value = (int)Math.Ceiling(quantity * packQty);

            var minUnit = MinUnit(packQty, scale);
            if (value < minUnit) return minUnit;

            var maxUnit = (int)Math.Ceiling(maxQuantity * packQty);
            if (value > maxUnit) return maxUnit;

            return value;
        }



        public static decimal CeilingQuantity(int units, int packQty)
        {
            return CeilingQuantity(units, packQty, GetMaxQuantityValue(packQty));
        }
        public static decimal CeilingQuantity(int units, int packQty, decimal maxQuantity)
        {
            return CeilingQuantity(units, packQty, 2, maxQuantity);
        }
        public static decimal CeilingQuantity(int units, int packQty, int scale, decimal maxQuantity)
        {
            return CeilingQuantity(0m, units, packQty, scale, maxQuantity);
        }
        public static decimal CeilingQuantity(decimal quantity, int packQty)
        {
            return CeilingQuantity(quantity, packQty, 2);
        }
        public static decimal CeilingQuantity(decimal quantity, int packQty, int scale)
        {
            return CeilingQuantity(quantity, packQty, scale, GetMaxQuantityValue(packQty));
        }
        public static decimal CeilingQuantity(decimal quantity, int packQty, decimal maxQuantity)
        {
            return CeilingQuantity(quantity, packQty, 2, maxQuantity);
        }
        public static decimal CeilingQuantity(decimal quantity, int packQty, int scale, decimal maxQuantity)
        {
            return CeilingQuantity(quantity, 0, packQty, scale, maxQuantity);
        }
        public static decimal CeilingQuantity(decimal quantity, int units, int packQty, int scale, decimal maxQuantity)
        {
            if (quantity == 0m && units == 0) return 0m;
            if (quantity == maxQuantity) return quantity;

            if (units == 0)
                units = CeilingUnit(quantity, packQty, scale);

            var value = Math.Round(units / (decimal)packQty, scale);

            //თუ დაყოფა არის 3 და დარჩენილი რაოდენობა არის 0.3334 და ვყიდით 1 ცალს = 0.3333 მაშინ გვრჩება 0.0001 და ეს სისულელეა.
            //ამიტომაც ვამრგვალებთ და ვაბრუნებთ 0.3334-ს
            //if (maxQuantity - value < MinQuantity(packQty, scale))
            //    return maxQuantity;

            return value <= maxQuantity ? value : maxQuantity;
        }
        #endregion

        #region Pow
        /// <summary>
        /// ახარისხება.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>ახარისხებული მნიშვნელობა</returns>
        public static int Pow(int x, int y)
        {
            return (int)Pow((decimal)x, y);
        }
        /// <summary>
        /// ახარისხება.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>ახარისხებული მნიშვნელობა</returns>
        public static decimal Pow(decimal x, int y)
        {
            return Pow(x, (decimal)y);
        }
        /// <summary>
        /// ახარისხება.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>ახარისხებული მნიშვნელობა</returns>
        public static decimal Pow(decimal x, decimal y)
        {
            return (decimal)Math.Pow((double)x, (double)y);
        }
        #endregion

        /// <summary>
        /// Shifts decimal point right to the end of number.
        /// e.g. 89.67 --> 8967 or 0.2674 --> 2674
        /// </summary>
        public static decimal ShiftDecimalPointRightToEnd(decimal value)
        {
            while (true)
            {
                if (decimal.Subtract(value, decimal.Truncate(value)) == 0)
                    break;

                value *= 10;
            }

            return value;
        }
    }
}
