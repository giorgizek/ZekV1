using System;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace Zek.Data.Serialization
{
    [Serializable]
    public class SqlParameterMessage
    {
        private SqlParameterMessage() { }
        public SqlParameterMessage(SqlParameter param)
        {
            ParameterName = param.ParameterName;
            SqlDbTypeID = (int)param.SqlDbType;

            Value = SqlCommandMessage.ConverToString(param);
        }

        [XmlAttribute("ParameterName")]
        public string ParameterName;

        [XmlAttribute("SqlDbTypeID")]
        public int SqlDbTypeID;

        [XmlAttribute("Value")]
        public string Value;
    }
}
