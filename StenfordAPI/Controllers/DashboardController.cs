using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Dashboard;
using StenfordAPI.Authmanager;
using StenfordAPI.Helper.Mapper.Dashboard;
using static Stenford.Common.Constants.Enums;
using StenfordAPI.Models;

namespace StenfordAPI.Controllers
{
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [AuthManager(UserType.Admin)]
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

        [AuthManager(UserType.SalesPerson)]
        [HttpGet]
        [Route("salesperson/dashboard")]
        public BaseResponse GetSalesPersonDashboard()
        {
            string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            try
            {
                var aspNetUserId = Guid.Parse(CV.AspNetUserId(token));
                var result = _dashboardRepository.GetSalesPersonDashboard(aspNetUserId);
                return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.DashboardFetched, result.ToModel()) : ApiException(Enums.StatusCode.ServerError, "GetSalesPersonDashboard", new Exception("Query failed"), ConstantMessage.InternalServerError);
            }
            catch (Exception ex)
            {
                return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
            }
        }
    }
}
