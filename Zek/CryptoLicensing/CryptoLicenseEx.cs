using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using LogicNP.CryptoLicensing;
using Zek.Core;
using Zek.Management;
using System.Text;

namespace Zek.CryptoLicensing
{
    public class CryptoLicenseEx : CryptoLicense
    {
        public CryptoLicenseEx()
        { Init(); }
        public CryptoLicenseEx(string validationKey) : base(validationKey) { Init(); }
        public CryptoLicenseEx(LicenseStorageMode storageMode, string validationKey) : base(storageMode, validationKey) { Init(); }
        public CryptoLicenseEx(string licenseCode, string validationKey) : base(licenseCode, validationKey) { Init(); }
        public CryptoLicenseEx(LicenseStorageMode storageMode, string storagePath, string validationKey) : base(storageMode, storagePath, validationKey) { Init(); }

        private void Init()
        {

        }


        /// <summary>
        /// License Web Service URL
        /// </summary>
        public string LicenseURL { get; set; }


        ///// <summary>
        ///// Gets license from Web Service
        ///// </summary>
        ///// <returns>if license is valid returns true.</returns>
        //public bool LoadLicenseFromWebPage()
        //{
        //    var args = new Dictionary<string, string>();
        //    args.Add("appName", AssemblyHelper.AssemblyTitle);
        //    args.Add("appVersion", AssemblyHelper.AssemblyVersion.ToString(3));
        //    args.Add("machineCode", GetLocalMachineCodeAsString());
        //    args.Add("machineName", Environment.MachineName);
        //    args.Add("userName", Environment.UserName);
        //    //args.Add("hash", Zek.Security.CryptoHelper.MD5Hex(DictionaryToString(args), "CryptoLicense"));

        //    LicenseCode = Zek.Net.WebRequestHelper.DownloadString(LicenseURL);
        //    //LicenseCode = Zek.Net.WebRequestHelper.InvokeWebService<string>(LicenseURL, "GetLicense", args);

        //    return Status == LicenseStatus.Valid;
        //}
        //private string DictionaryToString(Dictionary<string, string> args)
        //{
        //    var result = new StringBuilder();
        //    foreach (var value in args.Values)
        //    {
        //        result.Append(value);
        //    }

        //    return result.ToString();
        //}

        /// <summary>
        /// Loads license code from web service (invokes GetLicenseCode method from web service).
        /// </summary>
        /// <returns>returns true if loaded license code is valid (Status == LicenseStatus.Valid)</returns>
        public bool LoadQueryWebServiceLicenseCode()
        {
            LicenseCode = QueryWebService("GetLicenseCode", new string[] { }, new string[] { });
            return Status == LicenseStatus.Valid;
        }
        /// <summary>
        /// Invokes webservice GetLicenseCode method
        /// </summary>
        /// <returns>returns result from webservice</returns>
        public string QueryWebServiceLicenseCode()
        {
            return QueryWebService("GetLicenseCode", new string[] { }, new string[] { });
        }



        /// <summary>
        /// Returns Min value of RemainingUsageDays, RemainingUniqueUsageDays, DateExpires.Subtract(DateTime.Now).Days
        /// </summary>
        public int RemainingDays
        {
            get
            {
                int days = RemainingUsageDays;
                if (days > RemainingUniqueUsageDays) days = RemainingUniqueUsageDays;
                if (days > DateExpires.Subtract(DateTime.Now).Days) days = DateExpires.Subtract(DateTime.Now).Days;

                return days >= 0 ? days : 0;
            }
        }

        #region Override
        public override byte[] GetLocalMachineCode()
        {
            try
            {
                return MD5HexToBytesArray(CpuInfo.GetHardwareID());
            }
            catch { }

            return null;
        }
        public override bool IsMachineCodeValid()
        {
            try
            {
                var embeddedMachineCode = MachineCode;
                var localMachineCode = GetLocalMachineCode();

                return AreBuffersEqual(embeddedMachineCode, localMachineCode);
            }
            catch { }

            return false;

            // Fall back to base implementation if failed 
            //return base.IsMachineCodeValid();
        }
        /*public override void InitWebRequest(System.Net.HttpWebRequest request)
        {
            // Use this proxy for all communication with the license service 
            WebProxy proxyObject = new WebProxy("http://proxyserver:80/", true);
            request.Proxy = proxyObject;
        }*/
        #endregion

        #region Cryptography
        private static bool AreBuffersEqual(byte[] buff1, byte[] buff2)
        {
            try
            {
                if (buff1.Length != buff2.Length)
                    return false;
                for (var i = 0; i < buff1.Length; i++)
                {
                    if (buff1[i] != buff2[i])
                        return false;
                }
                return true;
            }
            catch { }

            return false;
        }
        private static byte[] MD5HexToBytesArray(string plainText)
        {
            using (var md5Hash = new MD5CryptoServiceProvider())
            {
                return md5Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            }
        }
        #endregion

        #region Instance
        private static readonly object Lock = new object();
        private static CryptoLicenseEx _instance;
        public static CryptoLicenseEx Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CryptoLicenseEx
                            {
                                StorageMode = LicenseStorageMode.ToRegistry
                            };
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// ამოწმებს არის თუ არა სწორი ლიცენზია ან თუ დროებითია.
        /// </summary>
        public static bool IsValid
        {
            get { return Instance.Status == LicenseStatus.Valid; }
        }

        public static bool ShowDialog()
        {
            return ShowDialog(AssemblyHelper.AssemblyTitle, string.Empty);
        }
        public static bool ShowDialog(string productName)
        {
            return ShowDialog(productName, string.Empty);
        }
        public static bool ShowDialog(string productName, string purchaseURL)
        {
            // Create 'eval info dialog' and set attributes 
            var dialog = new EvaluationInfoDialog(Instance) { ProductName = productName, PurchaseURL = purchaseURL, ShowOfflineActivationButton = true, TopMost = false, StartPosition = FormStartPosition.CenterScreen };

            return Instance.ShowEvaluationInfoDialog(dialog);
        }

        //private static void ResetCryptoLicenseTrial()
        //{
        //    RegistryHelper.DeleteDefaultValue(Microsoft.Win32.Registry.ClassesRoot, @"CLSID\{821b93c9-df90-3f74-b445-6f57ae232c973}\InProcServer32");
        //}
        #endregion
    }
}
