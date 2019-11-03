using System;
using System.Collections.Generic;
using System.Timers;

namespace Zek.Core
{
    public class BaseGlobalVariable
    {
        public static bool IsLogged => UserID != 0;
        public static int UserID { get; set; }

        private static string _userName = string.Empty;
        public static string UserName
        {
            get { return _userName; }
            set { _userName = value ?? string.Empty; }
        }

        private static string _password = string.Empty;
        public static string Password
        {
            get { return _password; }
            set { _password = value ?? string.Empty; }
        }

        public static int PersonID { get; set; }

        private static string _fullName = string.Empty;
        public static string FullName
        {
            get { return _fullName; }
            set { _fullName = value ?? string.Empty; }
        }


        private static string _applicationID = string.Empty;
        public static string ApplicationID
        {
            get { return _applicationID; }
            set { _applicationID = value ?? string.Empty; }
        }


        private static Timer _timer;
        private static DateTime _serverDateTime;
        public static DateTime ServerDateTime
        {
            get { return _serverDateTime.Ticks != 0 ? _serverDateTime : DateTime.Now; }
            set
            {
                _serverDateTime = value;
                if (_timer == null)
                {
                    _timer = new Timer(1000);
                    _timer.Elapsed += delegate {
                        _serverDateTime = _serverDateTime.AddSeconds(1d);
                    };
                    _timer.Start();
                }
            }
        }


        #region Permission
        private static Dictionary<string, int> _permissions = new Dictionary<string, int>();
        public static Dictionary<string, int> Permissions
        {
            get { return _permissions; }
            set
            {
                if (value == null)
                    value = new Dictionary<string, int>();
                _permissions = value;
            }
        }

        public static bool IsPermitted(string name, int permissionToCheck)
        {
            return BitwiseHelper.HasFlag(GetPermission(name), permissionToCheck);
        }
        public static int GetPermission(string name)
        {
            return Permissions.ContainsKey(name) ? Permissions[name] : 0;
        }

        //public static bool AdminLogIn(string user, string password)
        //{
        //    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password)) return false;

        //    string adminPassword = DateTime.Now.AddSeconds(-(DateTime.Now.Second % 10)).ToString("yyyyMMddHHmmss") + ((int)DateTime.Now.DayOfWeek).ToString();
        //    password = System.Text.RegularExpressions.Regex.Replace(password, @"\D", string.Empty);

        //    if (user == "sa" && password == adminPassword)
        //    {
        //        IsLogged = true;
        //        _IsAdmin = true;
        //        UserName = "sa";
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        #endregion

        public static void BaseReset()
        {
            UserID = 0;
            UserName = string.Empty;
            Password = string.Empty;
            Permissions.Clear();
        }
    }
}
