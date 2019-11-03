using System;
using System.Xml;
using System.Xml.Serialization;

namespace Zek.DataModel
{
    //[Serializable]
    //public class DD_Dictionary // : ISerializable
    //{
    //    public DD_Dictionary() { }
    //    public DD_Dictionary(int? id, string code, string name)
    //    {
    //        ID = id;
    //        Code = code;
    //        Name = name;
    //    }

    //    [XmlAttribute("ID")]
    //    public string XmlID
    //    {
    //        //get { return ID.HasValue ? XmlConvert.ToString(ID.Value) : null; }
    //        get { return ID.HasValue ? ID.Value.ToString() : null; }
    //        set { ID = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
    //    }
    //    [XmlIgnore]
    //    public int? ID { get; set; }

    //    [XmlAttribute]
    //    public string Code { get; set; }
    //    [XmlAttribute]
    //    public string Name { get; set; }

    //    public static bool operator ==(DD_Dictionary a, DD_Dictionary b)
    //    {
    //        // If both are null, or both are same instance, return true.
    //        if (object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }

    //        // If one is null, but not both, return false.
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }

    //        // Return true if the fields match:
    //        return a.ID == b.ID;
    //    }

    //    public static bool operator !=(DD_Dictionary a, DD_Dictionary b)
    //    {
    //        return !(a == b);
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        return ((obj is DD_Dictionary) && (this == (DD_Dictionary)obj));
    //    }
    //    public bool Equals(DD_Dictionary obj)
    //    {
    //        return this == obj;
    //    }
    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }        
    //}


    [Serializable]
    public class DD_Dictionary<T> : ICloneable
    //where T : IComparable//,  IConvertible//, IComparable<T>//, IEquatable<T>
    {
        public DD_Dictionary()
        {
            //var type = typeof(T);
            //if (type != typeof(bool)
            //        && type != typeof(byte) && type != typeof(short) && type != typeof(int) && type != typeof(long)
            //        && type != typeof(sbyte) && type != typeof(ushort) && type != typeof(uint) && type != typeof(ulong)
            //        && type != typeof(decimal) && type != typeof(double) && type != typeof(float)
            //        && type != typeof(string)
            //        && type != typeof(Guid)
            //        && type != typeof(DateTime) && type != typeof(DateTimeOffset))
            //    throw new ArgumentException("Generit Type can't be " + type.FullName, "T");
        }
        public DD_Dictionary(T id, string code, string name)
            : this()
        {
            ID = id;
            Code = code;
            Name = name;
        }


        ///IsNullable = false
        [XmlAttribute("ID")]
        public string XmlID
        {
            get
            {
                if (ID == null || (object)ID == DBNull.Value) return null;

                var type = typeof(T);

                if (type == typeof(bool))
                    return XmlConvert.ToString((bool)(object)ID);

                if (type == typeof(byte))
                    return XmlConvert.ToString((byte)(object)ID);
                if (type == typeof(short))
                    return XmlConvert.ToString((short)(object)ID);
                if (type == typeof(int))
                    return XmlConvert.ToString((int)(object)ID);
                if (type == typeof(long))
                    return XmlConvert.ToString((long)(object)ID);

                if (type == typeof(sbyte))
                    return XmlConvert.ToString((sbyte)(object)ID);
                if (type == typeof(ushort))
                    return XmlConvert.ToString((ushort)(object)ID);
                if (type == typeof(uint))
                    return XmlConvert.ToString((uint)(object)ID);
                if (type == typeof(ulong))
                    return XmlConvert.ToString((ulong)(object)ID);

                if (type == typeof(decimal))
                    return XmlConvert.ToString((decimal)(object)ID);
                if (type == typeof(double))
                    return XmlConvert.ToString((double)(object)ID);
                if (type == typeof(float))
                    return XmlConvert.ToString((float)(object)ID);

                if (type == typeof(string))
                    return (string)(object)ID;
                if (type == typeof(Guid))
                    return XmlConvert.ToString((Guid)(object)ID);
                if (type == typeof(DateTime))
                    return XmlConvert.ToString((DateTime)(object)ID, XmlDateTimeSerializationMode.Local);
                if (type == typeof(DateTimeOffset))
                    return XmlConvert.ToString((DateTimeOffset)(object)ID);

                return ID.ToString();
                //throw new InvalidCastException("Can't cast ID value into xml formatted string.");
            }
            set
            {
                if (value == null) ID = default(T);

                var type = typeof(T);

                if (type == typeof(bool))
                    ID = (T)(object)XmlConvert.ToBoolean(value);

                else if (type == typeof(byte))
                    ID = (T)(object)XmlConvert.ToByte(value);
                else if (type == typeof(short))
                    ID = (T)(object)XmlConvert.ToInt16(value);
                else if (type == typeof(int))
                    ID = (T)(object)XmlConvert.ToInt32(value);
                else if (type == typeof(long))
                    ID = (T)(object)XmlConvert.ToInt64(value);

                else if (type == typeof(sbyte))
                    ID = (T)(object)XmlConvert.ToSByte(value);
                else if (type == typeof(ushort))
                    ID = (T)(object)XmlConvert.ToUInt16(value);
                else if (type == typeof(uint))
                    ID = (T)(object)XmlConvert.ToUInt32(value);
                else if (type == typeof(ulong))
                    ID = (T)(object)XmlConvert.ToUInt64(value);

                else if (type == typeof(decimal))
                    ID = (T)(object)XmlConvert.ToDecimal(value);
                else if (type == typeof(double))
                    ID = (T)(object)XmlConvert.ToDouble(value);
                else if (type == typeof(float))
                    ID = (T)(object)XmlConvert.ToSingle(value);

                else if (type == typeof(string))
                    ID = (T)(object)value;
                else if (type == typeof(Guid))
                    ID = (T)(object)XmlConvert.ToGuid(value);
                else if (type == typeof(DateTime))
                    ID = (T)(object)XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Local);
                else if (type == typeof(DateTimeOffset))
                    ID = (T)(object)XmlConvert.ToDateTimeOffset(value);
                else
                    throw new InvalidCastException("Can't cast ID value into xml formatted string.");
            }
        }
        [XmlIgnore]
        public T ID { get; set; }

        [XmlAttribute]
        public string Code { get; set; }
        [XmlAttribute]
        public string Name { get; set; }

        public static bool operator ==(DD_Dictionary<T> a, DD_Dictionary<T> b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.ID.Equals(b.ID);
        }
        public static bool operator !=(DD_Dictionary<T> a, DD_Dictionary<T> b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            return obj is DD_Dictionary<T> && (this == (DD_Dictionary<T>)obj);
        }
        public bool Equals(DD_Dictionary<T> obj)
        {
            return this == obj;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }





        //Deserialization constructor.
        //public DD_Dictionary(SerializationInfo info, StreamingContext ctxt)
        //{
        //    //Get the values from info and assign them to the appropriate properties
        //    ID = (int?)info.GetValue("ID", typeof(int?));
        //    Code = (string)info.GetValue("Code", typeof(string));
        //    Name = (string)info.GetValue("Name", typeof(string));
        //}

        //Serialization function.
        //public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        //{
        //    //You can use any custom name for your name-value pair. But make sure you
        //    // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
        //    // then you should read the same with "EmployeeId"
        //    info.AddValue("ID", ID);
        //    info.AddValue("Code", Code);
        //    info.AddValue("Name", Name);
        //}


        public DD_Dictionary<T> Copy()
        {
            return new DD_Dictionary<T>(ID, Code, Name);
        }
        public object Clone()
        {
            return Copy();
        }
    }



    //[Serializable]
    //public class ListOfDD_Dictionary<T> : List<DD_Dictionary<T>>, ICloneable
    //{
    //    public ListOfDD_Dictionary()
    //    {
    //    }
    //    public ListOfDD_Dictionary(IEnumerable<DD_Dictionary<T>> collection)
    //        : base(collection)
    //    {
    //    }
    //    public ListOfDD_Dictionary(int count)
    //        : base(count)
    //    {
    //    }

    //    public ListOfDD_Dictionary<T> Copy()
    //    {
    //        var result = new ListOfDD_Dictionary<T>(Count);
    //        for (int i = 0; i < Count; i++)
    //        {
    //            if (this[i] == null)
    //                result.Add(null);

    //            result.Add(this[i].Copy());
    //        }

    //        return result;
    //    }

    //    public object Clone()
    //    {
    //        return Copy();
    //    }
    //}
}
