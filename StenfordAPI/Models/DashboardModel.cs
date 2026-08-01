namespace StenfordAPI.Models
{
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
}
