using AutoMapper;
using static Stenford.Domain.DTO;
using StenfordAPI.Models.Admin;

namespace StenfordAPI.Helper.Visit
{
	public static class VisitMapper
	{
		public static List<VisitModel> ToModel(this List<VisitDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<VisitDTO, VisitModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<VisitDTO>, List<VisitModel>>(entity);
		}

		public static VisitModel ToModel(this VisitDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<VisitDTO, VisitModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<VisitDTO, VisitModel>(entity);
		}
	}
}
