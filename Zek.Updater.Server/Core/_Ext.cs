using System.Globalization;
using System.Windows.Forms;

namespace Zek
{
    public static partial class Ext
    {

        public static string ToComputerSize(long value)
        {
            double valor = value;
            long i;
            var names = new[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            for (i = 0; i < names.Length && valor >= 1024; i++)
                valor /= 1024.0;
            return string.Format("{0:#,###.00} {1}", valor, names[i]);
        }


        public static void SetCustomDrawRowIndicator(DataGridView dataGridView)
        {
            if (dataGridView.RowHeadersWidth < 65) dataGridView.RowHeadersWidth = 65;
            dataGridView.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (e.ColumnIndex != 0) return;
                ((DataGridView)sender).Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString(CultureInfo.InvariantCulture);
            };
        }


    }
}
