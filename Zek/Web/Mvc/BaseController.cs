using System;
using System.Text;
using System.Web.Mvc;

namespace Zek.Web.Mvc
{
    public class BaseController : Controller
    {
        protected virtual JsonResult JsonModel(Enum errorCode, string errorMessage = null, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return JsonModel(new JsonModel(Convert.ToInt32(errorCode), errorMessage), contentType, contentEncoding, behavior);
        }
        protected virtual JsonResult JsonModel<T>(T value, Enum errorCode, string errorMessage = null, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return JsonModel(new JsonModel<T>(value, Convert.ToInt32(errorCode), errorMessage), contentType, contentEncoding, behavior);
        }


        protected virtual JsonResult JsonModel(int errorCode = 0, string errorMessage = null, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return JsonModel(new JsonModel(errorCode, errorMessage), contentType, contentEncoding, behavior);
        }
        protected virtual JsonResult JsonModel(JsonModel data, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return new JsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior };
        }
        protected virtual JsonResult JsonModel<T>(T value, int errorCode = 0, string errorMessage = null, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return JsonModel(new JsonModel<T>(value, errorCode, errorMessage), contentType, contentEncoding, behavior);
        }
        protected virtual JsonResult JsonModel<T>(JsonModel<T> data, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return new JsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior };
        }
    }
}
