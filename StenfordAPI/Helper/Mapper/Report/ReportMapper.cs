using AutoMapper;
using Stenford.Domain;
using StenfordAPI.Models.Admin;
using static Stenford.Domain.DTO;

namespace StenfordAPI.Helper.Mapper.Report
{
    public static class ReportMapper
    {
        public static ReportModel ToModel(this ReportDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReportDTO, ReportModel>();
                cfg.CreateMap<ReportRowDTO, ReportRowModel>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<ReportDTO, ReportModel>(entity);
        }
    }
}
