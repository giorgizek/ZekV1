using System.Web;
using System.Web.Mvc;

namespace Zek.Extensions.Web
{
    public static class HtmlHelperExtensions
    {
        public static string Id(this HtmlHelper htmlHelper)
        {
            return (string)HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];
        }


        public static string Controller(this HtmlHelper htmlHelper)
        {
            return (string)HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            return (string)HttpContext.Current.Request.RequestContext.RouteData.Values["action"];
        }



        //public static string Id(this HtmlHelper htmlHelper)
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

        //    if (routeValues.ContainsKey("id"))
        //        return (string)routeValues["id"];

        //    return HttpContext.Current.Request.QueryString.AllKeys.Contains("id") ? HttpContext.Current.Request.QueryString["id"] : string.Empty;
        //}

        //public static string Controller(this HtmlHelper htmlHelper)
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

        //    if (routeValues.ContainsKey("controller"))
        //        return (string)routeValues["controller"];

        //    return string.Empty;
        //}

        //public static string Action(this HtmlHelper htmlHelper)
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

        //    if (routeValues.ContainsKey("action"))
        //        return (string)routeValues["action"];

        //    return string.Empty;
        //}
    }
}
