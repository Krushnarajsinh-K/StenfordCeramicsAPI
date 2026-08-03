using Stenford.Domain;
using Stenford.Domain.DataContext;
using Stenford.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.SalesPerson
{
	public class SalesPersonRepository  : ISalesPersonRepository
	{
		private readonly ApplicationDbContext _context;

		public SalesPersonRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public List<SalesPersonDTO> GetSalesPersonDataList(int pageNumber, int pageSize, string? search, int? stateId, int? cityId)
		{
			if (pageNumber <= 0) pageNumber = 1;
			if (pageSize <= 0) pageSize = 10;

			var salesPersonList = (from salesPerson in _context.SecSalesPeople
								   join state in _context.LocStates on salesPerson.StateId equals state.StateId into stateJoin
								   from state in stateJoin.DefaultIfEmpty()
								   join city in _context.LocCities on salesPerson.CityId equals city.CityId into cityJoin
								   from city in cityJoin.DefaultIfEmpty()
								   where salesPerson.IsDeleted == false &&
										 salesPerson.Country.CountryName == "India" &&
										 (!stateId.HasValue || salesPerson.StateId == stateId) &&
										 (!cityId.HasValue || salesPerson.CityId == cityId) &&
										 (string.IsNullOrEmpty(search) ||
											  salesPerson.SalesPersonName.ToLower().Contains(search.ToLower()) ||
											  salesPerson.PrimaryContact.ToLower().Contains(search.ToLower()) ||
											  (state != null && state.StateName.ToLower().Contains(search.ToLower())) ||
											  (city != null && city.CityName.ToLower().Contains(search.ToLower())))
								   orderby salesPerson.SalesPersonId descending
								   select new SalesPersonDTO
								   {
									   SalesPersonId = salesPerson.SalesPersonId,
									   AspNetUserId = salesPerson.AspNetUserId,
									   SalesPersonName = salesPerson.SalesPersonName,
									   Email = salesPerson.Email,
									   ContactPerson = salesPerson.ContactPerson,
									   PrimaryContact = salesPerson.PrimaryContact,
									   SecondaryContact = salesPerson.SecondaryContact,
									   Address = salesPerson.Address,
									   CountryId = salesPerson.CountryId,
									   StateId = salesPerson.StateId,
									   State = state.StateName,
									   CityId = salesPerson.CityId,
									   City = city.CityName,
									   IsActive = salesPerson.IsActive,
									   VisitCount = salesPerson.VisVisits.Count()
								   }).ToList();

			int totalCount = salesPersonList.Count();

			var result = salesPersonList
							.Skip((pageNumber - 1) * pageSize)
							.Take(pageSize)
							.ToList();

			result.ForEach(x => x.TotalRecords = totalCount);
			return result;
		}

		public SalesPersonDTO AddSalesPerson(SalesPersonDTO salesPersonDTO, string aspnetUserId)
		{
			Guid createdBy = Guid.Parse(aspnetUserId);

			AspAspNetUser user = new AspAspNetUser();
			user.AspNetUserId = Guid.NewGuid();
			user.Username = salesPersonDTO.Email;
			user.PasswordHash = salesPersonDTO.Password; // already encrypted by controller
			user.CreatedAt = DateTime.Now;
			user.CreatedBy = createdBy;
			user.ModifiedAt = DateTime.Now;
			user.ModifiedBy = createdBy;
			_context.AspAspNetUsers.Add(user);

			SecSalesPerson salesPerson = new SecSalesPerson();
			salesPerson.AspNetUserId = user.AspNetUserId;
			salesPerson.SalesPersonName = salesPersonDTO.SalesPersonName;
			salesPerson.Email = salesPersonDTO.Email;
			salesPerson.Password = salesPersonDTO.Password; // already encrypted by controller
			salesPerson.ContactPerson = salesPersonDTO.ContactPerson;
			salesPerson.PrimaryContact = salesPersonDTO.PrimaryContact;
			salesPerson.SecondaryContact = salesPersonDTO.SecondaryContact;
			salesPerson.Address = salesPersonDTO.Address;
			salesPerson.CountryId = 101;
			salesPerson.StateId = salesPersonDTO.StateId;
			salesPerson.CityId = salesPersonDTO.CityId;
			salesPerson.IsActive = true;
			salesPerson.IsDeleted = false;
			salesPerson.CreatedAt = DateTime.Now;
			salesPerson.CreatedBy = createdBy;
			salesPerson.ModifiedAt = DateTime.Now;
			salesPerson.ModifiedBy = createdBy;
			_context.SecSalesPeople.Add(salesPerson);

			_context.SaveChanges();

			salesPersonDTO.SalesPersonId = salesPerson.SalesPersonId;
			salesPersonDTO.AspNetUserId = user.AspNetUserId;
			return salesPersonDTO;
		}

		public SalesPersonDTO EditSalesPerson(SalesPersonDTO salesPersonDTO, string aspnetUserId)
		{
			var salesPerson = _context.SecSalesPeople.FirstOrDefault(x => x.SalesPersonId == salesPersonDTO.SalesPersonId && x.IsDeleted == false);

			if (salesPerson == null)
			{
				return null;
			}

			salesPerson.SalesPersonName = salesPersonDTO.SalesPersonName;
			salesPerson.PrimaryContact = salesPersonDTO.PrimaryContact;
			salesPerson.StateId = salesPersonDTO.StateId;
			salesPerson.CityId = salesPersonDTO.CityId;
			salesPerson.IsActive = salesPersonDTO.IsActive;
			salesPerson.ModifiedAt = DateTime.Now;
			salesPerson.ModifiedBy = Guid.Parse(aspnetUserId);

			_context.SaveChanges();

			salesPersonDTO.AspNetUserId = salesPerson.AspNetUserId;
			return salesPersonDTO;
		}

		public bool IsSalesPersonNameExists(string salesPersonName)
		{
			return _context.SecSalesPeople.Any(x => x.SalesPersonName.ToLower() == salesPersonName.ToLower() && x.IsDeleted == false);
		}

		public bool DeleteSalesPerson(int salesPersonId, string aspnetUserId)
		{
			var salesPerson = _context.SecSalesPeople.FirstOrDefault(x => x.SalesPersonId == salesPersonId && x.IsDeleted == false);

			if (salesPerson == null)
			{
				return false;
			}

			salesPerson.IsDeleted = true;
			salesPerson.DeletedAt = DateTime.Now;
			salesPerson.DeletedBy = Guid.Parse(aspnetUserId);
			salesPerson.ModifiedAt = DateTime.Now;
			salesPerson.ModifiedBy = Guid.Parse(aspnetUserId);

			_context.SecSalesPeople.Update(salesPerson);
			_context.SaveChanges();
			return true;
		}

		public SalesPersonDetailDTO GetSalesPersonById(int salesPersonId)
		{
			var salesPerson = (from sp in _context.SecSalesPeople
							   join state in _context.LocStates on sp.StateId equals state.StateId into stateJoin
							   from state in stateJoin.DefaultIfEmpty()
							   join city in _context.LocCities on sp.CityId equals city.CityId into cityJoin
							   from city in cityJoin.DefaultIfEmpty()
							   where sp.SalesPersonId == salesPersonId && sp.IsDeleted == false
							   select new SalesPersonDetailDTO
							   {
								   SalesPersonId = sp.SalesPersonId,
								   SalesPersonName = sp.SalesPersonName,
								   PrimaryContact = sp.PrimaryContact,
								   State = state.StateName,
								   City = city.CityName,
								   IsActive = sp.IsActive
							   }).FirstOrDefault();

			if (salesPerson == null)
			{
				return null;
			}

			var visits = _context.VisVisits
				.Where(v => v.SalesPersonId == salesPersonId && v.IsDeleted == false)
				.ToList();

			salesPerson.TotalVisits = visits.Count;
			salesPerson.ShowroomCount = visits.Select(v => v.ShowroomId).Distinct().Count();
			salesPerson.ThisMonthVisits = visits.Count(v => v.VisitDate.Month == DateTime.Now.Month && v.VisitDate.Year == DateTime.Now.Year);

			salesPerson.VisitTimeline = (from v in _context.VisVisits
										 join showroom in _context.ShoShowrooms on v.ShowroomId equals showroom.ShowroomId
										 join city in _context.LocCities on showroom.CityId equals city.CityId
										 join state in _context.LocStates on showroom.StateId equals state.StateId
										 where v.SalesPersonId == salesPersonId && v.IsDeleted == false
										 orderby v.VisitDate descending
										 select new VisitTimelineDTO
										 {
											 SalesPersonName = salesPerson.SalesPersonName,
											 VisitDate = v.VisitDate,
											 ShowroomName = showroom.ShowroomName,
											 Location = city.CityName + ", " + state.StateName,
											 DiscussionNotes = v.DiscussionNotes,
											 Products = v.ProductsDiscussedString.Split("@#$%^&**&^%$#@", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()
										 }).ToList();

			return salesPerson;
		}
	}
}
