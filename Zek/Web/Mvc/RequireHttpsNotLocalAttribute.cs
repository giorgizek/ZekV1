using System;
using System.Web.Mvc;

namespace Zek.Web.Mvc
{
    /// <summary>
    /// თუ გვინდა რომ საიტი მთლიანად იყოს https-ზე მაშინ უნდა ჩავამატოთ:
    /// public class FilterConfig
    /// {
    ///     public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    ///     {
    ///         filters.Add(new HandleErrorAttribute());
    ///         filters.Add(new RequireHttpsNotLocalAttribute());
    ///     }
    /// }
    /// </summary>
    public class RequireHttpsNotLocalAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            if (filterContext.HttpContext.Request.IsLocal)
            {
                // when connection to the application is local, don't do any HTTPS stuff
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}
