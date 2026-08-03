using AutoMapper;
using static Stenford.Domain.DTO;
using StenfordAPI.Models;
using Stenford.Domain;

namespace StenfordAPI.Helper.Mapper.Showroom
{
	public static class ShowroomMapper
	{
		public static List<ShowroomModel> ToModel(this List<ShowroomDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ShowroomDTO, ShowroomModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<ShowroomDTO>, List<ShowroomModel>>(entity);
		}

		public static ShowroomDTO ToModel(this ShowroomModel entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ShowroomModel, ShowroomDTO>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<ShowroomModel, ShowroomDTO>(entity);
		}

		public static ShowroomModel ToModel(this ShowroomDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ShowroomDTO, ShowroomModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<ShowroomDTO, ShowroomModel>(entity);
		}

		public static ShowroomDetailModel ToModel(this DTO.ShowroomDetailDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ShowroomDetailDTO, ShowroomDetailModel>();
				cfg.CreateMap<ShowroomVisitTimelineDTO, ShowroomVisitTimelineModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<ShowroomDetailDTO, ShowroomDetailModel>(entity);
		}
	}
}
