using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Web;

namespace Zek.Data
{
    public class ExcelHelper
    {
        public static string GetConnectionString(string filePath)
        {
            var builder = new OleDbConnectionStringBuilder();
            var ext = Path.GetExtension(filePath);
            builder.DataSource = filePath;
            builder.Provider = ext == ".xlsx" ? "Microsoft.ACE.OLEDB.12.0" : "Microsoft.Jet.OLEDB.4.0";
            builder["Extended Properties"] = "Excel " + (ext == ".xlsx" ? "12.0" : "8.0") + ";HDR=Yes;IMEX=1";
            return builder.ConnectionString;
        }

        public static DataTable GetDataTable(string filePath, string headerNames = "*", string sheetName = "Sheet1$")
        {
            if (!sheetName.EndsWith("$")) sheetName += "$";

            return OleDbHelper.ExecuteDataTable(GetConnectionString(filePath), CommandType.Text, $"SELECT {headerNames} FROM [{sheetName}]");

            //using (var conn = new OleDbConnection(GetConnectionString(filePath)))
            //{
            //    try
            //    {
            //        conn.Open();
            //        var cmd = new OleDbCommand(string.Format("SELECT {0} FROM [{1}]", headerNames, sheetName), conn);
            //        var adapter = new OleDbDataAdapter { SelectCommand = cmd };
            //        var table = new DataTable();
            //        adapter.Fill(table);
            //        conn.Close();
            //        return table;
            //    }
            //    finally
            //    {
            //        conn.Close();
            //    }
            //}
        }

        public static List<T> ExecuteList<T>(string filePath, string headerNames = "*", string sheetName = "Sheet1$")
        {
            if (!sheetName.EndsWith("$")) sheetName += "$";
            return OleDbHelper.ExecuteList<T>(GetConnectionString(filePath), CommandType.Text, $"SELECT {headerNames} FROM [{sheetName}]");
        }


        /*Excel.ApplicationClass _excel = new Excel.ApplicationClass();        

        public Worksheet Activate()
        {
            // open new excel spreadsheet
            try
            {
                Excel.Workbook workbook = _excel.Workbooks.Add(Type.Missing);
                _excel.Visible = true;
                Worksheet ws = (Worksheet)_excel.ActiveSheet;
                ws.Activate();
                return ws;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }


        public void AddItemToSpreadsheet(int row, int column, Worksheet ws, string item)
        {
            ((Range)ws.Cells[row, column]).Value2 = item;
        }

        public void BoldRow(int row, Worksheet ws)
        {
            ((Range)ws.Cells[row, 1]).EntireRow.Font.Bold = true;
        }

        public void FormatColumn(Worksheet ws, int col, string format)
        {
            ((Range)ws.Cells[1, col]).EntireColumn.NumberFormat = format;
        }

        public void FormatColumnText(Worksheet ws, int col)
        {
            ((Range)ws.Cells[1, col]).EntireColumn.NumberFormat = "@";
        }


        public void SetColumnWidth(Worksheet ws, int col, int width)
        {
            ((Range)ws.Cells[1, col]).EntireColumn.ColumnWidth = width;
        }

        // autofit to contents
        public void AutoFitColumn(Worksheet ws, int col)
        {
            ((Range)ws.Cells[1, col]).EntireColumn.AutoFit();
        }*/


        public static void ExportToExcel(DataTable dataTable, string fileName)
        {
            var excelDoc = new StreamWriter(fileName);
            //const string startExcelXML = "<xml version>\r\n<Workbook " +
            //      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
            //      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
            //      "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
            //      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
            //      "office:spreadsheet\">\r\n <Styles>\r\n " +
            //      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
            //      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
            //      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
            //      "\r\n <Protection/>\r\n </Style>\r\n " +
            //      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
            //      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
            //      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
            //      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
            //      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
            //      "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
            //      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
            //      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
            //      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
            //      "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
            //      "</Styles>\r\n ";
            const string startExcelXml = @"<xml version>
<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:o=""urn:schemas-microsoft-com:office:office""
 xmlns:x=""urn:schemas-    microsoft-com:office:excel""
 xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">
 <Styles>
 <Style ss:ID=""Default"" ss:Name=""Normal"">
 <Alignment ss:Vertical=""Bottom""/>
 <Borders/>
 <Font/>
 <Interior/>
 <NumberFormat/>
 <Protection/>
 </Style>
 <Style ss:ID=""BoldColumn"">
 <Font x:Family=""Swiss"" ss:Bold=""1""/>
 </Style>
 <Style     ss:ID=""StringLiteral"">
 <NumberFormat ss:Format=""@""/>
 </Style>
 <Style ss:ID=""Decimal"">
 <NumberFormat ss:Format=""0.0000""/>
 </Style>
 <Style ss:ID=""Integer"">
 <NumberFormat ss:Format=""0""/>
 </Style>
 <Style ss:ID=""DateLiteral"">
 <NumberFormat ss:Format=""mm/dd/yyyy;@""/>
 </Style>
 </Styles>
 ";
            const string endExcelXml = "</Workbook>";

            var rowCount = 0;
            var sheetCount = 1;
            /*
           <xml version>
           <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
           xmlns:o="urn:schemas-microsoft-com:office:office"
           xmlns:x="urn:schemas-microsoft-com:office:excel"
           xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">
           <Styles>
           <Style ss:ID="Default" ss:Name="Normal">
             <Alignment ss:Vertical="Bottom"/>
             <Borders/>
             <Font/>
             <Interior/>
             <NumberFormat/>
             <Protection/>
           </Style>
           <Style ss:ID="BoldColumn">
             <Font x:Family="Swiss" ss:Bold="1"/>
           </Style>
           <Style ss:ID="StringLiteral">
             <NumberFormat ss:Format="@"/>
           </Style>
           <Style ss:ID="Decimal">
             <NumberFormat ss:Format="0.0000"/>
           </Style>
           <Style ss:ID="Integer">
             <NumberFormat ss:Format="0"/>
           </Style>
           <Style ss:ID="DateLiteral">
             <NumberFormat ss:Format="mm/dd/yyyy;@"/>
           </Style>
           </Styles>
           <Worksheet ss:Name="Sheet1">
           </Worksheet>
           </Workbook>
           */
            excelDoc.Write(startExcelXml);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>");
            for (var x = 0; x < dataTable.Columns.Count; x++)
            {
                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                excelDoc.Write(dataTable.Columns[x].ColumnName);
                excelDoc.Write("</Data></Cell>");
            }
            excelDoc.Write("</Row>");
            foreach (DataRow x in dataTable.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output

                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>"); //ID=" + rowCount + "

                for (var y = 0; y < dataTable.Columns.Count; y++)
                {
                    var rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            var xmLstring = x[y].ToString();
                            xmLstring = xmLstring.Trim();
                            xmLstring = xmLstring.Replace("&", "&");
                            xmLstring = xmLstring.Replace(">", ">");
                            xmLstring = xmLstring.Replace("<", "<");
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            excelDoc.Write(xmLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            var xmlDate = (DateTime)x[y];

                            var xmlDatetoString = xmlDate.Year +
                                                     "-" +
                                                     (xmlDate.Month < 10 ? "0" +
                                                                           xmlDate.Month : xmlDate.Month.ToString(CultureInfo.InvariantCulture)) +
                                                     "-" +
                                                     (xmlDate.Day < 10 ? "0" +
                                                                         xmlDate.Day : xmlDate.Day.ToString(CultureInfo.InvariantCulture)) +
                                                     "T" +
                                                     (xmlDate.Hour < 10 ? "0" +
                                                                          xmlDate.Hour : xmlDate.Hour.ToString(CultureInfo.InvariantCulture)) +
                                                     ":" +
                                                     (xmlDate.Minute < 10 ? "0" +
                                                                            xmlDate.Minute : xmlDate.Minute.ToString(CultureInfo.InvariantCulture)) +
                                                     ":" +
                                                     (xmlDate.Second < 10 ? "0" +
                                                                            xmlDate.Second : xmlDate.Second.ToString(CultureInfo.InvariantCulture)) +
                                                     ".000";
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\"><Data ss:Type=\"DateTime\">");
                            excelDoc.Write(xmlDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\"><Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                            excelDoc.Write("");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        default:
                            throw new Exception(rowType + " not handled.");
                    }
                }
                excelDoc.Write("</Row>");
            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXml);
            excelDoc.Close();
        }
        public static void ExportToExcel(DataView dataView, string fileName)
        {
            var excelDoc = new StreamWriter(fileName);
            const string startExcelXml = @"<xml version>
<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:o=""urn:schemas-microsoft-com:office:office""
 xmlns:x=""urn:schemas-    microsoft-com:office:excel""
 xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">
 <Styles>
 <Style ss:ID=""Default"" ss:Name=""Normal"">
 <Alignment ss:Vertical=""Bottom""/>
 <Borders/>
 <Font/>
 <Interior/>
 <NumberFormat/>
 <Protection/>
 </Style>
 <Style ss:ID=""BoldColumn"">
 <Font x:Family=""Swiss"" ss:Bold=""1""/>
 </Style>
 <Style     ss:ID=""StringLiteral"">
 <NumberFormat ss:Format=""@""/>
 </Style>
 <Style ss:ID=""Decimal"">
 <NumberFormat ss:Format=""0.0000""/>
 </Style>
 <Style ss:ID=""Integer"">
 <NumberFormat ss:Format=""0""/>
 </Style>
 <Style ss:ID=""DateLiteral"">
 <NumberFormat ss:Format=""mm/dd/yyyy;@""/>
 </Style>
 </Styles>
 ";
            const string endExcelXml = "</Workbook>";

            var rowCount = 0;
            var sheetCount = 1;
            excelDoc.Write(startExcelXml);
            excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>");
            for (var x = 0; x < dataView.Table.Columns.Count; x++)
            {
                excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                excelDoc.Write(dataView.Table.Columns[x].ColumnName);
                excelDoc.Write("</Data></Cell>");
            }
            excelDoc.Write("</Row>");
            foreach (DataRowView x in dataView)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output

                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.Write("</Table>");
                    excelDoc.Write(" </Worksheet>");
                    excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                    excelDoc.Write("<Table>");
                }
                excelDoc.Write("<Row>"); //ID=" + rowCount + "

                for (var y = 0; y < dataView.Table.Columns.Count; y++)
                {
                    var rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            var xmLstring = x[y].ToString();
                            xmLstring = xmLstring.Trim();
                            xmLstring = xmLstring.Replace("&", "&");
                            xmLstring = xmLstring.Replace(">", ">");
                            xmLstring = xmLstring.Replace("<", "<");
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">");
                            excelDoc.Write(xmLstring);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            var xmlDate = (DateTime)x[y];

                            var xmlDatetoString = xmlDate.Year +
                                                     "-" +
                                                     (xmlDate.Month < 10 ? "0" +
                                                                           xmlDate.Month : xmlDate.Month.ToString(CultureInfo.InvariantCulture)) +
                                                     "-" +
                                                     (xmlDate.Day < 10 ? "0" +
                                                                         xmlDate.Day : xmlDate.Day.ToString(CultureInfo.InvariantCulture)) +
                                                     "T" +
                                                     (xmlDate.Hour < 10 ? "0" +
                                                                          xmlDate.Hour : xmlDate.Hour.ToString(CultureInfo.InvariantCulture)) +
                                                     ":" +
                                                     (xmlDate.Minute < 10 ? "0" +
                                                                            xmlDate.Minute : xmlDate.Minute.ToString(CultureInfo.InvariantCulture)) +
                                                     ":" +
                                                     (xmlDate.Second < 10 ? "0" +
                                                                            xmlDate.Second : xmlDate.Second.ToString(CultureInfo.InvariantCulture)) +
                                                     ".000";
                            excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\"><Data ss:Type=\"DateTime\">");
                            excelDoc.Write(xmlDatetoString);
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Boolean":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Write("<Cell ss:StyleID=\"Integer\"><Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"Number\">");
                            excelDoc.Write(x[y].ToString());
                            excelDoc.Write("</Data></Cell>");
                            break;
                        case "System.DBNull":
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                            excelDoc.Write("");
                            excelDoc.Write("</Data></Cell>");
                            break;
                        default:
                            throw new Exception(rowType + " not handled.");
                    }
                }
                excelDoc.Write("</Row>");
            }
            excelDoc.Write("</Table>");
            excelDoc.Write(" </Worksheet>");
            excelDoc.Write(endExcelXml);
            excelDoc.Close();
        }
        /*private static void ExportToExcel(string fileName)
        {
            Microsoft.Office.Interop.Excel.Application wapp;
            Microsoft.Office.Interop.Excel.Worksheet wsheet;
            Microsoft.Office.Interop.Excel.Workbook wbook;
            wapp = new Microsoft.Office.Interop.Excel.Application();
            wapp.Visible = false;
            wbook = wapp.Workbooks.Add(true);
            wsheet = (Worksheet)wbook.ActiveSheet;
            try
            {
                int iX;
                int iY;
                for (int i = 0; i < this.DataGridResults.Columns.Count; i++)
                {
                    wsheet.Cells[1, i + 1] = this.DataGridResults.Columns[i].HeaderText;
                    wsheet.Font.Bold = true;
                }

                for (int i = 0; i < this.DataGridResults.Rows.Count; i++)
                {
                    DataGridViewRow row = this.DataGridResults.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        DataGridViewCell cell = row.Cells[j];
                        try
                        {
                            wsheet.Cells[i + 2, j + 1] = (cell.Value == null) ? "" : cell.Value.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                wapp.Visible = true;
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
            wapp.UserControl = true;
        }*/



        public static DataSet ImportExcelXLS(HttpPostedFile file, bool hasHeaders)
        {
            var fileName = Path.GetTempFileName();
            file.SaveAs(fileName);

            return ImportExcelXLS(fileName, hasHeaders);
        }
        private static DataSet ImportExcelXLS(string fileName, bool hasHeaders)
        {
            var hdr = hasHeaders ? "Yes" : "No";
            var strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=1\"";

            var output = new DataSet();

            using (var conn = new OleDbConnection(strConn))
            {
                conn.Open();

                var dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    var sheet = row["TABLE_NAME"].ToString();

                    var cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn) { CommandType = CommandType.Text };

                    var outputTable = new DataTable(sheet);
                    output.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);
                }
            }
            return output;
        }
    }
}
