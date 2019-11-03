using System.Web;

namespace Zek.Web
{
    public class FormHelper
    {
        /// <summary>
        /// Gets Form String
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Result</returns>
        public static string GetString(string name)
        {
            var result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request[name] != null)
                result = HttpContext.Current.Request[name];
            return result;
        }
    }
}
