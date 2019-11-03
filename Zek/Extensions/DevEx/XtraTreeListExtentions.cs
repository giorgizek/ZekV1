using DevExpress.XtraTreeList;

namespace Zek.Extensions.DevEx
{
    public static class XtraTreeListExtentions
    {
        public static void SetAutoFilterValue(this TreeList tl, string fieldName, string autoFilterValue)
        {
            tl.FocusedNode = tl.Nodes.AutoFilterNode;
            tl.FocusedColumn = tl.Columns.ColumnByFieldName(fieldName);
            tl.ShowEditor();
            tl.ActiveEditor.EditValue = autoFilterValue;
        }
    }
}
