using AutoMapper;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Manufacturing.VewModels.reports;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;

namespace Horizon.Areas.Manufacturing.Services
{
    public class ReportManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportManager(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<ManufacturingInfoVM>> ManufacturingReport(SearchDateVM searchDate)
        {
            var endDatePlus = searchDate.EndDate.ToEgyptionDate().AddDays(1);
            var lst = _mapper.Map<List<ManufacturingInfoVM>>(await _db.ManufacturingBatch.Where(date => date.BatchDate >= searchDate.StartDate.ToEgyptionDate() && date.BatchDate <= endDatePlus).ToListAsync());
            return lst;
        }
    }
}
