namespace StenfordAPI.Models
{
	public class ShowroomModel
	{
		public int? ShowroomId { get; set; }
		public string ShowroomName { get; set; } = null!;
		public string? GoogleLink { get; set; }
		public string DealerName { get; set; } = null!;
		public string ContactPersonName { get; set; } = null!;
		public string PrimaryContact { get; set; } = null!;
		public string? SecondaryContact { get; set; }
		public string Address { get; set; } = null!;
		public int StateId { get; set; }
		public int CityId { get; set; }
		// list/detail-only fields - ignored on add/edit requests
		public string? City { get; set; }
		public string? State { get; set; }
		public string? AddedAgo { get; set; }
		public int? TotalRecords { get; set; }
	}
}
