using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace Zek.Windows.Forms.DevEx
{
    public static class CheckedComboBoxExtentions
    {
        public static T[] GetCheckedItems<T>(this CheckedComboBoxEdit chkcmb)
        {
            if (chkcmb.Properties.Items.Count == 0) return null;

            var result = new List<T>();
            for (var i = 0; i < chkcmb.Properties.Items.Count; i++)
            {
                if (chkcmb.Properties.Items[i].CheckState != CheckState.Checked) continue;
                result.Add((T)chkcmb.Properties.Items[i].Value);
            }

            return result.ToArray();
        }
    }
}
