using System;
using System.Text;

namespace Zek.Test
{
    public partial class Func
    {
        public sealed class Ka
        {
            private static readonly string[] Months = { "იანვარი", "თებერვალი", "მარტი", "აპრილი", "მაისი", "ივნისი", "ივლისი", "აგვისტო", "სექტემბერი", "ოქტომბერი", "ნოემბერი", "დეკემბერი" };
            public static string DateToStr(DateTime date)
            {
                return $"{date.Day} {Months[date.Month - 1]} {date.Year}";
            }





            public static string CurrToStr(decimal value, string currency, string cent = "", bool cents = false)
            {
                var truncated = Math.Truncate(value);
                //if (StiOptions.Engine.UseRoundForToCurrencyWordsFunctions)//UseRoundForToCurrencyWordsFunctions = true;
                //{
                var tmpCent = (long)Math.Round((value - (long)truncated) * 100M);
                if (tmpCent > 99L)
                {
                    tmpCent = 0L;
                    truncated++;
                }
                //}
                //else
                //{
                //    num2 = (long)((value - ((long)d)) * 100M);
                //}
                string str = $"{NumToStr(truncated)} {currency}";
                if (cents)
                {
                    str += $" და {tmpCent:d2}" + $" {cent}";
                }
                return str;
            }

            /// <summary>
            /// Converts the specified value to its equivalent string representation.
            /// </summary>
            /// <param name="value">value - A value containing a number to convert.</param>
            /// <returns>Returns or does not return the string representation of the integer value.</returns>
            public static string NumToStr(long value)
            {
                var sb = new StringBuilder();
                if (value == 0L)
                {
                    sb.Append("ნული");
                }
                else
                {
                    if (value < 0L)
                    {
                        sb.Append("მინუს");
                        value = Math.Abs(value);
                    }

                    var rank = 1000000000000000000L;
                    AddRank(sb, ref rank, ref value, "კვანტილიონ");
                    AddRank(sb, ref rank, ref value, "კვადრილიონ");
                    AddRank(sb, ref rank, ref value, "ტრილიონ");
                    AddRank(sb, ref rank, ref value, "მილიარდ");
                    AddRank(sb, ref rank, ref value, "მილიონ");
                    AddRank(sb, ref rank, ref value, "ათას");
                    AddThousand(sb, value);
                }

                return sb.ToString();
            }
            public static string NumToStr(decimal value) => NumToStr((long)value);
            public static string NumToStr(double value) => NumToStr((long)value);


            private static readonly string[] Hundreds = { "ას", "ორას", "სამას", "ოთხას", "ხუთას", "ექვსას", "შვიდას", "რვაას", "ცხრაას" };
            private static void AddHundreds(StringBuilder sb, long value)
            {
                if (value == 0L) return;

                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(Hundreds[value - 1L]);
            }


            private static readonly string[] Units = { "ერთი", "ორი", "სამი", "ოთხი", "ხუთი", "ექვსი", "შვიდი", "რვა", "ცხრა", "ათი", "თერთმეტი", "თორმეტი", "ცამეტი", "თოთხმეტი", "თხუთმეტი", "თექვსმეტი", "ჩვიდმეტი", "თვრამეტი", "ცხრამეტი" };
            /// <summary>
            /// 1-დან 19-მდე დამატება
            /// </summary>
            /// <param name="sb"></param>
            /// <param name="value"></param>
            private static void AddUnits(StringBuilder sb, long value)
            {
                if (value == 0) return;

                if (value != 0)
                {
                    sb.Append(Units[value - 1L]);
                }
            }



            private static readonly string[] Tens = { "ოც", "ორმოც", "სამოც", "ოთხმოც" };
            private static void AddTens(StringBuilder sb, long value)
            {
                if (value == 0L) return;

                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }

                sb.Append(Tens[value - 1L]);
            }

            private static void AddThousand(StringBuilder sb, long value)
            {
                AddHundreds(sb, value / 100L);
                value = value % 100L;

                if (value == 0L)
                {
                    sb.Append("ი");
                }
                else if (value < 20L)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    AddUnits(sb, value);
                }
                else
                {
                    AddTens(sb, value / 20L);
                    sb.Append(value%20 != 0L ? "და" : "ი");
                    AddUnits(sb, value % 20L);
                }
            }

            private static void AddRank(StringBuilder sb, ref long rank, ref long value, string unit)
            {
                var num = value / rank;
                if (num > 0L)
                {
                    AddThousand(sb, num);

                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(unit);

                    value %= rank;
                }

                rank /= 1000L;
            }
        }
    }
}
