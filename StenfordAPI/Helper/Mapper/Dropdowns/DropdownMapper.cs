using AutoMapper;
using static StenfordAPI.Models.DropdownResponseModel.DropdownModel;
using Stenford.Domain;
using static Stenford.Domain.DTO;

namespace StenfordAPI.Helper.Mapper.Dropdowns
{
	public static class DropdownMapper
	{
		public static List<StateModel> ToModel(this List<StateDropdownDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<StateDropdownDTO, StateModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<StateDropdownDTO>, List<StateModel>>(entity);
		}

		public static List<CityModel> ToModel(this List<CityDropdownDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<CityDropdownDTO, CityModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<CityDropdownDTO>, List<CityModel>>(entity);
		} 

		public static List<ShowroomDropdownViewModel> ToModel(this List<ShowroomDropdownDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ShowroomDropdownDTO, ShowroomDropdownViewModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<ShowroomDropdownDTO>, List<ShowroomDropdownViewModel>>(entity);
		}
	}
}
