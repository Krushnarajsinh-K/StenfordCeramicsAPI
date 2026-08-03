using AutoMapper;
using static Stenford.Domain.DTO;
using StenfordAPI.Models.Admin;

namespace StenfordAPI.Helper.Mapper.Visit
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

        public static VisitMapModel ToModel(this VisitMapDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VisitMapDTO, VisitMapModel>();
                cfg.CreateMap<VisitMapPointDTO, VisitMapPointModel>();
                cfg.CreateMap<SalesPersonVisitCountDTO, SalesPersonVisitCountModel>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<VisitMapDTO, VisitMapModel>(entity);
        }

		public static VisitDTO ToModel(this VisitModel entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<VisitModel, VisitDTO>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<VisitModel, VisitDTO>(entity);
		}
	}
}
