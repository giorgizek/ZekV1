using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Zek.Extensions.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        static ClaimsPrincipalExtensions()
        {
            AuthenticationType = "Identity.Application";
        }


        //private static readonly string CookiePrefix = "Identity";
        //private static readonly string DefaultApplicationScheme = CookiePrefix + ".Application";
        public static string AuthenticationType { get; set; }

        /// <summary>
        /// Returns the User ID claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The User ID claim value, or null if the claim is not present.</returns>
        /// <remarks>The User ID claim is identified by <see cref="ClaimTypes.NameIdentifier"/>.</remarks>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Returns the Name claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The Name claim value, or null if the claim is not present.</returns>
        /// <remarks>The Name claim is identified by <see cref="ClaimsIdentity.DefaultNameClaimType"/>.</remarks>
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal.FindFirstValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Returns the Email claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The Email claim value, or null if the claim is not present.</returns>
        /// <remarks>The Email claim is identified by <see cref="ClaimTypes.Email"/>.</remarks>
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal.FindFirstValue(ClaimTypes.Email);
        }

        /// <summary>
        /// Returns true if the principal has an identity with the application cookie identity
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>True if the user is logged in with identity.</returns>
        public static bool IsSignedIn(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal.Identities != null && principal.Identities.Any(i => i.AuthenticationType == AuthenticationType);
        }

        /// <summary>
		///     Get the the collection of all roles for a <see cref="ClaimsPrincipal"/>.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="principal" /> is <c>null</c>.</exception>
		/// <returns>A collection that including all the roles for the <paramref name="principal" />. If the user has no roles, this method will return a empty collection.</returns>
        public static IEnumerable<string> GetUserRoles(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.FindAll(ClaimTypes.Role).Select(i => i.Value);
            //return principal.FindAll(ClaimsIdentity.DefaultRoleClaimType).Select(i => i.Value);
        }

        public static bool IsInAnyRole(this ClaimsPrincipal principal, params string[] roles)
        {
            return roles != null && roles.Any(principal.IsInRole);
        }

        //public static void Add(this ICollection<Claim> claims, string type, string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //        claims.Add(new Claim(type, value));
        //}

        //public static void AddNameIdentifier(this List<Claim> claims, string nameIdentifier) => claims.Add(ClaimTypes.NameIdentifier, nameIdentifier);
        //public static void AddName(this List<Claim> claims, string name) => claims.Add(ClaimTypes.Name, name);
        //public static void AddEmail(this List<Claim> claims, string email) => claims.Add(ClaimTypes.Email, email);
        //public static void AddGivenName(this List<Claim> claims, string givenName) => claims.Add(ClaimTypes.GivenName, givenName);
        //public static void AddSurname(this List<Claim> claims, string surname) => claims.Add(ClaimTypes.Surname, surname);
        //public static void AddRoles(this List<Claim> claims, IEnumerable<string> roles)
        //{
        //    if (roles != null)
        //        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        //}

        /*
        /// <summary>
        /// Returns the GroupSid claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The GroupSid claim value, or null if the claim is not present.</returns>
        /// <remarks>The GroupSid claim is identified by <see cref="ClaimTypes.GroupSid"/>.</remarks>
        public static string GetGroupSid(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.FindFirstValue(ClaimTypes.GroupSid);
        }*/
    }
}
