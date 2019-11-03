using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Zek.Service.SMS
{
    public class HttpToSmsBase
    {
        public static string ParseMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
                return mobile;

            var result = new string(mobile.ToCharArray().Where(char.IsDigit).ToArray());

            //foreach (var c in mobile)
            //    if (char.IsDigit(c)) result += c;

            if (result.StartsWith("995") && result.Length == 11)
                result = result.Insert(3, "5");
            else if (result.StartsWith("8") && result.Length == 9)
                result = "9955" + result.Substring(1);
            else if (result.StartsWith("5") && result.Length == 9)
                result = "995" + result;
            return result;
        }
        public static string SendWebRequest(string url)
        {
            var http = (HttpWebRequest)WebRequest.Create(new Uri(url));
            var response = http.GetResponse();

            string content;
            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                    throw new NullReferenceException("HttpWebRequest.GetResponse().GetResponseStream() is null");
                var sr = new StreamReader(stream);
                content = sr.ReadToEnd();
            }
            return content;



            //var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            //request.Credentials = CredentialCache.DefaultCredentials;
            //using (var response = request.GetResponse())
            //{
            //    using (var dataStream = response.GetResponseStream())
            //    {
            //        using (var reader = new StreamReader(dataStream))
            //        {
            //            content = reader.ReadToEnd();
            //            reader.Close();
            //        }
            //        dataStream.Close();
            //    }
            //    response.Close();
            //}

            //return content;
        }
    }
}
