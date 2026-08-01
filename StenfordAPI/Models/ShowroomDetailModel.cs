namespace StenfordAPI.Models
{
	public class ShowroomDetailModel
	{
		public int ShowroomId { get; set; }
		public string ShowroomName { get; set; } = null!;
		public string DealerName { get; set; } = null!;
		public string PrimaryContact { get; set; } = null!;
		public string Address { get; set; } = null!;
		public string City { get; set; } = null!;
		public string State { get; set; } = null!;
		public string LastVisit { get; set; } = null!;
		public List<ShowroomVisitTimelineModel> VisitTimeline { get; set; } = new();
	}

	public class ShowroomVisitTimelineModel
	{
		public string SalesPersonName { get; set; } = null!;
		public DateTime VisitDate { get; set; }
		public string DiscussionNotes { get; set; } = null!;
		public List<string> Products { get; set; } = new();
	}
}
