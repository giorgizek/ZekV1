using System.Data.SqlClient;
using System.Data;
using Zek.Configuration;

namespace Zek.Data
{
    /// <summary>
    /// მონაცემთა ბაზის დამხმარე კლასი.
    /// </summary>
    public class DatabaseHelper
    {
        public static void ExecuteGetButtonBrowse(BindingDataEventArgs e)
        {
            ExecuteGetButtonBrowse(e, "dbo.SP_GetButtonBrowse");
        }
        public static void ExecuteGetButtonBrowse(BindingDataEventArgs e, string storedProcedureName)
        {

            using (var conn = new SqlConnection(BaseAppConfig.ConnectionString))
            {
                var cmd = new SqlCommand(storedProcedureName, conn) { CommandType = CommandType.StoredProcedure };
                if (BaseAppConfig.CommandTimeout != null)
                    cmd.CommandTimeout = BaseAppConfig.CommandTimeout.Value;

                cmd.Parameters.Add("@objectName", SqlDbType.NVarChar).Value = e.DatabaseObjectName;
                cmd.Parameters.Add("@paramInt", SqlDbType.Int).Value = e.ParamInt;
                cmd.Parameters.Add("@paramString", SqlDbType.NVarChar).Value = e.ParamString;
                cmd.Parameters.Add("@paramGuid", SqlDbType.UniqueIdentifier).Value = e.ParamGuid;
                cmd.Parameters.Add("@paramDateTime", SqlDbType.DateTime).Value = e.ParamDateTime;


                conn.Open();
                var obj = cmd.ExecuteScalar();
                if (obj != null)
                    e.Text = obj.ToString();
            }
        }

        public static void ExecuteActionRecord(ActionRecordEventArgs e)
        {
            ExecuteActionRecord(e, "dbo.SP_ActionRecord");
        }
        public static void ExecuteActionRecord(ActionRecordEventArgs e, string storedProcedureName)
        {
            using (var conn = new SqlConnection(BaseAppConfig.ConnectionString))
            {
                var cmd = new SqlCommand(storedProcedureName, conn) { CommandType = CommandType.StoredProcedure };
                if (BaseAppConfig.CommandTimeout != null)
                    cmd.CommandTimeout = BaseAppConfig.CommandTimeout.Value;


                cmd.Parameters.Add("@objectName", SqlDbType.NVarChar).Value = e.ObjectName;
                cmd.Parameters.Add("@paramInt", SqlDbType.Int).Value = e.ParamInt;
                cmd.Parameters.Add("@paramString", SqlDbType.NVarChar).Value = e.ParamString;
                cmd.Parameters.Add("@paramGuid", SqlDbType.UniqueIdentifier).Value = e.ParamGuid;
                cmd.Parameters.Add("@paramDateTime", SqlDbType.DateTime).Value = e.ParamDateTime;
                cmd.Parameters.Add("@action", SqlDbType.Int).Value = (int)e.Action;
                cmd.Parameters.Add("@modifierID", SqlDbType.Int).Value = e.ModifierID;

                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();
            }
        }
    }
}
