using Stenford.Common.Utility;
using Stenford.Domain.DataContext;
using Stenford.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AspAspNetUser AreUserCredentialsValid(string userName, string encodedPassword)
        {
            var userRecord = _context.AspAspNetUsers
                .FirstOrDefault(x => x.Username == userName
                                     && x.PasswordHash == encodedPassword);

            return userRecord;
        }

        public UserJwtDTO GetAspNetUserDetail(Guid aspNetUserId)
        {
            var user = _context.AspAspNetUsers.FirstOrDefault(x => x.AspNetUserId == aspNetUserId);

            if (user == null)
            {
                return null;
            }

            var userWiseRole = _context.AspAspNetUserWiseRoles
    .FirstOrDefault(x => x.AspNetUserId == aspNetUserId && x.IsDeleted == false);

            var admin = _context.SecAdmins.FirstOrDefault(a => a.AspNetUserId == aspNetUserId && a.IsDeleted == false);
            if (admin != null)
            {
                return new UserJwtDTO
                {
                    AspNetUserID = aspNetUserId,
                    EmailId = user.Username,
                    UserName = admin.UserName,
                    RoleId = 1,
                    RoleName = "Admin",
                    AdminID = admin.AdminId,
                    SalesPersonID = 0,
                    AspNetUserWiseRoleId = userWiseRole.AspNetUserWiseRoleId
                };
            }

            var salesPerson = _context.SecSalesPeople.FirstOrDefault(sp => sp.AspNetUserId == aspNetUserId && sp.IsDeleted == false);
            if (salesPerson != null)
            {
                return new UserJwtDTO
                {
                    AspNetUserID = aspNetUserId,
                    EmailId = user.Username,
                    UserName = salesPerson.SalesPersonName,
                    RoleId = 2,
                    RoleName = "SalesPerson",
                    AdminID = 0,
                    SalesPersonID = salesPerson.SalesPersonId,
                    AspNetUserWiseRoleId = userWiseRole.AspNetUserWiseRoleId
                };
            }

            return null;
        }
    }
}
