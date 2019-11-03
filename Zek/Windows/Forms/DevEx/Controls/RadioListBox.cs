using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using DevExpress.Utils.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using Zek.Extensions;

namespace Zek.Windows.Forms.DevEx
{
    [DesignerCategory("")]
    [Description("Displays a radio list of items.")]
    [DefaultBindingProperty("EditValue"), DefaultEvent("EditValueChanged"), ToolboxItem(true)]
    public class RadioListBoxControl : CheckedListBoxControl
    {
        public RadioListBoxControl()
        {
            CheckOnClick = true;
            SelectionMode = SelectionMode.None;
        }

        protected override BaseControlPainter CreatePainter()
        {
            return new PainterRadioListBox();
        }

        protected override void SetItemCheckStateCore(int index, CheckState value)
        {
            if (value == CheckState.Checked)
                UnCheckAll();
            base.SetItemCheckStateCore(index, value);
            
            //EditValue = find
        }




        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object OldEditValue { get; private set; }


        [Category("Zek"), Description("წარმოიშვებმა მაშინვე როგორც კი EditValue შეიცვლება.")]
        public event EventHandler EditValueChanged;

        [Category("Zek"), Description("Fires when an end-user starts to modify the EditValue.")]
        public event  ChangingEventHandler EditValueChanging;

        private bool CompareEditValue(object value)
        {
            return (_editValue == value) || CompareEditValue(_editValue, value);
        }
        private bool CompareEditValue(object val1, object val2)
        {
            return val1.Compare(val2);
        }

        private object _editValue;
        [RefreshProperties(RefreshProperties.All), Localizable(true), Bindable(true), DefaultValue((string)null), TypeConverter(typeof(ObjectEditorTypeConverter)), DXCategory(@"Data"), Editor(typeof(UIObjectEditor), typeof(UITypeEditor))]
        public virtual object EditValue
        {
            get
            {
                return _editValue;
            }
            set
            {
                if (!CompareEditValue(value))
                {
                    OnEditValueChanging(new ChangingEventArgs(_editValue, value));
                }
            }
        }
        protected virtual void OnEditValueChanged()
        {
            try
            {
                //if (fEditValue == null)
                //{
                //    SetText(null);
                //}
                //else
                //{
                //    RefreshRecord(true);
                //}

                if (EditValueChanged != null)
                    EditValueChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "RadioListBoxControl.OnEditValueChanged()", MessageBoxIcon.Error);
            }
        }
        protected virtual void OnEditValueChanging(ChangingEventArgs e)
        {
            if (IsLoading)
            {
                OldEditValue = _editValue = e.NewValue;
                if (!CompareEditValue(e.NewValue))
                    OnEditValueChanged();
            }
            else
            {
                if (!CompareEditValue(_editValue, e.NewValue))
                {
                    if (EditValueChanging != null)
                        EditValueChanging(this, e);
                    if (e.Cancel) return;

                    OldEditValue = _editValue;
                    _editValue = e.NewValue;

                    OnEditValueChanged();
                }
            }
        }



        //public virtual object EditValue
        //{
        //    get
        //    {
        //        if (CheckedItems.Count == 0) return null;
        //        return CheckedItems[0];
        //    }
        //    set
        //    {
        //        var index = FindValueIndex(value, 0);
        //        if (index != -1)
        //            SetItemChecked(index, true);
        //        //todo: RadioListBoxControl.EditValue
        //    }
        //}

        private class PainterRadioListBox : PainterCheckedListBox
        {
            protected override void DrawItemCore(ControlGraphicsInfoArgs info, BaseListBoxViewInfo.ItemInfo itemInfo, ListBoxDrawItemEventArgs e)
            {
                itemInfo.State = DrawItemState.None;
                ((CheckedListBoxViewInfo.CheckedItemInfo)itemInfo).CheckArgs.CheckStyle = CheckStyles.Radio;
                base.DrawItemCore(info, itemInfo, e);
            }
        }
    }
}
