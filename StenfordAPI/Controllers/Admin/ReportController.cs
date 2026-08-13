using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Report;
using StenfordAPI.Authmanager;
using StenfordAPI.Helper.Mapper.Report;
using static Stenford.Common.Constants.Enums;

namespace StenfordAPI.Controllers.Admin
{
    [ApiController]
    [AuthManager(UserType.Admin)]
    [Route("report")]

    public class ReportController : BaseController
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        [Route("visitswisereport")]
        public BaseResponse GetReport([FromQuery] int? showroomId, [FromQuery] int? cityId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            try
            {
                if (!pageNumber.HasValue || !pageSize.HasValue)
                {
                    return ApiMessage(Enums.StatusCode.BadRequest, ConstantMessage.PageNumberAndPageSizeRequired);
                }

                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return ApiMessage(Enums.StatusCode.BadRequest, ConstantMessage.InvalidPageNumberOrPageSize);
                }

                var result = _reportRepository.GetReport(showroomId, cityId, fromDate, toDate, pageNumber.Value, pageSize.Value);
                return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.ReportFetched, result.ToModel()) : ApiException(Enums.StatusCode.ServerError, "GetReport", new Exception("Query failed"), ConstantMessage.InternalServerError);
            }
            catch (Exception ex)
            {
                return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
            }
        }
    }
}
