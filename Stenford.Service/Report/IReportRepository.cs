using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stenford.Domain.DTO;

namespace Stenford.Service.Report
{
    public interface IReportRepository
    {
        ReportDTO GetReport(int? showroomId, int? cityId, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
    }
}
