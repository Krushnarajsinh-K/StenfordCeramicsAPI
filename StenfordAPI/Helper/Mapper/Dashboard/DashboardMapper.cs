using AutoMapper;
using StenfordAPI.Models;
using static Stenford.Domain.DTO;

namespace StenfordAPI.Helper.Mapper.Dashboard
{
	public static class DashboardMapper
	{
		public static DashboardModel ToModel(this DashboardDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DashboardDTO, DashboardModel>();
				cfg.CreateMap<RecentVisitDTO, RecentVisitModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<DashboardDTO, DashboardModel>(entity);
		}

		public static SalesPersonDashboardModel ToModel(this SalesPersonDashboardDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<SalesPersonDashboardDTO, SalesPersonDashboardModel>();
				cfg.CreateMap<SalesPersonRecentVisitDTO, SalesPersonRecentVisitModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<SalesPersonDashboardDTO, SalesPersonDashboardModel>(entity);
		}
	}
}
