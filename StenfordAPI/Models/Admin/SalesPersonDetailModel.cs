namespace StenfordAPI.Models.Admin
{
	public class SalesPersonDetailModel
	{
		public int SalesPersonId { get; set; }
		public string SalesPersonName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PrimaryContact { get; set; } = null!;
        public string? SecondaryContact { get; set; }
        public string State { get; set; } = null!;
		public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
		public int TotalVisits { get; set; }
		public int ShowroomCount { get; set; }
		public int ThisMonthVisits { get; set; }
		public List<VisitTimelineModel> VisitTimeline { get; set; } = new();
	}

	public class VisitTimelineModel
	{
        public int VisitId { get; set; }

        public string SalesPersonName { get; set; } = null!;
		public DateTime VisitDate { get; set; }
		public string ShowroomName { get; set; } = null!;
		public string Location { get; set; } = null!;
		public string DiscussionNotes { get; set; } = null!;
		public List<string> Products { get; set; } = new();
	}
}