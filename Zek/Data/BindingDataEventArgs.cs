using System;
using Zek.Core;

namespace Zek.Data
{
    public interface IBindingDataEventArgs
    {
        string DatabaseObjectName { get; set; }
        int ParamInt { get; set; }
        string ParamString { get; set; }
        Guid ParamGuid { get; set; }
        DateTime ParamDateTime { get; set; }
        string Text { get; set; }
    }

    public class BindingDataEventArgs : EventArgs, IBindingDataEventArgs
    {
        public BindingDataEventArgs() { }
        public BindingDataEventArgs(int paramInt, string paramString, Guid paramGuid, DateTime paramDateTime) : this(null, paramInt, paramString, paramGuid, paramDateTime) { }
        public BindingDataEventArgs(string databaseObjectName, int paramInt, string paramString, Guid paramGuid, DateTime paramDateTime)
        {
            DatabaseObjectName = databaseObjectName;
            ParamInt = paramInt;
            ParamString = paramString;
            ParamGuid = paramGuid;
            ParamDateTime = paramDateTime;
        }

        public int ParamInt { get; set; }

        private string _paramString = string.Empty;
        public string ParamString
        {
            get { return _paramString; }
            set { if (value == null) value = string.Empty; _paramString = value; }
        }

        private Guid _paramGuid = Guid.Empty;
        public Guid ParamGuid
        {
            get { return _paramGuid; }
            set { _paramGuid = value; }
        }

        private DateTime _paramDateTime = DateTimeHelper.MinDate;
        public DateTime ParamDateTime
        {
            get { return _paramDateTime; }
            set
            {
                if (value < DateTimeHelper.MinDate)
                    value = DateTimeHelper.MinDate;
                else if (value > DateTimeHelper.MaxDate)
                    value = DateTimeHelper.MaxDate;

                _paramDateTime = value;
            }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { if (value == null) value = string.Empty; _text = value; }
        }

        public string DatabaseObjectName { get; set; }
    }
}
