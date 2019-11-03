using System;
using System.Reflection;
using System.Windows.Forms;
using Zek.Extensions;

namespace Zek.Windows.Forms
{
    public class ControlHelper
    {
        /// <summary>
        /// Finds a control recursive
        /// </summary>
        /// <typeparam name="T">Control class</typeparam>
        /// <param name="controls">Input control collection</param>
        /// <returns>Found control</returns>
        public static T FindControlRecursive<T>(Control.ControlCollection controls) where T : class
        {
            var found = default(T);

            if (controls != null && controls.Count > 0)
            {
                for (var i = 0; i < controls.Count; i++)
                {
                    if (controls[i] is T)
                    {
                        found = controls[i] as T;
                        break;
                    }
                    found = FindControlRecursive<T>(controls[i].Controls);
                    if (found != null)
                        break;
                }
            }

            return found;
        }

        public static void SetReadOnlyControlRecursive(Control control, bool readOnly)
        {
            if (control == null) return;

            var pi = control.GetType().GetProperty("ReadOnly", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (pi != null && pi.CanWrite)
                pi.SetValue(control, readOnly, null);

            foreach (Control ctrl in control.Controls)
            {
                SetReadOnlyControlRecursive(ctrl, readOnly);
            }
        }

        public static void SetEnabledControlRecursive(Control control, bool enabed)
        {
            if (control == null) return;

            var pi = control.GetType().GetProperty("Enabled", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (pi != null && pi.CanWrite)
                pi.SetValue(control, enabed, null);

            foreach (Control ctrl in control.Controls)
            {
                SetEnabledControlRecursive(ctrl, enabed);
            }
        }

        public static void SetChildrenReadOnly(Control ctrl, bool readOnly)
        {
            SetReadOnly(ctrl, false, true, readOnly);
        }

        public static void SetReadOnly(Control ctrl, bool readOnly)
        {
            SetReadOnly(ctrl, true, true, readOnly);
        }

        public static void SetReadOnly(Control ctrl, bool changeCurrent, bool changeChildren, bool readOnly)
        {
            if (!IsLabelControl(ctrl)) return;

            var isChanged = false;
            if (changeCurrent)
            {
                if (ctrl.TrySetPropertyValue("ReadOnly", readOnly))
                {
                    isChanged = true;
                }
                else if (!(ctrl is ScrollableControl))
                {
                    ctrl.Enabled = !readOnly;
                    isChanged = true;
                }
            }


            if (changeChildren && !isChanged && ctrl.Controls.Count > 0)
            {
                foreach (Control childControl in ctrl.Controls)
                    SetReadOnly(childControl, true, true, readOnly);
            }
        }

        public static void SetControlVisible(Control c, bool visible)
        {
            if (visible)
            {
                var indexZOrder = -1;
                if (c.Parent != null && c.Dock != DockStyle.None) indexZOrder = c.Parent.Controls.IndexOf(c);
                c.Visible = true;
                if (indexZOrder != -1) c.Parent.Controls.SetChildIndex(c, indexZOrder);
            }
            else
                c.Visible = false;
        }

        /// <summary>
        /// ამოწმებს სტანდარტული კონტროლია ტუ არა ეს (Label, LabelControl, ListBox, BaseListBoxControl).
        /// </summary>
        /// <param name="ctrl">კონტროლი რომლის შემოწმებაც გვინდა.</param>
        /// <returns>აბრუნებს trues თუ რომელიმე სტანდარტული კონტროლია.</returns>
        private static bool IsLabelControl(Control ctrl)
        {
            return !(ctrl is Label) &&
                   !(ctrl is ListBox);
        }

        /// <summary>
        /// Get's bounded control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Type GetBoundObjectType(Control control, string propertyName)
        {
            if (control == null) return null;

            var binding = control.DataBindings[propertyName];
            if (binding == null) return null;


            var bs = binding.DataSource as BindingSource;
            if (bs == null || bs.DataSource == null) return null;

            Type type;
            if (bs.DataSource is Type)
                type = bs.DataSource as Type;
            else
                type = bs.DataSource.GetType();
            var pi = type.GetProperty(binding.BindingMemberInfo.BindingField, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);


            return pi.PropertyType;
        }

    }
}
