using Stenford.Domain.DataContext;
using Stenford.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stenford.Service.Dropdown
{
	public class DropdownRepository : IDropdownRepository
	{
		private readonly ApplicationDbContext _context;

		public DropdownRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<DTO.StateDropdownDTO> GetStateList()
		{
			return _context.LocStates
				.Where(s => s.IsDeleted == false && s.CountryId == 101)
				.OrderBy(s => s.StateName)
				.Select(s => new DTO.StateDropdownDTO
				{
					Id = s.StateId,
					Data = s.StateName
				}).ToList();
		}

		public List<DTO.CityDropdownDTO> GetCityListByStateId(int stateId)
		{
			return _context.LocCities
				.Where(c => c.IsDeleted == false && c.StateId == stateId && c.CountryId == 101)
				.OrderBy(c => c.CityName)
				.Select(c => new DTO.CityDropdownDTO
				{
					Id = c.CityId,
					Data = c.CityName
				}).ToList();
		}
	}
}
