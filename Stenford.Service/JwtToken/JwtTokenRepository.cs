using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.JwtToken
{
	public class JwtTokenRepository : IJwtTokenRepository
	{
		private readonly IConfiguration _config;
		public JwtTokenRepository(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateJWTAuthetication(UserJwtDTO userJwt)
		{
            var associateID = userJwt.AdminID == 0 ? userJwt.SalesPersonID : userJwt.AdminID;

            var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, userJwt.EmailId),
				new Claim("AspNetUserID", userJwt.AspNetUserID.ToString()),
				new Claim("RoleId", userJwt.RoleId.ToString()),
				new Claim("RoleName", userJwt.RoleName.ToString()),
				new Claim("AdminID", userJwt.AdminID.ToString()),
				new Claim("SalesPersonID", userJwt.SalesPersonID.ToString()),
				new Claim("AssociateID", associateID.ToString()),
				new Claim("AspNetUserWiseRoleID", userJwt.AspNetUserWiseRoleId.ToString()),
				new Claim("UserName", userJwt.UserName.ToString()),
				new Claim(ClaimTypes.NameIdentifier, userJwt.EmailId),
			};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(10000);

			var token = new JwtSecurityToken(
				_config["Jwt:Issuer"],
				_config["Jwt:Audience"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		//public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
		//{
		//	jwtSecurityToken = null;
		//	if (token == null)
		//	{
		//		return false;
		//	}

		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
		//	try
		//	{
		//		tokenHandler.ValidateToken(token, new TokenValidationParameters
		//		{
		//			ValidateIssuerSigningKey = true,
		//			IssuerSigningKey = new SymmetricSecurityKey(key),
		//			ValidateIssuer = true,
		//			ValidateLifetime = true,
		//			ValidateAudience = false,
		//			ValidIssuer = _config["Jwt:Issuer"],
		//			ClockSkew = TimeSpan.Zero
		//		}, out SecurityToken validatedToken);

		//		jwtSecurityToken = (JwtSecurityToken)validatedToken;
		//		if (jwtSecurityToken != null)
		//			return true;
		//		return false;
		//	}
		//	catch (Exception ex)
		//	{
		//		return (false);
		//	}
		//}
	}
}
