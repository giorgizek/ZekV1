using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Zek.Threading
{
    public class MutexHelper
    {
        private static Mutex _mutex;
        /// <summary>
        /// ამოწმებს exe გაშვებულია თუ არა.
        /// </summary>
        /// <param name="checkAssemblyName">შეამოწმებს Assembly-ს სახელით.</param>
        /// <param name="checkLocation">შეამოწმებს exe-ს ადგილმდებარეობით.</param>
        /// <param name="checkUserName">შეამოწმებს exe-ს გამშვებ მომხმარებელს.</param>
        /// <param name="custom"></param>
        /// <returns>თუ დააბრუნა true, მაშინ exe გაშვებულია.</returns>
        public static bool IsAlreadyRunning(bool checkAssemblyName = true, bool checkLocation = false, bool checkUserName = false, string custom = null)
        {
            var createdNew = false;

            var name = string.Empty;

            if (checkAssemblyName)
                name += Assembly.GetEntryAssembly().GetName().FullName;
            if (checkLocation)
                name += "_" + Assembly.GetEntryAssembly().Location.Replace("\\", "/").Replace(":", "_");
            if (checkUserName)
                name += "_" + Environment.UserName;
            if (!string.IsNullOrWhiteSpace(custom))
                name += "_" + custom;

            if (name.Length == 0)
                throw new ArgumentException("Parameter required (checkAssemblyName or checkLocation or checkUserName or custom)");

            if (name.Length > 260)
                name = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(name))).Replace("-", string.Empty);
                //name = @"Global\" + MD5Hex(name);

            //Try to open existing mutex
            try
            {
                _mutex = new Mutex(true, @"Global\" + name, out createdNew);
                return !createdNew;
            }
            finally
            {
                if (_mutex != null && createdNew)
                    _mutex.ReleaseMutex();
            }
        }

        //private static string MD5Hex(string plainText)
        //{
        //    return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(plainText))).Replace("-", "").ToLowerInvariant();
        //}
    }
}
