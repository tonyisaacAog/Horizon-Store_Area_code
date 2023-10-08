using AutoMapper;
using Horizon.Data;
using MyInfrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Sales.ViewModel.reports;

namespace Horizon.Areas.Sales.Services
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
        public async Task<List<SaleVM>> ReprotSaleDateRange(SearchDateVM searchDate)
        {
            var endDatePlus = searchDate.EndDate.ToEgyptionDate().AddDays(1);
            var lst = _mapper.Map<List<SaleVM>>(await _db.Sales.Include(p => p.Client).Where(date => date.SalesDate >= searchDate.StartDate.ToEgyptionDate() && date.SalesDate <= endDatePlus).ToListAsync());
            return lst;
        }
    }
}
