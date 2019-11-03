using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zek.Web
{
    public static class ViewContextExtensions
    {
        //public static string GetController(this RazorPage page)
        //{
        //    return page.ViewContext.RouteData.Values["controller"].ToString();
        //}
        //public static string GetAction(this RazorPage page)
        //{
        //    return page.ViewContext.RouteData.Values["action"].ToString();
        //}
        //public static string GetArea(this RazorPage page)
        //{
        //    object areaObj;
        //    return page.ViewContext.RouteData.Values.TryGetValue("area", out areaObj)
        //        ? areaObj.ToString()
        //        : null;
        //}


        public static string GetController(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["controller"].ToString();
        }
        public static string GetAction(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["action"].ToString();
        }
        public static string GetArea(this ViewContext viewContext)
        {
            object areaObj;
            return viewContext.RouteData.Values.TryGetValue("area", out areaObj)
                ? areaObj.ToString()
                : null;
        }



        public static bool IsPost(this ViewContext viewContext)
        {
            return viewContext.HttpContext.Request.Method == "POST";
        }

        //public static bool IsPost(this RazorPage page)
        //{
        //    return page.ViewContext.IsPost();
        //}
    }
}
