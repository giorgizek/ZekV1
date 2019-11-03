using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Zek.Extensions;

namespace Zek.Configuration
{
    public class AppConfigHelper
    {
        /// <summary>
        /// Gets the bool value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(string key) => GetString(key).ToBoolean();

        /// <summary>
        /// Sets the bool value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, bool value) => Set(key, value ? 1 : 0);

        public static void Set(System.Configuration.Configuration config, string key, bool value) => Set(config, key, value ? 1 : 0);

        /// <summary>
        /// Gets the nullable bool value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool? GetNullableBool(string key) => GetNullableString(key).ToNullableBoolean();

        /// <summary>
        /// Sets the nullable bool value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, bool? value) => Set(key, value?.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, bool? value) => Set(config, key, value?.ToString(CultureInfo.InvariantCulture));


        /// <summary>
        ///  Gets the byte value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte GetByte(string key) => GetString(key).ToByte();

        /// <summary>
        /// Sets the byte value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, byte value) => Set(key, value.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, byte value) => Set(config, key, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Gets the nullable byte value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte? GetNullableByte(string key) => GetNullableString(key).ToNullableByte();

        /// <summary>
        /// Sets the nullable byte value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, byte? value) => Set(key, value?.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, byte? value) => Set(config, key, value?.ToString(CultureInfo.InvariantCulture));


        /// <summary>
        /// Gets the short value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static short GetInt16(string key) => GetString(key).ToInt16();

        /// <summary>
        /// Sets the short value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, short value) => Set(key, value.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, short value) => Set(config, key, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Gets the nullable int value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static short? GetNullableInt16(string key) => GetNullableString(key).ToNullableInt16();

        /// <summary>
        /// Sets the nullable short value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, short? value) => Set(key, value?.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, short? value) => Set(config, key, value?.ToString(CultureInfo.InvariantCulture));


        /// <summary>
        /// Gets the int value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt32(string key) => GetString(key).ToInt32();

        /// <summary>
        /// Sets the int value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, int value) => Set(key, value.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, int value) => Set(config, key, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Gets the nullable int value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int? GetNullableInt32(string key) => GetNullableString(key).ToNullableInt32();

        /// <summary>
        /// Sets the nullable int value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, int? value) => Set(key, value?.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, int? value) => Set(config, key, value?.ToString(CultureInfo.InvariantCulture));


        /// <summary>
        /// Gets the long value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetInt64(string key) => GetString(key).ToInt64();

        /// <summary>
        /// Sets the long value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, long value) => Set(key, value.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, long value) => Set(config, key, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Gets the nullable long value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long? GetNullableInt64(string key) => GetNullableString(key).ToNullableInt64();

        /// <summary>
        /// Sets the nullable long value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, long? value) => Set(key, value?.ToString(CultureInfo.InvariantCulture));

        public static void Set(System.Configuration.Configuration config, string key, long? value) => Set(config, key, value?.ToString(CultureInfo.InvariantCulture));


        /// <summary>
        /// Gets the decimal value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key) => GetString(key).ToDecimal();

        /// <summary>
        /// Sets the decimal value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, decimal value) => Set(key, value.ToString(CultureInfo.CurrentCulture));

        public static void Set(System.Configuration.Configuration config, string key, decimal value) => Set(config, key, value.ToString(CultureInfo.CurrentCulture));

        /// <summary>
        /// Gets the nullable decimal value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal? GetNullableDecimal(string key) => GetNullableString(key).ToNullableDecimal();

        /// <summary>
        /// Sets the nullable decimal value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, decimal? value) => Set(key, value?.ToString(CultureInfo.CurrentCulture));

        public static void Set(System.Configuration.Configuration config, string key, decimal? value) => Set(config, key, value?.ToString(CultureInfo.CurrentCulture));


        /// <summary>
        /// Gets the DateTime value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string key) => GetString(key).ToDateTime();

        /// <summary>
        /// Sets the DateTime value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, DateTime value) => Set(key, value.ToString(CultureInfo.CurrentCulture));

        public static void Set(System.Configuration.Configuration config, string key, DateTime value) => Set(config, key, value.ToString(CultureInfo.CurrentCulture));

        /// <summary>
        /// Gets the nullable DateTime value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime? GetNullableDateTime(string key) => GetNullableString(key).ToNullableDateTime();

        /// <summary>
        /// Sets the nullable DateTime value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, DateTime? value) => Set(key, value?.ToString(CultureInfo.CurrentCulture));

        public static void Set(System.Configuration.Configuration config, string key, DateTime? value) => Set(config, key, value?.ToString(CultureInfo.CurrentCulture));


        /// <summary>
        /// Gets the int[] value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static int[] GetInt32Array(string key, char separator = '|')
        {
            var array = GetString(key);
            return array.Split('|').Select(x => x.ToInt32()).ToArray();
        }



        /// <summary>
        /// Gets the string value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key) => ConfigurationManager.AppSettings[key] ?? string.Empty;

        /// <summary>
        /// Sets the string value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, string value) => ConfigurationManager.AppSettings[key] = value;

        public static void Set(System.Configuration.Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;
        }
        /// <summary>
        /// Gets the string value with the specified key in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetNullableString(string key) => ConfigurationManager.AppSettings[key];


        public static System.Configuration.Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel = ConfigurationUserLevel.None) => ConfigurationManager.OpenExeConfiguration(userLevel);


        //public static T GetSection<T>(ConfigurationUserLevel configLevel, bool createIfNotExists = false) where T : ConfigurationSection, new()
        //{
        //    var sectionname = typeof(T).Name;

        //    var config = ConfigurationManager.OpenExeConfiguration(configLevel);
        //    if (config.Sections[sectionname] == null)
        //    {
        //        if (!createIfNotExists)
        //            throw new ConfigurationErrorsException("No configuration provided for this type");

        //        var configsection = Activator.CreateInstance<T>();
        //        config.Sections.Add(sectionname, configsection);
        //        configsection.SectionInformation.ForceSave = true;
        //        config.Save(ConfigurationSaveMode.Minimal);
        //    }


        //    return (T)ConfigurationManager.GetSection(typeof(T).Name);
        //}



        ///// <summary>
        ///// Checks if app.config file exists and has read permission.
        ///// </summary>
        ///// <returns></returns>
        //public static bool OpenExeConfiguration()
        //{
        //    try
        //    {
        //        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        return true;
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Checks if app.config file exists.
        /// </summary>
        /// <returns>Returns true if app.config file exists.</returns>
        public static bool ExistsConfigFile() => ExistsConfigFile(Assembly.GetEntryAssembly());

        /// <summary>
        /// Checks if app.config file exists.
        /// </summary>
        /// <param name="assembly">Entry assembly</param>
        /// <returns>Returns true if app.config file exists.</returns>
        public static bool ExistsConfigFile(Assembly assembly) => File.Exists(assembly.Location + ".config");
    }
}
