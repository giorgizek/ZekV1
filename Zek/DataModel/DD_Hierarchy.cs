using System;
using System.Xml;
using System.Xml.Serialization;

namespace Zek.DataModel
{
    [Serializable]
    public class DD_Hierarchy
    {
        [XmlAttribute("ID")]
        public string XmlID
        {
            get { return ID.HasValue ? XmlConvert.ToString(ID.Value) : null; }
            set { ID = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
        }
        [XmlIgnore]
        public int? ID { get; set; }

        [XmlAttribute("ParentID")]
        public string XmlParentID
        {
            get { return ParentID.HasValue ? XmlConvert.ToString(ParentID.Value) : null; }
            set { ParentID = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
        }
        [XmlIgnore]
        public int? ParentID { get; set; }

        [XmlAttribute]
        public string Code { get; set; }
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Level { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DD_Hierarchy && (ID == ((DD_Hierarchy)obj).ID);
        }
        public bool Equals(DD_Hierarchy obj)
        {
            return Equals(this, obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
