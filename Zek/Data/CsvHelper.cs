using System.Data.OleDb;

namespace Zek.Data
{
    public class CsvHelper : OleDbHelper
    {
        protected CsvHelper() { }
        public static OleDbConnection GetOleDbConnection(string cvsDir)
        {
            return new OleDbConnection(GetOleDbConnectionString(cvsDir));
        }
        public static string GetOleDbConnectionString(string cvsDir)
        {
            var builder = new OleDbConnectionStringBuilder { Provider = "Microsoft.Jet.OLEDB.4.0", DataSource = cvsDir };
            builder.Add("Extended Properties", "text;HDR=Yes;FMT=Delimited");

            return builder.ConnectionString;
        }
    }
}
