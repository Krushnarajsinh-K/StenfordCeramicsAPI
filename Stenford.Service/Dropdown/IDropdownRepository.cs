
using Stenford.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Dropdown
{
	public interface IDropdownRepository
	{
		List<StateDropdownDTO> GetStateList();
		List<CityDropdownDTO> GetCityListByStateId(int stateId);
		List<ShowroomDropdownDTO> GetShowroomDropdownList();
	}
}
