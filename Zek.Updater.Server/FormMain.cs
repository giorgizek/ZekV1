using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Zek.Data;
using Zek.IO;

namespace Zek.Updater.Server
{
    public partial class FormMain : Form
    {
        private class FileInformation
        {
            public FileInformation(string rootDirectory, string file)
            {
                var fi = new FileInfo(file);

                Checked = true;
                File = fi.Name;
                Size = Ext.ToComputerSize(fi.Length)/* + " (" + fi.Length.ToString() + " Bytes)"*/;
                Hash = Ext.MD5HexFile(file);
                Path = file;
            }

            public bool Checked { get; set; }
            public string File { get; private set; }
            public string Size { get; set; }
            public string Path { get; private set; }
            public string Hash { get; private set; }
        }



        private bool _isChanged;
        private bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                if (value != _isChanged)
                {
                    _isChanged = value;
                    if (_isChanged) Text += @"*";
                    else Text = Text.TrimEnd('*');
                }
            }
        }

        private bool IsValid
        {
            get
            {
                var valid = true;
                if (txtDirectoryUpdate.Text.Trim().Length == 0)
                {
                    errorProvider.SetError(txtDirectoryUpdate, "Choose update directory");
                    valid = false;
                }
                if (txtDirectoryDestination.Text.Trim().Length == 0)
                {
                    errorProvider.SetError(txtDirectoryDestination, "Choose destination directory");
                    valid = false;
                }
                if (cmbAppExeName.Text.Trim().Length == 0)
                {
                    errorProvider.SetError(cmbAppExeName, "Choose app exe name");
                    valid = false;
                }
                //if (cmbCompress.Text.Trim().Length == 0)
                //{
                //    errorProvider.SetError(cmbCompress, "Choose compress");
                //    valid = false;
                //}
                return valid;
            }
        }

        private XmlUpdate TmpXmlUpdate
        {
            get
            {
                return new XmlUpdate
                {
                    AppName = txtApplicationName.Text.Trim(),
                    //AppFolderName = txtAppFolderName.Text.Trim(),
                    Version = txtVersion.Text.Trim(),
                    AppExeName = cmbAppExeName.Text.Trim(),
                    CompressFolderName = txtCompressFolderName.Text.Trim(),
                    Compress = cmbCompress.Text.Trim(),
                    DeleteBin = chkDeleteBin.Checked,
                    DeployDate = DateTime.Now
                };
            }
        }

        private List<FileInformation> _fileInfo = new List<FileInformation>();
        private void FillFiles()
        {
            dgvFile.DataSource = null;
            _fileInfo.Clear();
            FillFiles(txtDirectoryUpdate.Text.Trim(), txtDirectoryUpdate.Text.Trim(), 0, (int)numDirectoryLevel.Value);
            dgvFile.DataSource = _fileInfo;
            dgvFile.Refresh();
            cmbAppExeName.Items.Clear();
            cmbAppExeName.Items.AddRange(_fileInfo.Where(x => x.File.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase) && x.Path.Remove(0, txtDirectoryUpdate.Text.Trim().Length + 1).IndexOf('\\') == -1).Select(x => x.File).ToArray());
        }
        private void FillFiles(string rootDirectory, string mainDirectory, int level, int maxLevel)
        {
            foreach (var file in Directory.GetFiles(mainDirectory))
            {
                _fileInfo.Add(new FileInformation(rootDirectory, file));
            }

            if (level >= maxLevel) return;

            foreach (var dir in Directory.GetDirectories(mainDirectory))
            {
                FillFiles(rootDirectory, dir, level + 1, maxLevel);
            }
        }

        private readonly bool _deploy;
        private readonly bool _close;
        public FormMain(bool deploy, bool close)
        {
            InitializeComponent();

            Text += @" v" + Application.ProductVersion;
            numDirectoryLevel.Maximum = int.MaxValue;
            cmbCompress.SelectedIndex = 1;
            Ext.SetCustomDrawRowIndicator(dgvFile);
            _deploy = deploy;
            _close = close;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            txtDirectoryUpdate.Text = Ext.GetConfigString("DirectoryUpdate");
            numDirectoryLevel.Value = Ext.GetConfigInt32("DirectoryLevel");
            txtDirectoryDestination.Text = Ext.GetConfigString("DirectoryDestination");
            if (txtDirectoryUpdate.Text.Trim().Length > 0 && Directory.Exists(txtDirectoryUpdate.Text)) FillFiles();
            cmbAppExeName.Text = Ext.GetConfigString("AppExeName");
            if (!string.IsNullOrWhiteSpace(Ext.GetConfigString("AppName"))) txtApplicationName.Text = Ext.GetConfigString("AppName");//This must be under cmbAppExeName.Text = ConfigurationManager.AppSettings["AppExeName"];
            txtCompressFolderName.Text = Ext.GetConfigString("CompressFolderName");
            if (!string.IsNullOrWhiteSpace(Ext.GetConfigString("Compress"))) cmbCompress.Text = Ext.GetConfigString("Compress");
            chkDeleteBin.Checked = Ext.GetConfigBool("DeleteBin");

            IsChanged = false;
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (_deploy)
                btnDeploy_Click(sender, e);
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            var dlg = folderBrowserDialog.ShowDialog();
            if (dlg != DialogResult.OK) return;

            txtDirectoryUpdate.Text = folderBrowserDialog.SelectedPath;
            FillFiles();
        }
        private void btnDirectoryDestination_Click(object sender, EventArgs e)
        {
            var dlg = folderBrowserDialog.ShowDialog();
            if (dlg != DialogResult.OK) return;

            txtDirectoryDestination.Text = folderBrowserDialog.SelectedPath;
        }

        private void txtDirectoryUpdate_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtDirectoryUpdate, string.Empty);
            IsChanged = true;
        }
        private void numDirectoryLevel_ValueChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(numDirectoryLevel, string.Empty);
            IsChanged = true;
        }
        private void txtDirectoryDestination_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtDirectoryDestination, string.Empty);
            IsChanged = true;
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtApplicationName, string.Empty);
            IsChanged = true;
        }
        private void cmbAppExeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(cmbAppExeName, string.Empty);
            IsChanged = true;

            try
            {
                var file = _fileInfo.FirstOrDefault(x => x.Path == Path.Combine(txtDirectoryUpdate.Text.Trim(), cmbAppExeName.Text.Trim()));
                if (file != null)
                {
                    var asm = System.Reflection.Assembly.ReflectionOnlyLoadFrom(file.Path);
                    txtApplicationName.Text = asm.GetName().Name;
                    txtVersion.Text = asm.GetName().Version.ToString();
                }
            }
            catch { }
        }
        private void txtCompressFolderName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtCompressFolderName, string.Empty);
            IsChanged = true;
        }
        private void cmbCompress_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(cmbCompress, string.Empty);
            IsChanged = true;
        }
        private void chkDeleteBin_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(chkDeleteBin, string.Empty);
            IsChanged = true;
        }
        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtVersion, string.Empty);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsChanged) return;

            var dlg = MessageBox.Show("Do you want to save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dlg == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (dlg == DialogResult.Yes)
            {
                if (!IsValid)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    var xml = TmpXmlUpdate;
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    Ext.SetConfig(config, "DirectoryUpdate", txtDirectoryUpdate.Text.Trim());
                    Ext.SetConfig(config, "DirectoryLevel", numDirectoryLevel.Value.ToString("f00"));
                    Ext.SetConfig(config, "DirectoryDestination", txtDirectoryDestination.Text.Trim());
                    Ext.SetConfig(config, "AppExeName", xml.AppExeName);
                    Ext.SetConfig(config, "AppName", xml.AppName);
                    Ext.SetConfig(config, "CompressFolderName", xml.CompressFolderName);
                    Ext.SetConfig(config, "Compress", xml.Compress);
                    Ext.SetConfig(config, "DeleteBin", xml.DeleteBin);

                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
                catch
                {
                    e.Cancel = true;
                    throw;
                }
            }

        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bsDeploy.IsBusy)
                bsDeploy.CancelAsync();
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            if (!IsValid || !_fileInfo.Any(x => x.Checked)) return;

            if (bsDeploy.IsBusy)
            {
                MessageBox.Show("Please wait while deploy completed");
                return;
            }

            Application.DoEvents();
            Cursor = Cursors.WaitCursor;

            tslblProgress.Text = "Started...";
            progressFiles.Value = 0;

            bsDeploy.RunWorkerAsync(TmpXmlUpdate);
        }
        private void bsDeploy_DoWork(object sender, DoWorkEventArgs e)
        {
            var updateDir = txtDirectoryUpdate.Text.Trim();
            var destinationDir = txtDirectoryDestination.Text.Trim();
            var xmlFile = Path.Combine(destinationDir, "update.txt");

            var xml = (XmlUpdate)e.Argument;
            //add files into xml
            foreach (var item in _fileInfo.Where(x => x.Checked))
            {
                xml.Files.Add(new XmlUpdate.XmlFile(item.Path, item.Path.Substring(updateDir.Length + 1), item.Hash));
            }
            var compressDir = Path.Combine(destinationDir, xml.CompressFolderName);
            if (!Directory.Exists(compressDir)) Directory.CreateDirectory(compressDir);

            var deleteFiles = Directory.GetFiles(destinationDir).ToList();
            if (deleteFiles.Contains(xmlFile))
                deleteFiles.Remove(xmlFile);
            var deleteCompressedFiles = Directory.GetFiles(compressDir);

            var comparer = new PropertyComparer<XmlUpdate.XmlFile>("Hash");
            var distinct = xml.Files.Distinct(comparer).ToList();

            decimal max = deleteFiles.Count + deleteCompressedFiles.Length + distinct.Count;


            var oldCompressEqualsCurrent = false;
            if (File.Exists(xmlFile))
            {
                XmlUpdate oldXml = null;//Creating old serialized XmlUpdate from xml file
                try { oldXml = Ext.DeserializeXml<XmlUpdate>(File.ReadAllBytes(xmlFile)); }
                catch { }
                oldCompressEqualsCurrent = oldXml != null && oldXml.Compress == xml.Compress;
            }

            for (int i = 0; i < deleteFiles.Count; i++)
            {
                bsDeploy.ReportProgress((int)((1m + i) * progressFiles.Maximum / max), "Deleting: " + Path.GetFileName(deleteFiles[i]));
                File.Delete(deleteFiles[i]);
            }

            for (int i = 0; i < deleteCompressedFiles.Length; i++)
            {
                var file = Path.GetFileName(deleteCompressedFiles[i]);
                //if old compressed file equals current then just skip.
                if (oldCompressEqualsCurrent && distinct.Any(x => x.Hash + xml.Extension == file))
                    continue;

                bsDeploy.ReportProgress((int)((1m + deleteFiles.Count + i) * progressFiles.Maximum / max), "Deleting: " + file);
                File.Delete(deleteCompressedFiles[i]);
            }


            for (int i = 0; i < distinct.Count; i++)
            {
                var item = distinct[i];
                var file = Path.Combine(compressDir, item.Hash + xml.Extension);
                if (File.Exists(file)) continue;//if old compressed file already exists then skip


                bsDeploy.ReportProgress((int)((1 + deleteFiles.Count + deleteCompressedFiles.Length + i) * progressFiles.Maximum / max), "Compressing: " + Path.GetFileName(item.Path) + "  To: " + item.Hash + xml.Extension);
                switch (xml.Compress.ToLowerInvariant())
                {
                    case "":
                        File.Copy(item.Path, file);
                        break;

                    case "gzip":
                        GZipHelper.CompressFile(item.Path, file);
                        break;

                    //case "zip":
                    //    SharpZLibHelper.CompressFile(item.Path, Path.Combine(compressFolder, item.Hash + xml.Extension));
                    //    progressFiles.PerformStep();
                    //    break;

                    case "7zip":
                        SevenZipHelper.CompressFileLZMA(item.Path, file);
                        break;

                    default:
                        throw new ArgumentException("Invalid compress type (use: gzip, 7zip).");
                }
            }
            File.WriteAllText(xmlFile, Ext.SerializeXml(xml));
        }
        private void bsDeploy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tslblProgress.Text = e.UserState.ToString();
            progressFiles.Value = e.ProgressPercentage;
        }
        private void bsDeploy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;

            if (e.Cancelled)
            {
                tslblProgress.Text = "Cancelled!";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tslblProgress.Text = "Completed";
                progressFiles.Value = progressFiles.Minimum;

                if (_deploy && _close)
                    Close();
            }
        }

        
    }
}
