using Stenford.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Visit
{
	public interface IVisitRepository
	{
		List<VisitDTO> GetVisitList(int pageIndex, int pageSize, int? stateId, int? cityId, int? salesPersonId, DateTime? fromDate, DateTime? toDate);

		VisitDTO GetVisitById(int visitId);

		VisitMapDTO GetVisitMapPoints(int? stateId, int? cityId, int? salesPersonId, DateTime? fromDate, DateTime? toDate);

		VisitDTO AddVisit(VisitDTO visitDTO, Guid aspnetUserId);
	}
}
