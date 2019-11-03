//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using Zek.Core;

//namespace Zek.Data
//{
//    public class EPPlusHelper
//    {
//        public static DataTable GetDataTableFromExcel(string path, int fromRow = 1, int fromColumn = 1, int toColumn = 0, bool hasHeader = true)
//        {
//            using (var stream = File.OpenRead(path))
//            {
//                return GetDataTableFromExcel(stream, fromRow, fromColumn, toColumn, hasHeader);
//            }
//        }
//        public static DataTable GetDataTableFromExcel(Stream stream, int fromRow = 1, int fromColumn = 1, int toColumn = 0, bool hasHeader = true)
//        {
//            using (var pck = new ExcelPackage())
//            {
//                pck.Load(stream);
//                //using (var stream = File.OpenRead(path))
//                //{
//                //    pck.Load(stream);
//                //}
//                var worksheet = pck.Workbook.Worksheets.First();
//                worksheet.Cells["A1"].LoadFromDataTable(new DataTable(), false);
//                var table = new DataTable();
//                toColumn = toColumn == 0 ? worksheet.Dimension.End.Column : toColumn;
//                foreach (var firstRowCell in worksheet.Cells[fromRow, fromColumn, fromRow, toColumn])
//                {
//                    table.Columns.Add(hasHeader ? firstRowCell.Text : $"Column {firstRowCell.Start.Column}");
//                }
//                var startRow = hasHeader ? fromRow + 1 : fromRow;
//                for (var rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
//                {
//                    var wsRow = worksheet.Cells[rowNum, fromColumn, rowNum, toColumn];
//                    var row = table.NewRow();
//                    foreach (var cell in wsRow)
//                    {
//                        row[cell.Start.Column - fromColumn] = cell.Text;
//                    }
//                    table.Rows.Add(row);
//                }
//                return table;
//            }
//        }

//        public static List<T> GetClassFromExcel<T>(string path, int fromRow = 1, int fromColumn = 1, int toColumn = 0, bool hasHeader = true) where T : class
//        {
//            using (var stream = File.OpenRead(path))
//            {
//                return GetClassFromExcel<T>(stream, fromRow, fromColumn, toColumn, hasHeader);
//            }
//        }
//        public static List<T> GetClassFromExcel<T>(Stream stream, int fromRow = 1, int fromColumn = 1, int toColumn = 0, bool hasHeader = true) where T : class
//        {
//            using (var pck = new ExcelPackage())
//            {
//                var retList = new List<T>();

//                pck.Load(stream);
//                //using (var stream = File.OpenRead(path))
//                //{
//                //    pck.Load(stream);
//                //}

//                var worksheet = pck.Workbook.Worksheets.First();
//                var properties = typeof(T).GetProperties();

//                var startRow = hasHeader ? fromRow + 1 : fromRow;

//                if (hasHeader)
//                {
//                    var map = new Dictionary<string, int>();
//                    foreach (var firstRowCell in worksheet.Cells[fromRow, fromColumn, fromRow, worksheet.Dimension.End.Column])
//                    {
//                        var prop = properties.FirstOrDefault(x => x.Name.ToUpperInvariant() == firstRowCell.Text.Replace(" ", string.Empty).ToUpperInvariant());
//                        if (prop == null || map.ContainsKey(prop.Name)) continue;
//                        map.Add(prop.Name, firstRowCell.Start.Column);
//                    }

//                    if (map.Count == 0) return null;

//                    fromColumn = map.Values.Min();
//                    toColumn = map.Values.Max();

//                    properties = properties.Where(x => map.ContainsKey(x.Name)).ToArray();
//                    for (var rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
//                    {
//                        T obj = typeof(T) != typeof(string) ? Activator.CreateInstance<T>() : null;
//                        //worksheet.Cells.Select(x => map.Values.Contains(x.
//                        var wsRow = worksheet.Cells[rowNum, fromColumn, rowNum, toColumn];


//                        for (var i = 0; i < properties.Length; i++)
//                        {
//                            properties[i].SetValue(obj, ConvertHelper.ConvertType(wsRow[rowNum, map[properties[i].Name]].Text, properties[i].PropertyType), null);
//                        }
//                        retList.Add(obj);
//                    }
//                }
//                else
//                {
//                    toColumn = toColumn == 0 ? properties.Length : toColumn;
//                    for (var rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
//                    {
//                        var objT = Activator.CreateInstance<T>();

//                        var wsRow = worksheet.Cells[rowNum, fromColumn, rowNum, toColumn];

//                        for (var i = 0; i < properties.Length; i++)
//                        {
//                            properties[i].SetValue(objT, ConvertHelper.ConvertType(wsRow[rowNum, fromColumn + i].Text, properties[i].PropertyType), null);
//                        }
//                        retList.Add(objT);
//                    }
//                }
//                return retList;
//            }
//        }

//        public static MemoryStream DataSetToXlsx(DataSet ds)
//        {
//            var Result = new MemoryStream();
//            var pack = new ExcelPackage();

//            foreach (DataTable tbl in ds.Tables)
//            {
//                var ws = pack.Workbook.Worksheets.Add(tbl.TableName);
//                ws.Cells["A1"].LoadFromDataTable(tbl, true);
//            }

//            pack.SaveAs(Result);
//            return Result;
//        }
//    }
//}
