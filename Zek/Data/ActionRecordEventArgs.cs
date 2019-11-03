using System;
using Zek.Core;

namespace Zek.Data
{
    public class ActionRecordEventArgs : EventArgs
    {
        public ActionRecordEventArgs() { }
        public ActionRecordEventArgs(int paramInt, string paramString, Guid paramGuid, DateTime paramDateTime, DatabaseAction action)
            : this(null, paramInt, paramString, paramGuid, paramDateTime, action)
        {
        }
        public ActionRecordEventArgs(string objectName, int paramInt, string paramString, Guid paramGuid, DateTime paramDateTime, DatabaseAction action)
            : this(objectName, paramInt, paramString, paramGuid, paramDateTime, action, BaseGlobalVariable.UserID)
        {
        }
        public ActionRecordEventArgs(string objectName, int paramInt, string paramString, Guid paramGuid, DateTime paramDateTime, DatabaseAction action, int modifierID)
        {
            ObjectName = objectName;
            ParamInt = paramInt;
            ParamString = paramString;
            ParamGuid = paramGuid;
            ParamDateTime = paramDateTime;
            Action = action;
            ModifierID = modifierID;
        }


        public string ObjectName { get; set; }

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

        public DatabaseAction Action { get; set; }

        public int ModifierID { get; set; }
    }
}
