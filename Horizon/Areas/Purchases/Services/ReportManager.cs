using AutoMapper;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Purchases.ViewModel.reports;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;

namespace Horizon.Areas.Purchases.Services
{
    public class ReportManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportManager(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<PurchasingVM>> ReprotPurchasingDateRange(SearchDateVM searchDate)
        {
            var endDatePlus = searchDate.EndDate.ToEgyptionDate().AddDays(1);
            var lst =_mapper.Map<List<PurchasingVM>>(await _db.Purchasings.Include(p=>p.Supplier).Where(date => date.PurchasingDate >= searchDate.StartDate.ToEgyptionDate() && date.PurchasingDate <= endDatePlus).ToListAsync());
            return lst;
        }
    }
}
