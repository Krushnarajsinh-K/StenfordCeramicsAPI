using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Dashboard
{
	public interface IDashboardRepository
	{
		DashboardDTO GetAdminDashboard();
	}
}
