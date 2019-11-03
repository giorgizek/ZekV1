using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

namespace Zek.Web
{
    [Serializable]
    public enum TagRenderMode
    {
        Normal,
        StartTag,
        EndTag,
        SelfClosing
    }

    public class TagBuilder
    {
        public TagBuilder(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                throw new ArgumentException("ტეგის დასახელება ცარიელია.", nameof(tagName));

            TagName = tagName;
            Attributes = new Dictionary<string, string>(StringComparer.Ordinal);
        }


        public const string AttributeFormat = @" {0}=""{1}""";
        public const string ElementFormatEndTag = "</{0}>";
        public const string ElementFormatNormal = "<{0}{1}>{2}</{0}>";
        public const string ElementFormatSelfClosing = "<{0}{1} />";
        public const string ElementFormatStartTag = "<{0}{1}>";


        public IDictionary<string, string> Attributes
        {
            get;
            private set;
        }

        private string _idAttributeDotReplacement;
        public string IdAttributeDotReplacement
        {
            get
            {
                if (string.IsNullOrEmpty(_idAttributeDotReplacement))
                    _idAttributeDotReplacement = "_";

                return _idAttributeDotReplacement;
            }
            set
            {
                _idAttributeDotReplacement = value;
            }
        }

        private string _innerHtml;
        public string InnerHtml
        {
            get
            {
                return _innerHtml ?? string.Empty;
            }
            set
            {
                _innerHtml = value;
            }
        }

        public string TagName
        {
            get;
            private set;
        }



        public void AddCssClass(string value)
        {
            string currentValue;

            if (Attributes.TryGetValue("class", out currentValue))
            {
                Attributes["class"] = value + " " + currentValue;
            }
            else
            {
                Attributes["class"] = value;
            }
        }

        public void GenerateId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                MergeAttribute("id", id.Replace(".", IdAttributeDotReplacement));
            }
        }

        private string GetAttributesString()
        {
            var sb = new StringBuilder();
            foreach (var attribute in Attributes)
            {
                var key = attribute.Key;
                var value = HttpUtility.HtmlAttributeEncode(attribute.Value);
                sb.AppendFormat(CultureInfo.InvariantCulture, AttributeFormat, key, value);
            }
            return sb.ToString();
        }

        public void MergeAttribute(string key, string value)
        {
            MergeAttribute(key, value, false /* replaceExisting */);
        }
        public void MergeAttribute(string key, string value, bool replaceExisting)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("გადმოცემული პარამეტრი ცარიელია.", nameof(key));

            if (replaceExisting || !Attributes.ContainsKey(key))
            {
                Attributes[key] = value;
            }
        }

        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            MergeAttributes(attributes, false /* replaceExisting */);
        }
        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
        {
            if (attributes != null)
            {
                foreach (var entry in attributes)
                {
                    var key = Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
                    var value = Convert.ToString(entry.Value, CultureInfo.InvariantCulture);
                    MergeAttribute(key, value, replaceExisting);
                }
            }
        }

        public void SetInnerText(string innerText)
        {
            InnerHtml = HttpUtility.HtmlEncode(innerText);
        }

        public override string ToString()
        {
            return ToString(TagRenderMode.Normal);
        }
        public string ToString(TagRenderMode renderMode)
        {
            switch (renderMode)
            {
                case TagRenderMode.StartTag:
                    return String.Format(CultureInfo.InvariantCulture, ElementFormatStartTag, TagName, GetAttributesString());
                case TagRenderMode.EndTag:
                    return String.Format(CultureInfo.InvariantCulture, ElementFormatEndTag, TagName);
                case TagRenderMode.SelfClosing:
                    return String.Format(CultureInfo.InvariantCulture, ElementFormatSelfClosing, TagName, GetAttributesString());
                default:
                    return String.Format(CultureInfo.InvariantCulture, ElementFormatNormal, TagName, GetAttributesString(), InnerHtml);
            }
        }






        internal static string BuildTable(string name, IList items, IDictionary<string, object> attributes)
        {
            var sb = new StringBuilder();
            BuildTableHeader(sb, items[0].GetType());

            foreach (var item in items)
            {
                BuildTableRow(sb, item);
            }

            var builder = new TagBuilder("table");
            builder.MergeAttributes(attributes);
            builder.MergeAttribute("name", name);
            builder.InnerHtml = sb.ToString();
            return builder.ToString(TagRenderMode.Normal);
        }
        internal static void BuildTableHeader(StringBuilder sb, Type p)
        {
            sb.AppendLine("\t<tr>");
            foreach (var property in p.GetProperties())
            {
                sb.AppendFormat("\t\t<th>{0}</th>\n", property.Name);
            }
            sb.AppendLine("\t</tr>");
        }
        internal static void BuildTableRow(StringBuilder sb, object obj)
        {
            var objType = obj.GetType();
            sb.AppendLine("\t<tr>");
            foreach (var property in objType.GetProperties())
            {
                sb.AppendFormat("\t\t<td>{0}</td>\n", property.GetValue(obj, null));
            }
            sb.AppendLine("\t</tr>");
        }

    }
}
