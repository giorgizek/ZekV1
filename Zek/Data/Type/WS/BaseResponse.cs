using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Zek.Extensions;

namespace Zek.Data
{
    /// <summary>
    /// მოთხოვნის პასუხის ჰედერი, ის აბრუნებს ორ აუცილებელ პარამეტრს RequestId და Timestamp.
    /// </summary>
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse()
        {
            Timestamp = DateTime.Now;
        }
        public BaseResponse(string requestID, int errorCode = 0, string errorMessage = null)
            : this()
        {
            RequestID = requestID;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            //IsCopy = isCopy;
        }


        /// <summary>
        /// რექვესტის იდენთიფიკატორის ჰეში. ამით ხდება სერვისის ვალიდაცია.
        /// </summary>
        [DataMember]
        [XmlElement]
        public string RequestHash { get; set; }

        /// <summary>
        /// მოთხოვნის იდენტიფიკატორი.
        /// </summary>
        [DataMember]
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
        /// შეცდომის კოდი.
        /// </summary>
        [DataMember]
        [XmlElement]
        public int ErrorCode { get; set; }


        /// <summary>
        /// შეცდომის ტექსტი.
        /// </summary>
        [DataMember]
        [XmlElement]
        public string ErrorMessage { get; set; }


        /// <summary>
        /// შეცდომის ტექსტი.
        /// </summary>
        [DataMember]
        [XmlElement]
        public string Exception { get; set; }
    }
}
