using System.Web;
using Zek.Service.Bank;

namespace Zek.Web
{
    public class CallBack : BaseCartuCallBack
    {

        protected override void OnCheck(HttpContext context, CartuHelper.ConfirmRequest request)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(request.GetResponseToBank(true));
        }

        protected override void OnSuccess(HttpContext context, CartuHelper.ConfirmRequest request)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(request.GetResponseToBank(true));
        }

        protected override void OnNotSuccess(HttpContext context, CartuHelper.ConfirmRequest request)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(request.GetResponseToBank(true));
        }


        protected override void OnUnfinished(HttpContext context, CartuHelper.ConfirmRequest request)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(request.GetResponseToBank(true));
        }

        protected override void OnUnknown(HttpContext context, CartuHelper.ConfirmRequest request)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(request.GetResponseToBank(true));
        }

    }
}