using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stenford.Domain
{
	public class DTO
	{
		public class SalesPersonDTO
		{
			public int SalesPersonId { get; set; }
			public Guid AspNetUserId { get; set; }
			public string SalesPersonName { get; set; } = null!;
			public string Email { get; set; } = null!;
			public string Password { get; set; } = null!;

			public string ContactPerson { get; set; } = null!;
			public string PrimaryContact { get; set; } = null!;
			public string? SecondaryContact { get; set; }
			public string Address { get; set; } = null!;
			public int CountryId { get; set; }
			public int StateId { get; set; }
			public int CityId { get; set; }
			public string? City { get; set; }
			public string? State { get; set; }
			public bool IsActive { get; set; }
			public int VisitCount { get; set; }
			public int TotalRecords { get; set; }
		}

		public class StateDropdownDTO
		{
			public int Id { get; set; }
			public string? Data { get; set; }
		}

		public class CityDropdownDTO
		{
			public int Id { get; set; }
			public string? Data { get; set; }
		}

		public class SalesPersonDetailDTO
		{
			public int SalesPersonId { get; set; }
			public string SalesPersonName { get; set; } = null!;
			public string PrimaryContact { get; set; } = null!;
			public string State { get; set; } = null!;
			public string City { get; set; } = null!;
			public bool IsActive { get; set; }
			public int TotalVisits { get; set; }
			public int ShowroomCount { get; set; }
			public int ThisMonthVisits { get; set; }
			public List<VisitTimelineDTO> VisitTimeline { get; set; } = new();
		}
		public class VisitTimelineDTO
		{
			public string SalesPersonName { get; set; } = null!;
			public DateTime VisitDate { get; set; }
			public string ShowroomName { get; set; } = null!;
			public string Location { get; set; } = null!;
			public string DiscussionNotes { get; set; } = null!;
			public List<string> Products { get; set; } = new();
		}

		public class ShowroomDTO
		{
			public int ShowroomId { get; set; }
			public string ShowroomName { get; set; } = null!;
			public string? GoogleLink { get; set; }
			public string DealerName { get; set; } = null!;
			public string ContactPersonName { get; set; } = null!;
			public string PrimaryContact { get; set; } = null!;
			public string? SecondaryContact { get; set; }
			public string Address { get; set; } = null!;
			public int CountryId { get; set; }
			public int StateId { get; set; }
			public int CityId { get; set; }
			public string? City { get; set; }
			public string? State { get; set; }
			public string? AddedAgo { get; set; }
			public int TotalRecords { get; set; }
		}
	}
}
