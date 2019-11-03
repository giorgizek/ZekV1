using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Zek.Data;
using Zek.IO;

namespace Zek.Updater.Client
{
    public partial class UpdateForm : Form
    {
        private readonly XmlUpdate _xmlUpdate;
        private readonly bool _selfUpdate;
        public UpdateForm(XmlUpdate xmlUpdate, bool selfUpdate)
        {
            if (DesignMode) return;

            _xmlUpdate = xmlUpdate;
            _selfUpdate = selfUpdate;
            InitializeComponent();
            Text = _xmlUpdate.AppName + @" v" + _xmlUpdate.Version;
            if (_xmlUpdate.DeployDate.HasValue)
                Text += @" (" + _xmlUpdate.DeployDate.Value.ToString("G") + @")";
        }




        private List<FileDownload> _fileDownloadList;
        private string _tmpDir = string.Empty;
        private List<string> _doNotDeleteFileList;
        private void UpdateForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            _tmpDir = Path.Combine(AppConfig.ExecutableDir, "Tmp");
            if (!Directory.Exists(_tmpDir))
                Directory.CreateDirectory(_tmpDir);


            _fileDownloadList = new List<FileDownload>();
            if (_xmlUpdate.DeleteBin)
            {
                _doNotDeleteFileList = new List<string>();
                var executablePath = Application.ExecutablePath.ToLowerInvariant();
                _doNotDeleteFileList.Add(executablePath);
                _doNotDeleteFileList.Add(executablePath.Remove(executablePath.Length - ".exe".Length) + ".config");
                _doNotDeleteFileList.Add(executablePath + ".config");
            }

            //todo aq unda gavaketo + is magivrad Combine rom gaketdes
            var urlDir = _selfUpdate
                        ? AppConfig.UpdaterUrlDir + AppConfig.UpdaterDirSeperator + (!string.IsNullOrWhiteSpace(_xmlUpdate.CompressFolderName) ? _xmlUpdate.CompressFolderName + AppConfig.UpdaterDirSeperator : string.Empty)
                        : AppConfig.UpdateUrlDir + AppConfig.UpdateDirSeperator + (!string.IsNullOrWhiteSpace(_xmlUpdate.CompressFolderName) ? _xmlUpdate.CompressFolderName + AppConfig.UpdateDirSeperator : string.Empty);
            foreach (var file in _xmlUpdate.Files)
            {
                var tmplocalFile = _selfUpdate
                        ? Path.Combine(AppConfig.ExecutableDir, file.File)
                        : Path.Combine(AppConfig.AppExeFolder, file.File);
                if (_xmlUpdate.DeleteBin) _doNotDeleteFileList.Add(tmplocalFile.ToLowerInvariant());
                if (!File.Exists(tmplocalFile) || file.Hash != Ext.MD5HexFile(tmplocalFile))
                {
                    _fileDownloadList.Add(new FileDownload
                    {
                        Name = tmplocalFile.Substring(tmplocalFile.LastIndexOf(_selfUpdate ? AppConfig.UpdaterDirSeperator : AppConfig.UpdateDirSeperator) + 1),
                        Local = tmplocalFile,
                        Server = urlDir + file.Hash + _xmlUpdate.Extension,
                        Tmp = Path.Combine(_tmpDir, file.Hash + _xmlUpdate.Extension)
                    });
                }
            }

            if (_fileDownloadList.Count == 0) return;

            var comparer = new PropertyComparer<FileDownload>("Server");
            var tmpList = _fileDownloadList.Distinct(comparer).ToList();

            progressDownload.Maximum = tmpList.Count;
            progressDownload.Value = 0;
            progressDownload.UseWaitCursor = true;
            bwDownload.RunWorkerAsync(tmpList);
        }
        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bwDownload.IsBusy)
            {
                bwDownload.CancelAsync();
            }

            if (bwExtract.IsBusy)
            {
                bwExtract.CancelAsync();
            }
        }


        private void bwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            var url = string.Empty;
            var archive = string.Empty;
            try
            {
                if (bwDownload.CancellationPending)
                {
                    e.Cancel = true;
                    bwDownload.ReportProgress(0);
                    return;
                }

                var lst = (List<FileDownload>)e.Argument;
                foreach (var item in lst)
                {
                    if (bwDownload.CancellationPending)
                    {
                        e.Cancel = true;
                        bwDownload.ReportProgress(0);
                        return;
                    }
                    url = item.Server;
                    archive = item.Tmp;
                    bwDownload.ReportProgress(0, item.Name);
                    Ext.CopyFile(item.Server, item.Tmp, AppConfig.Proxy, AppConfig.ProxyUrl, AppConfig.ProxyUserName, AppConfig.ProxyPassword);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n\nUrl: " + url + "\n\nTmp Archive: " + archive, ex);
            }
        }
        private void bwDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblDownload.Text = "Downloading: " + e.UserState;
            progressDownload.PerformStep();
        }
        private void bwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblDownload.Text = "Cancelled!";
            }
            else if (e.Error != null)
            {
                //lblDownload.Text = "Error while downloading files.";
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblDownload.Text = "Completed";
                progressDownload.Value = progressDownload.Maximum;
                progressDownload.UseWaitCursor = false;

                progressExtract.Maximum = _fileDownloadList.Count;
                progressExtract.Value = 0;
                progressExtract.UseWaitCursor = true;
                bwExtract.RunWorkerAsync(_fileDownloadList);
            }
        }



        private void DeleteBin(string mainDirectory)
        {
            foreach (var dir in Directory.GetDirectories(mainDirectory))
            {
                DeleteBin(dir);
                if (Directory.GetFiles(dir).Length == 0 && Directory.GetDirectories(dir).Length == 0)
                {
                    try
                    {
                        Directory.Delete(dir);
                    }
                    catch { }
                }
            }

            foreach (var file in Directory.GetFiles(mainDirectory))
            {
                if (_doNotDeleteFileList == null || _doNotDeleteFileList.Count == 0 || !_doNotDeleteFileList.Contains(file.ToLowerInvariant()))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
            }
        }

        public string UpdaterAvailableFile;
        private void bwExtract_DoWork(object sender, DoWorkEventArgs e)
        {
            var archive = string.Empty;
            var file = string.Empty;
            try
            {
                if (bwExtract.CancellationPending)
                {
                    e.Cancel = true;
                    bwExtract.ReportProgress(0);
                    return;
                }

                var lst = (List<FileDownload>)e.Argument;
                foreach (var item in lst)
                {
                    if (bwExtract.CancellationPending)
                    {
                        e.Cancel = true;
                        bwExtract.ReportProgress(0);
                        return;
                    }

                    archive = item.Tmp;
                    file = item.Local;
                    bwExtract.ReportProgress(0, item.Name);

                    var dir = Path.GetDirectoryName(file);
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                    //todo roca sheicvleba saxeli da daerqmeba AppStart.exe mashin ra unda qnas???
                    if (_selfUpdate && file.EndsWith(_xmlUpdate.AppExeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        file = Ext.GetNewVersionExeFileName();
                    }

                    if (File.Exists(file))
                        File.Delete(file);
                    switch (_xmlUpdate.Compress.ToLowerInvariant())
                    {
                        case "":
                            File.Copy(archive, file);
                            break;

                        case "gzip":
                            GZipHelper.DecompressFile(archive, file);
                            break;

                        //case "zip":
                        //    SharpZLibHelper.DecompressFile(item.Tmp, item.Local);
                        //    break;

                        case "7zip":
                            SevenZipHelper.DecompressFileLZMA(archive, file);
                            //File.WriteAllBytes(item.Local, SevenZipHelper.Decompress(File.ReadAllBytes(item.Tmp)));
                            break;

                        default:
                            throw new ArgumentException("Invalid compress type (use: gzip, 7zip).");
                    }


                    //Don't delete file becouse we downloaded same hashed file one time.
                    //We'll delete all files after extract completes.
                    //try
                    //{
                    //    File.Delete(archive);
                    //}
                    //catch { }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n\nTmp Archive: " + archive + "\n\nFile: " + file, ex);
            }
        }
        private void bwExtract_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblExtract.Text = "Extracting: " + e.UserState;
            progressExtract.PerformStep();
        }
        private void bwExtract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblExtract.Text = "Cancelled!";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblExtract.Text = "Completed";
                progressExtract.Value = progressDownload.Maximum;
                progressExtract.UseWaitCursor = false;
            }

            foreach (var item in _fileDownloadList)
            {
                try
                {
                    if (File.Exists(item.Tmp))
                        File.Delete(item.Tmp);
                }
                catch { }
            }

            try
            {
                if (Directory.GetFiles(_tmpDir).Length == 0)
                    Directory.Delete(_tmpDir);
            }
            catch { }


            if (e.Cancelled || e.Error != null)
            {
                DialogResult = DialogResult.None;
                Close();
                return;
            }

            if (_xmlUpdate.DeleteBin && !_selfUpdate)
            {
                DeleteBin(AppConfig.AppExeFolder);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
