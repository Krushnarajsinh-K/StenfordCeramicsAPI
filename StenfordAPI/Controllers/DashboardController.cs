using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Dashboard;
using StenfordAPI.Helper.Mapper.Dashboard;

namespace StenfordAPI.Controllers
{
	[ApiController]
	//[Route("dashboard")]
	public class DashboardController : BaseController
	{
		private readonly IDashboardRepository _dashboardRepository;

		public DashboardController(IDashboardRepository dashboardRepository)
		{
			_dashboardRepository = dashboardRepository;
		}

		[HttpGet]
		[Route("admin/dashboard")]
		public BaseResponse GetAdminDashboard()
		{
			try
			{
				var result = _dashboardRepository.GetAdminDashboard();
				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.DashboardFetched, result.ToModel()) : ApiException(Enums.StatusCode.ServerError, "GetAdminDashboard", new Exception("Query failed"), ConstantMessage.InternalServerError);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpGet]
		[Route("salesperson/dashboard")]
		public BaseResponse GetSalesPersonDashboard()
		{
			try
			{
				var result = _dashboardRepository.GetSalesPersonDashboard();
				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.DashboardFetched, result.ToModel()) : ApiException(Enums.StatusCode.ServerError, "GetSalesPersonDashboard", new Exception("Query failed"), ConstantMessage.InternalServerError);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}
	}
}
