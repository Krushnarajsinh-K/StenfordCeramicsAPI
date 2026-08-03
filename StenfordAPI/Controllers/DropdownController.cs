using Microsoft.AspNetCore.Mvc;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.Dropdown;
using StenfordAPI.Helper.Mapper.Dropdowns;

namespace StenfordAPI.Controllers
{
	[ApiController]
	[Route("/dropdowns")]
	public class DropdownController : BaseController
	{
		private readonly IDropdownRepository _dropdownRepository;

		public DropdownController(IDropdownRepository dropdownRepository)
		{
			_dropdownRepository = dropdownRepository;
		}

		[HttpGet]
		[Route("states/list")]
		public BaseResponse GetStateList()
		{
			try
			{
				var stateList = _dropdownRepository.GetStateList().ToModel();
				return (stateList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.StateListFetched, stateList, stateList.Count) : ApiSuccess(Enums.StatusCode.Ok, "State List Empty!");
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpGet]
		[Route("cities")]
		public BaseResponse GetCityListByStateId([FromQuery] int? stateId)
		{
			try
			{
				if (!stateId.HasValue)
				{
					return ApiMessage(Enums.StatusCode.BadRequest, ConstantMessage.StateIdRequired);
				}
				var cityList = _dropdownRepository.GetCityListByStateId(stateId.Value).ToModel();
				return (cityList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.CityListFetched, cityList, cityList.Count) : ApiSuccess(Enums.StatusCode.Ok, "City List Empty!");
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpGet]
		[Route("showrooms")]
		public BaseResponse GetShowroomDropdownList()
		{
			try
			{
				var showroomList = _dropdownRepository.GetShowroomDropdownList().ToModel();
				return (showroomList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.ShowroomDropdownFetched, showroomList, showroomList.Count) : ApiSuccess(Enums.StatusCode.Ok, "Showroom List Empty!");
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}
	}
}
