using System;
using Zek.DataModel;
using Zek.Extensions;

namespace Zek.Data.Financial
{
    /// <summary>
    /// გადახდის დამხმარე კლასი.
    /// </summary>
    public class PaymentHelper
    {
        private static decimal InitDiscount(PaymentSchemaType paymentSchemaType, decimal discount)
        {
            switch (paymentSchemaType)
            {
                case PaymentSchemaType.OnceThroughWithDiscount:
                case PaymentSchemaType.HalfYearWithDiscount:
                case PaymentSchemaType.QuarterlyWithDiscount:
                case PaymentSchemaType.MonthlyWithDiscount:
                    return discount;
            }
            return 0m;
        }

        public static PaymentInfo[] GeneratePaymentSchema(PaymentSchemaType paymentSchemaType, DateTime startDate, DateTime endDate, decimal amount, decimal discount = 0m)
        {
            if (amount == 0m) return new PaymentInfo[0];

            discount = InitDiscount(paymentSchemaType, discount);

            amount *= 1m - discount / 100m;

            switch (paymentSchemaType)
            {
                case PaymentSchemaType.OnceThrough:
                case PaymentSchemaType.OnceThroughWithDiscount:
                    var schema = new PaymentInfo[1];
                    schema[0] = new PaymentInfo {Date = startDate, Amount = amount};
                    return schema;

                case PaymentSchemaType.HalfYear:
                case PaymentSchemaType.HalfYearWithDiscount:
                    return GeneratePaymentSchema(6, startDate, endDate, amount);

                case PaymentSchemaType.Quarterly:
                case PaymentSchemaType.QuarterlyWithDiscount:
                    return GeneratePaymentSchema(3, startDate, endDate, amount);

                case PaymentSchemaType.Monthly:
                case PaymentSchemaType.MonthlyWithDiscount:
                    return GeneratePaymentSchema(1, startDate, endDate, amount);

                //case PaymentSchemaType.OneQuarterlyEightMonthly:
                //    int firstQuarterMonthCount = (monthsCount < 4 ? monthsCount : 4);
                //    length = (monthsCount - firstQuarterMonthCount) + 1;
                //    schema = new PaymentInfo[length];

                //    schema[0] = new PaymentInfo();
                //    schema[0].Date = fromDate;
                //    schema[0].Amount = (length > 1 ? Math.Round(amountPerMonth * firstQuarterMonthCount, 2) : amount);
                //    amount -= (decimal)schema[0].Amount;

                //    //premium=120
                //    //count=12
                //    //premium=120-40(პირველი 4 თვე)
                //    for (int i = 1; i < length - 1; i++)
                //    {
                //        schema[i] = new PaymentInfo();
                //        schema[i].Date = schema[i - 1].Date.AddMonths(i == 1 ? 3 : 1);
                //        schema[i].Amount = amountPerMonth;
                //        amount -= amountPerMonth;
                //    }
                //    break;

                default:
                    throw new ArgumentException("choose correct payment type.", nameof(paymentSchemaType));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthsBetweenPayments">1 გადახდაში რამდენი თვე შედის მაგალითად ჯვარტრლურში 2</param>
        /// <param name="startDate">დაწყების თარიღი</param>
        /// <param name="endDate">დასრულების თარიღი.</param>
        /// <param name="amount">ჯამური თანხა</param>
        /// <returns></returns>
        private static PaymentInfo[] GeneratePaymentSchema(int monthsBetweenPayments, DateTime startDate, DateTime endDate, decimal amount)
        {
            //თვეების რაოდენობა.
            //თუ რამოდენიმე დღიანია მაშინ ერთჯერადად უნდა გადაიხადოს.
            var monthsCount = Math.Max(Math.Abs(startDate.SubtractMonth(endDate)), 1);

            //var remainingDays = Math.Abs((int)endDate.Subtract(startDate).TotalDays);

            //var zzz = (startDate.Date - endDate.Date).Days;


            //გადახდების რაოდენობა.
            var payCount = monthsCount / monthsBetweenPayments + (monthsCount % monthsBetweenPayments > 0 ? 1 : 0);

            //ერთ გადახდაზე გადასახდელი რაოდენობა.
            var amountPerPay = Math.Round(amount / payCount, 2);

            var schema = new PaymentInfo[payCount];
            for (var i = 0; i < payCount - 1; i++)
            {
                schema[i] = new PaymentInfo
                {
                    Date = i == 0 ? startDate : startDate.AddMonths(i*monthsBetweenPayments),
                    Amount = amountPerPay
                };

                amount -= amountPerPay;
            }

            if (amount > 0m)
            {
                schema[payCount - 1] = new PaymentInfo
                {
                    Date = payCount > 1 ? schema[payCount - 2].Date.AddMonths(monthsBetweenPayments) : startDate,
                    Amount = amount
                };
            }
            else
            {
                var schema2 = new PaymentInfo[payCount - 1];
                Array.Copy(schema, schema2, payCount - 1);
                return schema2;
            }

            return schema;
        }
    }
}
