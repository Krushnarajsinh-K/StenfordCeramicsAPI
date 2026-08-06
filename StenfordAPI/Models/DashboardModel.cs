namespace StenfordAPI.Models
{
	//admin
	public class DashboardModel
	{
		public string AdminName { get; set; } = null!;
		public DateTime AdminCreatedAt { get; set; }
		public int TotalSalespersons { get; set; }
		public int TotalShowrooms { get; set; }
		public int TotalVisits { get; set; }
		public int TodayVisits { get; set; }
		public int MonthlyVisitsCount { get; set; }
		public string MonthLabel { get; set; } = null!;
		public List<RecentVisitModel> RecentVisits { get; set; } = new();
	}

	public class RecentVisitModel
	{
		public int VisitId { get; set; }
		public string ShowroomName { get; set; } = null!;
		public string SalesPersonName { get; set; } = null!;
		public DateTime VisitDate { get; set; }
		public string Location { get; set; } = null!;
	}

	//sales

	public class SalesPersonDashboardModel
	{
		public string SalesPersonName { get; set; } = null!;
		public DateTime SalesPersonCreatedAt { get; set; }
		public int TotalVisits { get; set; }
		public int TodayVisits { get; set; }
		public int MonthlyVisits { get; set; }
		public List<SalesPersonRecentVisitModel> RecentVisits { get; set; } = new();
	}

	public class SalesPersonRecentVisitModel
	{
		public int VisitId { get; set; }
		public int ShowroomId { get; set; }
		public string ShowroomName { get; set; } = null!;
		public string ShowroomCreatedBySalesPersonName { get; set; } = null!;
		public string CityName { get; set; } = null!;
		public DateTime VisitDate { get; set; }
	}
}
