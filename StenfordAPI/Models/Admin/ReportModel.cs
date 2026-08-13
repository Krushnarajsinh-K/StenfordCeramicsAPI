namespace StenfordAPI.Models.Admin
{
    public class ReportModel
    {
        public int TotalVisits { get; set; }
        public int ShowroomCount { get; set; }
        public int StateCount { get; set; }
        public double AvgPerDay { get; set; }
        public List<ReportRowModel> VisitReport { get; set; } = new();
    }

    public class ReportRowModel
    {
        public string ShowroomName { get; set; } = null!;
        public string City { get; set; } = null!;
        public int VisitCount { get; set; }
        public DateTime LastVisit { get; set; }
        public int TotalRecords { get; set; }
    }
}
