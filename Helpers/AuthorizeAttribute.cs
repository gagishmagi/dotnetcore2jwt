using System;
using DotnetCore3Jwt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotnetCore3Jwt.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if(user == null){
                context.Result = new JsonResult(new {message = "Token is Missing or not given"}){
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
