using System.Drawing;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.UserSkins;

namespace Zek.Windows.Forms.DevEx
{
    /// <summary>
    /// DevExpress -ის კონტროლების მენეჯერი
    /// </summary>
    public class XtraControlHelper
    {
        //private static readonly Font _DefaultFont = new Font("BPG Glaho Arial", 9.75F);
        //private static readonly Font _DefaultTabPageFont = new Font("BPG Glaho Arial", 8.75F);

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
            if (!CheckControl(ctrl)) return;

            var isChanged = false;
            if (changeCurrent)
            {
                if (ctrl is BaseEdit)
                {
                    ((BaseEdit)ctrl).Properties.ReadOnly = readOnly;
                    isChanged = true;
                }
                else if (!(ctrl is ScrollableControl))
                {
                    ctrl.Enabled = !readOnly;
                    isChanged = true;
                }
            }

            
            if (!(!changeChildren || isChanged || ctrl.Controls.Count <= 0 || ctrl is BaseEdit))
            {
                foreach (Control childControl in ctrl.Controls)
                    SetReadOnly(childControl, true, true, readOnly);
            }
        }

        /// <summary>
        /// ამოწმებს სტანდარტული კონტროლია ტუ არა ეს (Label, LabelControl, ListBox, BaseListBoxControl).
        /// </summary>
        /// <param name="ctrl">კონტროლი რომლის შემოწმებაც გვინდა.</param>
        /// <returns>აბრუნებს trues თუ რომელიმე სტანდარტული კონტროლია.</returns>
        private static bool CheckControl(Control ctrl)
        {
            return !(ctrl is Label) &&
                   !(ctrl is LabelControl) &&
                   !(ctrl is ListBox) &&
                   !(ctrl is BaseListBoxControl);
        }

        //public static void SetAppearanceObjectFont(AppearanceObject appearanceObject)
        //{
        //    SetAppearanceObjectFont(appearanceObject, _DefaultFont);
        //}

        //public static void SetPageAppearanceFont(PageAppearance appearancePage)
        //{
        //    SetPageAppearanceFont(appearancePage, _DefaultTabPageFont);
        //}
        public static void SetPageAppearanceFont(PageAppearance appearancePage, Font font)
        {
            SetAppearanceObjectFont(appearancePage.Header, font);
            //SetAppearanceObjectFont(appearancePage.HeaderActive, font);
            SetAppearanceObjectFont(appearancePage.PageClient, font);
        }
        public static void SetAppearanceObjectFont(AppearanceObject appearanceObject, Font font)
        {
            appearanceObject.Font = font;
        }

        /// <summary>
        /// უკეთებს ინიციალიზაციას DevExpress კონტროლებს.
        /// უნდა გაეშვას პროგრამის დაწყებისთანავე.
        /// </summary>
        public static void InitDevExpress(bool registerSkins = true)
        {
            InitDevExpress(new Font("BPG Glaho Arial", 8.25F), registerSkins);
        }

        /// <summary>
        /// უკეთებს ინიციალიზაციას DevExpress კონტროლებს.
        /// უნდა გაეშვას პროგრამის დაწყებისთანავე.
        /// </summary>
        /// <param name="defaultFont">DevExpress კონტროლების სტანდარტული შრიფტი.</param>
        /// <param name="registerSkins"></param>
        public static void InitDevExpress(Font defaultFont, bool registerSkins = true)
        {
            using (new XtraForm())
            { }//ეს კვერცხობა იმიტომ უნდა რო. ყველა ფორმა დიდებოდა :( წინა ვერსიაში არ ჭირდებოდა

            if (registerSkins)
                RegisterSkins();

            AppearanceObject.ControlAppearance.Font = defaultFont;
            AppearanceObject.DefaultFont = defaultFont;
            AppearanceObject.EmptyAppearance.Font = defaultFont;
            SkinManager.EnableFormSkinsIfNotVista();
        }
        /// <summary>
        /// არეგისტრირებს სკინებს (Office).
        /// </summary>
        public static void RegisterSkins()
        {
            BonusSkins.Register();
        }
    }
}
