using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Zek.Extensions.DevEx
{
    public static class XtraGridExtentions
    {
        /// <summary>
        /// აბრუნებს მაუსი დაკლიკდა თუ არ Row-ზე.
        /// </summary>
        /// <param name="view">გრიდი სადაც დავაკლიკეთ მაუსი</param>
        /// <returns>აბრუნებს true-ს, თუ დაკლიკდა Row-ზე.</returns>
        public static bool IsRowClicked(this GridView view)
        {
            var info = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition));
            return info.InRow;
        }


        /// <summary>
        /// მარცხენა სვეტში ანიჭებს ავტო ნუმერაციას 1,2,3...
        /// </summary>
        /// <param name="view">გრიდი რომლის გადანომრვაც გვინდა.</param>
        public static void SetCustomDrawRowIndicator(this GridView view)
        {
            if (view == null) return;

            view.IndicatorWidth = 55;
            view.Invalidate();
            view.CustomDrawRowIndicator += delegate(object sender, RowIndicatorCustomDrawEventArgs e)
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    //e.Info.ImageIndex = -1;
                }
            };
        }

        /// <summary>
        /// მონიშვანვს ყველა ჩანაწერს (Select All), როცა მარცხენა ზედა უჯრას დაკლიკავენ.
        /// </summary>
        /// <param name="view">გრიდი რომლის მონიშვნაც გვინდა.</param>
        public static void SetSelectAllOnTopLeftColumnButtonClick(this GridView view)
        {
            view.MouseDown += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    var info = view.CalcHitInfo(e.Location);
                    if (info.HitTest == GridHitTest.ColumnButton && info.Column == null)
                    {
                        view.SelectAll();
                    }
                }
            };
        }

        /// <summary>
        /// ბლოკავს სვეტის მენიუში კნოპკა Customization-ს.
        /// </summary>
        /// <param name="view">გრიდი რომლის მენუს დაბლოკვაც გვინდა.</param>
        public static void DisableMenuColumnCustomization(this GridView view)
        {
            view.PopupMenuShowing += delegate(object sender, PopupMenuShowingEventArgs e)
            {
                if (e.MenuType == GridMenuType.Column)
                {
                    var miCustomize = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization);
                    if (miCustomize != null)
                        miCustomize.Enabled = false;
                }
            };
        }
        public static DXMenuItem GetItemByStringId(DXPopupMenu menu, GridStringId id)
        {
            foreach (DXMenuItem item in menu.Items)
                if (item.Caption == GridLocalizer.Active.GetLocalizedString(id))
                    return item;
            return null;
        }

        public static void SetNumericFormat(this GridView view, string formatString = "N2")
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            foreach (GridColumn col in view.Columns)
            {
                if (col.ColumnType == typeof(decimal) ||
                    col.ColumnType == typeof(decimal?) ||
                    col.ColumnType == typeof(double) ||
                    col.ColumnType == typeof(double?) ||
                    col.ColumnType == typeof(float) ||
                    col.ColumnType == typeof(float?) ||
                    col.UnboundType == UnboundColumnType.Decimal)
                {
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = formatString;

                    col.GroupFormat.FormatType = FormatType.Numeric;
                    col.GroupFormat.FormatString = formatString;
                }
            }
        }

        public static void SetGridFont(this GridView view)
        {
            SetGridFont(view, new Font("BPG Glaho Arial", 8.25F));
            //"BPG Glaho Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point
        }
        public static void SetGridFont(this GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)
                ap.Font = font;

            foreach (AppearanceObject ap in view.AppearancePrint)
                ap.Font = font;
        }
    }
}
