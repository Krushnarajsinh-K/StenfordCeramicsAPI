using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Common.Utility;
using Stenford.Controllers.Admin;
using Stenford.Domain;
using Stenford.Service.Visit;
using StenfordAPI.Authmanager;
using StenfordAPI.Helper.Mapper.Visit;
using StenfordAPI.Models;
using StenfordAPI.Models.Admin;
using System.Security.AccessControl;
using static Stenford.Common.Constants.Enums;

namespace StenfordAPI.Controllers.Admin
{
    [ApiController]
    [AuthManager(UserType.Admin,UserType.SalesPerson)]

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
				return (visitList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitListFetched, visitList, visitList.First().TotalRecords) : ApiSuccess(Enums.StatusCode.Ok, "Visit List Empty!",new List<int>());
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

        //[HttpPost]
        //[Route("add-record")]
        //public BaseResponse AddVisitRecord([FromForm] VisitAddModel model)
        //{
        //          //string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
        //          try
        //	{
        //		//var dto = model.ToModel();
        //		//var result = _visitRepository.AddVisit(model.ToModel(), Guid.Parse(CV.AspNetUserId(token)));
        //		var result = _visitRepository.AddVisit(model.ToModel(), Guid.Parse("11111111-1111-1111-1111-111111111111"));
        //              return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitAdded, result.ToModel());
        //	}
        //	catch (Exception ex)
        //	{
        //		return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
        //	}
        //}



        //[HttpPost]
        //[Route("add-record")]
        //public async Task<BaseResponse> AddVisitRecord([FromForm] VisitAddModel model)
        //{
        //    string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        //    try
        //    {
        //        string ftpFolder = "www";

        //        var dto = model.ToModel();

        //        if (model.VoiceNote != null)
        //            dto.VoiceNotePath = await FTPHelper.UploadFileFTPAsync(model.VoiceNote, ftpFolder);

        //        if (model.VisitingCardFront != null)
        //            dto.VisitingCardFrontPath = await FTPHelper.UploadFileFTPAsync(model.VisitingCardFront, ftpFolder);

        //        if (model.VisitingCardBack != null)
        //            dto.VisitingCardBackPath = await FTPHelper.UploadFileFTPAsync(model.VisitingCardBack, ftpFolder);

        //        if (model.ShowroomImages != null && model.ShowroomImages.Any())
        //        {
        //            foreach (var image in model.ShowroomImages)
        //            {
        //                var uploadedPath = await FTPHelper.UploadFileFTPAsync(image, ftpFolder);
        //                dto.ShowroomImages.Add(uploadedPath);
        //            }
        //        }
        //        var result = _visitRepository.AddVisit(model.ToModel(), Guid.Parse(CV.AspNetUserId(token)));

        //        //var result = _visitRepository.AddVisit(dto, Guid.Parse("22222222-2222-2222-2222-222222222222"));
        //        return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitAdded, result.ToModel());
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
        //    }
        //}

        [HttpPost]
        [Route("add-record")]
        public async Task<BaseResponse> AddVisitRecord([FromForm] VisitAddModel model)
        {
            string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            try
            {
                string ftpFolder = "www";
                var dto = model.ToModel();
                if (model.VoiceNote != null)
                    dto.VoiceNotePath = await FTPHelper.UploadFileFTPAsync(model.VoiceNote, ftpFolder);
                if (model.VisitingCardFront != null)
                    dto.VisitingCardFrontPath = await FTPHelper.UploadFileFTPAsync(model.VisitingCardFront, ftpFolder);
                if (model.VisitingCardBack != null)
                    dto.VisitingCardBackPath = await FTPHelper.UploadFileFTPAsync(model.VisitingCardBack, ftpFolder);
                if (model.ShowroomImages != null && model.ShowroomImages.Any())
                {
                    foreach (var image in model.ShowroomImages)
                    {
                        var uploadedPath = await FTPHelper.UploadFileFTPAsync(image, ftpFolder);
                        dto.ShowroomImages.Add(uploadedPath);
                    }
                }

                var result = _visitRepository.AddVisit(dto, Guid.Parse(CV.AspNetUserId(token)));   // ← FIXED: use "dto", not model.ToModel() again
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
            string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
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

                var aspNetUserId = Guid.Parse(CV.AspNetUserId(token));
                var result = _visitRepository.GetVisitHistoryList(aspNetUserId, pageIndex.Value, pageSize.Value, fromDate, toDate);
                return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.VisitHistoryFetched, result.ToModel()) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.SalesPersonNotFound);
            }
            catch (Exception ex)
            {
                return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
            }
        }
    }
}
