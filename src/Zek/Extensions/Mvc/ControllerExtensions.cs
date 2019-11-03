using Microsoft.AspNetCore.Mvc;
using Zek.Extensions.Security.Claims;

namespace Zek.Extensions.Mvc
{
    public static class ControllerExtensions
    {
        public static string GetUserId(this Controller controller) => controller.User.GetUserId();
        public static string GetUserName(this Controller controller) => controller.User.GetUserName();
        public static string GetEmail(this Controller controller) => controller.User.GetEmail();
    }
}
