using AutoMapper;
using Stenford.Domain;
using StenfordAPI.Models.Admin;
using static Stenford.Domain.DTO;

namespace StenfordAPI.Helper.Mapper.SalesPerson
{
	public static class SalesPersonMapper
	{
		public static List<SalesPersonModel> ToModel(this List<SalesPersonDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<SalesPersonDTO, SalesPersonModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<SalesPersonDTO>, List<SalesPersonModel>>(entity);
		}

		public static SalesPersonDTO ToModel(this SalesPersonModel entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<SalesPersonModel, SalesPersonDTO>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<SalesPersonModel, SalesPersonDTO>(entity);
		}

		public static SalesPersonModel ToModel(this SalesPersonDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<SalesPersonDTO, SalesPersonModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<SalesPersonDTO, SalesPersonModel>(entity);
		}

		public static SalesPersonDetailModel ToModel(this SalesPersonDetailDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<SalesPersonDetailDTO, SalesPersonDetailModel>();
				cfg.CreateMap<VisitTimelineDTO, VisitTimelineModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<SalesPersonDetailDTO, SalesPersonDetailModel>(entity);
		}
	}
}
