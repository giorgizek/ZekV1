using System.Windows.Forms;

namespace Zek.Windows.Forms
{
    public static class ListViewExcentions
    {
        public static void AutoResizeColumns(this ListView list)
        {
            foreach (ColumnHeader col in list.Columns)
            {
                var width = col.Width;

                // column items greatest width
                col.Width = -1;
                if (width > col.Width)
                    col.Width = width;

                // column header width
                col.Width = -2;
                if (width > col.Width)
                    col.Width = width;
            }
        }
    }
}
