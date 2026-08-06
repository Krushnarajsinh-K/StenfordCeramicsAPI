using Microsoft.AspNetCore.Mvc;
using QCLorence.API.Helper.StringUtility;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Account;
using Stenford.Service.JwtToken;
using StenfordAPI.Helper.Mapper.Account;
using StenfordAPI.Models;

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
    }
}
