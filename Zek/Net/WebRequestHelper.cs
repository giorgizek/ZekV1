using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Web;
using System.IO;
using Zek.Data.Serialization;

namespace Zek.Net
{
    public class WebRequestHelper
    {
        public static string SendData(string url, string method, Dictionary<string, string> args)
        {
            var requestData = new StringBuilder();
            if (args != null)
            {
                var i = 0;
                foreach (var key in args.Keys)
                {
                    i++;
                    if (i > 1)
                        //requestData += '&';
                        requestData.Append('&');

                    //requestData += HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(parameters[key]);
                    requestData.Append(HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(args[key]));
                }
            }

            return SendData(url, method, requestData.ToString());
        }
        public static string SendData(string url, string method, string requestData)
        {
            // ქმნის მოთხოვნას და ვუთითებთ URL-ს რომელიც მიიღებს ამ მოთხოვნას.
            var request = WebRequest.Create(url);

            // უყენებს მნიშვნელობას მოთხოვნას (GET, POST).
            request.Method = method;

            //აკონვერტირებ ბაიტების მასივში გადასაგზავნ მონაცემს.
            var byteArray = Encoding.UTF8.GetBytes(requestData);

            // ვუთითებთ კონტენტის ტიპის მნიშვნელობას.
            request.ContentType = "application/x-www-form-urlencoded";

            // ვუთითებთ კონკენტის ზომას.
            request.ContentLength = byteArray.Length;



            // ვიღებთ request stream-ს.
            var dataStream = request.GetRequestStream();

            // ვწერთ მონაცემს request stream-ში.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // ვიღებთ request stream-ს.
            // და ვწერთ მონაცემს request stream-ში.
            //using (Stream dataStream = request.GetRequestStream())
            //{
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //}



            // ვხურავთ Stream-ის ობიექტს.
            dataStream.Close();

            // ვიღებთ პასუხს.
            var response = (HttpWebResponse)request.GetResponse();

            // ვიღებთ სტატუსს.
            //((HttpWebResponse)response).StatusDescription;

            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(dataStream);

            // ვკითხულოვბთ სერვერიდან დაბრუნებულ კონტენტს.
            var responseFromServer = reader.ReadToEnd();



            //string responseFromServer = string.Empty;
            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    // Get the response stream  
            //    StreamReader reader = new StreamReader(response.GetResponseStream());

            //    // Console application output  
            //    responseFromServer += reader.ReadToEnd();
            //}



            // ასუფთავებს ნაკადებს
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }


        public static string DownloadString(string address)
        {
            return DownloadString(address, WebRequestMethods.Http.Get);
        }
        public static string DownloadString(string address, string requestMethod)
        {
            return DownloadString(address, requestMethod, null);
        }
        public static string DownloadString(string address, string requestMethod, string requestUserAgent)
        {
            return DownloadString(address, requestMethod, requestUserAgent, 100000);
        }
        public static string DownloadString(string address, string requestMethod, string requestUserAgent, int requestTimeout)
        {
            var responseFromServer = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(address);
            request.Timeout = requestTimeout;
            request.Method = requestMethod;
            request.UserAgent = requestUserAgent;
            request.Referer = address;


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                    }
                    dataStream.Close();
                }
                response.Close();
            }
            return responseFromServer;
           
        }
        //public static string DownloadString(string address, string requestMethod, string requestUserAgent, int requestTimeout)
        //{
        //    var downloadRequest = (HttpWebRequest)HttpWebRequest.Create(address);
        //    {
        //        downloadRequest.Timeout = requestTimeout;
        //        downloadRequest.Method = requestMethod;
        //        downloadRequest.UserAgent = requestUserAgent;
        //        downloadRequest.Referer = address;
        //    }

        //    using (var downloadResponse = (HttpWebResponse)downloadRequest.GetResponse())
        //    {
        //        using (StreamReader responseStreamReader = new StreamReader(downloadResponse.GetResponseStream()))
        //        {
        //            return responseStreamReader.ReadToEnd();
        //        }
        //    }
        //}



        public static T InvokeWebService<T>(string url, string methodName)
        {
            return InvokeWebService<T>(url, methodName, null);
        }
        public static T InvokeWebService<T>(string url, string methodName, Dictionary<string, string> args)
        {
            return SerializationHelper.DeserializeXmlString<T>(SendData(url + "/" + methodName, "POST", args).Replace(" xmlns=\"http://tempuri.org/\"", ""), false);
        }
    }
}
