using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static Stenford.Common.Constants.Enums;
using Stenford.Service.JwtToken;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;


namespace StenfordAPI.Authmanager
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthManager : Attribute, IAsyncAuthorizationFilter
    {
        private readonly UserType[] _roles;
        public AuthManager(params UserType[] roles)
        {
            _roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtTokenRepository>();

            if (jwtService == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            var token = await context.HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(token) || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            string roleNameClaimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == "RoleName")?.Value ?? "";
            if (string.IsNullOrEmpty(roleNameClaimValue))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            var userTypeClaim = GetEnumByDescription<UserType>(roleNameClaimValue);
            if (userTypeClaim == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            if (!_roles.Contains(userTypeClaim.Value))
            {
                context.Result = new JsonResult(new { message = "Access denied" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
