using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Zek.Windows.Forms
{
    public interface IFormattableColumn
    {
        event FormatCellEventHandler SetCellFormat;

    }


    #region Formattable TextBox Column
    public class FormattableTextBoxColumn : DataGridTextBoxColumn, IFormattableColumn
    {
        public event FormatCellEventHandler SetCellFormat;

        /// <summary>
        /// used to fire an event to retrieve formatting info
        /// and then draw the cell with this formatting info
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowIndex"></param>
        /// <param name="backBrush"></param>
        /// <param name="foreBrush"></param>
        /// <param name="alignToRight"></param>
        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowIndex, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            DataGridFormatCellEventArgs e = null;

            var callBaseClass = true;

            //fire the formatting event
            if (SetCellFormat != null)
            {
                var col = DataGridTableStyle.GridColumnStyles.IndexOf(this);
                e = new DataGridFormatCellEventArgs(rowIndex, col, GetColumnValueAtRow(source, rowIndex));
                SetCellFormat(this, e);
                if (e.BackBrush != null)
                    backBrush = e.BackBrush;

                //if these properties set, then must call drawstring
                if (e.ForeBrush != null || e.TextFont != null)
                {
                    if (e.ForeBrush == null)
                        e.ForeBrush = foreBrush;
                    if (e.TextFont == null)
                        e.TextFont = DataGridTableStyle.DataGrid.Font;
                    g.FillRectangle(backBrush, bounds);
                    var saveRegion = g.Clip;
                    var rect = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                    using (var newRegion = new Region(rect))
                    {
                        g.Clip = newRegion;
                        var charWidth = (int)Math.Ceiling(g.MeasureString("c", e.TextFont, 20, StringFormat.GenericTypographic).Width);

                        var s = GetColumnValueAtRow(source, rowIndex).ToString();
                        var maxChars = Math.Min(s.Length, (bounds.Width / charWidth));

                        try
                        {
                            g.DrawString(s.Substring(0, maxChars), e.TextFont, e.ForeBrush, bounds.X, bounds.Y + 2);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        } //empty catch
                        finally
                        {
                            g.Clip = saveRegion;
                        }
                    }
                    callBaseClass = false;
                }

                if (!e.UseBaseClassDrawing)
                {
                    callBaseClass = false;
                }
            }
            if (callBaseClass)
                base.Paint(g, bounds, source, rowIndex, backBrush, foreBrush, alignToRight);

            //clean up
            if (e != null)
            {
                if (e.BackBrushDispose)
                    e.BackBrush.Dispose();
                if (e.ForeBrushDispose)
                    e.ForeBrush.Dispose();
                if (e.TextFontDispose)
                    e.TextFont.Dispose();
            }
        }
    }

    #endregion

    #region Formattable Bool Column
    public class FormattableBooleanColumn : DataGridBoolColumn, IFormattableColumn
    {
        public event FormatCellEventHandler SetCellFormat;

        //overridden to fire BoolChange event and Formatting event
        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowIndex, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            var colNum = DataGridTableStyle.GridColumnStyles.IndexOf(this);

            //used to handle the boolchanging
            ManageBoolValueChanging(rowIndex, colNum);

            //fire formatting event
            DataGridFormatCellEventArgs e = null;
            var callBaseClass = true;
            if (SetCellFormat != null)
            {
                e = new DataGridFormatCellEventArgs(rowIndex, colNum, GetColumnValueAtRow(source, rowIndex));
                SetCellFormat(this, e);
                if (e.BackBrush != null)
                    backBrush = e.BackBrush;
                callBaseClass = e.UseBaseClassDrawing;
            }
            if (callBaseClass)
                base.Paint(g, bounds, source, rowIndex, backBrush, new SolidBrush(Color.Red), alignToRight);

            //clean up
            if (e != null)
            {
                if (e.BackBrushDispose)
                    e.BackBrush.Dispose();
                if (e.ForeBrushDispose)
                    e.ForeBrush.Dispose();
                if (e.TextFontDispose)
                    e.TextFont.Dispose();
            }
        }

        //changed event
        public event BoolValueChangedEventHandler BoolValueChanged;

        bool saveValue = false;
        int saveRow = -1;
        bool lockValue = false;
        bool beingEdited = false;
        public const int VK_SPACE = 32;// 0x20

        //needed to get the space bar changing of the bool value
        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        //set variables to start tracking bool changes
        protected override void Edit(CurrencyManager source, int rowIndex, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
        {
            lockValue = true;
            beingEdited = true;
            saveRow = rowIndex;
            saveValue = (bool)base.GetColumnValueAtRow(source, rowIndex);
            base.Edit(source, rowIndex, bounds, readOnly, instantText, cellIsVisible);
        }

        //turn off tracking bool changes
        protected override bool Commit(CurrencyManager dataSource, int rowIndex)
        {
            lockValue = true;
            beingEdited = false;
            return base.Commit(dataSource, rowIndex);
        }

        //fire the bool change event if the value changes
        private void ManageBoolValueChanging(int rowNum, int colNum)
        {
            var mousePos = DataGridTableStyle.DataGrid.PointToClient(Control.MousePosition);
            var dg = DataGridTableStyle.DataGrid;
            var isClickInCell = ((Control.MouseButtons == MouseButtons.Left) &&
                dg.GetCellBounds(dg.CurrentCell).Contains(mousePos));

            var changing = dg.Focused && (isClickInCell
                || GetKeyState(VK_SPACE) < 0); // or spacebar

            if (!lockValue && beingEdited && changing && saveRow == rowNum)
            {
                saveValue = !saveValue;
                lockValue = false;

                //fire the event
                if (BoolValueChanged != null)
                {
                    var e = new BoolValueChangedEventArgs(rowNum, colNum, saveValue);
                    BoolValueChanged(this, e);
                }
            }
            if (saveRow == rowNum)
                lockValue = false;
        }
    }
    #endregion

    #region CellFormatting Event
    public delegate void FormatCellEventHandler(object sender, DataGridFormatCellEventArgs e);

    public class DataGridFormatCellEventArgs : EventArgs
    {
        public DataGridFormatCellEventArgs(int rowIndex, int columnIndex, object value)
        {
            _RowIndex = rowIndex;
            _ColumnIndex = columnIndex;
            _TextFont = null;
            _BackBrush = null;
            _ForeBrush = null;
            _TextFontDispose = false;
            _BackBrushDispose = false;
            _ForeBrushDispose = false;
            _UseBaseClassDrawing = true;
            _Value = value;
        }

        private int _ColumnIndex;
        /// <summary>
        /// column being painted.
        /// </summary>
        public int ColumnIndex
        {
            get { return _ColumnIndex; }
            set { _ColumnIndex = value; }
        }

        private int _RowIndex;
        /// <summary>
        /// row being painted.
        /// </summary>
        public int RowIndex
        {
            get { return _RowIndex; }
            set { _RowIndex = value; }
        }


        private Font _TextFont;
        /// <summary>
        /// font used for drawing the text.
        /// </summary>
        public Font TextFont
        {
            get { return _TextFont; }
            set { _TextFont = value; }
        }


        private Brush _BackBrush;
        /// <summary>
        /// background brush.
        /// </summary>
        public Brush BackBrush
        {
            get { return _BackBrush; }
            set { _BackBrush = value; }
        }

        private Brush _ForeBrush;
        /// <summary>
        /// foreground brush.
        /// </summary>
        public Brush ForeBrush
        {
            get { return _ForeBrush; }
            set { _ForeBrush = value; }
        }

        private bool _TextFontDispose;
        /// <summary>
        /// set true if you want the Paint method to call Dispose on the font.
        /// </summary>
        public bool TextFontDispose
        {
            get { return _TextFontDispose; }
            set { _TextFontDispose = value; }
        }


        private bool _BackBrushDispose;
        /// <summary>
        /// set true if you want the Paint method to call Dispose on the brush
        /// </summary>
        public bool BackBrushDispose
        {
            get { return _BackBrushDispose; }
            set { _BackBrushDispose = value; }
        }

        private bool _ForeBrushDispose;
        /// <summary>
        /// set true if you want the Paint method to call Dispose on the brush.
        /// </summary>
        public bool ForeBrushDispose
        {
            get { return _ForeBrushDispose; }
            set { _ForeBrushDispose = value; }
        }

        private bool _UseBaseClassDrawing;
        //set true if you want the Paint method to call base class
        public bool UseBaseClassDrawing
        {
            get { return _UseBaseClassDrawing; }
            set { _UseBaseClassDrawing = value; }
        }


        private object _Value;
        //contains the current cell value
        public object Value => _Value;
    }
    #endregion

    #region BoolValueChanging Event
    public delegate void BoolValueChangedEventHandler(object sender, BoolValueChangedEventArgs e);

    public class BoolValueChangedEventArgs : EventArgs
    {
        public BoolValueChangedEventArgs(int rowIndex, int columnIndex, bool value)
        {
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _value = value;
        }

        private int _columnIndex;
        /// <summary>
        /// column to be painted.
        /// </summary>
        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }

        private int _rowIndex;
        /// <summary>
        /// row to be painted.
        /// </summary>
        public int RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        private bool _value;
        /// <summary>
        /// current value to be painted.
        /// </summary>
        public bool Value => _value;
    }
    #endregion
}
