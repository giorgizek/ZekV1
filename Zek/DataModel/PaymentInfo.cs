using System;
using System.Xml.Serialization;

namespace Zek.DataModel
{
    /// <summary>
    /// გადახდის ინფორმაციული კლასი.
    /// </summary>
    [Serializable]
    public class PaymentInfo
    {
        /// <summary>
        /// გადახდის თარიღი
        /// </summary>
        [XmlAttribute]
        public DateTime Date { get; set; }

        /// <summary>
        /// თანხა.
        /// </summary>
        [XmlAttribute]
        public decimal Amount { get; set; }
    }
}
