namespace StenfordAPI.Models.Admin
{
	public class VisitModel
	{
		public int VisitId { get; set; }
		public int ShowroomId { get; set; }
		public string? ShowroomName { get; set; }
		public string? Location { get; set; }
		public int SalesPersonId { get; set; }
		public string? SalesPersonName { get; set; }
		public DateTime VisitDate { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string DiscussionNotes { get; set; } = null!;
		public List<string> Products { get; set; } = new();
		public int TotalRecords { get; set; }
		public string? VoiceNotePath { get; set; }
		public string? VisitingCardFrontPath { get; set; }
		public string? VisitingCardBackPath { get; set; }
		public List<string> ShowroomImages { get; set; } = new();
	}

	public class VisitAddModel
	{
		public int VisitId { get; set; }
		public int ShowroomId { get; set; }
		public DateTime VisitDate { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string DiscussionNotes { get; set; } = null!;
		public List<string> Products { get; set; } = new();

		//attachement
		public IFormFile? VoiceNote { get; set; }
		public IFormFile? VisitingCardFront { get; set; }
		public IFormFile? VisitingCardBack { get; set; }
		public List<IFormFile>? ShowroomImages { get; set; }
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