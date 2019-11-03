using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace Zek.Extensions.DevEx
{
    public static class XtraBarExtentions
    {
        /// <summary>
        /// Add BarItem into bar.
        /// </summary>
        /// <typeparam name="T">Type of BarItem</typeparam>
        /// <param name="bar">Bar</param>
        /// <param name="caption">Caption of BarItem</param>
        /// <param name="glyph">Glyph of BarItem</param>
        /// <param name="key">key of BarItem (ItemShortcut).</param>
        /// <param name="itemClick">ItemClick of BarItem</param>
        /// <returns>Returns BarItem</returns>
        public static T AddBarItem<T>(this Bar bar, string caption, Image glyph = null, Keys key = Keys.None, ItemClickEventHandler itemClick = null) where T : BarItem, new()
        {
            var item = new T
            {
                Caption = caption,
                Glyph = glyph
            };

            if (key != Keys.None)
                item.ItemShortcut = new BarShortcut(key);

            bar.AddItem(item);
            if (itemClick != null)
                item.ItemClick += itemClick;
            return item;
        }
        /// <summary>
        /// Add BarItem into barSubItem.
        /// </summary>
        /// <typeparam name="T">Type of BarItem.</typeparam>
        /// <param name="barSubItem">Owner item</param>
        /// <param name="caption">Caption of BarItem.</param>
        /// <param name="glyph">Glyph of BarItem.</param>
        /// <param name="key">Key of BarItem (ItemShortcut).</param>
        /// <param name="itemClick">ItemClick of BarItem.</param>
        /// <returns>Returns added BarItem</returns>
        public static T AddBarItem<T>(this BarSubItem barSubItem, string caption, Image glyph, Keys key = Keys.None, ItemClickEventHandler itemClick = null) where T : BarItem, new()
        {
            var item = new T
            {
                Caption = caption,
                Glyph = glyph
            };

            if (key != Keys.None)
                item.ItemShortcut = new BarShortcut(key);

            barSubItem.AddItem(item);
            if (itemClick != null)
                item.ItemClick += itemClick;
            return item;
        }

        /// <summary>
        /// Insert BarItem into bar.
        /// </summary>
        /// <typeparam name="T">Type of BarItem.</typeparam>
        /// <param name="bar">Bar</param>
        /// <param name="index">Index of BarItem.</param>
        /// <param name="caption">Caption of BarItem.</param>
        /// <param name="glyph">Glyph of BarItem.</param>
        /// <param name="key">Key of BarItem (ItemShortcut).</param>
        /// <param name="itemClick">ItemClick of BarItem.</param>
        /// <returns>Returns inserted BarItem</returns>
        public static T InsertBarItem<T>(this Bar bar, int index, string caption, Image glyph = null, Keys key = Keys.None, ItemClickEventHandler itemClick = null) where T : BarItem, new()
        {
            var item = new T
            {
                Caption = caption,
                Glyph = glyph
            };

            if (key != Keys.None)
                item.ItemShortcut = new BarShortcut(key);


            if (bar.LinksPersistInfo.Count > 0 && bar.ItemLinks.Count == 0)
            {
                if (index > bar.LinksPersistInfo.Count)
                    index = bar.LinksPersistInfo.Count;

                bar.LinksPersistInfo.Insert(index, new LinkPersistInfo(item));
            }
            else if (bar.ItemLinks.Count > 0)
            {
                if (index > bar.ItemLinks.Count)
                    index = bar.ItemLinks.Count;

                bar.InsertItem(bar.ItemLinks[index], item);
            }
            else
            {
                bar.AddItem(item);
            }

            //bar.ItemLinks.Insert(index, item);

            if (itemClick != null)
                item.ItemClick += itemClick;

            return item;
        }

        /// <summary>
        /// Add AddBarCheckItem into bar.
        /// </summary>
        /// <typeparam name="T">Type of BarCheckItem.</typeparam>
        /// <param name="bar">Bar</param>
        /// <param name="caption">Caption of BarCheckItem.</param>
        /// <param name="glyph">Glyph of BarCheckItem.</param>
        /// <param name="key">Key of BarCheckItem (ItemShortcut).</param>
        /// <param name="checkedChanged">CheckedChanged of BarCheckItem.</param>
        /// <returns>Returns inserted BarItem</returns>
        public static BarCheckItem AddBarCheckItem<T>(this Bar bar, string caption, Image glyph = null, Keys key = Keys.None, ItemClickEventHandler checkedChanged = null) where T : BarCheckItem, new()
        {
            var item = AddBarItem<T>(bar, caption, glyph, key);

            if (checkedChanged != null && item != null)
                item.CheckedChanged += checkedChanged;

            return item;
        }
        /// <summary>
        /// Add AddBarCheckItem into bar.
        /// </summary>
        /// <typeparam name="T">Type of BarCheckItem.</typeparam>
        /// <param name="barSubItem">BarSubItem</param>
        /// <param name="caption">Caption of BarCheckItem.</param>
        /// <param name="glyph">Glyph of BarCheckItem.</param>
        /// <param name="key">Key of BarCheckItem (ItemShortcut).</param>
        /// <param name="checkedChanged">CheckedChanged of BarCheckItem.</param>
        /// <returns>Returns inserted BarItem</returns>
        public static T AddBarCheckItem<T>(this BarSubItem barSubItem, string caption, Image glyph = null, Keys key = Keys.None, ItemClickEventHandler checkedChanged = null) where T : BarCheckItem, new()
        {
            var item = AddBarItem<T>(barSubItem, caption, glyph, key);

            if (checkedChanged != null && item != null)
                item.CheckedChanged += checkedChanged;

            return item;
        }





        /// <summary>
        /// ქმნის ქვემენიუს barItem-ის ადგილას და სვამს barItem-ს მასში
        /// </summary>
        /// <param name="barItem">მენიუს Item, რომლის ჩასმაც გვინდა SubMenu-ში.</param>
        /// <param name="caption">ახალ SubMenu-ს დასახელება.</param>
        /// <returns>აბრუნებს ახალ SubMenu-ს.</returns>
        public static BarSubItem MoveIntoBarSubItem(this BarItem barItem, string caption)
        {
            return MoveIntoBarSubItem(barItem, caption, null);
        }
        /// <summary>
        /// ქმნის ქვემენიუს barItem-ის ადგილას და სვამს barItem-ს მასში
        /// </summary>
        /// <param name="barItem">მენიუს Item, რომლის ჩასმაც გვინდა SubMenu-ში.</param>
        /// <param name="caption">ახალ SubMenu-ს დასახელება.</param>
        /// <param name="subMenuGlyph">ახალ SubMenu-ს იკონკა.</param>
        /// <returns>აბრუნებს ახალ SubMenu-ს.</returns>
        public static BarSubItem MoveIntoBarSubItem(this BarItem barItem, string caption, Image subMenuGlyph)
        {
            return MoveIntoBarSubItem(barItem, caption, subMenuGlyph, BarItemPaintStyle.Standard);
        }
        /// <summary>
        /// ქმნის ქვემენიუს barItem-ის ადგილას და სვამს barItem-ს მასში
        /// </summary>
        /// <param name="barItem">მენიუს Item, რომლის ჩასმაც გვინდა SubMenu-ში.</param>
        /// <param name="caption">ახალ SubMenu-ს დასახელება.</param>
        /// <param name="glyph">ახალ SubMenu-ს იკონკა.</param>
        /// <param name="paintStyle">ახალ SubMenu-ს ვიზუალური სტილი.</param>
        /// <returns>აბრუნებს ახალ SubMenu-ს.</returns>
        public static BarSubItem MoveIntoBarSubItem(this BarItem barItem, string caption, Image glyph, BarItemPaintStyle paintStyle)
        {
            barItem.Manager.ForceInitialize();

            var ownerItem = barItem.Links[0].OwnerItem as BarSubItem;
            Bar bar = null;
            int index;
            var beginGroup = barItem.Links[0].BeginGroup;

            if (ownerItem != null)
                index = FindLinkIndex(ownerItem, barItem);
            else
            {
                bar = barItem.Links[0].Bar;
                index = FindLinkIndex(bar, barItem);
            }

            barItem.Links.Clear();
            var subMenu = new BarSubItem(barItem.Manager, caption, new[] { barItem })
            {
                Glyph = glyph,
                PaintStyle = paintStyle
            };


            if (ownerItem != null)
                ownerItem.ItemLinks.Insert(index, subMenu).BeginGroup = beginGroup;
            else
                bar.ItemLinks.Insert(index, subMenu).BeginGroup = beginGroup;

            return subMenu;
        }



        /// <summary>
        /// პოულობს ლინკის ინდექს.
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="barItem"></param>
        /// <returns></returns>
        private static int FindLinkIndex(Bar bar, BarItem barItem)
        {
            int i;
            for (i = 0; i < bar.ItemLinks.Count; i++)
            {
                if (bar.ItemLinks[i].Item == barItem)
                    return i;
            }

            return i;
        }
        /// <summary>
        /// პოულობს ლინკის ინდექს.
        /// </summary>
        /// <param name="ownerMenu"></param>
        /// <param name="barItem"></param>
        /// <returns></returns>
        private static int FindLinkIndex(BarSubItem ownerMenu, BarItem barItem)
        {
            int i;
            for (i = 0; i < ownerMenu.ItemLinks.Count; i++)
            {
                if (ownerMenu.ItemLinks[i].Item == barItem)
                    return i;
            }

            return i;
        }



        //#region Skins
        //private static void OnFormSkins_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (DevExpress.Skins.SkinManager.AllowFormSkins)
        //    {
        //        DevExpress.Skins.SkinManager.DisableFormSkins();
        //    }
        //    else
        //    {
        //        DevExpress.Skins.SkinManager.EnableFormSkins();
        //    }

        //    DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        //}
        //#endregion
    }



    /*public class XtraBarHelper
    {
        public XtraBarHelper(BarManager barManager, BarSubItem subItem, BarCheckItem miAllowFormSkins, string skinName, bool allowFormSkins)
        {
            _BarManager = barManager;
            _SubItem = subItem;
            _miAllowFormSkins = miAllowFormSkins;


            _BarManager.ForceInitialize();
            for (int i = 0; i < DevExpress.Skins.SkinManager.Default.Skins.Count; i++)
            {
                DevExpress.XtraBars.BarButtonItem item = new DevExpress.XtraBars.BarButtonItem(_BarManager, _SkinMask + DevExpress.Skins.SkinManager.Default.Skins[i].SkinName);
                _SubItem.AddItem(item);
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(OnSkinClick);

                if (skinName == DevExpress.Skins.SkinManager.Default.Skins[i].SkinName && skinName != DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
                    OnSkinClick(this, new DevExpress.XtraBars.ItemClickEventArgs(item, (item.Links.Count > 0 ? item.Links[0] : null)));
            }


            if (DevExpress.Skins.SkinManager.AllowFormSkins)
                DevExpress.Skins.SkinManager.DisableFormSkins();

            if (allowFormSkins)
                OnSwitchFormSkinStyle_Click(this, new DevExpress.XtraBars.ItemClickEventArgs(_miAllowFormSkins, (miAllowFormSkins.Links.Count > 0 ? miAllowFormSkins.Links[0] : null)));
        }

        private string _SkinMask = "თემა: ";
        private BarManager _BarManager;
        private BarSubItem _SubItem;
        private BarCheckItem _miAllowFormSkins;

        private void OnSwitchFormSkinStyle_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DevExpress.Skins.SkinManager.AllowFormSkins)
            {
                DevExpress.Skins.SkinManager.DisableFormSkins();
                _miAllowFormSkins.Checked = false;
            }
            else
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
                _miAllowFormSkins.Checked = true;
            }
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        }

        private void OnSkinClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string skinName = e.Item.Caption.Replace(_SkinMask, string.Empty);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            _BarManager.GetController().PaintStyleName = "Skin";
            _SubItem.Caption = e.Item.Caption;
            _SubItem.Hint = _SubItem.Caption;
            _SubItem.ImageIndex = -1;
        }

        public void IPS_Init()
        {
            DevExpress.XtraBars.BarItem item = null;
            for (int i = 0; i < _BarManager.Items.Count; i++)
                if (_BarManager.Items[i].Description == _BarManager.GetController().PaintStyleName)
                    item = _BarManager.Items[i];
            InitPaintStyle(item);
        }

        public void InitPaintStyle(BarItem item)
        {
            if (item == null) return;
            _SubItem.ImageIndex = item.ImageIndex;
            _SubItem.Caption = item.Caption;
            _SubItem.Hint = item.Description;
        }
    }*/
}
