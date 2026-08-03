using Stenford.Domain.DataContext;
using Stenford.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Dropdown
{
	public class DropdownRepository : IDropdownRepository
	{
		private readonly ApplicationDbContext _context;

		public DropdownRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<StateDropdownDTO> GetStateList()
		{
			return _context.LocStates
				.Where(s => s.IsDeleted == false && s.CountryId == 101)
				.OrderBy(s => s.StateName)
				.Select(s => new StateDropdownDTO
				{
					Id = s.StateId,
					Data = s.StateName
				}).ToList();
		}

		public List<CityDropdownDTO> GetCityListByStateId(int stateId)
		{
			return _context.LocCities
				.Where(c => c.IsDeleted == false && c.StateId == stateId && c.CountryId == 101)
				.OrderBy(c => c.CityName)
				.Select(c => new CityDropdownDTO
				{
					Id = c.CityId,
					Data = c.CityName
				}).ToList();
		}

		public List<ShowroomDropdownDTO> GetShowroomDropdownList()
		{
			return _context.ShoShowrooms
				.Where(s => s.IsDeleted == false)
				.OrderBy(s => s.ShowroomName)
				.Select(s => new ShowroomDropdownDTO
				{
					Id = s.ShowroomId,
					Data = s.ShowroomName
				}).ToList();
		}
	}
}
