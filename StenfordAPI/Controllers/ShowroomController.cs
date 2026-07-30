using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.SalesPerson;
using Stenford.Service.Showroom;
using StenfordAPI.Helper.Mapper.Showroom;
using StenfordAPI.Models;

namespace StenfordAPI.Controllers
{
	[ApiController]
	[Route("showrooms")]
	public class ShowroomController : BaseController
	{
		private readonly IShowroomRepository _showroomRepository;

		public ShowroomController(IShowroomRepository showroomRepository)
		{
			_showroomRepository = showroomRepository;
		}

		[HttpGet]
		[Route("list")]
		public BaseResponse GetShowroomList([FromQuery] string? searchText, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] int? stateId, [FromQuery] int? cityId)
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

				var showroomList = _showroomRepository.GetShowroomDataList(searchText, pageNumber.Value, pageSize.Value, stateId, cityId).ToModel();
				return (showroomList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.ShowroomListFetched, showroomList, showroomList.First().TotalRecords) : ApiSuccess(Enums.StatusCode.Ok, "Showroom List Empty!");
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpPost]
		[Route("add")]
		public BaseResponse AddShowroom([FromBody] ShowroomModel model)
		{
			try
			{
				var dto = model.ToModel();
				var result = _showroomRepository.AddShowroom(dto, Guid.Parse("11111111-1111-1111-1111-111111111111"));
				return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.ShowroomAdded, result.ToModel());
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpPost]
		[Route("edit")]
		public BaseResponse EditShowroom([FromBody] ShowroomModel model)
		{
			try
			{
				var dto = model.ToModel();
				var result = _showroomRepository.EditShowroom(dto, Guid.Parse("11111111-1111-1111-1111-111111111111"));
				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.ShowroomUpdated, result.ToModel()) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.ShowroomNotFound);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpDelete]
		[Route("delete")]
		public BaseResponse DeleteShowroom([FromQuery]int showroomId)
		{
			try
			{
				var result = _showroomRepository.DeleteShowroom(showroomId, Guid.Parse("11111111-1111-1111-1111-111111111111"));
				return result ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.ShowroomDeleted) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.ShowroomNotFound);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}
	}
}
