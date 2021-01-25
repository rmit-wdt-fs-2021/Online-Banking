using AdminApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AdminApp.Filters
{
    public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Session.GetString(nameof(Admin.Username));
            if (!string.IsNullOrEmpty(user))
                context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
