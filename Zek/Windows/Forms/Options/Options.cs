using System;
using System.ComponentModel;
using System.Collections;
using Zek.Extensions;

namespace Zek.Windows.Forms
{
    #region BaseOptionChangedEventArgs
    public class BaseOptionChangedEventArgs : EventArgs
    {
        public BaseOptionChangedEventArgs() : this(string.Empty, null, null) { }
        public BaseOptionChangedEventArgs(string name, object oldValue, object newValue)
        {
            _name = name;
            _oldValue = oldValue;
            NewValue = newValue;
        }

        #region Fields

        readonly string _name;
        readonly object _oldValue;

        #endregion

        #region Properties
        public string Name => _name;
        public object OldValue => _oldValue;
        public object NewValue { get; set; }

        #endregion
    }
    #endregion

    #region BaseOptionChangedEventHandler
    public delegate void BaseOptionChangedEventHandler(object sender, BaseOptionChangedEventArgs e);
    #endregion
    
    #region BaseOptions
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BaseOptions : INotifyPropertyChanged
    {
        private int _lockUpdate;
        protected internal BaseOptionChangedEventHandler ChangedCore;
        [Category("Events")]
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Assign(BaseOptions options) { }
        public virtual void BeginUpdate() { _lockUpdate++; }
        public virtual void EndUpdate()
        {
            if (--_lockUpdate == 0)
            {
                OnChanged(new BaseOptionChangedEventArgs());
            }
        }
        public virtual void CancelUpdate() { _lockUpdate--; }
        protected internal virtual bool IsLockUpdate => _lockUpdate != 0;


        protected void OnChanged(string option, bool oldValue, bool newValue)
        {
            OnChanged(option, oldValue ? true : (object)false, newValue ? true : (object)false);
        }
        protected void OnChanged(string option, object oldValue, object newValue)
        {
            if (IsLockUpdate) return;
            RaiseOnChanged(new BaseOptionChangedEventArgs(option, oldValue, newValue));
        }
        
        protected internal virtual void OnChanged(BaseOptionChangedEventArgs e)
        {
            RaisePropertyChanged(e.Name);

            if (IsLockUpdate) return;
            RaiseOnChanged(e);
        }
        protected virtual void RaiseOnChanged(BaseOptionChangedEventArgs e)
        {
            if (ChangedCore != null) ChangedCore(this, e);
        }
        protected internal virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }



        public override string ToString()
        {
            var res = string.Empty;
            var pdsColl = TypeDescriptor.GetProperties(this);
            var pds = new PropertyDescriptor[pdsColl.Count];
            pdsColl.CopyTo(pds, 0);
            Array.Sort(pds, new PDComparer());
            foreach (var pd in pds)
            {
                if (pd.IsBrowsable && pd.PropertyType == typeof(bool) && (bool)pd.GetValue(this) && !string.IsNullOrEmpty(pd.Name))
                {
                    if (res.Length != 0) res += ", ";
                    res += pd.Name;
                }
            }
            return res;
        }

        protected static bool Compare(object val1, object val2)
        {
            return val1.Compare(val2);
        }

        class PDComparer : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                PropertyDescriptor pd1 = a as PropertyDescriptor, pd2 = b as PropertyDescriptor;
                return Comparer.Default.Compare(pd1.Name, pd2.Name);
            }
        }
        public virtual void Reset()
        {
            var pdColl = TypeDescriptor.GetProperties(this);
            BeginUpdate();
            try
            {
                foreach (PropertyDescriptor pd in pdColl)
                {
                    pd.ResetValue(this);
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }
    #endregion


    //ფროფერთიებზე აქვს ყველას.
    //DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)



    //[Category("Events"), Description("წამოიშვება მაშინვე როცა შეიცვლება AutoCheckPermission.")]
    //public event EventHandler EditValueChanged
    //{
    //    add { this.Events.AddHandler(editValueChanged, value); }
    //    remove { this.Events.RemoveHandler(editValueChanged, value); }
    //}
}
