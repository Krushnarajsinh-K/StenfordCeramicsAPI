using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.JwtToken
{
	public interface IJwtTokenRepository
	{
		public string GenerateJWTAuthetication(UserJwtDTO userJwt);
		//public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
	}
}
