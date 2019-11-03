using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using Zek.Core;
using Zek.Extensions;
using Zek.Properties;

namespace Zek.Windows.Forms
{
    public partial class ExceptionForm : Form//, IComparer<AssemblyName>
    {
        public ExceptionForm(Exception ex)
        {
            InitializeComponent();
            Icon = Icons.warning;

            _date = DateTime.Now;
            _exception = ex;
            //Version version = Assembly.GetAssembly(base.GetType()).GetName().Version;



            txtMessage.Text = ex.Message;
            //if (ex as DbEntityValidationException != null)
            //{
            //    var dbEx = ex as DbEntityValidationException;
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            txtMessage.Text += Environment.NewLine + string.Format("{0}: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //            //Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //        }
            //    }
            //}


            txtApplication.Text = Application.ProductName;
            txtFramework.Text = RuntimeEnvironment.GetSystemVersion();
            //txtVersion.Text = string.Format("Version: {0} from {1:D}", version.ToString(3), "13 July 2009");
            var buildDate = DateTime.MinValue;
            try { buildDate = AssemblyHelper.LastWriteTime; }
            catch { }
            txtVersion.Text = $"Version: {AssemblyHelper.AssemblyVersion.ToString(3)} from {buildDate:D}";

            txtMessage2.Text = ex.Message;
            txtSource.Text = ex.Source;
            txtStackTrace.Text = ex.StackTrace;



            dgvInnerExceptions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgvInnerExceptions.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            var innerExceptionTable = new DataTable();
            innerExceptionTable.Columns.Add("Level", typeof(int));
            innerExceptionTable.Columns.Add("Type");
            innerExceptionTable.Columns.Add("Message");
            innerExceptionTable.Columns.Add("Source");
            innerExceptionTable.Columns.Add("TargetSite");
            innerExceptionTable.Columns.Add("StackTrace");
            var inner = ex.InnerException;
            var level = 1;
            while (inner != null)
            {
                innerExceptionTable.Rows.Add(
                        level++,
                        inner.GetType().FullName,
                        inner.Message,
                        inner.Source,
                        inner.TargetSite,
                        inner.StackTrace
                );
                inner = inner.InnerException;
            }
            dgvInnerExceptions.DataSource = innerExceptionTable;





            listViewAssemblies.Clear();
            listViewAssemblies.Columns.Add("Name", 320, HorizontalAlignment.Left);
            listViewAssemblies.Columns.Add("Version", 150, HorizontalAlignment.Left);
            var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().OrderBy(x => x.Name);
            //Array.Sort(referencedAssemblies, this);
            foreach (var name in referencedAssemblies)
            {
                var item = new ListViewItem { Text = name.Name };
                item.SubItems.Add(name.Version.ToString());
                listViewAssemblies.Items.Add(item);
            }
        }

        private readonly DateTime _date;
        private readonly Exception _exception;

        //public int Compare(AssemblyName x, AssemblyName y)
        //{
        //    return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        //}


        //private string CreateExceptionString(Exception ex, string indent = "")
        //{
        //    var sb = new StringBuilder();
        //    CreateExceptionString(ex, sb, indent);
        //    return sb.ToString();
        //}
        //private void CreateExceptionString(Exception ex, StringBuilder sb, string indent)
        //{
        //    if (indent == null)
        //        indent = string.Empty;
        //    else if (indent.Length > 0)
        //    {
        //        sb.AppendLine(indent + "[Inner Exception]");
        //        sb.AppendLine(indent + "Type: " + ex.GetType().FullName);
        //        sb.AppendLine(indent + "Message: " + ex.Message);
        //        sb.AppendLine(indent + "Source: " + ex.Source);
        //        sb.AppendLine(indent + "TargetSite: " + ex.TargetSite);
        //        sb.AppendLine(indent + "StackTrace: " + ex.StackTrace);
        //    }

        //    if (indent.Length == 0)
        //    {
        //        sb.AppendLine(indent + "Type:       \t" + ex.GetType().FullName);
        //        sb.AppendLine(indent + "Message:    \t" + ex.Message);
        //        sb.AppendLine(indent + "Source:     \t" + ex.Source);
        //        sb.AppendLine(indent + "TargetSite: \t" + ex.TargetSite);
        //        sb.AppendLine(indent + "StackTrace: \t" + ex.StackTrace);
        //    }

        //    if (ex.InnerException != null)
        //    {
        //        sb.AppendLine();
        //        CreateExceptionString(ex.InnerException, sb, indent + "  ");
        //    }
        //}
        private string GetExceptionString()
        {
            return _exception.ToExceptionString(Text, txtInformation.Text, _date);
            /*var sb = new StringBuilder();
            sb.AppendLine("----------------------------");
            sb.AppendLine("[Customer Explanation]")
                .AppendLine()
                .AppendLine(txtInformation.Text)
                .AppendLine();

            sb.AppendLine("----------------------------");
            sb.AppendLine("[General Info]")
                .AppendLine()
                .AppendLine("Application:\t" + txtApplication.Text)
                .AppendLine("Framework:  \t" + txtFramework.Text)
                .AppendLine("Version:    \t" + txtVersion.Text)
                .AppendLine("MachineName:\t" + Environment.MachineName)
                .AppendLine("OSVersion:  \t" + Environment.OSVersion.VersionString)
                .AppendLine("UserName:   \t" + Environment.UserName)
                .AppendLine("Date:       \t" + _date.ToString(CultureInfo.InvariantCulture))
                .AppendLine();

            sb.AppendLine("----------------------------");
            sb.AppendLine("[Exception Info]")
                .AppendLine("Title:      \t" + Text)
                .AppendLine(CreateExceptionString(_exception));

            sb.AppendLine("----------------------------");
            sb.AppendLine("[Assemblies]");
            var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            Array.Sort(referencedAssemblies, this);
            foreach (var name in referencedAssemblies)
            {
                sb.AppendLine(string.Format("{0}, Version = {1}", name.Name, name.Version));
            }
            return sb.ToString();*/
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = @"Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.FileName = "Exception.txt";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dialog.FileName, GetExceptionString());
                    //using (var writer = new StreamWriter(dialog.FileName))
                    //{
                    //    writer.Write(GetExceptionString());
                    //    writer.Flush();
                    //    writer.Close();
                    //}
                }
            }
        }

        private void btnClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(GetExceptionString(), true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnTextboxKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox && e.Control && e.KeyCode == Keys.A && !e.Alt && !e.Shift)
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}