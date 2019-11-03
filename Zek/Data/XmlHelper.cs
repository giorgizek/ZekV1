using System.Xml;
using System.IO;
using System.Xml.XPath;

namespace Zek.Data
{
    public class XmlHelper
    {
        public static string FormatXml(string unformattedXml)
        {
            return FormatXml(new XmlDocument { InnerXml = unformattedXml });
        }
        public static string FormatXml(XmlDocument doc)
        {
            StringWriter stringWriter = null;
            try
            {
                // Create a stream buffer that can be read as a string
                stringWriter = new StringWriter();

                // Create a specialized writer for XML code
                using (var xtw = new XmlTextWriter(stringWriter))
                {
                    // Set the writer to use indented (hierarchical) elements
                    xtw.Formatting = Formatting.Indented;

                    // Write the XML document to the stream
                    doc.WriteTo(xtw);
                    xtw.Flush();
                    xtw.Close();

                    stringWriter.Flush();
                    stringWriter.Close();

                    // Return the stream as a string
                    return stringWriter.ToString();
                }

            }
            finally
            {
                stringWriter?.Dispose();
            }

        }

        public static bool CheckXml(string xml)
        {
            new XmlDocument { InnerXml = xml };
            return true;
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
            var node = doc.GetElementsByTagName(name)[0];
            return node != null ? node.InnerText : string.Empty;
        }

        /*public static string CreateXmlValue(string xml, string key, bool value)
        {
            return CreateXmlValue(xml, key, value, false);
        }
        public static string CreateXmlValue(string xml, string key, bool value, bool formatXml)
        {
            return CreateXmlValue(xml, key, (value ? 1 : 0), formatXml);
        }
        public static string CreateXmlValue(string xml, string key, int value)
        {
            return CreateXmlValue(xml, key, value, false);
        }
        public static string CreateXmlValue(string xml, string key, int value, bool formatXml)
        {
            return CreateXmlValue(xml, key, value.ToString(), formatXml);
        }
        public static string CreateXmlValue(string xml, string key, string value)
        {
            return CreateXmlValue(xml, key, value, false);
        }
        public static string CreateXmlValue(string xml, string key, string value, bool formatXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml != null ? xml : "<Config />");
            SetXmlValue(doc, key, value);

            return (formatXml ? FormatXml(doc.InnerXml) : doc.InnerXml);
        }

        public static void SetXmlValue(XmlDocument doc, string key, bool value)
        {
            SetXmlValue(doc, key, (value ? 1 : 0));
        }
        public static void SetXmlValue(XmlDocument doc, string key, int value)
        {
            SetXmlValue(doc, key, value.ToString());
        }
        public static void SetXmlValue(XmlDocument doc, string key, string value)
        {
            XmlElement element = doc.CreateElement(key);
            element.InnerText = value;
            doc.DocumentElement.AppendChild(element);
        }*/




        /// <summary>
        /// Gets the attribute node in the XmlDocument instance 
        /// with the given element and attribute name.
        /// </summary>
        /// <param name="xmlDoc">
        /// XmlDocument instance to be searched.
        /// </param>
        /// <param name="element">
        /// Example XML Invoice request: 
        /// invoice:invoice/invoice:tiers_garant/invoice:provider
        /// </param>
        /// <param name="attribute">
        /// Example XML Invoice request: ean_party
        /// </param>
        /// <returns>
        /// The attribute node if the the attribute has been found. 
        /// Otherwise null.
        /// </returns>
        public static XmlNode FindAttribute(XmlDocument xmlDoc, string element, string attribute)
        {
            var elementNode = FindElement(xmlDoc, element);
            if (elementNode == null)
                return null;

            var attributeNode = elementNode.Attributes.GetNamedItem(attribute);
            return attributeNode;
        }

        /// <summary>
        /// Same as FindAttribute but this method takes n 
        /// parent elements. It checks each element for the 
        /// attribute and returns the first match.
        /// </summary>
        /// <param name="xmlDoc">
        /// XmlDocument instance to be searched.
        /// </param>
        /// <param name="elements">
        /// An array of elements.
        /// </param>
        /// <param name="attribute">
        /// See above.
        /// </param>
        /// <returns>
        /// The attribute node if the the attribute has been found. 
        /// Otherwise null.
        /// </returns>
        public static XmlNode FindAttributeOfAnyElement(XmlDocument xmlDoc, string[] elements, string attribute)
        {
            foreach (var element in elements)
            {
                var node = FindAttribute(xmlDoc, element, attribute);
                if (node != null)
                    return node;
            }
            return null;
        }

        /// <summary>
        /// The attributes of the root element have to be 
        /// accessed differently to the other elements.
        /// </summary>
        /// <param name="xmlDoc">
        /// XmlDocument instance to be searched.
        /// </param>
        /// <param name="attribute">
        ///  XML Invoice request: role
        /// </param>
        /// <returns>
        /// The attribute node if the the attribute has been found. 
        /// Otherwise null.
        /// </returns>
        public static XmlNode FindRootAttribute(XmlDocument xmlDoc, string attribute)
        {
            XmlNode rootElementNode = xmlDoc.DocumentElement;
            var attributeNode = rootElementNode.Attributes.GetNamedItem(attribute);
            return attributeNode;
        }

        /// <summary>
        /// Gets the element node in the XmlDocument instance 
        /// with the given element name.
        /// </summary>
        /// <param name="xmlDoc">
        /// XmlDocument instance to be searched.
        /// </param>
        /// <param name="element">
        /// Example XML Invoice request: 
        /// invoice:invoice/invoice:tiers_garant/invoice:provider
        /// </param>
        /// <returns>
        /// The element node if the the element has been found. 
        /// Otherwise null.
        /// </returns>
        public static XmlNode FindElement(XmlDocument xmlDoc, string element)
        {
            var namespaceManager = GetNamespaces(xmlDoc);

            var root = xmlDoc.DocumentElement;
            var elementNode = root.SelectSingleNode(element, namespaceManager);
            return elementNode;
        }

        /// <summary>
        /// If an Xml has more than one namespace, we need a 
        /// namespace manager. This method reads the namespaces from 
        /// the xml with XPath to fill the namespace manager.
        /// </summary>
        /// <param name="xmlDoc">
        /// XmlDocument that has more than one namespace.
        /// </param>
        /// <returns>
        /// A filled namespace manager.
        /// </returns>
        public static XmlNamespaceManager GetNamespaces(XmlDocument xmlDoc)
        {
            var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            var navigator = xmlDoc.DocumentElement.CreateNavigator();

            if (navigator.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml))
            {
                do
                {
                    namespaceManager.AddNamespace(navigator.Name, navigator.Value);
                } while (navigator.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml));
            }
            return namespaceManager;
        }
    }
}
