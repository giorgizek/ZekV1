using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace Zek.Extensions.Web
{
    public static class ModelStateExtensions
    {
        /* controller
        if (!ModelState.IsValid)
        {
            return Json(new { Errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
        }
        */


        /* javascript
        function DisplayErrors(errors) {
            for (var i = 0; i < errors.length; i++) {
                $("<label for='" + errors[i].Key + "' class='error'></label>")
                .html(errors[i].Value[0]).appendTo($("input#" + errors[i].Key).parent());
            }
        }
        */


        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Any());
            }
            return null;
        }
    }
}
