using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Zek.Data
{
    [DataContract]
    [Serializable]
    public class WSRequest<T> : BaseRequest
    {
        public WSRequest()
        {
        }
        public WSRequest(string applicationKey, string requestID, DateTime? timestamp = null, string language = null)
            : base(applicationKey, requestID, timestamp, language)
        {

        }

        [DataMember]
        [XmlElement]
        public T Value { get; set; }
    }
}
