using AutoMapper;
using Data.Services;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;
using MyInfrastructure.Model;

namespace Horizon.Areas.Purchases.Services
{
    public class SupplierManager2
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly SaveManager<SupplierVM> _supplierSaveManager;

        public SupplierManager2(ApplicationDbContext db, IMapper mapper, 
            SaveManager<SupplierVM> supplierSaveManager)
        {
            _db = db;
            _mapper = mapper;
            _supplierSaveManager = supplierSaveManager;
        }

        public async Task<List<SupplierVM>> GetAll()
        => _mapper.Map<List<SupplierVM>>(await _db.Suppliers.ToListAsync());

        public async Task<SupplierVM> CheckAndReturn(int Id)
        {
            var exist = await CheckSupplierExist(Id);
            return exist ? await GetById(Id) : throw new Exception("لا يوجد مورد متاح !");
        }

        public async Task<bool> CheckSupplierExist(int Id)
            => await _db.Suppliers.AnyAsync(x => x.Id == Id);

        public async Task<SupplierVM> GetById(int Id)
            => _mapper.Map<SupplierVM>(await _db.Suppliers.FindAsync(Id));


        public async Task<FeedBackWithMessages>  ManageSupplierData(SupplierVM supplier, bool ForDelete =false)
        {
            if(ForDelete)
                return await _supplierSaveManager.SaveTransactionAsync(DeleteSupplierFunc, supplier);
            if ( supplier.Id>0)
                return await _supplierSaveManager.SaveTransactionAsync(UpdateSupplierFunc, supplier);
            else
                return await _supplierSaveManager.SaveTransactionAsync(AddNewSupplierFunc, supplier);
        }
        private async Task<SupplierVM> AddNewSupplierFunc (SupplierVM supplier)
        {
            var newSupplier = _mapper.Map<Supplier>(supplier);
            await _db.AddAsync(newSupplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        private async Task<SupplierVM> UpdateSupplierFunc(SupplierVM supplier)
        {
            var newSupplier = _mapper.Map<Supplier>(supplier);
            _db.Update(newSupplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        private async Task<SupplierVM> DeleteSupplierFunc(SupplierVM supplier)
        {
            var Supplier = await CheckAndReturn(supplier.Id);
            _db.Remove(Supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

    }
}
