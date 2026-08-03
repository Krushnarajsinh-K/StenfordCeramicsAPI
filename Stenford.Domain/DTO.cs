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
				public string? Recentvisit { get; set; }
				public int TotalRecords { get; set; }
			}

			public class ShowroomDetailDTO
			{
				public int ShowroomId { get; set; }
				public string ShowroomName { get; set; } = null!;
				public string DealerName { get; set; } = null!;
				public string PrimaryContact { get; set; } = null!;
				public string Address { get; set; } = null!;
				public string City { get; set; } = null!;
				public string State { get; set; } = null!;
				public string LastVisit { get; set; } = null!;
				public List<ShowroomVisitTimelineDTO> VisitTimeline { get; set; } = new();
			}

			public class ShowroomVisitTimelineDTO
			{
				public string SalesPersonName { get; set; } = null!;
				public DateTime VisitDate { get; set; }
				public string DiscussionNotes { get; set; } = null!;
				public List<string> Products { get; set; } = new();
			}

		public class VisitDTO
		{
			//public int VisitId { get; set; }
			//public int ShowroomId { get; set; }
			//public string ShowroomName { get; set; } = null!;
			//public string Location { get; set; } = null!;
			//public int SalesPersonId { get; set; }
			//public string SalesPersonName { get; set; } = null!;
			//public DateTime VisitDate { get; set; }
			//public decimal Latitude { get; set; }
			//public decimal Longitude { get; set; }
			//public string DiscussionNotes { get; set; } = null!;
			//public List<string> Products { get; set; } = new();
			//public int TotalRecords { get; set; }
			////attachment
			//public string? VoiceNotePath { get; set; }
			//public string? VisitingCardFrontPath { get; set; }
			//public string? VisitingCardBackPath { get; set; }
			//public List<string> ShowroomImages { get; set; } = new();

			public int VisitId { get; set; }
			public int ShowroomId { get; set; }
			public string? ShowroomName { get; set; }      // was: string ... = null!
			public string? Location { get; set; }          // was: string ... = null!
			public int SalesPersonId { get; set; }
			public string? SalesPersonName { get; set; }   // was: string ... = null!
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

		//public class VisitDTO
		//{
		//	public int VisitId { get; set; }
		//	public string ShowroomName { get; set; } = null!;
		//	public string Location { get; set; } = null!;
		//	public int SalesPersonId { get; set; }
		//	public string SalesPersonName { get; set; } = null!;
		//	public DateTime VisitDate { get; set; }
		//	public string DiscussionNotes { get; set; } = null!;
		//	public List<string> Products { get; set; } = new();
		//	public int TotalRecords { get; set; }

		//	//get by id
		//	public decimal? Latitude { get; set; }
		//	public decimal? Longitude { get; set; }
		//	public string? VoiceNoteUrl { get; set; }
		//	public List<string> VisitingCardImages { get; set; } = new();
		//	public List<string> ShowroomImages { get; set; } = new();
		//}

		public class VisitMapDTO
		{
			public List<VisitMapPointDTO> Points { get; set; } = new();
			public List<SalesPersonVisitCountDTO> SalesPersonCounts { get; set; } = new();
		}

			public class VisitMapPointDTO
			{
				public int VisitId { get; set; }
				public decimal Latitude { get; set; }
				public decimal Longitude { get; set; }
				public int SalesPersonId { get; set; }	
				public string SalesPersonName { get; set; } = null!;
			}

			public class SalesPersonVisitCountDTO
			{
				public int SalesPersonId { get; set; }
				public string SalesPersonName { get; set; } = null!;
				public int VisitCount { get; set; }
			}

			public class DashboardDTO
			{
				public string AdminName { get; set; } = null!;
				public DateTime AdminCreatedAt { get; set; }
				public int TotalSalespersons { get; set; }
				public int TotalShowrooms { get; set; }
				public int TotalVisits { get; set; }
				public int TodayVisits { get; set; }
				public int MonthlyVisitsCount { get; set; }
				public string MonthLabel { get; set; } = null!;
				public List<RecentVisitDTO> RecentVisits { get; set; } = new();
			}

			public class RecentVisitDTO
			{
				public int VisitId { get; set; }
				public string ShowroomName { get; set; } = null!;
				public string SalesPersonName { get; set; } = null!;
				public DateTime VisitDate { get; set; }
				public string Location { get; set; } = null!;
			}

			public class ShowroomDropdownDTO
			{
				public int Id { get; set; }
				public string? Data { get; set; }
			}
		}
	}
