using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Zek.Extensions;

namespace Zek.Data
{
    /// <summary>
    /// მოთხოვნის ჰედერი, მას გადაეცემა ორი აუცილებელი პარამეტრი ApplicationKey და RequestId.
    /// </summary>
    [DataContract]
    [Serializable]
    public class BaseRequest
    {
        public BaseRequest()
        {
        }
        public BaseRequest(string applicationKey, string requestID, DateTime? timestamp = null, string language = null)
            : this()
        {
            ApplicationKey = applicationKey;
            RequestID = requestID;
            Timestamp = timestamp ?? DateTime.Now;
            Language = language;
        }

        /// <summary>
        /// აპლიკაციის იდენტიფიკატორი.
        /// </summary>
        [DataMember(IsRequired = true, EmitDefaultValue = true, Name = "ApplicationKey", Order = 1)]
        [XmlElement]
        public string ApplicationKey { get; set; }

        /// <summary>
        /// რექვესტის იდენთიფიკატორის ჰეში. ამით ხდება სერვისის ვალიდაცია.
        /// </summary>
        [DataMember]
        [XmlElement]
        public string RequestHash { get; set; }

        /// <summary>
        /// მოთხოვნის იდენტიფიკატორი.
        /// </summary>
        [DataMember(IsRequired = true, EmitDefaultValue = true, Name = "RequestID", Order = 2)]
        [XmlElement]
        public string RequestID { get; set; }



        /// <summary>
        /// მოთხოვნის დრო.
        /// </summary>
        [DataMember]
        [XmlIgnore]
        public DateTime Timestamp { get; set; }

        [IgnoreDataMember]
        [XmlElement("Timestamp")]
        public string TimestampString
        {
            get { return Timestamp.ToUniversalDateTimeMillisecondString(); }
            set { Timestamp = value.ParseUniversalDateTime(); }
        }



        /// <summary>
        /// ენის კოდი ka-GE, en-US, ru-RU
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = true, Name = "Language", Order = 4)]
        [XmlElement]
        public string Language { get; set; }


        //public bool IsValidRequestHash(string hash)
        //{
        //    return hash == PasswordCryptoHelper.EncryptRequestID(RequestID, Timestamp);
        //}
    }
}
