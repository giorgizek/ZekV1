using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Zek.Windows.Forms
{
    public static class DataGridExtention
    {
        /// <summary>
        /// Forces a repaint of given row.
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="row"></param>
        public static void RefreshRow(this DataGrid dataGrid, int row)
        {
            var rect = dataGrid.GetCellBounds(row, 0);
            rect = new Rectangle(rect.Right, rect.Top, dataGrid.Width, rect.Height);
            dataGrid.Invalidate(rect);
        }

        public static DataGridTableStyle CreateDataGridTableStyleWithCellFormat(this DataTable dataTable, FormatCellEventHandler formatCellEventHandler)
        {
            return CreateDataGridTableStyleWithCellFormat(dataTable, formatCellEventHandler, null);
        }
        public static DataGridTableStyle CreateDataGridTableStyleWithCellFormat(this DataTable dataTable, FormatCellEventHandler formatCellEventHandler, string nullText)
        {
            var dataGridTableStyle = new DataGridTableStyle();
            dataGridTableStyle.MappingName = dataTable.TableName;

            foreach (DataColumn col in dataTable.Columns)
            {
                DataGridColumnStyle column = null;
                if (col.DataType == typeof(bool))
                {
                    column = new FormattableBooleanColumn();
                    ((FormattableBooleanColumn)column).SetCellFormat += new FormatCellEventHandler(formatCellEventHandler);
                }
                else
                {
                    column = new FormattableTextBoxColumn();
                    ((FormattableTextBoxColumn)column).SetCellFormat += new FormatCellEventHandler(formatCellEventHandler);
                }
                column.MappingName = col.ColumnName;
                column.HeaderText = col.Caption;
                //column.Width = 30;
                if (nullText == null)
                    column.NullText = nullText;

                dataGridTableStyle.GridColumnStyles.Add(column);
            }
            return dataGridTableStyle;
        }
        public static void SetFormatCellEventHandler(this DataGrid dataGrid, FormatCellEventHandler formatCellEventHandler)
        {
            if (dataGrid == null)
                throw new ArgumentNullException(nameof(dataGrid));

            if (dataGrid.DataSource == null) return;


            if (dataGrid.TableStyles.Count > 0)
                dataGrid.TableStyles.Clear();


            if (dataGrid.DataSource is DataTable)
                dataGrid.TableStyles.Add(CreateDataGridTableStyleWithCellFormat((DataTable)dataGrid.DataSource, formatCellEventHandler));
            else if (dataGrid.DataSource is DataSet)
            {
                foreach (DataTable dataTable in ((DataSet)dataGrid.DataSource).Tables)
                {
                    dataGrid.TableStyles.Add(CreateDataGridTableStyleWithCellFormat(dataTable, formatCellEventHandler));
                }
            }
        }
        public static void SetFormatCellEventHandler(this DataGridTableStyle dataGridTableStyle, FormatCellEventHandler formatCellEventHandler)
        {
            if (dataGridTableStyle == null)
                throw new ArgumentNullException(nameof(dataGridTableStyle));
            if (formatCellEventHandler == null)
                throw new ArgumentNullException(nameof(formatCellEventHandler));

            foreach (DataGridColumnStyle dataGridColumnStyle in dataGridTableStyle.GridColumnStyles)
            {
                if (dataGridColumnStyle is IFormattableColumn)
                {
                    ((IFormattableColumn)dataGridColumnStyle).SetCellFormat += new FormatCellEventHandler(formatCellEventHandler);
                }
            }
        }
    }
}
