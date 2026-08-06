using Stenford.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Account
{
	public interface IAccountRepository
	{
		AspAspNetUser AreUserCredentialsValid(string userName, string password);
		UserJwtDTO GetAspNetUserDetail(Guid aspNetUserId);
	}
}
