using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Zek.Extensions.DevEx;

namespace Zek.Windows.Forms.DevEx
{
    public class XtraGridHelper
    {

        /// <summary>
        /// SetReadOnly(view, true);
        /// OptionsView.ShowFooter = true;
        /// OptionsPrint.AutoWidth = false;
        /// BestFitMaxRowCount = 50;
        /// SetCustomDrawRowIndicator(view);
        /// OptionsSelection.MultiSelect = true;
        /// SetSelectAllOnTopLeftColumnButtonClick(view);
        /// InitGridView(view);
        /// </summary>
        /// <param name="view"></param>
        public static void InitListFormGrid(GridView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view), "InitGrid(DevExpress.XtraGrid.Views.Grid.GridView view)");
            SetReadOnly(view, true);

            view.OptionsView.ShowFooter = true;
            view.OptionsPrint.AutoWidth = false;
            view.BestFitMaxRowCount = 50;

            view.SetCustomDrawRowIndicator();
            view.OptionsSelection.MultiSelect = true;

            view.SetSelectAllOnTopLeftColumnButtonClick();
            InitGridView(view);
        }


        /// <summary>
        /// InitGrid(grid, true)
        /// </summary>
        /// <param name="grid">გრიდი, რომლის ინიციალიზაციაც გვინდა.</param>
        public static void InitGrid(GridControl grid)
        {
            InitGrid(grid, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">გრიდი, რომლის ინიციალიზაციაც გვინდა.</param>
        /// <param name="initEmbeddedNavigator">true-ს დროს ნავიგატორის კონტროლსაც გაუკეთდება ინიციალიზაცია.</param>
        public static void InitGrid(GridControl grid, bool initEmbeddedNavigator)
        {
            if (grid == null) return;

            InitBaseView(grid.MainView);

            if (initEmbeddedNavigator)
                InitControlNavigator(grid.EmbeddedNavigator);
        }

        private static void InitBaseView(BaseView view)
        {
            if (view == null) return;
            if (view is GridView) InitGridView((GridView)view);
        }
        public static void InitGridView(GridView view)
        {
            //if (view == null) return;

            //Font font = new Font("BPG Glaho Arial", 8.25f);
            //Font font = new System.Drawing.Font("BPG Glaho Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            //SetGridFont(gridView, font);

            //view.GroupPanelText = "გადმოიტანეთ სვეტის სათაური, რათა დააჯგუფოთ ამ სვეტის მიხედვით";
        }

      
        public static void SetAppearanceFont(BaseAppearanceCollection appearances)
        {
            SetAppearanceFont(appearances, new Font("BPG Glaho Arial", 8.25F));
        }
        public static void SetAppearanceFont(BaseAppearanceCollection appearances, Font font)
        {
            foreach (AppearanceObject obj in appearances)
            {
                obj.Font = font;
            }
        }

        /// <summary>
        /// მალავს ან აჩენს სისტემურ სვეტებს.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="show"></param>
        public static void ShowHideSystemColumns(GridView view, bool show)
        {
            if (show)
            {
                foreach (GridColumn col in view.Columns)
                {
                    if (!col.Visible)
                    {
                        col.Tag = true;
                        col.Visible = true;
                    }
                }
            }
            else
            {
                foreach (GridColumn col in view.Columns)
                {
                    if (col.Visible && col.Tag != null && col.Tag is bool && (bool)col.Tag)
                        col.Visible = false;
                }
            }
        }
        //public static void ShowHideSystemColumns(GridView view, bool show)
        //{
        //    if (show)
        //    {
        //        foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
        //        {
        //            if (!col.Visible && col.Tag != null && col.Tag is int && (int)col.Tag > -1)
        //                col.VisibleIndex = view.VisibleColumns.Count;
        //        }

        //        foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
        //        {
        //            if (col.Tag != null && col.Tag is int && (int)col.Tag > -1)
        //            {
        //                col.VisibleIndex = (int)col.Tag;
        //                col.Tag = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
        //        {
        //            if (col.Visible && col.Tag != null && col.Tag is bool && (bool)col.Tag)
        //                col.Tag = col.VisibleIndex;
        //        }


        //        foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
        //        {
        //            if (col.Visible && col.Tag != null && col.Tag is int && (int)col.Tag > -1)
        //                col.Visible = false;
        //        }
        //    }
        //}

        /// <summary>
        /// ავტომატურად ჭიმავს სვეტებს.
        /// </summary>
        /// <param name="view"></param>
        public static void BestFitColumns(GridView view)
        {
            view.OptionsView.ColumnAutoWidth = false;
            view.BestFitColumns();

            var info = (GridViewInfo)view.GetViewInfo();
            var columnTotalWidth = info.ViewRects.ColumnTotalWidth != 0 ? info.ViewRects.ColumnTotalWidth : CalcVisibleColumnsWith(view);
            if (info.ViewRects.ColumnPanelWidth >= columnTotalWidth)
            {
                view.OptionsView.ColumnAutoWidth = true;
                view.BestFitColumns();
            }
        }

        public static int CalcVisibleColumnsWith(GridView view)
        {
            if (view == null) return 0;
            var width = 0;
            foreach (GridColumn col in view.VisibleColumns)
            {
                width += col.Width;
            }

            return width;
        }

        /// <summary>
        /// რთავს/თიშავს გრიდვიუს.
        /// </summary>
        /// <param name="view">გრიდვიუ</param>
        /// <param name="readOnly">ჩართვა/გამორთვა.</param>
        public static void SetReadOnly(GridView view, bool readOnly)
        {
            view.OptionsBehavior.Editable = !readOnly;
            //for (int i = 0; i < gridView.Columns.Count; i++)
            //{
            //    gridView.Columns[i].OptionsColumn.ReadOnly = true;
            //}
        }
        /// <summary>
        /// რთავს/თიშავს გრიდს და მის შიდა კონტროლებს.
        /// </summary>
        /// <param name="grid">გრიდკონტროლი</param>
        /// <param name="readOnly">ჩართვა/გამორთვა.</param>
        public static void SetReadOnly(GridControl grid, bool readOnly)
        {
            SetReadOnly(grid.EmbeddedNavigator, readOnly);
        }
        /// <summary>
        /// რთავს/თიშავს გრიდს და მის შიდა კონტროლებს.
        /// </summary>
        /// <param name="grid">გრიდკონტროლი</param>
        /// <param name="readOnly">ჩართვა/გამორთვა.</param>
        /// <param name="embeddedNavigator">როცა true, მაშინ EmbeddedNavigator-საც შეეხება.</param>
        public static void SetReadOnly(GridControl grid, bool readOnly, bool embeddedNavigator)
        {
            SetReadOnly(grid, readOnly, embeddedNavigator, false);
        }
        /// <summary>
        /// რთავს/თიშავს გრიდს და მის შიდა კონტროლებს.
        /// </summary>
        /// <param name="grid">გრიდკონტროლი.</param>
        /// <param name="readOnly">ჩართვა/გამორთვა.</param>
        /// <param name="embeddedNavigator">როცა true, მაშინ EmbeddedNavigator-საც შეეხება.</param>
        /// <param name="customButtons">როცა true, მაშინ EmbeddedNavigator.СustomButtons-საც შეეხება.</param>
        public static void SetReadOnly(GridControl grid, bool readOnly, bool embeddedNavigator, bool customButtons)
        {
            if (grid.MainView != null && grid.MainView is GridView)
            {
                SetReadOnly((GridView)grid.MainView, readOnly);
            }
            SetReadOnly(grid.EmbeddedNavigator, readOnly, customButtons);
        }
        public static void SetReadOnly(ControlNavigator embeddedNavigator, bool readOnly)
        {
            SetReadOnly(embeddedNavigator, readOnly, false);
        }
        public static void SetReadOnly(ControlNavigator embeddedNavigator, bool readOnly, bool customButtons)
        {
            embeddedNavigator.Buttons.Append.Enabled = !readOnly;
            embeddedNavigator.Buttons.Edit.Enabled = !readOnly;
            embeddedNavigator.Buttons.Remove.Enabled = !readOnly;
            embeddedNavigator.Buttons.CancelEdit.Enabled = !readOnly;
            embeddedNavigator.Buttons.EndEdit.Enabled = !readOnly;

            if (customButtons)
            {
                SetReadOnly(embeddedNavigator.Buttons.CustomButtons, readOnly);
            }
        }
        public static void SetReadOnly(NavigatorCustomButtons customButtons, bool readOnly)
        {
            for (var i = 0; i < customButtons.Count; i++)
            {
                customButtons[i].Enabled = !readOnly;
            }
        }

 


        public static string GetSaveFileDialogFilter()
        {
            return "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|HTML Document|*.htm|MHTML Document|*.mht|Adobe Acrobat Document|*.pdf|Rich Text Format (RTF)|*.rtf|Text Document|*.txt";
        }

        public static void Export(GridView view, string fileName, bool openAfterExport = false, bool throwError = false)
        {
            var ext = new FileInfo(fileName).Extension;

            switch (ext)
            {
                case ".htm":
                case ".html":
                    view.ExportToHtml(fileName);
                    break;

                case ".mht":
                    view.ExportToMht(fileName);
                    break;

                case ".pdf":
                    view.ExportToPdf(fileName);
                    break;

                case ".rtf":
                    view.ExportToRtf(fileName);
                    break;

                case ".txt":
                    view.ExportToText(fileName);
                    break;

                case ".xls":
                    view.ExportToXls(fileName);
                    break;

                case ".xlsx":
                    view.ExportToXlsx(fileName);
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
                    if (throwError) throw;
                }
            }
        }




        

        /// <summary>
        /// InitControlNavigator(controlNavigator, true);
        /// </summary>
        /// <param name="controlNavigator"></param>
        public static void InitControlNavigator(ControlNavigator controlNavigator)
        {
            InitControlNavigator(controlNavigator, true);
        }
        public static void InitControlNavigator(ControlNavigator controlNavigator, bool initButtons)
        {
            controlNavigator.TextStringFormat = "ჩანაწერი {0} / {1}-დან";
            if (initButtons) InitControlNavigatorButtons(controlNavigator.Buttons);
        }
        public static void InitControlNavigatorButtons(ControlNavigatorButtons controlNavigatorButtons)
        {
            controlNavigatorButtons.First.Hint = "დასაწყისი";
            controlNavigatorButtons.PrevPage.Hint = "წინა გვერდი";
            controlNavigatorButtons.Prev.Hint = "წინა";
            //
            controlNavigatorButtons.Next.Hint = "შემდეგი";
            controlNavigatorButtons.NextPage.Hint = "შემდეგი გვერდი";
            controlNavigatorButtons.Last.Hint = "ბოლო";
            //
            controlNavigatorButtons.Append.Hint = "დამატება";
            controlNavigatorButtons.Edit.Hint = "შეცვლა";
            controlNavigatorButtons.Remove.Hint = "წაშლა";
            controlNavigatorButtons.EndEdit.Hint = "ცვლილების დაფიქსირება";
            controlNavigatorButtons.CancelEdit.Hint = "ცვლილების უარყოფა";
        }

        /// <summary>
        /// IndexOrdinalNumbers(view, col, 0);
        /// </summary>
        /// <param name="view"></param>
        /// <param name="col"></param>
        public static void IndexOrdinalNumbers(GridView view, GridColumn col)
        {
            IndexOrdinalNumbers(view, col, 0);
        }
        /// <summary>
        /// ნომრავს row-ებს 1,2,3...
        /// </summary>
        /// <param name="view"></param>
        /// <param name="col"></param>
        /// <param name="startIndex">row-ს ინდექსი რომლიდანაც უნდა დაიწყოს გადანომრვა.</param>
        public static void IndexOrdinalNumbers(GridView view, GridColumn col, int startIndex)
        {
            for (var rowHandle = startIndex; rowHandle < view.RowCount; rowHandle++)
                view.SetRowCellValue(rowHandle, col, rowHandle + 1);
        }

        
    }
}
