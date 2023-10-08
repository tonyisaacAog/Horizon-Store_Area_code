using AutoMapper;
using Data.Services;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Manufacturing.Models;
using MyInfrastructure.Filters;
using MyInfrastructure.Model;
using Services;

namespace Horizon.Areas.Store.Services
{
    public class StoreItemManager : GenericSettingsManager<StoreItem, StoreItemVM>
    {
        private readonly StoreTransactionManager _storeTransactionManager;
        private readonly SaveManager<StoreItemDestoryVM> _saveDestroyManager;
        private readonly StoreLocBalanceTransManager _storeLocBalanceTransManager;
        private readonly StoreTransDetailsManager _storeTransDetailsManager;
        public StoreItemManager(ApplicationDbContext db, IMapper mapper, SaveManager<StoreItemVM> saveManager,
            StoreTransactionManager storeTransactionManager,
            SaveManager<StoreItemDestoryVM> saveDestroyManager,
            StoreLocBalanceTransManager storeLocBalanceTransManager,
            StoreTransDetailsManager storeTransDetailsManager)  
            : base(db, mapper, saveManager)
        {
            _storeTransactionManager = storeTransactionManager;
            _saveDestroyManager = saveDestroyManager;
            _storeLocBalanceTransManager = storeLocBalanceTransManager;
            _storeTransDetailsManager = storeTransDetailsManager;
        }
        [ModelValidationWithJsonFeedBackFilter]
        public async Task<FeedBackWithMessages> SaveDestroyOperation(StoreItemDestoryVM vm)
         => await _saveDestroyManager.SaveTransactionAsync(SaveDestroyAmountFunc, vm);




        //start point for all
        private async Task<StoreItemDestoryVM> SaveDestroyAmountFunc(StoreItemDestoryVM vm)
        {
         
            var DestroyedQty = vm.StoreItem.Quantity;
            var QtyBeforeDestory = vm.StoreItem.CurrentQty;
            var BalanceBeforeDestory = vm.StoreItem.Balance;
            var unitPrice = BalanceBeforeDestory / QtyBeforeDestory;
            var BalanceAfterDestroy = DestroyedQty * unitPrice;
            var item = await _db.StoreItems.FindAsync(vm.StoreItem.Id);
            item.UpdateQTY(DestroyedQty, false);
            item.UpdateBalance(BalanceAfterDestroy, false);
            item.UpdateDestroyedQty(DestroyedQty,true);
            _db.StoreItems.Update(item);

            //// Store Transaction
            int StoreTransId = await _storeTransactionManager.DoStoreTansDestroy(vm);
            ////Update Store Location Balance
            var LocationBalanceAfter = await _storeLocBalanceTransManager.UpdateStoreLocationBalanceForDestroyProduct(item.Id, vm.StoreLocationId, (int)item.DestroyedQty);
            //// Store Transaction
            await _storeTransDetailsManager.DoStoreTransactionDetailsForDestroyQty((int)DestroyedQty, item, StoreTransId, LocationBalanceAfter);
            return vm;
        }

    }
}
