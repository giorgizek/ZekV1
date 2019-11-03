using System.Globalization;
using System.Xml;
using Zek.Data;

namespace Zek.Configuration
{
    public class XmlConfigHelper
    {
        #region Xml Configuration
        public static bool CheckXml(string xml)
        {
            try
            {
                new XmlDocument { InnerXml = xml };
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool GetXmlBoolValue(string xml, string key)
        {
            return GetXmlIntValue(xml, key) == 1;
        }
        public static int GetXmlIntValue(string xml, string key)
        {
            var value = GetXmlStringValue(xml, key);
            if (value.Length == 0) return 0;

            int result;
            int.TryParse(value, out result);
            return result;
        }
        public static string GetXmlStringValue(string xml, string key)
        {
            return GetElementsByTagName(xml, key);
        }

        private static string GetElementsByTagName(string xml, string name)
        {
            try
            {
                return GetElementsByTagName(new XmlDocument { InnerXml = xml }, name);
            }
            catch
            {
                return string.Empty;
            }
        }
        private static string GetElementsByTagName(XmlDocument doc, string name)
        {
            //doc.GetElementById(name);
            var node = doc.GetElementsByTagName(name)[0];
            return node != null ? node.InnerText : string.Empty;
        }

        public static string CreateXmlValue(string xml, string key, bool value)
        {
            return CreateXmlValue(xml, key, value, false);
        }
        public static string CreateXmlValue(string xml, string key, bool value, bool formatXml)
        {
            return CreateXmlValue(xml, key, value ? 1 : 0, formatXml);
        }
        public static string CreateXmlValue(string xml, string key, int value)
        {
            return CreateXmlValue(xml, key, value, false);
        }
        public static string CreateXmlValue(string xml, string key, int value, bool formatXml)
        {
            return CreateXmlValue(xml, key, value.ToString(CultureInfo.InvariantCulture), formatXml);
        }
        public static string CreateXmlValue(string xml, string key, string value)
        {
            return CreateXmlValue(xml, key, value, false);
        }
        public static string CreateXmlValue(string xml, string key, string value, bool formatXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml ?? "<Config />");
            SetXmlValue(doc, key, value);

            return formatXml ? XmlHelper.FormatXml(doc.InnerXml) : doc.InnerXml;
        }

        public static void SetXmlValue(XmlDocument doc, string key, bool value)
        {
            SetXmlValue(doc, key, value ? 1 : 0);
        }
        public static void SetXmlValue(XmlDocument doc, string key, int value)
        {
            SetXmlValue(doc, key, value.ToString(CultureInfo.InvariantCulture));
        }
        public static void SetXmlValue(XmlDocument doc, string key, string value)
        {
            var element = doc.CreateElement(key);
            element.InnerText = value;
            if (doc.DocumentElement != null)
                doc.DocumentElement.AppendChild(element);
        }
        #endregion
    }
}
