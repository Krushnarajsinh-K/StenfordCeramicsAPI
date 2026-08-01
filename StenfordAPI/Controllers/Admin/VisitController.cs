using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Visit;
using StenfordAPI.Helper.Visit;

namespace StenfordAPI.Controllers.Admin
{
	[ApiController]
	[Route("visits")]
	public class VisitController : BaseController
	{
		private readonly IVisitRepository _visitRepository;

		public VisitController(IVisitRepository visitRepository)
		{
			_visitRepository = visitRepository;
		}

		[HttpGet]
		[Route("list")]
		public BaseResponse GetVisitList([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] int? stateId, [FromQuery] int? cityId, [FromQuery] int? salesPersonId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
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

				var visitList = _visitRepository.GetVisitList(pageNumber.Value, pageSize.Value, stateId, cityId, salesPersonId, fromDate, toDate).ToModel();
				return (visitList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitListFetched, visitList, visitList.First().TotalRecords) : ApiSuccess(Enums.StatusCode.Ok, "Visit List Empty!");
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpGet]
		[Route("details")]
		public BaseResponse GetVisitById([FromQuery]int visitId)
		{
			try
			{
				var result = _visitRepository.GetVisitById(visitId);
				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitFetched, result.ToModel()) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.VisitNotFound);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}
	}
}
