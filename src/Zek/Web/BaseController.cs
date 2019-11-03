using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Zek.Extensions;
using Zek.Extensions.Mvc;
using Zek.Model.ViewModel;

namespace Zek.Web
{
    public class BaseController : Controller
    {
        public string Title
        {
            get => ViewData[nameof(Title)]?.ToString();
            set => ViewData[nameof(Title)] = value;
        }
        public string ReturnUrl { set => ViewData[nameof(ReturnUrl)] = value; }



        private int? _userId;
        protected virtual int UserId
        {
            get
            {
                if (_userId == null)
                    _userId = this.GetUserId().ToInt32();
                return _userId.Value;
            }
        }

        //protected virtual int UserId => HttpContext.Session.GetInt32(nameof(UserId));
        //protected virtual void SetUserId(int userId)
        //{
        //    HttpContext.Session.SetInt32(nameof(UserId), userId);
        //}

        //protected virtual int CurrentUserId
        //{
        //    get { return HttpContext.Session.GetInt32(nameof(CurrentUserId)); }
        //    set { HttpContext.Session.SetInt32(nameof(CurrentUserId), value); }
        //}
        protected virtual byte CurrentCultureId => 2;

        [NonAction]
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [NonAction]
        protected IActionResult Error()
        {
            return View(nameof(Error));
        }
        [NonAction]
        protected IActionResult Alert(AlertViewModel model)
        {
            if (!string.IsNullOrEmpty(model.PageTitle))
                Title = model.PageTitle;

            return View(nameof(Alert), model);
        }

        protected IActionResult DisplayTempalate<TModel>(TModel model)
        {
            var viewName = "DisplayTemplates" + typeof(TModel).Name;
            return PartialView(viewName, model);
        }
        protected IActionResult EditorTemplate<TModel>(TModel model)
        {
            var viewName = "EditorTemplates" + typeof(TModel).Name;
            return PartialView(viewName, model);
        }


        //[NonAction]
        //public virtual OkObjectResult Ok(int errorCode = 0, string message = null)
        //{
        //    return Ok(new JsonModel(errorCode, message));
        //}
        //[NonAction]
        //public virtual OkObjectResult Ok<T>(T value, string message) => Ok<T>(value, 0, message);
        //[NonAction]
        //public virtual OkObjectResult Ok<T>(T value, int errorCode = 0, string message = null)
        //{
        //    return recursiulad midis!!! Ok(new JsonModel<T>
        //    {

        //    });
        //}


        //public IActionResult IfAjaxPartialView(string partialViewName, object model)
        //{
        //    if (Request.IsAjaxRequest())
        //        return PartialView(partialViewName, model);
        //    return View(model);
        //}
    }
}
