using Stenford.Domain;
using Stenford.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Common.Constants.Enums;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Visit
{
	public class VisitRepository : IVisitRepository
	{
		private readonly ApplicationDbContext _context;

		public VisitRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public List<VisitDTO> GetVisitList(int pageIndex, int pageSize, int? stateId, int? cityId, int? salesPersonId, DateTime? fromDate, DateTime? toDate)
		{
			try
			{
				List<VisitDTO> visitObj = (from v in _context.VisVisits
											   join showroom in _context.ShoShowrooms on v.ShowroomId equals showroom.ShowroomId
											   join sp in _context.SecSalesPeople on v.SalesPersonId equals sp.SalesPersonId
											   join city in _context.LocCities on showroom.CityId equals city.CityId into cityJoin
											   from city in cityJoin.DefaultIfEmpty()
											   where v.IsDeleted != true && showroom.IsDeleted != true && 
											   (!stateId.HasValue || showroom.StateId == stateId) &&
											   (!cityId.HasValue || showroom.CityId == cityId) &&
											   (!salesPersonId.HasValue || v.SalesPersonId == salesPersonId) &&
											   (!fromDate.HasValue || v.VisitDate >= fromDate) &&
											   (!toDate.HasValue || v.VisitDate <= toDate)
											   orderby v.VisitDate descending
											   select new VisitDTO
											   {
												   VisitId = v.VisitId,
												   ShowroomName = showroom.ShowroomName,
												   Location = city.CityName,
												   SalesPersonId = sp.SalesPersonId,
												   SalesPersonName = sp.SalesPersonName,
												   VisitDate = v.VisitDate,
												   DiscussionNotes = v.DiscussionNotes,
												   Products = v.ProductsDiscussedString.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()	
											   }).ToList();

				if (visitObj.Any())
				{
					var totalRecords = visitObj.Count;
					visitObj = visitObj.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
					visitObj.First().TotalRecords = totalRecords;
				}

				return visitObj;
			}
			catch
			{
				return null;
			}
		}

		public VisitDTO GetVisitById(int visitId)
		{
			var visit = (from v in _context.VisVisits
						 join showroom in _context.ShoShowrooms on v.ShowroomId equals showroom.ShowroomId
						 join sp in _context.SecSalesPeople on v.SalesPersonId equals sp.SalesPersonId
						 join city in _context.LocCities on showroom.CityId equals city.CityId into cityJoin
						 from city in cityJoin.DefaultIfEmpty()
						 join state in _context.LocStates on showroom.StateId equals state.StateId into stateJoin
						 from state in stateJoin.DefaultIfEmpty()
						 where v.VisitId == visitId && v.IsDeleted == false && showroom.IsDeleted == false
						 select new VisitDTO
						 {
							 VisitId = v.VisitId,
							 ShowroomName = showroom.ShowroomName,
							 Location = city.CityName + ", " + state.StateName,
							 SalesPersonId = sp.SalesPersonId,
							 SalesPersonName = sp.SalesPersonName,
							 VisitDate = v.VisitDate,
							 DiscussionNotes = v.DiscussionNotes,
							 Products = v.ProductsDiscussedString.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList(),
							 Latitude = v.Latitude,
							 Longitude = v.Longitude
						 }).FirstOrDefault();

			if (visit == null)
			{
				return null;
			}

			var attachments = _context.VisVisitWiseAttachments
				.Where(a => a.VisitId == visitId && a.IsDeleted == false)
				.ToList();

			visit.VoiceNoteUrl = attachments.FirstOrDefault(a => a.AttachmentType == (int)AttachmentType.VoiceNote)?.AttachmentPath;
			visit.VisitingCardImages = attachments.Where(a => a.AttachmentType == (int)AttachmentType.VisitingCard).Select(a => a.AttachmentPath).ToList();
			visit.ShowroomImages = attachments.Where(a => a.AttachmentType == (int)AttachmentType.ShowroomImage).Select(a => a.AttachmentPath).ToList();

			return visit;
		}
	}
}
