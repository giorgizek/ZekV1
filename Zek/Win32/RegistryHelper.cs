using System;
using Microsoft.Win32;

namespace Zek.Win32
{
    public class RegistryHelper
    {
        public static object GetValue(RegistryKey rootKey, string path, string name)
        {
            using (var key = rootKey.OpenSubKey(path))
            {
                return key != null ? key.GetValue(name) : null;
            }
        }
        public static void SetValue(RegistryKey rootKey, string path, string name, object value)
        {
            SetValue(rootKey, path, name, value, RegistryValueKind.Unknown);
        }
        public static void SetValue(RegistryKey rootKey, string path, string name, object value, RegistryValueKind registryValueKind)
        {
            using (var key = rootKey.OpenSubKey(path, true))
            {
                if (key != null)
                    key.SetValue(name, value, registryValueKind);
                else throw new Exception("Can't find key (" + rootKey + ")");
            }
        }

        ///// <summary>
        /////method for deleting a specified value in HKEY_CLASSES_ROOT
        ///// </summary>
        ///// <param name="path">registry key's path that we want deleted
        ///// <param name="name">name of the value to be deletedd</param>
        //public static void DeleteRegistryKeyValue(RegistryKey rootKey, string path, string name)
        //{
        //    //get into HKEY_CLASSES_ROOT
        //    RegistryKey key = rootKey;

        //    //split the provided path into parts
        //    string[] pathParts = path.Split('\\');

        //    //if it's numm or it's length iz 0 (zero) then get out
        //    if (pathParts == null || pathParts.Length == 0)
        //        return;

        //    //loop through each part of the path
        //    for (int i = 0; i < pathParts.Length; i++)
        //    {
        //        //open the subkey for this index
        //        key = key.OpenSubKey(pathParts[i], true);

        //        //if no subkey exists then get out
        //        if (key == null)
        //            return;
        //        //otherwise delete this value
        //        if (i == pathParts.Length - 1)
        //            key.DeleteValue(name, false);
        //    }
        //}

        public static void DeleteDefaultValue(RegistryKey rootKey, string subKey)
        {
            SetValue(rootKey, subKey, string.Empty, string.Empty);
        }
        public static void DeleteValue(RegistryKey rootKey, string subKey, string name)
        {
            using (var key = rootKey.OpenSubKey(subKey, true))
            {
                if (key != null) key.DeleteValue(name);
            }
        }
        public static void DeleteSubKey(RegistryKey rootKey, string subKey, string name)
        {
            using (var key = rootKey.OpenSubKey(subKey, true))
            {
                if (key != null) key.DeleteSubKey(name);
            }
        }
    }
}
