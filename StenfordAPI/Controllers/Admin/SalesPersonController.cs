using Microsoft.AspNetCore.Mvc;
using QCLorence.API.Helper.StringUtility;
using Stenford.Common.Constants;
using Stenford.Controllers.Admin;
using Stenford.Service.SalesPerson;
using StenfordAPI.Helper.Mapper.SalesPerson;
using StenfordAPI.Models.Admin;

namespace StenfordAPI.Controllers.Admin
{
	[ApiController]
	[Route("admin/salespersons")]

	public class SalesPersonController : BaseController
	{
		private readonly ISalesPersonRepository _salesPersonRepository;

		public SalesPersonController(ISalesPersonRepository salesPersonRepository)
		{
			_salesPersonRepository = salesPersonRepository;
		}

		[HttpGet]
		[Route("list")]
		public BaseResponse GetSalesPersonList([FromQuery] string? searchText, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] int? stateId, [FromQuery] int? cityId)
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
				var salesPersonList = _salesPersonRepository.GetSalesPersonDataList(pageNumber.Value, pageSize.Value, searchText, stateId, cityId).ToModel();
				return (salesPersonList.Any()) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.SalesPersonListFetched, salesPersonList, salesPersonList.First()?.TotalRecords) : ApiSuccess(Enums.StatusCode.Ok, "SalesPerson List Empty!");
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpPost]
		[Route("add")]
		public BaseResponse AddSalesPerson([FromBody] SalesPersonModel model)
		{
			try
			{
				if (_salesPersonRepository.IsSalesPersonNameExists(model.SalesPersonName))
				{
					return ApiMessage(Enums.StatusCode.BadRequest, ConstantMessage.SalesPersonNameAlreadyExists);
				}
				var dto = model.ToModel();
				dto.Password = StringUtility.EncryptString(model.Password);

				var result = _salesPersonRepository.AddSalesPerson(dto, "11111111-1111-1111-1111-111111111111");
				return ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.SalesPersonAdded, result.ToModel());
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpPost]
		[Route("edit")]
		public BaseResponse EditSalesPerson([FromBody] SalesPersonModel model)
		{
			try
			{
				var dto = model.ToModel();
				var result = _salesPersonRepository.EditSalesPerson(dto, "11111111-1111-1111-1111-111111111111");

				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.SalesPersonUpdated, result.ToModel()) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.SalesPersonNotFound);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpDelete]
		[Route("delete")]
		public BaseResponse DeleteSalesPerson([FromQuery] int salesPersonId)
		{
			try
			{
				var result = _salesPersonRepository.DeleteSalesPerson(salesPersonId, "11111111-1111-1111-1111-111111111111");
				return result ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.SalesPersonDeleted) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.SalesPersonNotFound);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}

		[HttpGet]
		[Route("details")]
		public BaseResponse GetSalesPersonById([FromQuery] int salesPersonId)
		{
			try
			{
				var result = _salesPersonRepository.GetSalesPersonById(salesPersonId);
				return (result != null) ? ApiSuccess(Enums.StatusCode.Ok, ConstantMessage.SalesPersonFetched, result.ToModel()) : ApiMessage(Enums.StatusCode.NotFound, ConstantMessage.SalesPersonNotFound);
			}
			catch (Exception ex)
			{
				return ApiException(Enums.StatusCode.ServerError, ex.Message, ex, ConstantMessage.InternalServerError);
			}
		}
	}
}
