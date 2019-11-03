using System;
using System.Web;

namespace Zek.Service.SMS
{
    public class MagtiHttpToSMS : HttpToSmsBase
    {
        public class SmscReturnCodes
        {
            public static string Successfull = "0000";
            public static string InternalError = "0001";
            public static string NoChargeGiven = "0002";
            public static string InvalidRequest = "0003";
            public static string InvalidQuery = "0004";
            public static string EmptyMessage = "0005";
            public static string PrefixError = "0006";
            public static string MsisdnError = "0007";
        }

        public static string Url = "http://81.95.160.47/mt/sendsms";

        public static string GetFormatedURL(string userName, string password, string clientID, string mobile, string text)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("userName is null or empty");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("password is null or empty");
            if (string.IsNullOrWhiteSpace(clientID))
                throw new ArgumentException("clientID is null or empty");
            if (string.IsNullOrWhiteSpace(mobile))
                throw new ArgumentException("mobile is null or empty");
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            return $"{Url}?username={userName}&password={password}&client_id={clientID}&service_id=1&type=text&to={ParseMobile(mobile)}&text={HttpUtility.UrlEncode(text)}";
        }
        public static string Send(string userName, string password, string clientID, string mobile, string text)
        {
            return SendWebRequest(GetFormatedURL(userName, password, clientID, mobile, text));
        }
    }
}
