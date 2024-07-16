using AutoMapper;
using Data.Services;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
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
            if (vm.PurchaseDetails.Count <= 0)
            {
                throw new Exception("لا يمكن حفظ فاتورة مشتريات بدون اضافة مواد خام للفاتورة");
            }
            var NewPurchase = _mapper.Map<Purchasing>(vm.PurchaseInfo);
            NewPurchase.SupplierId = vm.Supplier.Id;
            await _db.AddAsync(NewPurchase);
            await _db.SaveChangesAsync();
            await _purchaseTransactionManager.DoPurchaseTransactions(vm, NewPurchase.Id);
            return vm;
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


        public async Task<FeedBackWithMessages> SavePurchaseForProduct(PurchaseContainerForProduct vm)
          => await _PurchaseForProductSaveManager.SaveTransactionAsync(SavePurchaseForProductFunc, vm);


        private async Task<PurchaseContainerForProduct> SavePurchaseForProductFunc(PurchaseContainerForProduct vm)
        {
            if (vm.PurchaseDetails.Count <= 0)
            {
                throw new Exception("لا يمكن حفظ فاتورة مشتريات بدون اضافة مواد خام للفاتورة");
            }
            var NewPurchase = _mapper.Map<Purchasing>(vm.PurchaseInfo);
            NewPurchase.SupplierId = vm.SupplierId;
            NewPurchase.StoreItemId = vm.StoreItem.Id;
            NewPurchase.PriceItemsRaw = vm.PurchaseInfo.PriceItemsRaw;
            await _db.AddAsync(NewPurchase);
            await _db.SaveChangesAsync();
            await _purchaseTransactionManager.DoPurchaseTransactionsForProduct(vm, NewPurchase.Id);
            return vm;
        }





        public async Task<List<PurchasingVM>> GetAll()
       => _mapper.Map<List<PurchasingVM>>(await _db.Purchasings.Include(s => s.Supplier).ToListAsync());

        public async Task<PurchaseContainer> DetailsPurchase(int purchasesId)
        {
            PurchaseContainer purchaseContainer = new PurchaseContainer();
            var purchase = _db.Purchasings.Include(s => s.Supplier).FirstOrDefault(p => p.Id == purchasesId);
            purchaseContainer.Supplier = _mapper.Map<SupplierVM>(purchase?.Supplier);
            purchaseContainer.PurchaseInfo = _mapper.Map<PurchaseInfoVM>(purchase);
            purchaseContainer.PurchaseDetails = await _purchaseTransactionManager.GetPurchasesDetails(purchasesId);
            return purchaseContainer;
        }



    }
}
