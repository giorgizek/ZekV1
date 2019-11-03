﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Zek.Extensions;
using Zek.Model.Accounting;
using Zek.Model.ViewModel.Ecomm;

namespace Zek.Services
{
    public class EcommService
    {
        //public TbcService(string certificateFile, string certificatePassword, string baseUrl) : this(certificateFile, certificatePassword, baseUrl + "MerchantHandler", baseUrl + "ClientHandler")
        //{
        //}

        public EcommService(EcommServiceOptions options) : this(options.CertificateFile, options.CertificatePassword, options.ServerUrl, options.ClientUrl)
        {

        }

        public EcommService(string certificateFile, string certificatePassword, string serverUrl, string clientUrl = null)
        {
            if (string.IsNullOrEmpty(certificateFile))
                throw new ArgumentException("Certificate file is required", nameof(certificateFile));

            if (string.IsNullOrEmpty(certificatePassword))
                throw new ArgumentException("Certificate password is required", nameof(certificatePassword));

            if (string.IsNullOrEmpty(serverUrl))
                throw new ArgumentException("Server Url is required", nameof(serverUrl));

            //if (string.IsNullOrEmpty(clientUrl))
            //    throw new ArgumentException("Client Url is required", nameof(clientUrl));


            _certificateFile = certificateFile;
            _certificatePassword = certificatePassword;
            _serverUrl = serverUrl;
            _clientUrl = clientUrl;
        }

        private readonly string _serverUrl;
        private readonly string _clientUrl;
        private readonly string _certificateFile;
        private readonly string _certificatePassword;

        /*      
         
         var sb = new StringBuilder($"command=v&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&msg_type=SMS");
         if (!string.IsNullOrEmpty(merchantTransactionId))
         sb.Append($"&mrch_transaction_id={merchantTransactionId}");
         
        */

        public static bool IsCompleted(EcommResult result)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (result)
            {
                case EcommResult.None:
                case EcommResult.Created:
                case EcommResult.Pending:
                    return false;
            }
            return true;
        }

        private async Task<string> PostAsync(string parameters)
        {
            var clientHandler = new HttpClientHandler();
            var clientCertificate = new X509Certificate2(File.ReadAllBytes(_certificateFile), _certificatePassword/*, X509KeyStorageFlags.MachineKeySet*/);

            clientHandler.ClientCertificates.Add(clientCertificate);
            clientHandler.ServerCertificateCustomValidationCallback += (message, certificate2, x509Chain, sslPolicyErrors) => true;

#if NET461
            
#endif

#if NETSTANDARD1_6

#endif
            //todo clientHandler.SslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12;
            clientHandler.UseProxy = false;

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.PostAsync(_serverUrl + "?" + parameters, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private static ResultViewModel Deserialize(string response)
        {
            if (string.IsNullOrEmpty(response))
                return null;

            var model = new ResultViewModel();
            var lines = response.Split(new[] { '\r', '\n', }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var index = line.IndexOf(":", StringComparison.Ordinal);
                if (index == -1)
                    continue;

                var key = line.Substring(0, index).ToUpperInvariant();
                switch (key)
                {
                    case "TRANSACTION_ID":
                        model.TransactionId = line.Substring(index + 1).Trim();
                        break;

                    case "ERROR":
                        model.Error = line.Substring(index + 1).Trim();
                        break;

                    case "RESULT_CODE":
                        model.ResultCode = line.Substring(index + 1).Trim();
                        break;

                    case "RRN":
                        model.Rrn = line.Substring(index + 1).Trim();
                        break;

                    case "APPROVAL_CODE":
                        model.ApprovalCode = line.Substring(index + 1).Trim();
                        break;

                    case "CARD_NUMBER":
                        model.CardNumber = line.Substring(index + 1).Trim();
                        break;

                    case "RESULT":
                        model.ResultText = line.Substring(index + 1).Trim();
                        break;

                    case "RESULT_PS":
                        model.ResultPaymentServerText = line.Substring(index + 1).Trim();
                        break;

                    case "3DSECURE":
                        model.Secure3DText = line.Substring(index + 1).Trim();
                        break;

                    case "AAV":
                        model.Aav = line.Substring(index + 1).Trim();
                        break;

                    case "RECC_PMNT_ID":
                        model.RegularPaymentId = line.Substring(index + 1).Trim();
                        break;

                    case "RECC_PMNT_EXPIRY":
                        model.RegularPaymentExpiry = line.Substring(index + 1).Trim();
                        break;

                    case "MRCH_TRANSACTION_ID":
                        model.MerchantTransactionId = line.Substring(index + 1).Trim();
                        break;

                    case "WARNING":
                        model.Warning = line.Substring(index + 1).Trim();
                        break;

                    case "FLD_075":
                        model.Fld075 = line.Substring(index + 1).Trim();
                        model.CreditReversalCount = model.Fld075.ToInt32();
                        break;

                    case "FLD_076":
                        model.Fld076 = line.Substring(index + 1).Trim();
                        model.DebitTransactionCount = model.Fld076.ToInt32();
                        break;

                    case "FLD_087":
                        model.Fld087 = line.Substring(index + 1).Trim();
                        model.CreditReversalTotal = model.Fld087.ToInt64();
                        break;

                    case "FLD_088":
                        model.Fld088 = line.Substring(index + 1).Trim();
                        model.DebitTransactionTotal = model.Fld088.ToInt32();
                        break;
                }
            }

            return model;
        }


        /// <summary>
        /// Readdressed url to ECOMM payment server sothat to enter card data.Data is entered using the template provided by the merchant.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public string GetClientRedirectUrl(string transactionId = null)
        {
            if (string.IsNullOrEmpty(_clientUrl))
                throw new ArgumentException("Client Url is required in constructor", nameof(_clientUrl));

            return _clientUrl + (!string.IsNullOrEmpty(transactionId) ? "?trans_id=" + WebUtility.UrlEncode(transactionId) : string.Empty);
        }


        /// <summary>
        /// Registering transactions / Регистрация транзакций
        /// </summary>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with ‘error:‘.</returns>
        public async Task<TransactionResponseViewModel> RegisterTransactionAsync(int amount, ISO4217 currency, string clientIp, string description, string language)
        {
            return await RegisterTransactionAsync(amount, (int)currency, clientIp, description, language);
            //var parameters2 = new Dictionary<string, string>
            //{
            //    ["command"] = "v",
            //    ["amount"] = model.Amount.ToString("F"),
            //    ["currency"] = ((int)model.Currency).ToString("F"),
            //    ["client_ip_addr"] = 
            //};
        }
        public async Task<TransactionResponseViewModel> RegisterTransactionAsync(int amount, int currency, string clientIp, string description, string language)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount parameter is required", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 15)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            //if (string.IsNullOrEmpty(description))
            //    throw new ArgumentException("Description parameter is required", nameof(description));
            if (description != null && description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=v&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&msg_type=SMS");
            var result = Deserialize(response);

            return new TransactionResponseViewModel
            {
                TransactionId = result.TransactionId,

                Error = result.Error,
                Response = response
            };
        }


        /// <summary>
        /// Registering DMS authorization / Регистрация DMS авторизации
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public async Task<TransactionResponseViewModel> RegisterDmsAuthorizationAsync(int amount, ISO4217 currency, string clientIp, string description, string language)
        {
            return await RegisterDmsAuthorizationAsync(amount, (int)currency, clientIp, description, language);
        }
        /// <summary>
        /// Registering DMS authorization / Регистрация DMS авторизации
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language"></param>
        /// <returns>transaction identifier (28 characters in base64 encoding). In case of an error, the returned string of symbols begins with ‘error:‘.</returns>
        public async Task<TransactionResponseViewModel> RegisterDmsAuthorizationAsync(int amount, int currency, string clientIp, string description, string language)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount parameter is required", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 15)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=a&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&msg_type=DMS");
            var result = Deserialize(response);
            return new TransactionResponseViewModel
            {
                TransactionId = result.TransactionId,
                Error = result.Error,
                Response = response
            };
        }

        /// <summary>
        /// Executing a DMS transaction / Выполнение DMS транзакции
        /// </summary>
        /// <param name="authorizationId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language"></param>
        /// <returns>
        /// RESULT: result
        /// RESULT_CODE: result_code
        /// RRN: rrn
        /// APPROVAL_CODE: app_code
        /// CARD_NUMBER pan
        /// 
        /// 
        /// result – transaction results: OK – successful transaction, FAILED – failed transaction
        /// result_code – transaction result code returned from Card Suite Processing RTPS(3 digits)
        /// rrn – retrieval reference number returned from Card Suite Processing RTPS(12 characters)
        /// app_code – approval code returned from Card Suite Processing RTPS(max 6 characters)
        /// pan – masked card number
        /// 
        /// 
        /// Example of the result:
        /// RESULT: OK
        /// RESULT_CODE: 000
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********9999
        /// </returns>
        public async Task<ExecuteDmsTransactionViewModel> ExecuteDmsTransactionAsync(string authorizationId, int amount, ISO4217 currency, string clientIp, string description, string language)
        {
            return await ExecuteDmsTransactionAsync(authorizationId, amount, (int)currency, clientIp, description, language);
        }
        /// <summary>
        /// Executing a DMS transaction / Выполнение DMS транзакции
        /// </summary>
        /// <param name="authorizationId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="clientIp"></param>
        /// <param name="description"></param>
        /// <param name="language"></param>
        /// <returns>
        /// RESULT: result
        /// RESULT_CODE: result_code
        /// RRN: rrn
        /// APPROVAL_CODE: app_code
        /// CARD_NUMBER pan
        /// 
        /// 
        /// result – transaction results: OK – successful transaction, FAILED – failed transaction
        /// result_code – transaction result code returned from Card Suite Processing RTPS(3 digits)
        /// rrn – retrieval reference number returned from Card Suite Processing RTPS(12 characters)
        /// app_code – approval code returned from Card Suite Processing RTPS(max 6 characters)
        /// pan – masked card number
        /// 
        /// 
        /// Example of the result:
        /// RESULT: OK
        /// RESULT_CODE: 000
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********9999
        /// </returns>
        public async Task<ExecuteDmsTransactionViewModel> ExecuteDmsTransactionAsync(string authorizationId, int amount, int currency, string clientIp, string description, string language)
        {
            if (string.IsNullOrEmpty(authorizationId))
                throw new ArgumentException("Authorization ID parameter is required", nameof(authorizationId));
            if (amount <= 0)
                throw new ArgumentException("Amount parameter is required", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 15)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=t&trans_id={WebUtility.UrlEncode(authorizationId)}&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&msg_type=DMS");
            var result = Deserialize(response);

            return new ExecuteDmsTransactionViewModel
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                Rrn = result.Rrn,
                ApprovalCode = result.ApprovalCode,
                CardNumber = result.CardNumber,

                Error = result.Error,
                Response = response
            };
        }


        /// <summary>
        /// Transaction result / Результат транзакции
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="clientIp"></param>
        /// <returns>
        /// RESULT: OK
        /// RESULT_PS: FINISHED
        /// RESULT_CODE: 000
        /// 3DSECURE: ATTEMPTED
        /// RRN: 123456789012
        /// APPROVAL_CODE: 123456
        /// CARD_NUMBER: 9***********9999
        /// RECC_PMNT_ID: 1258
        /// RECC_PMNT_EXPIRY: 1108
        ///  </returns>
        public async Task<GetTransactionResultResponseViewModel> GetTransactionResultAsync(string transactionId, string clientIp)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("Transaction ID parameter is required", nameof(transactionId));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));

            var response = await PostAsync($"command=c&trans_id={WebUtility.UrlEncode(transactionId)}&client_ip_addr={clientIp}");
            var result = Deserialize(response);

            return new GetTransactionResultResponseViewModel
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                ResultPaymentServerText = result.ResultPaymentServerText,
                ResultPaymentServer = result.ResultPaymentServer,
                Secure3DText = result.Secure3DText,
                Secure3D = result.Secure3D,
                Rrn = result.Rrn,
                ApprovalCode = result.ApprovalCode,
                CardNumber = result.CardNumber,
                Aav = result.Aav,
                RegularPaymentId = result.RegularPaymentId,
                RegularPaymentExpiryText = result.RegularPaymentExpiry,
                MerchantTransactionId = result.MerchantTransactionId,

                Error = result.Error,
                Warning = result.Warning,
                Response = response
            };
        }


        /// <summary>
        /// რევერსალი უბრუნებს თანხას წარმატებით საკომისიოს გარეშე იმავე დღეს. მეორე დღეს უკვე მუშავდება როგორც რეფანდი. გამოიყენება პროგრამული/სისტემური შეცდომის დროს.
        /// Transaction reversal / Откат транзакции
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <param name="suspectedFraud">suspected_fraud необязательный параметр – флаг, указывающий, что откат делается из-за подозрения в мошенничестве.В таких случаях значение этого параметра должно быть установлено в "да". Если этот параметр используется, возможен откат только полной суммы.</param>
        /// <returns></returns>
        public async Task<ReverseResponseViewModel> ReverseAsync(string transactionId, int? amount = null, bool suspectedFraud = false)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("TransactionId parameter is required", nameof(transactionId));

            var sb = new StringBuilder($"command=r&trans_id={WebUtility.UrlEncode(transactionId)}");
            if (amount != null)
                sb.Append($"&amount={amount:F0}");
            if (suspectedFraud)
                sb.Append("&suspected_fraud=yes");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new ReverseResponseViewModel
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,

                Error = result.Error,
                Warning = result.Warning,
                Response = response,
            };
        }


        /// <summary>
        /// რეფანდი არის ფინანსურად შესრულებული ოპერაციის თანხის ფინანსურად დაბრუნება, გადარიცხვის საკომისიოს გათვალისწინებით. ძირითადად კლიენტის მოთხოვნით.
        /// Transaction refund / Возврат суммы транзакции
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<RefundResponseViewModel> RefundAsync(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("Transaction ID parameter is required", nameof(transactionId));

            var response = await PostAsync($"command=k&trans_id={WebUtility.UrlEncode(transactionId)}");
            var result = Deserialize(response);

            return new RefundResponseViewModel
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                RefundTransactionId = result.RefundTransactionId,

                Error = result.Error,
                Warning = result.Warning,
                Response = response
            };
        }


        /// <summary>
        /// Credit transaction / Кредит транзакция
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<CreditViewModel> CreditAsync(string transactionId, int? amount = null)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentException("TransactionId parameter is required", nameof(transactionId));

            var sb = new StringBuilder($"command=g&trans_id={WebUtility.UrlEncode(transactionId)}");
            if (amount != null)
                sb.Append($"&amount={amount:F0}");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new CreditViewModel
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                RefundTransactionId = result.RefundTransactionId,

                Error = result.Error,
                Warning = result.Warning
            };
        }



        /// <summary>
        /// End of business day / Завершение бизнес-дня
        /// </summary>
        /// <returns></returns>
        public async Task<CloseDayViewModel> CloseDayAsync()
        {
            var response = await PostAsync("command=b");
            var result = Deserialize(response);

            return new CloseDayViewModel
            {
                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,

                Fld075 = result.Fld075,
                CreditReversalCount = result.CreditReversalCount,
                Fld076 = result.Fld076,
                DebitTransactionCount = result.DebitTransactionCount,
                Fld087 = result.Fld087,
                CreditReversalTotal = result.CreditReversalTotal,
                Fld088 = result.Fld088,
                DebitTransactionTotal = result.DebitTransactionTotal
            };
        }


        /// <summary>
        /// Request for SMS transaction/DMS authorization registration
        /// </summary>
        /// <param name="amount">transaction amount in fractional units, mandatory (up to 12 digits)</param>
        /// <param name="currency">transaction currency code (ISO 4217), mandatory, (3 digits)</param>
        /// <param name="clientIp">client’s IP address, mandatory (15 characters)</param>
        /// <param name="description">transaction details (up to 125 characters)</param>
        /// <param name="language"></param>
        /// <param name="regularPaymentId">merchant-selected regular payment identifier</param>
        /// <param name="expiry">preferred deadline for a regular payment MMYY</param>
        /// <param name="overwriteExistingRecurring">If recurring payment for current client (card) is already defined for template, it needs to be overwritten. Overwriting recurring payments can be done by specifying additional parameter perspayee_overwrite=1. In this case all existing recurring payments for template defined for current client (card) will be deleted.</param>
        /// <param name="dms"></param>
        /// <returns></returns>
        public async Task<RegisterRegularPaymentViewModel> RegisterRegularPaymentAsync(int amount, ISO4217 currency, string clientIp, string description, string language, string regularPaymentId, DateTime expiry, bool overwriteExistingRecurring = false, bool dms = false)
        {
            return await RegisterRegularPaymentAsync(amount, (int)currency, clientIp, description, language, regularPaymentId, expiry, overwriteExistingRecurring, dms);
        }

        /// <summary>
        /// Request for SMS transaction/DMS authorization registration / регистрации регулярного платежа авторизацией с первого платежа:
        /// </summary>
        /// <param name="amount">transaction amount in fractional units, mandatory (up to 12 digits)</param>
        /// <param name="currency">transaction currency code (ISO 4217), mandatory, (3 digits)</param>
        /// <param name="clientIp">client’s IP address, mandatory (15 characters)</param>
        /// <param name="description">transaction details (up to 125 characters)</param>
        /// <param name="language"></param>
        /// <param name="regularPaymentId">merchant-selected regular payment identifier</param>
        /// <param name="expiry">preferred deadline for a regular payment MMYY</param>
        /// <param name="overwriteExistingRecurring">If recurring payment for current client (card) is already defined for template, it needs to be overwritten. Overwriting recurring payments can be done by specifying additional parameter perspayee_overwrite=1. In this case all existing recurring payments for template defined for current client (card) will be deleted.</param>
        /// <param name="dms"></param>
        /// <returns></returns>
        public async Task<RegisterRegularPaymentViewModel> RegisterRegularPaymentAsync(int amount, int currency, string clientIp, string description, string language, string regularPaymentId, DateTime expiry, bool overwriteExistingRecurring = false, bool dms = false)
        {
            if (amount < 0)
                throw new ArgumentException("Amount parameter is invalid", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 15)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));
            //if (string.IsNullOrEmpty(regularPaymentId))
            //    throw new ArgumentException("Merchant-selected regular payment identifier parameter is required", nameof(regularPaymentId));



            var command = amount != 0
                ? dms ? "d" : "z"
                : "p";
            var msgType = amount != 0
                ? dms ? "DMS" : "SMS"
                : "AUTH";


            var sb = new StringBuilder($"command={command}");
            if (amount != 0)
                sb.Append($"&amount={amount:F0}");

            sb.Append($"&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language=<language>&msg_type={msgType}&biller_client_id={regularPaymentId}&perspayee_expiry={expiry:MMyy}&perspayee_gen=1");
            if (overwriteExistingRecurring)
                sb.Append("&perspayee_overwrite=1");

            var response = await PostAsync(sb.ToString());
            var result = Deserialize(response);

            return new RegisterRegularPaymentViewModel
            {
                RegularPaymentId = result.RegularPaymentId,
                TransactionId = result.TransactionId,
                RegularPaymentExpiry = result.RegularPaymentExpiry
            };
        }

        public async Task<ExecuteTransactionViewModel> ExecuteRegularPaymentAsync(int amount, ISO4217 currency, string clientIp, string description, string language, string regularPaymentId)
        {
            return await ExecuteRegularPaymentAsync(amount, (int)currency, clientIp, description, language, regularPaymentId);
        }
        public async Task<ExecuteTransactionViewModel> ExecuteRegularPaymentAsync(int amount, int currency, string clientIp, string description, string language, string regularPaymentId)
        {
            if (amount < 0)
                throw new ArgumentException("Amount parameter is invalid", nameof(amount));
            if (currency <= 0)
                throw new ArgumentException("Currency parameter is required", nameof(currency));
            if (string.IsNullOrEmpty(clientIp))
                throw new ArgumentException("Client IP parameter is required", nameof(clientIp));
            if (clientIp.Length > 15)
                throw new ArgumentException("Client IP parameter max length is 15", nameof(clientIp));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description parameter is required", nameof(description));
            if (description.Length > 125)
                throw new ArgumentException("Description parameter max length is 125", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language parameter is required", nameof(language));
            if (language.Length > 32)
                throw new ArgumentException("Language parameter max length is 32", nameof(language));

            var response = await PostAsync($"command=e&amount={amount:F0}&currency={currency:F0}&client_ip_addr={clientIp}&description={description}&language={language}&biller_client_id={WebUtility.UrlEncode(regularPaymentId)}");
            var result = Deserialize(response);

            return new ExecuteRegularPaymentViewModel
            {
                TransactionId = result.TransactionId,

                ResultText = result.ResultText,
                Result = result.Result,
                ResultCode = result.ResultCode,
                Rrn = result.Rrn,
                ApprovalCode = result.ApprovalCode,

                Error = result.Error,
                Response = response
            };
        }


    }


}
