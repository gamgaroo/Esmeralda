using System;
using System.Text;
using Gamgaroo.Esmeralda.App.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gamgaroo.Esmeralda.App.Filters
{
    public sealed class BasicAuthorizationFilter : IAuthorizationFilter
    {
        private readonly AdminCredentials _adminCredentials;

        public BasicAuthorizationFilter(AdminCredentials adminCredentials)
        {
            _adminCredentials = adminCredentials;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword =
                    authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                var decodedUsernamePassword =
                    Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];

                if (IsAuthorized(username, password))
                    return;
            }

            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Result = new UnauthorizedResult();
        }

        public bool IsAuthorized(string username, string password)
        {
            return username.Equals(_adminCredentials.Username) &&
                   password.Equals(_adminCredentials.Password);
        }
    }
}