using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Idis.WebApi
{
    /// <summary>
    /// Specifies that the class or method that this attribute is applied to requires the specified authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata
                         .Any(meta => meta is AllowAnonymousAttribute);

            if (allowAnonymous) return;

            var user = (UserModel)context.HttpContext.Items["User"];
            if (user == null)
            {
                if (context.HttpContext.Request.Method == HttpMethods.Head)
                {
                    context.HttpContext.Response.Headers.Add("api-authorize", "Unauthorized");
                    return;
                }

                // Not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
