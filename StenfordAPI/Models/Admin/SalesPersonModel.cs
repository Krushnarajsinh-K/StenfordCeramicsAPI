namespace StenfordAPI.Models.Admin
{
	public class SalesPersonModel
	{
		public int? SalesPersonId { get; set; }
		public string SalesPersonName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string ContactPerson { get; set; } = null!;
		public string PrimaryContact { get; set; } = null!;
		public string? SecondaryContact { get; set; }
		public string Address { get; set; } = null!;
		public int StateId { get; set; }
		public int CityId { get; set; }

		// list/detail-only fields - ignored on add/edit requests
		public string? City { get; set; }
		public string? State { get; set; }
		public int? VisitCount { get; set; }
		public bool? IsActive { get; set; }
		public int? TotalRecords { get; set; }
	}	
}
