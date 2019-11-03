using System;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace Zek.Windows.Forms
{
    [Serializable]
    public enum InputLanguages
    {
        US = 1033,
        Russian = 1049,
        Georgian = 1079
    }

    public class InputLanguageHelper
    {
        //private static string GetLayoutName(InputLanguages lang)
        //{
        //    switch (lang)
        //    {
        //        case InputLanguages.US:
        //            return "US";
        //        case InputLanguages.Georgian:
        //            return "Georgian (Lat)";
        //        case InputLanguages.Russian:
        //            return "Russian";
        //    }

        //    return string.Empty;
        //}
        //public static void SetCurrentInputLanguage(InputLanguages lang)
        //{
        //    string layoutName = GetLayoutName(lang);
        //    if (layoutName.Length == 0)
        //        return;

        //    for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
        //    {
        //        if (InputLanguage.InstalledInputLanguages[i].LayoutName == layoutName)
        //        {
        //            if (InputLanguage.CurrentInputLanguage.LayoutName != InputLanguage.InstalledInputLanguages[i].LayoutName)
        //                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[i];
        //            return;
        //        }
        //    }
        //}

        public static void SetCurrentInputLanguage(string name)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo(name));
        }
        public static void SetCurrentInputLanguage(InputLanguages lang)
        {
            SetCurrentInputLanguage((int)lang);
        }
        public static void SetCurrentInputLanguage(int culture)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo(culture));
        }

        public static string[] GetInstalledInputLanguages()
        {
            var layoutNames = new string[InputLanguage.InstalledInputLanguages.Count];
            for (var i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                var layoutName = InputLanguage.InstalledInputLanguages[i].LayoutName;
                layoutNames[i] = layoutName;
                //comboBox.Items.Add(System.Windows.Forms.InputLanguage.InstalledInputLanguages[i].LayoutName);
            }
            //Application.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[comboBox.SelectedIndex];
            return layoutNames;
        }

        /// <summary>
        /// ინიციალიზაციას უკეთებს კულტურას.
        /// </summary>
        public static void InitGeorgianCultureInfo()
        {
            InitCultureInfo("ka-GE");
        }
        /// <summary>
        /// ინიციალიზაციას უკეთებს კულტურას.
        /// </summary>
        /// <param name="name"></param>
        public static void InitCultureInfo(string name)
        {
            InitCultureInfo(new CultureInfo(name));
        }
        /// <summary>
        /// ინიციალიზაციას უკეთებს კულტურას.
        /// </summary>
        /// <param name="ci">კულტურა.</param>
        public static void InitCultureInfo(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ka-GE");
            //Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            //Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
