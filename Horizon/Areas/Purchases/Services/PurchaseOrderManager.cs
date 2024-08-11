using AutoMapper;
using Data.Services;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.Services;
using Horizon.Data;
using Horizon.Models.Shared;
using JasperFx.CodeGeneration.Frames;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;
using MyInfrastructure.Model;

namespace Horizon.Areas.Purchases.Services
{
    public class PurchaseOrderManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly SaveManager<PurchaseOrderVM> _purchaseOrderSaveManager;
        public PurchaseOrderManager(ApplicationDbContext db,IMapper mapper, SaveManager<PurchaseOrderVM> purchaseOrderSaveManager)
        {
            _db = db;
            _mapper = mapper;
            _purchaseOrderSaveManager = purchaseOrderSaveManager;
        }
        public async Task<List<PurchaseOrderVM>> GetAll()
            => _mapper.Map<List<PurchaseOrderVM>>(await _db.PurchaseOrders.Include(s => s.Supplier).ToListAsync());
        public async Task<List<PurchaseOrderVM>> GetAllNotStoreInStock()
            => _mapper.Map<List<PurchaseOrderVM>>(await _db.PurchaseOrders.Include(s => s.Supplier).Where(obj=>obj.IsStoreInStock==false).ToListAsync());
        public async Task<List<PurchaseOrderVM>> GetAllNotStoreInStockContainStoreItem(int StoreItemId)
          => _mapper.Map<List<PurchaseOrderVM>>(await _db.PurchaseOrderDetails.Include(obj=>obj.PurchaseOrder).ThenInclude(obj=>obj.Supplier).Where(obj=>obj.StoreItemId == StoreItemId&& obj.IsCreatedASPurchasing==false).Select(obj=>obj.PurchaseOrder).ToListAsync());
        public async Task<PurchaseOrderVM> PurchaseOrderDetails(int id)
            =>  _mapper.Map<PurchaseOrderVM>(await _db.PurchaseOrders.Include(obj=>obj.Supplier).Include(obj=>obj.PurchaseOrderDetails).ThenInclude(obj=>obj.StoreItem).FirstOrDefaultAsync(obj => obj.Id == id));
        public async Task<FeedBackWithMessages> SavePurchaseOrder(PurchaseOrderVM vm)
          => await _purchaseOrderSaveManager.SaveTransactionAsync(SavePurchaseOrderFunc, vm);
        private async Task<PurchaseOrderVM> SavePurchaseOrderFunc(PurchaseOrderVM vm)
        {
            if (vm.Id > 0)
            {
                await UpdatePurchaseOrder(vm);
            }
            else
            {
                 await SaveNewPurchaseOrder(vm);
            }
            return vm;
        }

        private async Task UpdatePurchaseOrder(PurchaseOrderVM vm)
        {

            if( vm.IsStoreInStock ) throw new Exception("تم تفريغ امر الانتاج لايمكن التعديل عليه ");
            var check = _db.PurchaseOrderDetails.Where(obj => obj.PurchaseOrderId == vm.Id).Any(obj => obj.IsCreatedASPurchasing == true);
            if( check ) throw new Exception("لا يمكن التعديل على امر الانتاج لان يتم تفريغه");
            if (vm.PurchaseOrderDetails.Where(obj => obj.RecordStatus != RecordStatus.Deleted).Count() <= 0)
            {
                throw new Exception("لا يمكن حفظ بيانات امر الانتاج يجب اضافة بيانات");
            }
            var purchaseOrder = await _db.PurchaseOrders.FirstOrDefaultAsync(obj => obj.Id == vm.Id);
            if (purchaseOrder == null) throw new Exception("امر الانتاج عير موجود");
            purchaseOrder.Date = vm.PurchaseOrderDate.ToEgyptionDate();
            purchaseOrder.TotalAmount = vm.TotalAmount;
            purchaseOrder.SupplierId = vm.SupplierId;
            purchaseOrder.PurchaseOrderNumber = vm.PurchaseOrderNumber;
            _db.Update(purchaseOrder);
            await _db.SaveChangesAsync();
            await AddDetailsForPurchaseOrder(purchaseOrder.Id, vm.PurchaseOrderDetails);
        }

        private async Task SaveNewPurchaseOrder(PurchaseOrderVM vm)
        {
            if (vm.PurchaseOrderDetails.Where(obj=>obj.RecordStatus != RecordStatus.Deleted).Count() <= 0)
            {
                throw new Exception("لا يمكن حفظ بيانات امر الانتاج يجب اضافة بيانات");
            }
            var NewPurchaseOrder = _mapper.Map<PurchaseOrder>(vm);
            NewPurchaseOrder.PurchaseOrderDetails = null;
            await _db.AddAsync(NewPurchaseOrder);
            await _db.SaveChangesAsync();
            await AddDetailsForPurchaseOrder(NewPurchaseOrder.Id, vm.PurchaseOrderDetails);
        }

        private async Task AddDetailsForPurchaseOrder(int id, IEnumerable<PurchaseOrderDetailsVM> details)
        {
            foreach (var item in details)
            {
                var config = _mapper.Map<PurchaseOrderDetails>(item);
                config.PurchaseOrderId = id;
                config.Notes = item.Notes;
                switch (item.RecordStatus)
                {
                    case RecordStatus.Added:
                        var itemConfigueDel = await _db.PurchaseOrderDetails.IgnoreQueryFilters()
                            .FirstOrDefaultAsync(obj => obj.PurchaseOrderId == id && obj.StoreItemId == item.StoreItemId && obj.IsDeleted == true);
                        if (itemConfigueDel != null)
                        {
                            itemConfigueDel.IsDeleted = false;
                            itemConfigueDel.StoreItemAmount = item.StoreItemAmount;
                            _db.Update(itemConfigueDel);
                        }
                        else
                        {
                            await _db.AddAsync(config);
                        }
                        break;
                    case RecordStatus.Updated:
                        _db.Update(config);
                        break;
                    case RecordStatus.Deleted:
                        _db.Remove(config);
                        break;
                    case RecordStatus.UnChanged:
                        break;
                    default:
                        break;
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
