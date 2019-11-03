using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;

namespace Zek.Data.Serialization
{
    [Serializable]
    public sealed class SqlCommandMessage
    {
        private SqlCommandMessage() { }
        public SqlCommandMessage(SqlCommand cmd)
        {
            InitBySqlCommand(cmd);
        }
        public SqlCommandMessage(string serializedString)
        {
            var msg = SerializationHelper.DeserializeXmlString<SqlCommandMessage>(serializedString);
            InitBySqlCommand(msg.Command);
        }

        [NonSerialized]
        private SqlCommand _command;
        [XmlIgnore]
        public SqlCommand Command
        {
            get
            {
                if (_command == null)
                    CreateSqlCommand();

                return _command;
            }
        }

        public string CommandText;
        public CommandType CommandType;
        public int CommandTimeout;
        public List<SqlParameterMessage> Parameters = new List<SqlParameterMessage>();

        private void InitBySqlCommand(SqlCommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _command = cmd;

            CommandText = cmd.CommandText;
            CommandTimeout = cmd.CommandTimeout;
            CommandType = cmd.CommandType;
            foreach (SqlParameter param in cmd.Parameters)
            {
                Parameters.Add(new SqlParameterMessage(param));
            }
        }
        public void CreateSqlCommand()
        {
            _command = new SqlCommand(CommandText) { CommandType = CommandType };
            foreach (var param in Parameters)
            {
                _command.Parameters.Add(param.ParameterName, (SqlDbType)param.SqlDbTypeID).Value = ConverToSqlDBType(param);
            }
        }
        public string XmlSerialize()
        {
            return SerializationHelper.SerializeXmlString(this);
        }

        public static object ConverToSqlDBType(SqlParameterMessage param)
        {
            var sqlDbType = (SqlDbType)param.SqlDbTypeID;

            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    return XmlConvert.ToInt64(param.Value);

                //case SqlDbType.Binary:

                case SqlDbType.Bit:
                    return XmlConvert.ToBoolean(param.Value);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return param.Value;

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    return XmlConvert.ToDateTime(param.Value, XmlDateTimeSerializationMode.RoundtripKind);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return XmlConvert.ToDecimal(param.Value);

                case SqlDbType.Float:
                    return XmlConvert.ToDouble(param.Value);

                //case SqlDbType.Image:

                case SqlDbType.Int:
                    return XmlConvert.ToInt32(param.Value);

                case SqlDbType.Real:
                    return XmlConvert.ToSingle(param.Value);

                case SqlDbType.UniqueIdentifier:
                    return XmlConvert.ToGuid(param.Value);

                case SqlDbType.SmallInt:
                    return XmlConvert.ToInt16(param.Value);

                case SqlDbType.Timestamp:
                    return XmlConvert.ToTimeSpan(param.Value);

                case SqlDbType.TinyInt:
                    return XmlConvert.ToByte(param.Value);

                //case SqlDbType.VarBinary:
                //case SqlDbType.Variant:
                //case SqlDbType.Udt:
                //case SqlDbType.Structured:

                default:
                    return param.Value;
            }
        }
        public static string ConverToString(SqlParameter param)
        {
            switch (param.SqlDbType)
            {
                case SqlDbType.BigInt:
                    return XmlConvert.ToString((long)param.Value);

                //case SqlDbType.Binary:

                case SqlDbType.Bit:
                    return XmlConvert.ToString((bool)param.Value);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return (string)param.Value;

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    return XmlConvert.ToString((DateTime)param.Value, XmlDateTimeSerializationMode.RoundtripKind);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return XmlConvert.ToString((decimal)param.Value);

                case SqlDbType.Float:
                    return XmlConvert.ToString((double)param.Value);
                    
                //case SqlDbType.Image:

                case SqlDbType.Int:
                    return XmlConvert.ToString((int)param.Value);

                case SqlDbType.Real:
                    return XmlConvert.ToString((float)param.Value);

                case SqlDbType.UniqueIdentifier:
                    return XmlConvert.ToString((Guid)param.Value);

                case SqlDbType.SmallInt:
                    return XmlConvert.ToString((short)param.Value);

                case SqlDbType.Timestamp:
                    return XmlConvert.ToString((TimeSpan)param.Value);

                case SqlDbType.TinyInt:
                    return XmlConvert.ToString((byte)param.Value);

                //case SqlDbType.VarBinary:
                //case SqlDbType.Variant:
                //case SqlDbType.Udt:
                //case SqlDbType.Structured:

                default:
                    return param.Value.ToString();
            }
        }
    }
}
