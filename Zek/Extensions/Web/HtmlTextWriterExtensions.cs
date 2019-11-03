using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Zek.Extensions.Web
{
    public static class HtmlTextWriterExtensions
    {
        public static void AddAttributes(this HtmlTextWriter writer, IDictionary<string, object> attributes)
        {
            if (attributes.Any())
            {
                foreach (var pair in attributes)
                {
                    writer.AddAttribute(pair.Key, pair.Value.ToString(), true);
                }
            }
        }
    }
}
