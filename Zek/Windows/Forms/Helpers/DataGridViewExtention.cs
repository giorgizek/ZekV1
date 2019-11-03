using System.Windows.Forms;
using System.Drawing;
using Zek.Extensions;
using System.Data;

namespace Zek.Windows.Forms
{
    public static class DataGridViewExtention
    {
        public static void ShowHideSystemColumns(this DataGridView dataGridView, bool show)
        {
            if (show)
            {
                for (var i = 0; i < dataGridView.Columns.Count; i++)
                {
                    if (!dataGridView.Columns[i].Visible)
                    {
                        dataGridView.Columns[i].Visible = show;
                        dataGridView.Columns[i].Tag = true;
                    }
                }
            }
            else
            {
                for (var i = 0; i < dataGridView.Columns.Count; i++)
                {
                    if (dataGridView.Columns[i].Visible && dataGridView.Columns[i].Tag != null && dataGridView.Columns[i].Tag is bool)
                    {
                        dataGridView.Columns[i].Visible = show;
                        dataGridView.Columns[i].Tag = null;
                    }
                }
            }
        }

        public static void SetFont(this DataGridView dataGridView, Font font)
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = font;
            dataGridView.DefaultCellStyle.Font = font;
        }

        public static void SetReadOnly(this DataGridView dataGridView, bool readOnly)
        {
            dataGridView.ReadOnly = readOnly;
        }
        public static bool IsSelectedRowsCellValue(this DataGridView dataGridView, int columnIndex, object value)
        {
            var flag = true;
            for (var i = 0; i < dataGridView.SelectedRows.Count; i++)
            {
                if (!dataGridView[columnIndex, i].Value.Compare(value))
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }
        public static bool IsSelectedRowsCellValue(this DataGridView dataGridView, string columnName, object value)
        {
            var flag = true;
            for (var i = 0; i < dataGridView.SelectedRows.Count; i++)
            {
                if (!dataGridView[columnName, i].Value.Compare(value))
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }
        public static void SetCustomDrawRowIndicator(this DataGridView dataGridView)
        {
            if (dataGridView.RowHeadersWidth < 65) dataGridView.RowHeadersWidth = 65;
            dataGridView.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (e.ColumnIndex != 0) return;
                ((DataGridView)sender).Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            };
        }
        public static int IndexOfColumn(this DataGridView dataGridView, string dataPropertyName)
        {
            for (var i = 0; i < dataGridView.Columns.Count; i++)
            {
                if (dataGridView.Columns[i].DataPropertyName == dataPropertyName)
                    return i;
            }

            return -1;
        }


        public static void ClearError(this DataGridView dataGridView, int rowIndex, int columnIndex)
        {
            SetError(dataGridView, rowIndex, columnIndex, string.Empty);
        }
        public static void SetError(this DataGridView dataGridView, int rowIndex, int columnIndex, string error)
        {
            var obj = dataGridView.Rows[rowIndex].DataBoundItem;
            if (obj != null && obj is DataRowView)
                ((DataRowView)obj).Row.SetColumnError(dataGridView.Columns[columnIndex].DataPropertyName, error);
        }
        public static void DeleteSelectedRows(this DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                dataGridView.Rows.Remove(row);
            }
        }
    }
}
