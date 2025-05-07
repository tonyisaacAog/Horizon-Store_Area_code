using AutoMapper;
using BoldReports.Linq;
using Data.Services;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Transaction;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Model;
using Services;
using System.IO.Packaging;

namespace Horizon.Areas.Purchases.Services
{
    public class PurchaseManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly GenericSettingsManager<Supplier, SupplierVM> _SupplierManager;
        private readonly GenericSettingsManager<StoreItem, StoreItemVM> _StoreItemManager;
        private readonly SaveManager<PurchaseContainer> _PurchaseSaveManager;
        private readonly SaveManager<PurchaseContainerForProduct> _PurchaseForProductSaveManager;
        private readonly PurchaseTransactionManager _purchaseTransactionManager;
        private readonly ItemConfigureManager _itemConfigurationManager;
        public PurchaseManager(ApplicationDbContext db,
            IMapper mapper,
            GenericSettingsManager<Supplier, SupplierVM> SupplierManager,
            SaveManager<PurchaseContainer> PurchaseSaveManager,
            PurchaseTransactionManager purchaseTransactionManager,
            GenericSettingsManager<StoreItem,StoreItemVM> StoreItemManager,
            ItemConfigureManager itemConfigureManager,
            SaveManager<PurchaseContainerForProduct> purchaseForProductSaveManager)
        {
            _db = db;
            _mapper = mapper;
            _SupplierManager = SupplierManager;
            _PurchaseSaveManager = PurchaseSaveManager;
            _purchaseTransactionManager = purchaseTransactionManager;
            _StoreItemManager = StoreItemManager;
            _itemConfigurationManager = itemConfigureManager;
            _PurchaseForProductSaveManager = purchaseForProductSaveManager;
        }




        public async Task<PurchaseContainer> NewPurchase(int SupplierId)
        {
            var vm = new PurchaseContainer();
            vm.Supplier = await _SupplierManager.CheckAndReturn(SupplierId);
            return vm;
        }

        public async Task<FeedBackWithMessages> SavePurchase(PurchaseContainer vm)
            => await _PurchaseSaveManager.SaveTransactionAsync(SavePurchaseFunc, vm);

        private async Task<PurchaseContainer> SavePurchaseFunc(PurchaseContainer vm)
        {
            if (vm.PurchaseDetails.Count <= 0 && vm.PurchaseStoreItemDetails.Count <= 0 )
            {
                throw new Exception("لا يمكن حفظ فاتورة مشتريات بدون اضافة مواد خام او منتجات للفاتورة");
            }
            var NewPurchase = _mapper.Map<Purchasing>(vm.PurchaseInfo);
            NewPurchase.SupplierId = vm.Supplier.Id;
            NewPurchase.TotalAmount = vm.PurchaseDetails.Sum(obj => obj.Qty * obj.UnitPrice) + vm.PurchaseStoreItemDetails.Sum(obj => obj.UnitPrice);
            await _db.AddAsync(NewPurchase);
            await _db.SaveChangesAsync();
            await SaveDetailsForPurchasing(NewPurchase.Id,vm);
            await _purchaseTransactionManager.DoPurchaseTransactions(vm, NewPurchase.Id);
            await _purchaseTransactionManager.DoPurchaseTransactionsForProduct(vm,NewPurchase.Id);
            return vm;
        }

        private async Task SaveDetailsForPurchasing(int id,PurchaseContainer vm)
        {
            var detailStoreItemRaw = vm.PurchaseDetails.Select(obj => new PurchasingDetails
            {
                StoreItemsRawId = obj.StoreItemId,
                Amount = obj.Qty,
                UnitPrice = obj.UnitPrice,
                TotalSales = obj.Qty * obj.UnitPrice,
                PurchasingId = id,
                DetailType = DetailType.Item
            });
            var detailStoreItem  = vm.PurchaseStoreItemDetails.Select(obj => new PurchasingDetails
            {
                StoreItemId = obj.StoreItemId,
                Amount = obj.Qty,
                UnitPrice = obj.UnitPrice,
                TotalSales =   obj.UnitPrice,
                PurchasingId = id,
                DetailType = DetailType.Product
            });
            await _db.AddRangeAsync(detailStoreItemRaw);
            await _db.AddRangeAsync(detailStoreItem);
        }

        public async Task<PurchaseContainerForProduct> NewPurchaseForProduct(int ProductId)
        {
            var vm = new PurchaseContainerForProduct();
            var PurchaseLst = new List<PurchaseStoreTransactionVM>();
            var productConfiguration = await _itemConfigurationManager.GetDataStoreItem(ProductId, 
                x => x.StoreItemsRaw.RawItemTypeId == 1&&x.StoreItemId==ProductId);
            vm.StoreItem = productConfiguration.StoreItemVM;
            foreach (var item in productConfiguration.ItemConfigurationVMs)
            {
                PurchaseStoreTransactionVM PurchaseTransaction = new PurchaseStoreTransactionVM()
                {
                    ConfigueQty = item.MinimumAmount,
                    StoreItemId = item.StoreItemRawId,
                    StoreItemName = item.StoreItemsRawName,
                    Qty = 0,
                    UnitPrice = 1,
                };
                PurchaseLst.Add(PurchaseTransaction);
            }
            vm.PurchaseDetails.AddRange(PurchaseLst);
            return vm;
        }
        public async Task<PurchaseContainerForProduct> NewPurchaseStoreRawForProduct(int ProductId)
        {
            var vm = new PurchaseContainerForProduct();
            var PurchaseLst = new List<PurchaseStoreTransactionVM>();
            var productConfiguration = await _itemConfigurationManager.GetDataStoreItem(ProductId,
                x => x.StoreItemsRaw.RawItemTypeId != 1 && x.StoreItemId == ProductId);
            vm.StoreItem = productConfiguration.StoreItemVM;
            foreach( var item in productConfiguration.ItemConfigurationVMs )
            {
                PurchaseStoreTransactionVM PurchaseTransaction = new PurchaseStoreTransactionVM()
                {
                    ConfigueQty = item.MinimumAmount,
                    StoreItemId = item.StoreItemRawId,
                    StoreItemName = item.StoreItemsRawName,
                    Qty = 0,
                    UnitPrice = 1,
                };
                PurchaseLst.Add(PurchaseTransaction);
            }
            vm.PurchaseDetails.AddRange(PurchaseLst);
            return vm;
        }
        


        public async Task<FeedBackWithMessages> SavePurchaseForProduct(PurchaseContainerForProduct vm)
          => await _PurchaseForProductSaveManager.SaveTransactionAsync(SavePurchaseForProductFunc, vm);


        private async Task<PurchaseContainerForProduct> SavePurchaseForProductFunc(PurchaseContainerForProduct vm)
        {
            if (vm.PurchaseDetails.Count <= 0)
            {
                throw new Exception("لا يمكن حفظ فاتورة مشتريات بدون اضافة مواد خام للفاتورة");
            }
            // check purchase order
            var purchaseOrder = await _db.PurchaseOrders.FirstOrDefaultAsync(obj => obj.Id == vm.PurchaseOrderId);
            if(purchaseOrder == null ) { throw new Exception("امر الانتاج غير موجود"); }
            if(purchaseOrder.IsStoreInStock == true) { throw new Exception("امر الانتاج تم تفريغه"); }
            var purchaseOrderDeails = await _db.PurchaseOrderDetails.FirstOrDefaultAsync(obj => obj.PurchaseOrderId == vm.PurchaseOrderId&&obj.StoreItemId == vm.StoreItem.Id);
            if( purchaseOrderDeails == null ) { throw new Exception("المنتج الخام غير موجود فى امر الانتاج"); }
            if( purchaseOrderDeails.IsCreatedASPurchasing==true ) { throw new Exception("المنتج الخام تم تفريغة امر الانتاج وتم عمل اذن اضافة بالمنتج"); }
            if( purchaseOrderDeails.StoreItemAmount != vm.StoreItem.Quantity ) { throw new Exception("الكمية المضافة فى اذن اضافة خامات ليست كما هى فى امر الانتاج"); }


            var NewPurchase = _mapper.Map<Purchasing>(vm.PurchaseInfo);
            NewPurchase.SupplierId = purchaseOrder.SupplierId;
            NewPurchase.PurchaseOrderId = purchaseOrder.Id;
            NewPurchase.StoreItemId = vm.StoreItem.Id;
            NewPurchase.PriceItemsRaw = vm.PurchaseInfo.PriceItemsRaw;
            NewPurchase.AmountStoreItem = vm.StoreItem.Quantity;
            await _db.AddAsync(NewPurchase);

            //update item in purchase order 
            purchaseOrderDeails.IsCreatedASPurchasing = true;
            _db.Update(purchaseOrderDeails);
            await _db.SaveChangesAsync();

            //update purchase order if all item collected convert it to true in is Store in Stock
            var exist = _db.PurchaseOrderDetails.Any(obj => obj.IsCreatedASPurchasing == false);
            if( !exist )
            {
                purchaseOrder.IsStoreInStock = true;
                _db.Update(purchaseOrder);
                await _db.SaveChangesAsync();
            }

            await _purchaseTransactionManager.DoPurchaseTransactionsForProduct(vm, NewPurchase.Id);
            return vm;
        }





        public async Task<List<PurchasingVM>> GetAll()
       => _mapper.Map<List<PurchasingVM>>(await _db.Purchasings.Include(s => s.Supplier).ToListAsync());

        public async Task<PurchaseContainer> DetailsPurchase(int purchasesId)
        {
            PurchaseContainer purchaseContainer = new PurchaseContainer();
            var purchase = _db.Purchasings.Include(s => s.Supplier).Include(obj=>obj.PurchaseOrder).FirstOrDefault(p => p.Id == purchasesId);
            purchaseContainer.Supplier = _mapper.Map<SupplierVM>(purchase?.Supplier);
            purchaseContainer.PurchaseInfo = _mapper.Map<PurchaseInfoVM>(purchase);
            purchaseContainer.PurchaseOrder = _mapper.Map<PurchaseOrderVM>(purchase?.PurchaseOrder);
            purchaseContainer.PurchaseDetails = await _purchaseTransactionManager.GetPurchasesDetails(purchasesId);
            return purchaseContainer;
        }

        public async Task<PurchaseContainer> GetDetailsPurchase(int purchasesId)
        {
            PurchaseContainer purchaseContainer = new PurchaseContainer();
            var purchase = _db.Purchasings.Include(s => s.Supplier).FirstOrDefault(p => p.Id == purchasesId);
            purchaseContainer.Supplier = _mapper.Map<SupplierVM>(purchase?.Supplier);
            purchaseContainer.PurchaseInfo = _mapper.Map<PurchaseInfoVM>(purchase);
            purchaseContainer.PurchaseInfo.InvoiceNum = string.IsNullOrEmpty(purchase.InvoiceNum) ? "NA" : purchase.InvoiceNum;
            purchaseContainer.PurchaseDetails = await _db.PurchasingDetails.Include(obj => obj.StoreItemsRaw).Where(obj => obj.StoreItemsRaw!=null&& obj.DetailType == DetailType.Item && obj.PurchasingId == purchasesId)
                .Select(obj => new PurchaseStoreTransactionVM { Id = obj.Id,Qty = obj.Amount,StoreItemId = (int)obj.StoreItemsRawId,UnitPrice = obj.UnitPrice,StoreItemName = obj.StoreItemsRaw.ItemName }).ToListAsync();
            purchaseContainer.PurchaseStoreItemDetails = await _db.PurchasingDetails.Include(obj => obj.StoreItem).Where(obj => obj.StoreItem!=null&& obj.DetailType == DetailType.Product && obj.PurchasingId == purchasesId)
                .Select(obj => new PurchaseStoreTransactionVM { Id = obj.Id,Qty = obj.Amount,StoreItemId = (int)obj.StoreItemId,UnitPrice = obj.UnitPrice, StoreItemName = obj.StoreItem.ProductName }).ToListAsync();
            return purchaseContainer;
        }





    }
}
