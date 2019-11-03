using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using Zek.WSPayge;

namespace Zek.Service.Bank
{
    public class PaygeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        public enum ResponseCode
        {
            Success = 0,
            SystemError = -100,
            SignatureNotEquals = -1,
            InvalidParameters = -6,
            UnknowMerchant = -11,
            UnknownIpAddress = -12,
            InsufficientFunds = -13,
            FimiError = -14,
        }

        /// <summary>
        /// სერვისში არსებული მეთოდი რომელიც აწარმოებს პოს რექარენთ გადახდას, პოსში გატარებული ტრანზაქციის ჩეკზე არსებულ კოდზე და პოსის იდენტიფიკატორზე დაყრდნობით, ეს პარამეტრები თავის მხრვ მერჩანტმა ერთჯერადად უდნა შეინახოს და ქვემოთ მოყვანილ გადახდის მეთოდს გადმოაწოდოს. გადახდა სრულდება ყოველთვის პირველადი ინდეტიფიკატორით (ის რომელიც პოსის ჩეკზე იბეჭდება პირველი გადახდისას)
        /// </summary>
        /// <param name="url">ვებ სერვისის მისამართი.</param>
        /// <param name="merchantSecretKey">pay.ge ზე გადახდებისათვის განკუთვნილი მერჩანტის საიდუმლო კოდი.</param>
        /// <param name="merchantCodName">მერჩანტის კოდური დასახელება (pay.ge ზე, მაგალითად. www.magariMagazia.ge - MAGARIMAGAZIAGE)</param>
        /// <param name="merchantTranCode">მერჩანტის მხარეს უნიკალური ტრანზაქციის იდენტიფიკატორი.</param>
        /// <param name="terminalID">პოს ტერმინალის იდენტიფიკატორი რომლითაც მოხდა პირველი გადახდა (პოსის ID - მაგ. D905) </param>
        /// <param name="parentTransactionCode">პოსის ჩეკზე დაგენერირებული იდენტიფიკატორი (TWO ID).</param>
        /// <param name="description">ინფორმაცია გადახდის შესახებ (მაქსიმუმ 150 სიმბოლო).</param>
        /// <param name="amount">გადასახადი თანხა თეთრებში (მაგ. 1 GEL  იქნება 100 მიშვნელობა).</param>
        /// <param name="currency">ვალუტა (რიცხვითი მნიშნელობა GEL – 981 , USD – 840, 978 -EUR).</param>
        /// <param name="signature">SHA256 (MerchantSecretKey+ merchantCodName + parentTransactionCode + merchantTranCode + description+ amount+ currency + terminalID);</param>
        public static RecurringPosPaymentResponse RecurringPosPayment(string url, string merchantSecretKey, string merchantCodName, string merchantTranCode, string terminalID, string parentTransactionCode, string description, int amount, Currency currency, string signature = null)
        {
            return RecurringPosPayment(url, merchantSecretKey, merchantCodName, merchantTranCode, terminalID, parentTransactionCode, description, amount, (int)currency, signature);
        }

        /// <summary>
        /// სერვისში არსებული მეთოდი რომელიც აწარმოებს პოს რექარენთ გადახდას, პოსში გატარებული ტრანზაქციის ჩეკზე არსებულ კოდზე და პოსის იდენტიფიკატორზე დაყრდნობით, ეს პარამეტრები თავის მხრვ მერჩანტმა ერთჯერადად უდნა შეინახოს და ქვემოთ მოყვანილ გადახდის მეთოდს გადმოაწოდოს. გადახდა სრულდება ყოველთვის პირველადი ინდეტიფიკატორით (ის რომელიც პოსის ჩეკზე იბეჭდება პირველი გადახდისას)
        /// </summary>
        /// <param name="url">ვებ სერვისის მისამართი.</param>
        /// <param name="merchantSecretKey">pay.ge ზე გადახდებისათვის განკუთვნილი მერჩანტის საიდუმლო კოდი.</param>
        /// <param name="merchantCodName">მერჩანტის კოდური დასახელება (pay.ge ზე, მაგალითად. www.magariMagazia.ge - MAGARIMAGAZIAGE).</param>
        /// <param name="merchantTranCode">მერჩანტის მხარეს უნიკალური ტრანზაქციის იდენტიფიკატორი.</param>
        /// <param name="terminalID">პოს ტერმინალის იდენტიფიკატორი რომლითაც მოხდა პირველი გადახდა (პოსის ID - მაგ. D905).</param>
        /// <param name="parentTransactionCode">პოსის ჩეკზე დაგენერირებული იდენტიფიკატორი (TWO ID).</param>
        /// <param name="description">ინფორმაცია გადახდის შესახებ (მაქსიმუმ 150 სიმბოლო).</param>
        /// <param name="amount">გადასახადი თანხა თეთრებში (მაგ. 1 GEL  იქნება 100 მიშვნელობა).</param>
        /// <param name="currency">ვალუტა (რიცხვითი მნიშნელობა GEL – 981 , USD – 840, 978 -EUR).</param>
        /// <param name="signature">SHA256 (MerchantSecretKey+ merchantCodName + parentTransactionCode + merchantTranCode + description+ amount+ currency + terminalID);</param>
        public static RecurringPosPaymentResponse RecurringPosPayment(string url, string merchantSecretKey, string merchantCodName, string merchantTranCode, string terminalID, string parentTransactionCode, string description, int amount, int currency, string signature = null)
        {
            if (string.IsNullOrWhiteSpace(merchantSecretKey))
                throw new ArgumentException(@"Merchant Secret Key is null or white-space.", nameof(merchantSecretKey));
            if (string.IsNullOrWhiteSpace(merchantCodName))
                throw new ArgumentException(@"Merchant Cod Name is null or white-space.", nameof(merchantCodName));
            if (string.IsNullOrWhiteSpace(merchantTranCode))
                throw new ArgumentException(@"Merchant Tran Name is null or white-space.", nameof(merchantTranCode));
            if (string.IsNullOrWhiteSpace(terminalID))
                throw new ArgumentException(@"Terminal ID is null or white-space.", nameof(terminalID));
            if (string.IsNullOrWhiteSpace(parentTransactionCode))
                throw new ArgumentException(@"Parent Transaction Code is null or white-space.", nameof(parentTransactionCode));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(@"Description is null or white-space.", nameof(description));
            if (amount < 1)
                throw new ArgumentException(@"Amount must be greater then 0.", nameof(amount));

            if (!Enum.IsDefined(typeof(Currency), currency))
                throw new ArgumentException(@"Currency is not valid.", nameof(currency));


            if (string.IsNullOrWhiteSpace(signature))
                signature = SHA256Hex(merchantSecretKey + merchantCodName + parentTransactionCode + merchantTranCode + description + amount + currency + terminalID);

            using (var ws = CreatePaygeServiceClient(url))
            {
                return ws.RecurringPosPayment(merchantCodName, merchantTranCode, terminalID, parentTransactionCode, description, amount, currency, signature);
            }
        }
        private static PaygeServiceClient CreatePaygeServiceClient(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return new PaygeServiceClient();


            var binding = new BasicHttpBinding();
            var remoteAddress = new EndpointAddress(url);

            return new PaygeServiceClient(binding, remoteAddress);
        }

        /// <summary>
        /// ამოწმებს პოს ტერმინალით გადახდებს:
        /// იპ მისამართი არ არის  დაშვებული = -12,
        /// მერჩანთი იდენტიფიცირება ვერ მოხერხდა = -11,
        /// ჰეშირებული პარამეტრი არ ემთხვევა = -1,
        /// ოპერაცია წარმატებულია = 0,
        /// სისტემური შეცდომა = -100
        /// მიმდინარე პარამეტრები ოპერაცია ვერ მოიძებნა (ანუ შეცდომით არის ტერმინალის იდ ან სხვა პარამეტრი) = -2,
        /// </summary>
        /// <param name="url">ებ სერვისის მისამართი.</param>
        /// <param name="merchantSecretKey"></param>
        /// <param name="merchantCodName">მერჩანტის CodeName აირს (payge - ში რაც აქვს)</param>
        /// <param name="amount">pay.ge ზე გადახდებისათვის განკუთვნილი მერჩანტის საიდუმლო კოდი.</param>
        /// <param name="terminalID">პოს ტერმინალის იდენტიფიკატორი რომლითაც მოხდა პირველი გადახდა (პოსის ID - მაგ. D905)</param>
        /// <param name="transactionID"></param>
        /// <param name="signature"></param>
        public static CheckRecurringPaymentResponse CheckPosPayment(string url, string merchantSecretKey, string merchantCodName, int amount, string terminalID, string transactionID, string signature = null)
        {
            if (string.IsNullOrWhiteSpace(merchantSecretKey))
                throw new ArgumentException(@"Merchant Secret Key is null or white-space.", nameof(merchantSecretKey));
            if (string.IsNullOrWhiteSpace(merchantCodName))
                throw new ArgumentException(@"Merchant Cod Name is null or white-space.", nameof(merchantCodName));
            if (amount < 1)
                throw new ArgumentException(@"Amount must be greater then 0.", nameof(amount));
            if (string.IsNullOrWhiteSpace(terminalID))
                throw new ArgumentException(@"Terminal ID is null or white-space.", nameof(terminalID));
            if (string.IsNullOrWhiteSpace(transactionID))
                throw new ArgumentException(@"Transaction ID is null or white-space.", nameof(transactionID));

            if (string.IsNullOrWhiteSpace(signature))
                signature = SHA256Hex(merchantSecretKey + merchantCodName + amount + terminalID + transactionID);

            using (var ws = CreatePaygeServiceClient(url))
            {
                return ws.CheckPosPayment(merchantCodName, amount, terminalID, transactionID, signature);
            }
        }

        private static string SHA256Hex(string plainText)
        {
            using (var md5 = new SHA256Managed())
            {
                var buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                return BitConverter.ToString(buffer).Replace("-", string.Empty).ToUpperInvariant();
            }
        }
    }
}
