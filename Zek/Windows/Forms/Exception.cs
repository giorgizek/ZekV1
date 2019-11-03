using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Zek.Core;

namespace Zek.Windows.Forms
{
    ///// <summary>
    ///// Custom შეცდომის დროს ფორმის ტიპები.
    ///// გამოიყენება SQL-ში RAISERROR('', 16, 1)-ის დროს.
    ///// </summary>
    //[Serializable]
    //public enum CustomExceptionFormTypes
    //{
    //    /// <summary>
    //    /// იძახებს ExceptionForm.Show()-ს.
    //    /// </summary>
    //    ExceptionForm,
    //    /// <summary>
    //    /// იძახებს MessageBox.Show()-ს.
    //    /// </summary>
    //    MessageBox,
    //    /// <summary>
    //    /// იძახებს XtraMessagebox.Show()-ს.
    //    /// </summary>
    //    XtraMessageBox
    //}

    [Serializable]
    public class MessageBoxException : Exception
    {
        public MessageBoxException()
        {

        }
        public MessageBoxException(string message)
            : base(message)
        {
        }
        public MessageBoxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    [Serializable]
    public class XtraMessageBoxException : Exception
    {
        public XtraMessageBoxException()
        {

        }
        public XtraMessageBoxException(string message)
            : base(message)
        {
        }
        public XtraMessageBoxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }


    public class ExceptionHelper
    {
        //private static CustomExceptionFormTypes _customExceptionFormType = CustomExceptionFormTypes.MessageBox;
        ///// <summary>
        ///// გამოიყენება SQL-ში RAISERROR('', 16, 1)-ის დროს.
        ///// </summary>
        //public static CustomExceptionFormTypes CustomExceptionFormType
        //{
        //    get { return _customExceptionFormType; }
        //    set { _customExceptionFormType = value; }
        //}

        //[DllImport("user32.dll")]
        //private static extern bool MessageBeep(int uType);

        private static string MessageBoxIconTostring(MessageBoxIcon icon)
        {
            
            return icon != MessageBoxIcon.None ? icon.ToString() : "Exception";
        }

        /// <summary>
        /// იჭერს ყველა შეცდომას.
        /// </summary>
        public static void HandleAllExceptions()
        {
            HandleUnhandledException();
            HandleThreadException();
        }

        /// <summary>
        /// იჭერს Unhandled Thread Exception-ს
        /// </summary>
        public static void HandleThreadException()
        {
            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += OnThreadException;
        }
        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Show(e.Exception, "Unhandled Thread Exception");
        }

        /// <summary>
        /// იჭერს Unhandled UI Exception-ს
        /// </summary>
        public static void HandleUnhandledException()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
                Show(ex, "Unhandled UI Exception");
        }

        public static DialogResult Show(Exception ex, string caption = null, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            return Show(null, ex, caption, icon);
        }
        public static DialogResult Show(IWin32Window owner, Exception ex, string caption = null, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            NativeMethods.MessageBeep((int)icon);

            if (ex is MessageBoxException)
                return MessageBox.Show(owner, ex.Message, caption, MessageBoxButtons.OK, icon);
            if (ex is XtraMessageBoxException)
                return XtraMessageBoxShow(owner, ex, caption, icon);
            //if (ex is SqlException)
            //{
            //    SqlException sqlex = (SqlException)ex;
            //    if (sqlex.Number == 50000 && sqlex.Class == 16 && sqlex.State == 1)
            //    {
            //        switch (CustomExceptionFormType)
            //        {
            //            case CustomExceptionFormTypes.MessageBox:
            //                return MessageBox.Show(owner, ex.Message, caption, MessageBoxButtons.OK, icon);

            //            case CustomExceptionFormTypes.XtraMessageBox:
            //                return XtraMessageBoxShow(owner, ex, caption, icon);
            //        }
            //    }
            //}

            if (caption == null)
                caption = MessageBoxIconTostring(icon);

            return new ExceptionForm(ex)
            {
                Text = caption
            }.ShowDialog(owner);
        }

        private static DialogResult XtraMessageBoxShow(IWin32Window owner, Exception ex, string caption, MessageBoxIcon icon)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(owner, ex.Message, caption, MessageBoxButtons.OK, icon);
        }

        public static string GetExceptionString(Exception ex, string message = null)
        {
            var buildDate = DateTime.MinValue;
            try { buildDate = AssemblyHelper.LastWriteTime; }
            catch { }

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(message))
            {
                sb.AppendLine("----------------------------")
                    .AppendLine("[Customer Explanation]")
                    .AppendLine()
                    .AppendLine(message)
                    .AppendLine();
            }

            sb.AppendLine("----------------------------")
                .AppendLine("[General Info]")
                .AppendLine()
                .AppendLine("Application:\t" + Application.ProductName)
                .AppendLine("Framework:  \t" + RuntimeEnvironment.GetSystemVersion())
                .AppendLine("Version:    \t" + $"Version: {AssemblyHelper.AssemblyVersion.ToString(3)} from {buildDate:D}")
                .AppendLine("MachineName:\t" + Environment.MachineName)
                .AppendLine("OSVersion: \t" + Environment.OSVersion.VersionString)
                .AppendLine("UserName:   \t" + Environment.UserName)
                .AppendLine();

            sb.AppendLine("----------------------------")
                .AppendLine("[Exception Info]")
                .AppendLine("Message:    \t" + ex.Message)
                .AppendLine()
                .AppendLine("Source:     \t" + ex.Source)
                .AppendLine()
                .AppendLine("StackTrace:")
                .AppendLine(ex.StackTrace)
                .AppendLine();

            sb.AppendLine("----------------------------");
            sb.AppendLine("[Assemblies]");
            var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            Array.Sort(referencedAssemblies, AssemblyHelper.Compare);
            foreach (var name in referencedAssemblies)
            {
                sb.AppendLine($"{name.Name}, Version = {name.Version}");
            }
            return sb.ToString();
        }
    }
}
