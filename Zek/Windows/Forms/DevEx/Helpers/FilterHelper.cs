using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using Zek.Data;
using DevExpress.XtraEditors;

namespace Zek.Windows.Forms.DevEx
{
    public class FilterHelperEx : FilterHelper
    {
        public static string GetWhereClause(BaseCheckedListBoxControl chkl, string field)
        {
            if (chkl.CheckedItems.Count == 0) return string.Empty;

            var items = new object[chkl.CheckedItems.Count];
            for (var i = 0; i < chkl.CheckedItems.Count; i++)
            {
                items[i] = chkl.CheckedItems[i];
            }

            return GetWhereClause(field, WhereOperator.In, true, items);
        }

        public static string GetWhereClause(CheckedComboBoxEdit checkedComboBoxEdit, string field)
        {
            var items = (from CheckedListBoxItem item in checkedComboBoxEdit.Properties.Items
                         where item.CheckState == CheckState.Checked
                         select item.Value).ToArray();
            return items.Length == 1
                ? GetWhereClause(field, WhereOperator.Equals, true, items[0])
                : GetWhereClause(field, WhereOperator.In, true, items);
        }
    }
}
