using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Zek.Data
{
    /// <summary>
    /// მოთხოვნის ჰედერის სტანდარტული ვარიანტი
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    [Serializable]
    public class WSResponse<T> : BaseResponse
    {
        public WSResponse()
        {
        }

        public void Fill(BaseRequest request)
        {
            
        }

        public WSResponse(string requestID, int errorCode = 0, string errorMessage = null)
            : base(requestID, errorCode, errorMessage)
        {

        }

        [DataMember]
        [XmlElement]
        public T Value { get; set; }
    }
}
