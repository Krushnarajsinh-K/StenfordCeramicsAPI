using Microsoft.AspNetCore.Mvc;
using QCLorence.API.Helper.StringUtility;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Account;
using Stenford.Service.JwtToken;
using StenfordAPI.Authmanager;
using StenfordAPI.Helper.Mapper.Account;
using StenfordAPI.Models;
using static Stenford.Common.Constants.Enums;

namespace StenfordAPI.Controllers
{
	[ApiController]
	[Route("auth")]
	public class AccountController : BaseController
	{

		private readonly IAccountRepository _accountRepository;
		private readonly IJwtTokenRepository _jwtTokenRepository;

		public AccountController(IAccountRepository accountRepository, IJwtTokenRepository jwtTokenRepository)
		{
			_accountRepository = accountRepository;
			_jwtTokenRepository = jwtTokenRepository;
		}

		[HttpPost]
		[Route("login")]
		public BaseResponse Login([FromBody] LoginModel model)
		{
			try
			{
				var encodedPassword = StringUtility.EncryptString(model.Password);

				var userRecord = _accountRepository.AreUserCredentialsValid(model.UserName, encodedPassword);
				if (userRecord == null)
				{
					return ApiMessage(Enums.StatusCode.Unauthorized, ConstantMessage.InvalidCredentials);
				}

				var userDetail = _accountRepository.GetAspNetUserDetail(userRecord.AspNetUserId);
				if (userDetail == null)
				{
					return ApiMessage(Enums.StatusCode.Unauthorized, ConstantMessage.InvalidCredentials);
				}

				var token = _jwtTokenRepository.GenerateJWTAuthetication(userDetail);
				userDetail.Token = token;

				return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.LoginSuccessful, userDetail.ToModel());
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

        [HttpGet]
        [Route("/passwordDecrypt")]
        public IActionResult GetPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return NotFound("Password is required");
                }

                var decryptedPass = StringUtility.DecryptString(password);
                return Ok(decryptedPass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("/passwordEncrypt")]
        public IActionResult GetEncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return NotFound("Password is required");
                }

                var decryptedPass = StringUtility.EncryptString(password);
                return Ok(decryptedPass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("test-token-claims")]
        public BaseResponse TestTokenClaims([FromQuery] string token)
        {
            try
            {
                var result = new
                {
                    //AspNetUserId = CV.AspNetUserId(token),
                    //AdminId = CV.AdminId(token),
                    //SalesPersonId = CV.SalesPersonId(token),
                    //AssociateId = CV.AssociateId(token),
                    //AspNetUserWiseRoleId = CV.AspNetUserWiseRoleId(token),
                    //RoleName = CV.RoleName(token),
                    //UserName = CV.UserName(token),
                    //ClaimTypesEmail = CV.ClaimTypesEmail(token),
                    //ClaimTypesNameIdentifier = CV.ClaimTypesNameIdentifier(token)

                    AspNetUserId = CV.AspNetUserId(token),
                    AdminId = int.Parse(CV.AdminId(token)),
                    SalesPersonId = int.Parse(CV.SalesPersonId(token)),
                    AssociateId = int.Parse(CV.AssociateId(token)),
                    AspNetUserWiseRoleId = int.Parse(CV.AspNetUserWiseRoleId(token)),
                    RoleName = CV.RoleName(token),
                    UserName = CV.UserName(token),
                    ClaimTypesEmail = CV.ClaimTypesEmail(token),
                    ClaimTypesNameIdentifier = CV.ClaimTypesNameIdentifier(token)
                };

                return ApiSuccess(Enums.StatusCode.Ok, "Token claims extracted", result);
            }
            catch (Exception ex)
            {
                return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
            }
        }

        [AuthManager(UserType.Admin, UserType.SalesPerson)]
        [HttpGet]
        [Route("getprofile")]
        public BaseResponse GetProfile()
        {
            string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            try
            {
                var aspNetUserId = Guid.Parse(CV.AspNetUserId(token));
                var result = _accountRepository.GetProfile(aspNetUserId);
                if (result == null)
                {
                    return ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.SalesPersonNotFound);
                }

                var message = result.AdminID.HasValue ? ConstantMessage.AdminProfileFetched : ConstantMessage.SalesPersonFetched;
                return ApiSuccess(Enums.StatusCode.Ok, message, result.ToModel());
            }
            catch (Exception ex)
            {
                return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
            }
        }
    }
}
