using System.IdentityModel.Tokens.Jwt;

namespace StenfordAPI.Models
{
    public class CV
    {
        public static string? AspNetUserId(string token) =>
            GetClaimValue(token, "AspNetUserID");

        public static string? AdminId(string token) =>
            GetClaimValue(token, "AdminID");

        public static string? SalesPersonId(string token) =>
            GetClaimValue(token, "SalesPersonID");

        public static string? AssociateId(string token) =>
            GetClaimValue(token, "AssociateID");

        public static string? AspNetUserWiseRoleId(string token) =>
            GetClaimValue(token, "AspNetUserWiseRoleID");

        public static string? RoleName(string token) =>
            GetClaimValue(token, "RoleName");

        public static string? UserName(string token) =>
            GetClaimValue(token, "UserName");

        public static string? ClaimTypesEmail(string token) =>
            GetClaimValue(token, System.Security.Claims.ClaimTypes.Email);

        public static string? ClaimTypesNameIdentifier(string token) =>
            GetClaimValue(token, System.Security.Claims.ClaimTypes.NameIdentifier);

        private static string? GetClaimValue(string token, string claimType)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return null;

            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
