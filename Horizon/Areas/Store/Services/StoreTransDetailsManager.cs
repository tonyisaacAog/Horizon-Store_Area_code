using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Transaction;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;

namespace Horizon.Areas.Store.Services
{
    public class StoreTransDetailsManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly StoreLocBalanceTransManager _storeLocBalanceTransManager;

        public StoreTransDetailsManager(ApplicationDbContext db,
                                            IMapper mapper,
                                            StoreLocBalanceTransManager storeLocBalanceTransManager)
        {
            _db = db;
            _mapper = mapper;
            _storeLocBalanceTransManager = storeLocBalanceTransManager;
        }

       

        public async Task DoStoreTransactionDetails(int Qty, StoreItem item, int StoreLocationId, int StoreTransId, int ManufacturingId, int QtyAfter)
        {
         
            var StoreTrans = new StoreTransactionDetails()
            {
                StoreTransId = StoreTransId,
                StoreItemId = item.Id,
                QTY = Qty,
                UnitPrice =   item.Balance/ item.CurrentQty,
                QtyBalanceAfterDestination = QtyAfter,
                AmountBalanceAfter = item.Balance,
                ManfactId = ManufacturingId,
                ManfactUnitCost = 0,
            };
            await _db.AddAsync(StoreTrans);
            await _db.SaveChangesAsync();
        }


        private async Task<int> checkItemInStoreAndUpdateQty(int itemId,int StoreLocationId,int Qty)
        {

            var itemInStoreBala = _db.StoreLocationsBalance.Where(s => s.LocationId == StoreLocationId && s.StoreItemId == itemId).FirstOrDefault();
            int beforeQty = itemInStoreBala?.QTY??0;
            if (itemInStoreBala==null)
            {
                throw new Exception("هذا المنتج غير موجود فى المخزن");
            }
            else
            {
                await _storeLocBalanceTransManager.UpdateStoreLocationBalanceForProduct(itemId, StoreLocationId, Qty, false);
            }
            return beforeQty;
        }

        public async Task DoSaleTransactionsDetails(SalesContainer vm, int StoreTransId)
        {
            var TransDate = vm.SaleInfo.SalesDate.ToEgyptionDate();
            var StoreTransDetailsList = new List<StoreTransactionDetails>();
            foreach (var item in vm.SaleDetails)
            {
                var BeforQty = await checkItemInStoreAndUpdateQty(item.StoreItemId, vm.SaleInfo.StoreLocationId,item.QTY);
                var product = await _db.StoreItems.FindAsync(item.StoreItemId);
                product.UpdateQTY(item.QTY, false);
                product.UpdateBalance(item.QTY * item.UnitPrice, false);
                _db.Update(product);
                var StoreTransDetails = _mapper.Map<StoreTransactionDetails>(item);
                StoreTransDetails.UnitPrice = item.UnitPrice;
                StoreTransDetails.QtyBalanceAfterSource = BeforQty - item.QTY;
                StoreTransDetails.AmountBalanceAfter = product.Balance;
                StoreTransDetails.StoreTransId = StoreTransId;
                StoreTransDetails.QTY = item.QTY;
                StoreTransDetailsList.Add(StoreTransDetails);
            }
            await _db.AddRangeAsync(StoreTransDetailsList);
            await _db.SaveChangesAsync();
        }


        public async Task<List<SaleStoreTransactionVM>> GetSalesDetails(int SalesId)
        {
            var storetrans = await _db.StoreTransactions.Where(s => s.SourceId == SalesId && s.TransType == StoreTransTypeEnum.Sale).FirstOrDefaultAsync();
            return _mapper.Map<List<SaleStoreTransactionVM>>
                (await _db.StoreTransactionDetails.Where(t => t.StoreTransId == storetrans.Id).Include(item => item.StoreItem).ToListAsync());
        }



        public async Task DoStoreTransactionDetailsForDestroyQty(int Qty, StoreItem item, int StoreTransId, int QtyAfter)
        {

            var StoreTrans = new StoreTransactionDetails()
            {
                StoreTransId = StoreTransId,
                StoreItemId = item.Id,
                QTY = Qty,
                UnitPrice = item.Balance / item.CurrentQty,
                QtyBalanceAfterDestination = QtyAfter,
                AmountBalanceAfter = item.Balance,
                ManfactId = 0,
                ManfactUnitCost = 0,
                QtyBalanceAfterSource = QtyAfter,
               
            };
            await _db.AddAsync(StoreTrans);
            await _db.SaveChangesAsync();
        }
    }
}
