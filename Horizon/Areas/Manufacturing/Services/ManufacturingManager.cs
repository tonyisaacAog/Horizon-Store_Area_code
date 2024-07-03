using AutoMapper;
using Data.Services;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Orders.Services;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Configuration;
using Horizon.Areas.Store.ViewModel.ItemRawReport;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Horizon.Models.Shared;
using Lamar;
using Manufacturing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Filters;
using MyInfrastructure.Model;
using Services;

namespace Horizon.Areas.Manufacturing.Services
{
    public class ManufacturingManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly SaveManager<ManufacturingContainer> _ManufacturingSaveManager;
        private readonly ManufacturngTransManager _ManufacturngTransManager;
        private readonly StoreTransactionManager _StoreTransactionManager;
        private readonly StoreTransDetailsManager _storeTransDetailsManager;
        private readonly StoreLocBalanceTransManager _storeLocBalanceTransManager;
        private readonly OrderConfigureManager _orderConfigureManager;
        public readonly OrderManager _orderManager;
        public ManufacturingManager(ApplicationDbContext db,
            IMapper mapper,
            GenericSettingsManager<StoreItem, StoreItemVM> StoreItemManager,
            SaveManager<ManufacturingContainer> ManufacturingSaveManager,
            ManufacturngTransManager manufacturngTransManager,
            StoreTransactionManager storeTransactionManager,
            StoreTransDetailsManager storeTransDetailsManager,
            StoreLocBalanceTransManager storeLocBalanceTransManager,
            OrderConfigureManager orderConfigureManager,
            OrderManager orderManager
           )

        {
            _db = db;
            _mapper = mapper;
            _ManufacturingSaveManager = ManufacturingSaveManager;
            _ManufacturngTransManager = manufacturngTransManager;
            _StoreTransactionManager = storeTransactionManager;
            _storeTransDetailsManager = storeTransDetailsManager;
            _storeLocBalanceTransManager = storeLocBalanceTransManager;
            _orderConfigureManager = orderConfigureManager;
            _orderManager = orderManager;
        }


        public async Task<ManufacturingContainerDisplay> DetailsManufacturing(int Id)
        {
            var container = new ManufacturingContainerDisplay();
            var manufacturingInfo = _db.ManufacturingBatch.FirstOrDefault(p => p.Id == Id);
            var storeTransations = _db.StoreTransactions.FirstOrDefault(t => t.SourceId == Id && t.TransType == StoreTransTypeEnum.Manufacturing);
            var storeLocation = _db.StoreLocations.FirstOrDefault(s => s.Id == storeTransations.DestinationId);
            var transDetails = _db.StoreTransactionDetails.Include(i=>i.StoreItem).ThenInclude(f=>f.Family).FirstOrDefault(d=>d.StoreTransId==storeTransations.Id); 
            container.ManufacturingInfoVM = _mapper.Map<ManufacturingInfoVM>(manufacturingInfo);
            container.StoreLocationName = storeLocation.LocationName;
            container.ProductConfigurations.StoreItemVM = _mapper.Map<StoreItemVM>(transDetails.StoreItem);
            container.ProductConfigurations.StoreItemVM.Quantity = transDetails.QTY;
            var storeTransactionRaw = _db.StoreTransactionsRaw.Include(s=>s.StoreItems).Where(s => s.ManfacturingId == manufacturingInfo.Id&&s.TransType== StoreRawTransTypeEnum.Manfacturing).ToList();
            List<StoreItemRawTransactionVM> vmLst = new List<StoreItemRawTransactionVM>();
            foreach(var item in storeTransactionRaw)
            {
                StoreItemRawTransactionVM vm = new StoreItemRawTransactionVM();
                vm.StoreItemRawName = item.StoreItems.ItemName;
                vm.QTY = item.Qty;
                vm.UnitPrice = item.UnitPrice;
                vmLst.Add(vm);
            }

            container.ProductConfigurations.ItemConfigurationVM = vmLst;
            return container;
            
        }
        public async Task<ManufacturingContainer> GetConfigurationProduct(int Id)
        {
            var vm = new ManufacturingContainer();
            
            vm.ProductConfigurations.StoreItemVM =  _mapper.Map<StoreItemVM>(await _db.StoreItems.Where(s => s.Id == Id).Include(f=>f.Family).FirstOrDefaultAsync());
            vm.ProductConfigurations.Id = Id;
            vm.ProductConfigurations.ItemConfigurationVM = await _db.ItemConfgurations.Include(p => p.StoreItemsRaw)
                .Where(d => d.StoreItemId == Id)
                .Select(obj=>new ItemConfigurationVM
            {
                Id=obj.StoreItemRawId,
                MinimumAmount = obj.MinimumAmount,
                StoreItemsRawName = obj.StoreItemsRaw.ItemName,
                StoreItemRawId = obj.StoreItemRawId,
                UsedQTY = 0,
                CurrentQty =obj.StoreItemsRaw.QTY
            }).ToListAsync();
           
            return vm;
        }


        [ModelValidationWithJsonFeedBackFilter]
        public async Task<FeedBackWithMessages> SaveManufacturing(ManufacturingContainer vm)
        => await _ManufacturingSaveManager.SaveTransactionAsync(SaveManufacturingFunc, vm);




        //start point for all
        private async Task<ManufacturingContainer> SaveManufacturingFunc(ManufacturingContainer vm)
        {
            if (vm.ProductConfigurations.ItemConfigurationVM.Count <= 0)
            {
                throw new Exception("لا يمكن عمل امر تجميع لمنتج لم يتم تحديد له مستلزمات الانتاج");
            }
            var Qty = vm.ProductConfigurations.StoreItemVM.Quantity;
            // Save Manfacturing
            var NewManufacturng = _mapper.Map<ManufacturingBatch>(vm.ManufacturingInfoVM);
            await _db.AddAsync(NewManufacturng);
            await _db.SaveChangesAsync();
            if( vm.IsManufacturingForOrder )
            {
                NewManufacturng.BatchNumber = $"{NewManufacturng.Id}-{vm.Order.NoOfOrder}";
                await _db.SaveChangesAsync();
            }
            // Store Raw Updates
            var CostItem = await _ManufacturngTransManager.DoItemRawTransactions(vm,NewManufacturng.Id);
            // Update Product
            var item = await _db.StoreItems.FindAsync(vm.ProductConfigurations.StoreItemVM.Id);
            item.UpdateQTY(Qty, true);
            item.UpdateBalance(CostItem, true);
            _db.StoreItems.Update(item);
            // Store Transaction
            int StoreTransId = await _StoreTransactionManager.DoStoreTransManufacturing(vm, NewManufacturng.Id);
            //Update Store Location Balance
            var LocationBalanceAfter =await _storeLocBalanceTransManager.UpdateStoreLocationBalanceForProduct(item.Id, vm.StoreLocationId, Qty, true);
            // Store Transaction
            await _storeTransDetailsManager.DoStoreTransactionDetails(Qty, item, vm.StoreLocationId, StoreTransId, NewManufacturng.Id, LocationBalanceAfter);
            if(vm.param>0)
            {
                await _orderConfigureManager.UpdateOrderDetailStatus(vm.param);
            }
            return vm;
        }


       public async Task<ManufacturingContainer> GetExtraConfigurationForOrder(int Id)
        {
            var container = await _orderConfigureManager.GetExtraConfiguration(Id);
            var order = await _orderManager.GetOrderByDetailsId(container.param);
            container.Order = order;
            container.IsManufacturingForOrder = true;
            return container;
        }


    }
}
