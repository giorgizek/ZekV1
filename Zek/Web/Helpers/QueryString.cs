using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;

namespace Zek.Web
{
    /// <summary>
    /// Class for parsing URL parameters (parameters).
    /// </summary>
    public class QueryString : ICloneable
    {
        #region Construction
        /// <summary>
        /// Constructor.
        /// </summary>
        public QueryString()
        {
            if (HttpContext.Current != null && HttpContext.Current.Handler != null && HttpContext.Current.Handler is Page)
            {
                _currentPage = HttpContext.Current.Handler as Page;
                FromUrl(_currentPage);
            }
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public QueryString(Page currentPage)
        {
            FromUrl(currentPage);
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public QueryString(string url)
        {
            FromUrl(url);
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public QueryString(Uri uri)
        {
            FromUrl(uri.AbsoluteUri);
        }
        #endregion

        #region Public properties.
        /// <summary>
        /// Access an parameter value by the parameter name.
        /// </summary>
        public string this[string index]
        {
            get { return _queryString[index]; }
            set { _queryString[index] = value; }
        }

        /// <summary>
        /// Get the complete string including the BeforeUrl and
        /// all current parameters.
        /// </summary>
        public string All
        {
            get { return BeforeUrl + Make(); }
            set { FromUrl(value); }
        }

        /// <summary>
        /// The URL that comes before the actual name-value pair parameters.
        /// </summary>
        private string _beforeUrl = string.Empty;
        /// <summary>
        /// The URL that comes before the actual name-value pair parameters.
        /// </summary>
        public string BeforeUrl
        {
            get
            {
                return _beforeUrl;
            }
            set
            {
                _beforeUrl = value;
            }
        }
        #endregion

        #region Public operations.
        /// <summary>
        /// Appends a query string onto an existing URL. Takes care 
        /// of worrying about whether to add "&..." or "?...".
        /// </summary>
        /// <param name="url">The URL to be extended.</param>
        /// <param name="queryString">The query string to add.</param>
        /// <returns>Returns the complete URL.</returns>
        public static string AppendQueryString(string url, string queryString)
        {
            var result = url.TrimEnd('?', '&');

            if (result.IndexOf("?", StringComparison.Ordinal) >= 0)
                return url + "&" + queryString;
            return url + "?" + queryString;
        }

        /// <summary>
        /// Check whether an parameter with a given name exists.
        /// </summary>
        /// <param name="parameterName">The name of the parameter
        /// to check for.</param>
        /// <returns>Returns TRUE if the parameter is present and
        /// has a non-empty value, returns FALSE otherwise.</returns>
        public bool HasParameter(string parameterName)
        {
            if (parameterName == null || parameterName.Trim().Length <= 0)
            {
                return false;
            }
            parameterName = parameterName.Trim();
            var v = this[parameterName];

            if (v == null || v.Trim().Length <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set or replace a single parameter.
        /// </summary>
        /// <param name="name">The name of the parameter to set.</param>
        /// <param name="value">The value of the parameter to set.</param>
        public void SetParameter(string name, string value)
        {
            _queryString[name] = value;
        }

        /// <summary>
        /// Removes an parameter (if exists) with the given name.
        /// </summary>
        /// <param name="name">The name of the parameter to remove.</param>
        public void RemoveParameter(string name)
        {
            _queryString.Remove(name);
        }

        /// <summary>
        /// Removes all parameters.
        /// </summary>
        public void RemoveAllParameters()
        {
            _queryString.Clear();
        }

        /// <summary>
        /// Get an parameter value by a given name.
        /// </summary>
        /// <param name="name">The name of the parameter value to retrieve.</param>
        /// <returns>Returns an empty string (NOT null) if the parameter
        /// is not found.</returns>
        public string GetParameter(string name)
        {
            var result = _queryString[name];

            if (string.IsNullOrEmpty(result))
            {
                if (_currentPage != null)
                {
                    result = _currentPage.Request.Form[name];
                }

                // try session, also.
                if (string.IsNullOrEmpty(result))
                {
                    if (_currentPage != null)
                    {
                        var o = _currentPage.Session[name];
                        if (o != null)
                        {
                            result = o.ToString();
                        }
                    }
                }

                // Try cookies, also.
                if (string.IsNullOrEmpty(result))
                {
                    if (_currentPage != null)
                    {
                        var c = _currentPage.Request.Cookies[name];
                        if (c != null)
                        {
                            result = c.Value;
                        }
                    }
                }
            }

            return result ?? string.Empty;
        }
        #endregion

        #region Reading from an URL.
        /// <summary>
        /// Parse a query string and insert the found parameters
        /// into the collection of this class.
        /// </summary>
        public void FromUrl(Page page)
        {
            if (page != null)
            {
                _currentPage = page;
                FromUrl(_currentPage.Request.RawUrl);
            }
        }

        /// <summary>
        /// Parse a query string and insert the found parameters
        /// into the collection of this class.
        /// </summary>
        public void FromUrl(Uri uri)
        {
            if (uri != null)
            {
                FromUrl(uri.AbsoluteUri);
            }
        }

        /// <summary>
        /// Parse a query string and insert the found parameters
        /// into the collection of this class.
        /// </summary>
        public void FromUrl(string url)
        {
            if (url != null)
            {
                _queryString.Clear();

                // store the part before, too.
                var qPos = url.IndexOf("?", StringComparison.Ordinal);
                if (qPos >= 0)
                {
                    BeforeUrl = url.Substring(0, qPos - 0);
                    url = url.Substring(qPos + 1);
                }
                else
                {
                    BeforeUrl = url;
                }

                if (url.Length > 0 && url.Substring(0, 1) == "?")
                {
                    url = url.Substring(1);
                }

                // break the values.
                var pairs = url.Split('&');
                foreach (var pair in pairs)
                {
                    var a = string.Empty;
                    var b = string.Empty;

                    var singular = pair.Split('=');

                    var j = 0;
                    foreach (var one in singular)
                    {
                        if (j == 0)
                        {
                            a = one;
                        }
                        else
                        {
                            b = one;
                        }

                        j++;
                    }

                    // store.
                    SetParameter(a, HttpUtility.UrlDecode(b));
                }
            }
        }
        #endregion

        #region Making a string from the parameters.
        /// <summary>
        /// Build a single string from the current name-value pairs inside
        /// this class.
        /// </summary>
        /// <returns>Returns the complete string.</returns>
        public string Make()
        {
            var result = "?";

            foreach (string name in _queryString)
            {
                var val = _queryString[name];

                if (!string.IsNullOrEmpty(val))
                    result += name + "=" + HttpUtility.UrlEncode(val) + "&";
            }

            //return result;
            return result.TrimEnd('?', '&');
        }

        /// <summary>
        /// Build a single string from the current name-value pairs inside
        /// this class. Replace/add the name-value pairs passed as 
        /// parameters to this function.
        /// </summary>
        /// <returns>Returns the complete string.</returns>
        public string Make(string name1, string value1)
        {
            return Make(name1, value1, string.Empty, string.Empty);
        }

        /// <summary>
        /// Build a single string from the current name-value pairs inside
        /// this class. Replace/add the name-value pair(s) passed as 
        /// parameters to this function.
        /// </summary>
        /// <returns>Returns the complete string.</returns>
        public string Make(string name1, string value1, string name2, string value2)
        {
            return Make(name1, value1, name2, value2, string.Empty, string.Empty);
        }

        /// <summary>
        /// Build a single string from the current name-value pairs inside
        /// this class. Replace/add the name-value pair(s) passed as 
        /// parameters to this function.
        /// </summary>
        /// <returns>Returns the complete string.</returns>
        public string Make(string name1, string value1, string name2, string value2, string name3, string value3)
        {
            return Make(name1, value1, name2, value2, name3, value3, string.Empty, string.Empty);
        }

        /// <summary>
        /// Build a single string from the current name-value pairs inside
        /// this class. Replace/add the name-value pair(s) passed as 
        /// parameters to this function.
        /// </summary>
        /// <returns>Returns the complete string.</returns>
        public string Make(string name1, string value1, string name2, string value2, string name3, string value3, string name4, string value4)
        {
            return Make(name1, value1, name2, value2, name3, value3, name4, value4, string.Empty, string.Empty);
        }

        /// <summary>
        /// Build a single string from the current name-value pairs inside
        /// this class. Replace/add the name-value pair(s) passed as 
        /// parameters to this function.
        /// </summary>
        /// <returns>Returns the complete string.</returns>
        public string Make(string name1, string value1, string name2, string value2, string name3, string value3, string name4, string value4, string name5, string value5)
        {
            var old5 = GetParameter(name5);
            var old4 = GetParameter(name4);
            var old3 = GetParameter(name3);
            var old2 = GetParameter(name2);
            var old1 = GetParameter(name1);

            SetParameter(name5, value5);
            SetParameter(name4, value4);
            SetParameter(name3, value3);
            SetParameter(name2, value2);
            SetParameter(name1, value1);

            var result = Make();

            SetParameter(name5, old5);
            SetParameter(name4, old4);
            SetParameter(name3, old3);
            SetParameter(name2, old2);
            SetParameter(name1, old1);

            return result;
        }
        #endregion

        #region ICloneable member.
        /// <summary>
        /// Makes a deep copy.
        /// </summary>
        public object Clone()
        {
            var dst = new QueryString {_currentPage = _currentPage, BeforeUrl = BeforeUrl};

            // Clone.
            foreach (string key in _queryString.Keys)
            {
                dst._queryString[key] = _queryString[key];
            }

            return dst;
        }
        #endregion

        #region Private
        /// <summary>
        /// The page that is currently loaded.
        /// </summary>
        private Page _currentPage;

        /// <summary>
        /// The collection to store the name-value pairs.
        /// </summary>
        private NameValueCollection _queryString = new NameValueCollection();
        #endregion
    }
}
