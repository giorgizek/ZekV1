using System;
using System.Windows.Forms;
using System.Drawing;

namespace Zek.Windows.Forms
{
    public class MenuHelper
    {
        public static ToolStripMenuItem CreateToolStripMenuItem(string text, Keys keys, Image image)
        {
            var item = new ToolStripMenuItem();
            if (image != null)
                item.Image = image;

            item.Name = text;
            if (keys != Keys.None)
                item.ShortcutKeys = keys;

            //this.tsiLogIn.Size = new System.Drawing.Size(183, 30);
            item.Text = text;
            //this.tsiLogIn.Click += new System.EventHandler(this.tsiLogIn_Click);

            return item;
        }
        
        public static ToolStripMenuItem AddToolStripMenu(MenuStrip menuStrip, int index, string text, Image image)
        {
            return AddToolStripMenu(menuStrip, index, text, Keys.None, image);
        }
        public static ToolStripMenuItem AddToolStripMenu(MenuStrip menuStrip, int index, string text)
        {
            return AddToolStripMenu(menuStrip, index, text, Keys.None);
        }
        public static ToolStripMenuItem AddToolStripMenu(MenuStrip menuStrip, int index, string text, Keys keys)
        {
            return AddToolStripMenu(menuStrip, index, text, keys, null);
        }
        public static ToolStripMenuItem AddToolStripMenu(MenuStrip menuStrip, int index, string text, Keys keys, Image image)
        {
            var subItem = CreateToolStripMenuItem(text, keys, image);

            Add(menuStrip, subItem);

            return subItem;
        }
        public static ToolStripMenuItem InsertToolStripMenu(MenuStrip menuStrip, int index, string text, Image image)
        {
            return InsertToolStripMenu(menuStrip, index, text, Keys.None, image);
        }
        public static ToolStripMenuItem InsertToolStripMenu(MenuStrip menuStrip, int index, string text)
        {
            return InsertToolStripMenu(menuStrip, index, text, Keys.None);
        }
        public static ToolStripMenuItem InsertToolStripMenu(MenuStrip menuStrip, int index, string text, Keys keys)
        {
            return InsertToolStripMenu(menuStrip, index, text, keys, null);
        }
        public static ToolStripMenuItem InsertToolStripMenu(MenuStrip menuStrip, int index, string text, Keys keys, Image image)
        {
            var subItem = CreateToolStripMenuItem(text, keys, image);

            Insert(menuStrip, index, subItem);

            return subItem;
        }


        public static ToolStripSeparator AddToolStripSeparator(ToolStrip toolStrip)
        {
            var separator = new ToolStripSeparator();
            Add(toolStrip, separator);
            return separator;
        }
        public static ToolStripComboBox AddToolStripComboBox(ToolStrip toolStrip, ComboBox comboBox)
        {
            return AddToolStripComboBox(toolStrip, comboBox, null);
        }
        public static ToolStripComboBox AddToolStripComboBox(ToolStrip toolStrip, ComboBox comboBox, EventHandler selectedIndexChanged)
        {
            var subItem = new ToolStripComboBox();
            subItem.ComboBox.DropDownStyle = comboBox.DropDownStyle;
            foreach (var item in comboBox.Items)
            {
                subItem.ComboBox.Items.Add(item);
            }

            subItem.ComboBox.DataSource = comboBox.DataSource;
            subItem.ComboBox.DisplayMember = comboBox.DisplayMember;
            subItem.ComboBox.ValueMember = comboBox.ValueMember;
            subItem.ComboBox.SelectedIndexChanged += selectedIndexChanged;

            Add(toolStrip, subItem);
            return subItem;
        }

        public static void Add(ToolStrip toolStrip, ToolStripItem subItem)
        {
            toolStrip.Items.Add(subItem);
        }
        public static void Insert(MenuStrip toolStrip, int index, ToolStripItem subItem)
        {
            toolStrip.Items.Insert(index, subItem);
        }


        public static ToolStripMenuItem AddToolStripMenu(ToolStripMenuItem item, string text, Image image)
        {
            return AddToolStripMenu(item, text, Keys.None, image);
        }
        public static ToolStripMenuItem AddToolStripMenu(ToolStripMenuItem item, string text)
        {
            return AddToolStripMenu(item, text, Keys.None);
        }
        public static ToolStripMenuItem AddToolStripMenu(ToolStripMenuItem item, string text, Keys keys)
        {
            return AddToolStripMenu(item, text, keys, null);
        }
        public static ToolStripMenuItem AddToolStripMenu(ToolStripMenuItem item, string text, Keys keys, Image image)
        {
            return AddToolStripMenu(item, text, keys, image, null);
        }
        public static ToolStripMenuItem AddToolStripMenu(ToolStripMenuItem item, string text, Keys keys, Image image, EventHandler itemClickMethod)
        {
            var subItem = CreateToolStripMenuItem(text, keys, image);
            if (itemClickMethod != null)
                subItem.Click += itemClickMethod;

            Add(item, subItem);

            return subItem;
        }
        public static void Add(ToolStripMenuItem item, ToolStripItem subItem)
        {
            item.DropDownItems.Add(subItem);
        }
        public static ToolStripMenuItem InsertToolStripMenu(ToolStripMenuItem subItem, int index, string text, Image image)
        {
            return InsertToolStripMenu(subItem, index, text, Keys.None, image);
        }
        public static ToolStripMenuItem InsertToolStripMenu(ToolStripMenuItem subItem, int index, string text)
        {
            return InsertToolStripMenu(subItem, index, text, Keys.None);
        }
        public static ToolStripMenuItem InsertToolStripMenu(ToolStripMenuItem subItem, int index, string text, Keys keys)
        {
            return InsertToolStripMenu(subItem, index, text, keys, null);
        }
        public static ToolStripMenuItem InsertToolStripMenu(ToolStripMenuItem item, int index, string text, Keys keys, Image image)
        {
            var subItem = CreateToolStripMenuItem(text, keys, image);

            Insert(item, index, subItem);

            return subItem;
        }
        public static void Insert(ToolStripMenuItem item, int index, ToolStripItem subItem)
        {
            item.DropDownItems.Insert(index, subItem);
        }        
    }
}
