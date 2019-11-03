using System;
using System.Collections;
using System.Data;
using System.Reflection;
using DevExpress.XtraEditors;
using Zek.Core;
using Zek.Utils;

namespace Zek.Extensions.DevEx
{
    public static class CheckedListBoxExtentions
    {
        public static T[] GetCheckedItems<T>(this BaseCheckedListBoxControl chkl)
        {
            if (chkl.CheckedItems.Count == 0) return null;
            var result = new T[chkl.CheckedItems.Count];
            for (var i = 0; i < chkl.CheckedItems.Count; i++)
            {
                result[i] = (T)chkl.CheckedItems[i];
            }

            return result;
        }

        public static void SetCheckedItems(this BaseCheckedListBoxControl chkl, Array array, bool check = true, bool uncheckAll = false)
        {
            if (chkl.ItemCount == 0) return;
            for (var i = 0; i < chkl.ItemCount; i++)
            {
                var value = chkl.GetItemValue(i);
                if (Array.IndexOf(array, value) != -1)
                    chkl.SetItemChecked(i, true);
                else if (uncheckAll)
                    chkl.SetItemChecked(i, false);
            }
        }

        public static void SetCheckedItems(this BaseCheckedListBoxControl chkl, IList array, bool check = true, bool uncheckAll = false)
        {
            if (chkl.ItemCount == 0) return;
            for (var i = 0; i < chkl.ItemCount; i++)
            {

                var value = chkl.GetItemValue(i);
                if (array.Contains(value))
                    chkl.SetItemChecked(i, true);
                else if (uncheckAll)
                    chkl.SetItemChecked(i, false);
            }
        }


        public static int GetCheckedItemsBitwiseAddFlag(this BaseCheckedListBoxControl chkl)
        {
            var flags = 0;
            for (var i = 0; i < chkl.CheckedItems.Count; i++)
            {
                flags = BitwiseHelper.AddFlag(flags, ConvertHelper.ToInt32(chkl.CheckedItems[i]));
            }
            return flags;
        }
        public static int GetCheckedItemsBitwiseAddFlag(this BaseCheckedListBoxControl chkl, string name)
        {
            var flags = 0;

            FieldInfo fi = null;
            PropertyInfo pi = null;
            if (chkl.ItemCount > 0)
            {
                fi = chkl.GetItem(0).GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                pi = chkl.GetItem(0).GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            }

            if (fi != null)
            {
                foreach (var item in chkl.CheckedItems)
                    flags = BitwiseHelper.AddFlag(flags, ConvertHelper.ToInt32(fi.GetValue(item)));
            }

            if (pi != null)
            {
                foreach (var item in chkl.CheckedItems)
                    flags = BitwiseHelper.AddFlag(flags, ConvertHelper.ToInt32(pi.GetValue(item, null)));
            }

            if (fi == null && pi == null && chkl.GetItem(0) is DataRowView)
            {
                foreach (var item in chkl.CheckedItems)
                    flags = BitwiseHelper.AddFlag(flags, ConvertHelper.ToInt32(((DataRowView)item)[name]));
            }

            return flags;
        }


        public static void SetCheckedItemsBitwiseHasFlag(this BaseCheckedListBoxControl chkl, int flags)
        {
            for (var i = 0; i < chkl.ItemCount; i++)
                chkl.SetItemChecked(i, BitwiseHelper.HasFlag(flags, ConvertHelper.ToInt32(chkl.GetItemValue(i))));
        }
        public static void SetCheckedItemsBitwiseHasFlag(this BaseCheckedListBoxControl chkl, string name, int flags)
        {
            FieldInfo fi = null;
            PropertyInfo pi = null;
            if (chkl.ItemCount > 0)
            {
                fi = chkl.GetItem(0).GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                pi = chkl.GetItem(0).GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            }


            if (fi != null)
            {
                for (var i = 0; i < chkl.ItemCount; i++)
                    chkl.SetItemChecked(i, BitwiseHelper.HasFlag(flags, ConvertHelper.ToInt32(fi.GetValue(chkl.GetItem(i)))));
            }

            if (pi != null)
            {
                for (var i = 0; i < chkl.ItemCount; i++)
                    chkl.SetItemChecked(i, BitwiseHelper.HasFlag(flags, ConvertHelper.ToInt32(pi.GetValue(chkl.GetItem(i), null))));
            }

            if (fi == null && pi == null && chkl.GetItem(0) is DataRowView)
            {
                for (var i = 0; i < chkl.ItemCount; i++)
                    chkl.SetItemChecked(i, BitwiseHelper.HasFlag(flags, ConvertHelper.ToInt32(((DataRowView)chkl.GetItem(i))[name])));
            }
        }
    }
}
