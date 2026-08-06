using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Visit;
using StenfordAPI.Helper.Mapper.Visit;
using StenfordAPI.Models.Admin;
using System.Security.AccessControl;

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
		[Route("map/details")]
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

		[HttpGet]
		[Route("map/co-ordinates")]
		public BaseResponse GetVisitMapPoints([FromQuery] int? stateId, [FromQuery] int? cityId, [FromQuery] int? salesPersonId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
		{
			try
			{
				var result = _visitRepository.GetVisitMapPoints(stateId, cityId, salesPersonId, fromDate, toDate);
				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitMapFetched, result.ToModel()) : ApiException(Enums.StatusCode.ServerError, "GetVisitMapPoints", new Exception("Query failed"), ConstantMessage.InternalServerError);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		//[HttpPost]
		//[Route("add-record")]
		//public BaseResponse AddVisitRecord([FromForm] VisitModel model)
		//{
		//	try
		//	{
		//		var result = _visitRepository.AddVisit(model.ToModel(), Guid.Parse("22222222-2222-2222-2222-222222222222")).ToModel();
		//		return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitAdded, result);
		//	}	
		//	catch (Exception ex)
		//	{
		//		return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
		//	}
		//}

		[HttpPost]
		[Route("add-record")]
		public BaseResponse AddVisitRecord([FromForm] VisitAddModel model)
		{
			try
			{
				//var dto = model.ToModel();
				var result = _visitRepository.AddVisit(model.ToModel(), Guid.Parse("22222222-2222-2222-2222-222222222222"));
				return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitAdded, result.ToModel());
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpGet]
		[Route("history")]
		public BaseResponse GetVisitHistoryList(int? pageIndex, int? pageSize, DateTime? fromDate, DateTime? toDate)
		{
			try
			{
				if (!pageIndex.HasValue || !pageSize.HasValue)
				{
					return ApiMessage(Enums.StatusCode.BadRequest, ConstantMessage.PageNumberAndPageSizeRequired);
				}
				if (pageIndex <= 0 || pageSize <= 0)
				{
					return ApiMessage(Enums.StatusCode.BadRequest, ConstantMessage.InvalidPageNumberOrPageSize);
				}

				var result = _visitRepository.GetVisitHistoryList(pageIndex.Value, pageSize.Value, fromDate, toDate);
				return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitHistoryFetched, result.ToModel());
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}
	}
}
