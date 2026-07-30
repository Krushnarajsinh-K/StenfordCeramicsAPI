using AutoMapper;
using static StenfordAPI.Models.DropdownResponseModel.DropdownModel;
using Stenford.Domain;

namespace StenfordAPI.Helper.Mapper.Dropdowns
{
	public static class DropdownMapper
	{
		public static List<StateModel> ToModel(this List<DTO.StateDropdownDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DTO.StateDropdownDTO, StateModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<DTO.StateDropdownDTO>, List<StateModel>>(entity);
		}

		public static List<CityModel> ToModel(this List<DTO.CityDropdownDTO> entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DTO.CityDropdownDTO, CityModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<List<DTO.CityDropdownDTO>, List<CityModel>>(entity);
		}
	}
}
