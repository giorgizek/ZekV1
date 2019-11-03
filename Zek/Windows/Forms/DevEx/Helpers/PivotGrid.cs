using System;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraPivotGrid;
using System.Windows.Forms;

namespace Zek.Windows.Forms.DevEx
{
    public static class PivotGridExtention
    {
        public static string GetSaveFileDialogFilter()
        {
            return "Excel 2007 Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|HTML Document|*.htm|MHTML Document|*.mht|Adobe Acrobat Document|*.pdf|Rich Text Format (RTF)|*.rtf|Text Document|*.txt";
        }

        public static void ShowExportToDialog(this PivotGridControl pvtGrid, IWin32Window owner = null, bool openAfterExport = false)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = GetSaveFileDialogFilter();
                //var dialogResult = dlg.ShowDialog(owner);
                if (dlg.ShowDialog(owner) != DialogResult.OK) return;

                ExportTo(pvtGrid, dlg.FileName, openAfterExport);
            }
        }

        public static void ExportTo(this PivotGridControl pvtGrid, string fileName, bool openAfterExport = false)
        {
            var ext = new FileInfo(fileName).Extension;
            switch (ext)
            {
                case ".csv":
                    pvtGrid.ExportToCsv(fileName);
                    break;

                case ".htm":
                case ".html":
                    pvtGrid.ExportToHtml(fileName);
                    break;

                case ".bmp":
                    pvtGrid.ExportToImage(fileName);
                    break;

                case ".mht":
                    pvtGrid.ExportToMht(fileName);
                    break;

                case ".pdf":
                    pvtGrid.ExportToPdf(fileName);
                    break;

                case ".rtf":
                    pvtGrid.ExportToRtf(fileName);
                    break;

                case ".txt":
                    pvtGrid.ExportToText(fileName);
                    break;

                case ".xls":
                    pvtGrid.ExportToXls(fileName);
                    break;

                case ".xlsx":
                    pvtGrid.ExportToXlsx(fileName);
                    break;

                default:
                    throw new Exception("გთხოვთ აირჩიოთ სწორი ფაილის ტიპი.");
            }


            if (openAfterExport)
            {
                try
                {
                    Process.Start(fileName);
                }
                catch
                {
                    //if (throwError) throw;
                }
            }
        }
    }
}
