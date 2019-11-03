using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Zek.Extensions.Security.Claims;

namespace Zek.Extensions.Http.Authentication
{
    public static class AuthenticationExtensions
    {

        /// <summary>
        /// Custom authentication
        /// </summary>
        /// <param name="authenticationManager">HttpContext.Authentication</param>
        /// <param name="userId">User ID</param>
        /// <param name="userName">UserName</param>
        /// <param name="isPersistent"></param>
        /// <param name="roles">Array of roles</param>
        /// <returns></returns>
        public static async Task SignInAsync(this AuthenticationManager authenticationManager, string userId, string userName, bool isPersistent, IEnumerable<string> roles = null) => await SignInAsync(authenticationManager, userId, userName, roles: roles, properties: new AuthenticationProperties
        {
            IsPersistent = isPersistent
        });

        /// <summary>
        /// Custom authentication
        /// </summary>
        /// <param name="authenticationManager">HttpContext.Authentication</param>
        /// <param name="userId">User ID</param>
        /// <param name="userName">UserName</param>
        /// <param name="email">Email</param>
        /// <param name="isPersistent"></param>
        /// <param name="roles">Array of roles</param>
        /// <returns></returns>
        public static async Task SignInAsync(this AuthenticationManager authenticationManager, string userId, string userName, string email, bool isPersistent, IEnumerable<string> roles = null) => await SignInAsync(authenticationManager, userId, userName,email, roles: roles, properties: new AuthenticationProperties
        {
            IsPersistent = isPersistent
        });

        /// <summary>
        /// Custom authentication
        /// </summary>
        /// <param name="authenticationManager">HttpContext.Authentication</param>
        /// <param name="userId">User ID</param>
        /// <param name="userName">UserName</param>
        /// <param name="email">Email</param>
        /// <param name="roles">Array of roles</param>
        /// <param name="properties">Additional arbitrary values which may be used by particular authentication types.</param>
        /// <returns></returns>
        public static async Task SignInAsync(
            this AuthenticationManager authenticationManager,
            string userId,
            string userName,
            string email = null,
            //string givenName = null,
            //string surname = null,
            IEnumerable<string> roles = null,
            AuthenticationProperties properties = null)
        {
            if (authenticationManager == null)
                throw new ArgumentNullException(nameof(authenticationManager));
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID parameter is required.", nameof(userId));
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("UserName parameter is required.", nameof(userName));

            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.CookiePath, ".contoso.com"),//CookieDomain - the domain name the cookie will be served to. By default this is the host name the request was sent to. The browser will only serve the cookie to a matching host name. You may wish to adjust this to have cookies available to any host in your domain. For example setting the cookie domain to .contoso.com will make it available to contoso.com, www.contoso.com, staging.www.contoso.com etc.
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            };

            if (!string.IsNullOrEmpty(email))
                claims.Add(new Claim(ClaimTypes.Email, email));
            //if (!string.IsNullOrEmpty(givenName))
            //    claims.Add(new Claim(ClaimTypes.GivenName, givenName));
            //if (!string.IsNullOrEmpty(surname))
            //    claims.Add(new Claim(ClaimTypes.Surname, surname));
            //if (!string.IsNullOrEmpty(groupSid))
            //    claims.Add(new Claim(ClaimTypes.GroupSid, groupSid));
            if (roles != null)
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            await SignInAsync(authenticationManager, claims, properties);
        }

        /// <summary>
        /// Custom authentication
        /// </summary>
        /// <param name="authenticationManager">HttpContext.Authentication</param>
        /// <param name="claims"></param>
        /// <param name="properties">Additional arbitrary values which may be used by particular authentication types.</param>
        /// <returns></returns>
        public static async Task SignInAsync(this AuthenticationManager authenticationManager, IEnumerable<Claim> claims, AuthenticationProperties properties = null)
        {
            if (authenticationManager == null)
                throw new ArgumentNullException(nameof(authenticationManager));

            var identity = new ClaimsIdentity(claims, ClaimsPrincipalExtensions.AuthenticationType);
            var principal = new ClaimsPrincipal(identity);

            await authenticationManager.SignInAsync(ClaimsPrincipalExtensions.AuthenticationType, principal, properties);
        }

        public static async Task SignOutAsync(this AuthenticationManager authenticationManager, AuthenticationProperties properties = null)
        {
            await authenticationManager.SignOutAsync(ClaimsPrincipalExtensions.AuthenticationType, properties);
        }
    }
}