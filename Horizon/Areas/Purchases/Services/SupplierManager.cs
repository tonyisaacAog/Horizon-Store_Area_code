using AutoMapper;
using Data.Services;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Horizon.Areas.Purchases.Services
{
    public class SupplierManager : GenericSettingsManager<Supplier, SupplierVM>
    {
        private readonly SaveManager<SupplierVM> _saveSupplierManager;

        public SupplierManager(ApplicationDbContext db, IMapper mapper, SaveManager<SupplierVM> saveSupplierManager) : base(db, mapper, saveSupplierManager)
        {
            _saveSupplierManager = saveSupplierManager;
        }

        public async Task<(List<SupplierVM> items, int totalCount)> SearchSuppliers(string term, int page, int pageSize)
        {
            var query = _db.Suppliers
                   .Where(x => x.SupplierName.Contains(term))
                   .OrderBy(x => x.SupplierName);

            var totalCount = query.Count();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new SupplierVM { Id = x.Id, SupplierName = x.SupplierName })
                .ToListAsync();
            return (items, totalCount);
        }
    }
}
