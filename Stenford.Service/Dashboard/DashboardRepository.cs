using Stenford.Domain;
using Stenford.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Dashboard
{
	public class DashboardRepository : IDashboardRepository
	{
		private readonly ApplicationDbContext _context;

		public DashboardRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public DashboardDTO GetAdminDashboard()
		{
			try
			{
				var admin = _context.SecAdmins.FirstOrDefault(a => a.IsDeleted == false);

				var dashboard = new DashboardDTO();
				dashboard.AdminName = admin.UserName;
				dashboard.AdminCreatedAt = admin.CreatedAt;

				dashboard.TotalSalespersons = _context.SecSalesPeople.Count(sp => sp.IsDeleted == false);
				dashboard.TotalShowrooms = _context.ShoShowrooms.Count(s => s.IsDeleted == false);
				dashboard.TotalVisits = _context.VisVisits.Count(v => v.IsDeleted == false);
				dashboard.TodayVisits = _context.VisVisits.Count(v => v.IsDeleted == false && v.VisitDate.Date == DateTime.Now.Date);

				dashboard.MonthlyVisitsCount = _context.VisVisits.Count(v => v.IsDeleted == false && v.VisitDate.Month == DateTime.Now.Month && v.VisitDate.Year == DateTime.Now.Year);
				dashboard.MonthLabel = DateTime.Now.ToString("MMM yyyy");

				dashboard.RecentVisits = (from v in _context.VisVisits
										  join showroom in _context.ShoShowrooms on v.ShowroomId equals showroom.ShowroomId
										  join sp in _context.SecSalesPeople on v.SalesPersonId equals sp.SalesPersonId
										  join city in _context.LocCities on showroom.CityId equals city.CityId into cityJoin
										  from city in cityJoin.DefaultIfEmpty()
										  where v.IsDeleted != true 
										  orderby v.CreatedAt ascending
										  select new RecentVisitDTO
										  {
											  VisitId = v.VisitId,
											  ShowroomName = showroom.ShowroomName,
											  SalesPersonName = sp.SalesPersonName,
											  VisitDate = v.VisitDate,
											  Location = city.CityName
										  }).Take(3).ToList();

				return dashboard;
			}
			catch
			{
				return null;
			}
		}

		public SalesPersonDashboardDTO GetSalesPersonDashboard()
		{
			try
			{
				int salesPersonId = 13; // static placeholder until JWT

				var salesPerson = _context.SecSalesPeople.FirstOrDefault(sp => sp.SalesPersonId == salesPersonId && sp.IsDeleted == false);

				if (salesPerson == null)
				{
					return null;
				}

				var dashboard = new DTO.SalesPersonDashboardDTO();
				dashboard.SalesPersonName = salesPerson.SalesPersonName;
				dashboard.SalesPersonCreatedAt = salesPerson.CreatedAt;

				dashboard.TotalVisits = _context.VisVisits.Count(v => v.SalesPersonId == salesPersonId && v.IsDeleted == false);
				dashboard.TodayVisits = _context.VisVisits.Count(v => v.SalesPersonId == salesPersonId && v.IsDeleted == false && v.VisitDate.Date == DateTime.Now.Date);
				dashboard.MonthlyVisits = _context.VisVisits.Count(v => v.SalesPersonId == salesPersonId && v.IsDeleted == false && v.VisitDate.Month == DateTime.Now.Month && v.VisitDate.Year == DateTime.Now.Year);

				dashboard.RecentVisits = (from v in _context.VisVisits
										  join showroom in _context.ShoShowrooms on v.ShowroomId equals showroom.ShowroomId
										  join city in _context.LocCities on showroom.CityId equals city.CityId into cityJoin
										  from city in cityJoin.DefaultIfEmpty()
										  join creator in _context.SecSalesPeople on showroom.CreatedBy equals creator.AspNetUserId into creatorJoin
										  from creator in creatorJoin.DefaultIfEmpty()
										  where v.SalesPersonId == salesPersonId && v.IsDeleted != true 
										  orderby v.VisitDate descending
										  select new DTO.SalesPersonRecentVisitDTO
										  {
											  VisitId = v.VisitId,
											  ShowroomId = v.ShowroomId,
											  ShowroomName = showroom.ShowroomName,
											  ShowroomCreatedSalesPersonName = creator.SalesPersonName,
											  CityName = city.CityName,
											  VisitDate = v.VisitDate
										  }).Take(5).ToList();

				return dashboard;
			}
			catch
			{
				return null;
			}
		}
	}
}
