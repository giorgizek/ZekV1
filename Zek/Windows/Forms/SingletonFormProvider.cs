using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Zek.Windows.Forms
{
    public class SingletonFormProvider
    {
        private static readonly Dictionary<string, Form> Forms = new Dictionary<string, Form>();
        private static void Remove(object sender, FormClosedEventArgs e)
        {
            var frm = sender as Form;
            if (frm == null) return;

            frm.FormClosed -= Remove;
            Forms.Remove(GetFormKey(frm));
        }
        private static string GetFormKey(Form frm)
        {
            IntPtr senderHandle;
            object primaryKey;

            GetProperties(frm, out senderHandle, out primaryKey);
            return GetFormKey(frm.GetType(), senderHandle, primaryKey);
        }
        private static string GetFormKey(Type formType, IntPtr senderHandle, object primaryKey)
        {
            return formType.GUID + "_" + senderHandle + (primaryKey != null ? "_" + primaryKey : string.Empty);
        }
        private static void SetProperties(Form frm, IntPtr senderHandle, object primaryKey)
        {
            if (frm == null || frm.IsDisposed) return;
            var type = frm.GetType();

            var pi = type.GetProperty("SenderHandle", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (pi != null && pi.CanWrite && pi.PropertyType.ToString() == "System.IntPtr")
            {
                pi.SetValue(frm, senderHandle, null);
            }

            pi = type.GetProperty("PrimaryKey", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (pi != null && pi.CanWrite)
            {
                pi.SetValue(frm, primaryKey, null);
            }
        }
        private static void GetProperties(Form frm, out IntPtr senderHandle, out object primaryKey)
        {
            senderHandle = IntPtr.Zero;
            primaryKey = null;

            if (frm == null || frm.IsDisposed) return;
            var type = frm.GetType();

            var pi = type.GetProperty("SenderHandle", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (pi != null && pi.CanRead && pi.PropertyType.ToString() == "System.IntPtr")
            {
                senderHandle = (IntPtr)pi.GetValue(frm, null);
            }

            pi = type.GetProperty("PrimaryKey", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (pi != null && pi.CanRead)
            {
                primaryKey = pi.GetValue(frm, null);
            }
        }


        public static T ShowInstance<T>(Form mdiParent = null, Form owner = null, IntPtr senderHandle = default(IntPtr), object primaryKey = null, params object[] args) where T : Form
        {
            return (T)ShowInstance(typeof(T), mdiParent, owner, senderHandle, primaryKey, args);
        }

        //public static T ShowInstance<T>() where T : Form
        //{
        //    return ShowInstance<T>(null, null, IntPtr.Zero, null, null);
        //}
        //public static T ShowInstance<T>(Form mdiParent) where T : Form
        //{
        //    return ShowInstance<T>(mdiParent, null, IntPtr.Zero, null, null);
        //}
        //public static T ShowInstance<T>(Form mdiParent, Form owner) where T : Form
        //{
        //    return ShowInstance<T>(mdiParent, owner, IntPtr.Zero, null, null);
        //}
        //public static T ShowInstance<T>(Form mdiParent, Form owner, IntPtr senderHandle) where T : Form
        //{
        //    return ShowInstance<T>(mdiParent, owner, senderHandle, null, null);
        //}
        //public static T ShowInstance<T>(Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey) where T : Form
        //{
        //    return ShowInstance<T>(mdiParent, owner, senderHandle, primaryKey, null);
        //}
        //public static T ShowInstance<T>(Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey, params object[] args) where T : Form
        //{
        //    return (T)ShowInstance(typeof(T), mdiParent, owner, senderHandle, primaryKey, args);
        //}


        //private static Form ShowInstance(Type formType)
        //{
        //    return ShowInstance(formType, null, null, IntPtr.Zero, null, null);
        //}
        //private static Form ShowInstance(Type formType, Form mdiParent)
        //{
        //    return ShowInstance(formType, mdiParent, null, IntPtr.Zero, null, null);
        //}
        //private static Form ShowInstance(Type formType, Form mdiParent, Form owner)
        //{
        //    return ShowInstance(formType, mdiParent, owner, IntPtr.Zero, null, null);
        //}
        //private static Form ShowInstance(Type formType, Form mdiParent, Form owner, IntPtr senderHandle)
        //{
        //    return ShowInstance(formType, mdiParent, owner, senderHandle, null, null);
        //}
        //private static Form ShowInstance(Type formType, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey)
        //{
        //    return ShowInstance(formType, mdiParent, owner, senderHandle, primaryKey, null);
        //}
        private static Form ShowInstance(Type formType, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey, params object[] args)
        {
            var formKey = GetFormKey(formType, senderHandle, primaryKey);

            Form frm = null;
            if (Forms.ContainsKey(formKey))
            {
                frm = Forms[formKey];
                if (frm.IsDisposed)
                {
                    Forms.Remove(formKey);
                    frm = null;
                }
            }

            if (frm == null)
            {
                frm = FormHelper.CreateInstance(formType, mdiParent, owner, args);
                SetProperties(frm, senderHandle, primaryKey);
                Forms.Add(formKey, frm);
                frm.FormClosed += Remove;
            }

            frm.Show();
            frm.BringToFront();
            return frm;
        }


        #region Enum Forms
        public static bool ContainsForm<T>(IntPtr senderHandle, object primaryKey)
        {
            return ContainsForm(typeof(T), senderHandle, primaryKey);
        }
        public static bool ContainsListForm(Enum formEnum, IntPtr senderHandle, object primaryKey)
        {
            if (formEnum == null) throw new ArgumentNullException(nameof(formEnum));

            var typekey = Convert.ToInt32(formEnum);
            if (!Types.ContainsKey(typekey)) return false;

            return ContainsForm(Types[typekey].List, senderHandle, primaryKey);
        }
        public static bool ContainsEditForm(Enum formEnum, IntPtr senderHandle, object primaryKey)
        {
            if (formEnum == null) throw new ArgumentNullException(nameof(formEnum));

            var typeKey = Convert.ToInt32(formEnum);
            if (!Types.ContainsKey(typeKey)) return false;

            return ContainsForm(Types[typeKey].Edit, senderHandle, primaryKey);
        }
        public static bool ContainsForm(Type formType, IntPtr senderHandle, object primaryKey)
        {
            var key = GetFormKey(formType, senderHandle, primaryKey);
            return Forms.ContainsKey(key);
        }

        private class Value
        {
            public Type List;
            public Type Edit;
        }
        private static readonly Dictionary<int, Value> Types = new Dictionary<int, Value>();
        public static void RegisterListForm<TListForm>(Enum formEnum) where TListForm : Form
        {
            Register<TListForm, Form>(formEnum);
        }
        public static void RegisterEditForm<TEditForm>(Enum formEnum) where TEditForm : Form
        {
            Register<Form, TEditForm>(formEnum);
        }
        public static void Register<TListForm, TEditForm>(Enum formEnum)
            where TListForm : Form
            where TEditForm : Form
        {
            if (formEnum == null) throw new ArgumentNullException(nameof(formEnum));
            var typeKey = Convert.ToInt32(formEnum);

            Value item;
            if (!Types.ContainsKey(typeKey))
            {
                item = new Value();
                Types.Add(typeKey, item);
            }

            item = new Value
            {
                List = typeof(TListForm) != typeof(Form) ? typeof(TListForm) : null,
                Edit = typeof(TEditForm) != typeof(Form) ? typeof(TEditForm) : null
            };

            if (item.List != null || item.Edit != null)
                Types[typeKey] = item;
            else
                Types.Remove(typeKey);
        }


        public static Form ShowListForm(Enum formEnum)
        {
            return ShowListForm(formEnum, null, null, IntPtr.Zero, null, null);
        }
        public static Form ShowListForm(Enum formEnum, Form mdiParent)
        {
            return ShowListForm(formEnum, mdiParent, null, IntPtr.Zero, null, null);
        }
        public static Form ShowListForm(Enum formEnum, Form mdiParent, Form owner)
        {
            return ShowListForm(formEnum, mdiParent, owner, IntPtr.Zero, null, null);
        }
        public static Form ShowListForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle)
        {
            return ShowListForm(formEnum, mdiParent, owner, senderHandle, null, null);
        }
        public static Form ShowListForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey)
        {
            return ShowListForm(formEnum, mdiParent, owner, senderHandle, primaryKey, null);
        }
        public static Form ShowListForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey, params object[] args)
        {
            if (formEnum == null) throw new ArgumentNullException(nameof(formEnum));
            var typeKey = Convert.ToInt32(formEnum);
            if (Types.ContainsKey(typeKey))
            {
                return ShowInstance(Types[typeKey].List, mdiParent, owner, senderHandle, primaryKey, args);
            }
            return null;
        }


        public static DialogResult ShowDialogForm(Enum formEnum)
        {
            return ShowDialogForm(formEnum, null, null, IntPtr.Zero, null, null);
        }
        public static DialogResult ShowDialogForm(Enum formEnum, Form mdiParent)
        {
            return ShowDialogForm(formEnum, mdiParent, null, IntPtr.Zero, null, null);
        }
        public static DialogResult ShowDialogForm(Enum formEnum, Form mdiParent, Form owner)
        {
            return ShowDialogForm(formEnum, mdiParent, owner, IntPtr.Zero, null, null);
        }
        public static DialogResult ShowDialogForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle)
        {
            return ShowDialogForm(formEnum, mdiParent, owner, senderHandle, null, null);
        }
        public static DialogResult ShowDialogForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey)
        {
            return ShowDialogForm(formEnum, mdiParent, owner, senderHandle, primaryKey, null);
        }
        public static DialogResult ShowDialogForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey, params object[] args)
        {
            if (formEnum == null) throw new ArgumentNullException(nameof(formEnum));

            var typeKey = Convert.ToInt32(formEnum);
            if (Types.ContainsKey(typeKey))
            {
                return FormHelper.ShowDialog(Types[typeKey].Edit, owner, primaryKey, args);
            }
            return DialogResult.Cancel;
        }


        public static Form ShowEditForm(Enum formEnum)
        {
            return ShowEditForm(formEnum, null, null, IntPtr.Zero, null, null);
        }
        public static Form ShowEditForm(Enum formEnum, Form mdiParent)
        {
            return ShowEditForm(formEnum, mdiParent, null, IntPtr.Zero, null, null);
        }
        public static Form ShowEditForm(Enum formEnum, Form mdiParent, Form owner)
        {
            return ShowEditForm(formEnum, mdiParent, owner, IntPtr.Zero, null, null);
        }
        public static Form ShowEditForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle)
        {
            return ShowEditForm(formEnum, mdiParent, owner, senderHandle, null, null);
        }
        public static Form ShowEditForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey)
        {
            return ShowEditForm(formEnum, mdiParent, owner, senderHandle, primaryKey, null);
        }
        public static Form ShowEditForm(Enum formEnum, Form mdiParent, Form owner, IntPtr senderHandle, object primaryKey, params object[] args)
        {
            if (formEnum == null) throw new ArgumentNullException(nameof(formEnum));
            var typeKey = Convert.ToInt32(formEnum);
            if (Types.ContainsKey(typeKey))
            {
                return ShowInstance(Types[typeKey].Edit, mdiParent, owner, senderHandle, primaryKey, args);
            }
            return null;
        }
        #endregion
    }
}
