using System;

namespace Zek.Service.SMS
{
    public class GeocellHttpToSMSHelper : HttpToSmsBase
    {
        public static string Url = "http://91.151.128.64:7777/pls/sms/phttp2sms.Process";

        public static string GetFormatedURL(string geocellID, string mobile, string text)
        {
            if (string.IsNullOrWhiteSpace(geocellID))
                throw new ArgumentException("geocellID is null or empty");
            if (string.IsNullOrWhiteSpace(mobile))
                throw new ArgumentException("mobile is null or empty");
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            return $"{Url}?src={geocellID}&dst={ParseMobile(mobile)}&txt={text}";
        }
        /// <summary>
        /// გზავნის მესიჯს ჯეოსელის სერვისით.
        /// </summary>
        /// <param name="geocellID">Unique identifier of CC (is provided by Geocell)</param>
        /// <param name="mobile">Subscribers GSM number in format (995577######, 995593######,995555######)</param>
        /// <param name="text">SMS text</param>
        public static string Send(string geocellID, string mobile, string text)
        {
            return SendWebRequest(GetFormatedURL(geocellID, mobile, text));
        }
    }
}
