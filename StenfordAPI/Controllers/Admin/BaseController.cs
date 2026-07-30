using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stenford.Common.Constants;
//using Stenford.Common.Email;
using Stenford.Common.Constants;
using Stenford.Common.Email;
namespace Stenford.Controllers.Admin
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected string GetApplicationType()
		{
			Microsoft.Extensions.Primitives.StringValues _ApplicationType;
			HttpContext.Request.Headers.TryGetValue("ApplicationType", out _ApplicationType);
			return _ApplicationType.FirstOrDefault();
		}
		protected List<string> GetModelStateErrors(ModelStateDictionary modelState)
		{
			List<string> modelErrors = new List<string>();
			modelState.Values.AsEnumerable().ToList().ForEach(d =>
			{
				if (d.Errors.Count > 0)
				{
					modelErrors.Add(d.Errors[0].ErrorMessage);
				}
			});
			return modelErrors;
		}

		// common api return function for all kind of response with success in project
		protected BaseResponse ApiSuccess(Enums.StatusCode statusCode, string message, object? data = null, int? totalCount = 0)
		{
			Response.StatusCode = (int)statusCode;
			var response = new BaseResponse()
			{
				IsSuccessfull = true,
				StatusCode = statusCode,
				StatusMessage = Enums.GetStatusCodeString(statusCode),
				Message = message,
				Data = data
			};
			if (totalCount != 0)
			{
				response.TotalCount = totalCount;
			}
			return response;
		}

		// common api return function for all kind of response with message with/without data in project
		protected BaseResponse ApiMessage(Enums.StatusCode statusCode, string message, object? data = null, bool isSuccessfull = false)
		{
			Response.StatusCode = (int)statusCode;
			var response = new BaseResponse()
			{
				IsSuccessfull = isSuccessfull,
				StatusCode = statusCode,
				StatusMessage = Enums.GetStatusCodeString(statusCode),
				Message = message,
				Data = data
			};

			return response;
		}

		protected BaseResponse ApiException(Enums.StatusCode statusCode, string exceptionIn, Exception ex, string? message = null)
		{
			Response.StatusCode = (int)statusCode;
			string emailBody = $@"
							<h3>Exception Details</h3>
							<p><strong>Username:</strong>N/A</p> 
							<p><strong>URL:</strong> {Request.Host}</p>
							<p><strong>Exception Path:</strong> {ex.HelpLink}</p>
							<p><strong>Endpoint:</strong> {ex.Message}</p>
							<p><strong>Route Values:</strong> {ex.Source}</p>
							<p><strong>Inner Exception:</strong> {ex.InnerException?.Message}</p>
							<p><strong>Exception Message:</strong> {ex.Message}</p>
							<p><strong>Error:</strong> <pre>{ex.ToString()}</pre>
							<p><strong>Stack Trace:</strong> <pre>
						{ex.StackTrace}</pre>";

			EmailHelper.SendMail("chudasamakrushnarajsinh05@gmail.com", "Stenford News Api Exception Mail " + DateTime.Now, emailBody, ", rahul.shah@uniqueconsumerservices.com");

			var response = new BaseResponse()
			{
				IsSuccessfull = false,
				StatusCode = statusCode,
				StatusMessage = Enums.GetStatusCodeString(statusCode),
				Message = String.IsNullOrEmpty(message) ? "Something goes wrong, please try again later." : message
			};

			return response;
		}
		public static string GetMailTemplate(string path, string templateFileName)
		{
			// Initialize an empty string to store the mail template content
			string strMailTemplet = string.Empty;

			// Read the contents of the mail template file
			using (StreamReader sr = new StreamReader(path + "/" + templateFileName))
			{
				// Read each line of the file until the end
				string sLine;
				while ((sLine = sr.ReadLine()) != null)
				{
					// Append the line to the mail template content
					strMailTemplet += sLine;
				}
			}
			// Return the mail template content
			return strMailTemplet;
		}

		protected UserBaseResponse UserApiSuccess(Enums.StatusCode statusCode, string message, object? data = null, bool? isUserRegistered = false)
		{
			Response.StatusCode = (int)statusCode;
			var response = new UserBaseResponse()
			{
				IsSuccessfull = true,
				StatusCode = statusCode,
				StatusMessage = Enums.GetStatusCodeString(statusCode),
				Message = message,
				Data = data,
				IsUserRegistered = isUserRegistered
			};
			return response;
		}
		protected UserBaseResponse UserApiMessage(Enums.StatusCode statusCode, string message, object? data = null, bool isSuccessfull = false, bool? isUserRegistered = false)
		{
			Response.StatusCode = (int)statusCode;
			var response = new UserBaseResponse()
			{
				IsSuccessfull = isSuccessfull,
				StatusCode = statusCode,
				StatusMessage = Enums.GetStatusCodeString(statusCode),
				Message = message,
				Data = data,
				IsUserRegistered = isUserRegistered
			};

			return response;
		}

		protected UserBaseResponse UserApiException(Enums.StatusCode statusCode, string exceptionIn, Exception ex, string? message = null, bool? isUserRegistered = false)
		{
			//var _logger = EngineContext.Resolve<ILogger<BaseController>>();
			//_logger.LogError($"Exception in {exceptionIn} | {ex.Message}", ex);

			//if (ConfigItems.IsDevelopmentMode)
			//    message += "::" + ex.Message;
			Response.StatusCode = (int)statusCode;
			string emailBody = $@"
							<h3>Exception Details</h3>
							<p><strong>Username:</strong> xde</p> 
							<p><strong>URL:</strong> {Request.Host}</p>
							<p><strong>Exception Path:</strong> {ex.HelpLink}</p>
							<p><strong>Endpoint:</strong> {ex.Message}</p>
							<p><strong>Route Values:</strong> {ex.Source}</p>
							<p><strong>Inner Exception:</strong> {ex.InnerException?.Message}</p>
							<p><strong>Exception Message:</strong> {ex.Message}</p>
							<p><strong>Error:</strong> <pre>{ex.ToString()}</pre>
							<p><strong>Stack Trace:</strong> <pre>
						{ex.StackTrace}</pre>";

			EmailHelper.SendMail("knowidont499@gmail.com", "RB News Api Exception Mail " + DateTime.Now, emailBody, "test.uniqueitsolution@gmail.com, rahul.shah@uniqueconsumerservices.com");

			var response = new UserBaseResponse()
			{
				IsSuccessfull = false,
				StatusCode = statusCode,
				StatusMessage = Enums.GetStatusCodeString(statusCode),
				Message = String.IsNullOrEmpty(message) ? "Something goes wrong, please try again later." : message,
				IsUserRegistered = false
			};

			return response;
		}
	}
}