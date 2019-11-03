using System;
using System.Windows.Forms;

namespace Zek.Windows.Forms
{
    public delegate void InvokeHandler(params object[] args);

    public class FormHelper
    {
        public static Form CreateInstance(string formTypeName)
        {
            return CreateInstance(formTypeName, null, null, null);
        }
        public static Form CreateInstance(string formTypeName, Form mdiParent)
        {
            return CreateInstance(formTypeName, mdiParent, null, null);
        }
        public static Form CreateInstance(string formTypeName, Form mdiParent, Form owner)
        {
            return CreateInstance(formTypeName, mdiParent, owner, null);
        }
        public static Form CreateInstance(string formTypeName, Form mdiParent, Form owner, params object[] args)
        {
            var formType = Type.GetType(formTypeName);
            return CreateInstance(formType, mdiParent, owner, args);
        }

        public static Form CreateInstance(Type formType)
        {
            return CreateInstance(formType, null, null, null);
        }
        public static Form CreateInstance(Type formType, params object[] args)
        {
            return CreateInstance(formType, null, null, args);
        }
        public static Form CreateInstance(Type formType, Form mdiParent)
        {
            return CreateInstance(formType, mdiParent, null, null);
        }
        public static Form CreateInstance(Type formType, Form mdiParent, Form owner)
        {
            return CreateInstance(formType, mdiParent, owner, null);
        }
        public static Form CreateInstance(Type formType, Form mdiParent, Form owner, params object[] args)
        {
            var frm = (Form)Activator.CreateInstance(formType, args);
            frm.MdiParent = mdiParent;
            
            if (mdiParent == null)
                frm.Owner = owner;

            return frm;
        }


        public static T CreateInstance<T>() where T : Form
        {
            return CreateInstance<T>(null, null, null);
        }
        public static T CreateInstance<T>(Form mdiParent) where T : Form
        {
            return CreateInstance<T>(mdiParent, null, null);
        }
        public static T CreateInstance<T>(params object[] args) where T : Form
        {
            return CreateInstance<T>(null, null, args);
        }
        public static T CreateInstance<T>(Form mdiParent, params object[] args) where T : Form
        {
            return CreateInstance<T>(mdiParent, null, args);
        }
        public static T CreateInstance<T>(Form mdiParent, Form owner, params object[] args) where T : Form
        {
            return (T)CreateInstance(typeof(T), mdiParent, owner, args);
        }

        public static Form Show(string formTypeName)
        {
            return Show(formTypeName, null, null, null);
        }
        public static Form Show(string formTypeName, Form mdiParent)
        {
            return Show(formTypeName, mdiParent, null, null);
        }
        public static Form Show(string formTypeName, params object[] args)
        {
            return Show(formTypeName, null, null, args);
        }
        public static Form Show(string formTypeName, Form mdiParent, Form owner)
        {
            return Show(formTypeName, mdiParent, owner, null);
        }
        public static Form Show(string formTypeName, Form mdiParent, Form owner, params object[] args)
        {
            var frm = CreateInstance(formTypeName, mdiParent, owner, args);
            frm.Show();
            frm.BringToFront();
            return frm;
        }

        public static T Show<T>() where T : Form
        {
            return Show<T>(null, null, null);
        }
        public static T Show<T>(Form mdiParent) where T : Form
        {
            return Show<T>(mdiParent, null, null);
        }
        public static T Show<T>(params object[] args) where T : Form
        {
            return Show<T>(null, null, args);
        }
        public static T Show<T>(Form mdiParent, Form owner) where T : Form
        {
            return Show<T>(mdiParent, owner, null);
        }
        public static T Show<T>(Form mdiParent, Form owner, params object[] args) where T : Form
        {
            var frm = CreateInstance<T>(mdiParent, owner, args);
            frm.Show();
            frm.BringToFront();
            return frm;
        }


        public static DialogResult ShowDialog(string formTypeName)
        {
            return ShowDialog(formTypeName, null, null);
        }
        public static DialogResult ShowDialog(string formTypeName, IWin32Window owner)
        {
            return ShowDialog(formTypeName, owner, null);
        }
        public static DialogResult ShowDialog(string formTypeName, params object[] args)
        {
            return ShowDialog(formTypeName, null, args);
        }
        public static DialogResult ShowDialog(string formTypeName, IWin32Window owner, params object[] args)
        {
            var frm = CreateInstance(formTypeName, null, null, args);
            if (frm == null) return DialogResult.Cancel;

            return frm.ShowDialog(owner);
        }


        public static DialogResult ShowDialog(Type formType)
        {
            return ShowDialog(formType, null, null);
        }
        public static DialogResult ShowDialog(Type formType, IWin32Window owner)
        {
            return ShowDialog(formType, owner, null);
        }
        public static DialogResult ShowDialog(Type formType, params object[] args)
        {
            return ShowDialog(formType, null, args);
        }
        public static DialogResult ShowDialog(Type formType, IWin32Window owner, params object[] args)
        {
            var frm = CreateInstance(formType, null, null, args);
            if (frm == null) return DialogResult.Cancel;

            return frm.ShowDialog(owner);
        }


        public static DialogResult ShowDialog<T>() where T : Form
        {
            return ShowDialog<T>(null, null);
        }
        public static DialogResult ShowDialog<T>(IWin32Window owner) where T : Form
        {
            return ShowDialog<T>(owner, null);
        }
        public static DialogResult ShowDialog<T>(params object[] args) where T : Form
        {
            return ShowDialog<T>(null, args);
        }
        public static DialogResult ShowDialog<T>(IWin32Window owner, params object[] args) where T : Form
        {
            var frm = CreateInstance<T>(args);
            if (frm == null) return DialogResult.Cancel;

            return frm.ShowDialog(owner);
        }
    }
}