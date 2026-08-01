namespace StenfordAPI.Models.Admin
{
	public class VisitModel
	{
		public int VisitId { get; set; }
		public string ShowroomName { get; set; } = null!;
		public string Location { get; set; } = null!;
		public int SalesPersonId { get; set; }
		public string SalesPersonName { get; set; } = null!;
		public DateTime VisitDate { get; set; }
		public string DiscussionNotes { get; set; } = null!;
		public List<string> Products { get; set; } = new();
		public int TotalRecords { get; set; }

		//get by id
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public string? VoiceNoteUrl { get; set; }
		public List<string> VisitingCardImages { get; set; } = new();
		public List<string> ShowroomImages { get; set; } = new();
	}

	public class VisitMapModel
	{
		public List<VisitMapPointModel> Points { get; set; } = new();
		public List<SalesPersonVisitCountModel> SalesPersonCounts { get; set; } = new();
	}

	public class VisitMapPointModel
	{
		public int VisitId { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public int SalesPersonId { get; set; }
		public string SalesPersonName { get; set; } = null!;
	}

	public class SalesPersonVisitCountModel
	{
		public int SalesPersonId { get; set; }
		public string SalesPersonName { get; set; } = null!;
		public int VisitCount { get; set; }
	}
}
