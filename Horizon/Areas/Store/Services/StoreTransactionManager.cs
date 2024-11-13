using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Main;
using Horizon.Data;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Horizon.Areas.Manufacturing.VewModels;
using MyInfrastructure.Extensions;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Store.ViewModel.Main;
using BoldReports.RDL.DOM;
using Horizon.Areas.Store.ViewModel.Settings;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.ViewModel.Transaction;
using Data.Services;
using MyInfrastructure.Model;

namespace Horizon.Areas.Store.Services
{
    public class StoreTransactionManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly SaveManager<(SalesContainer,int)> _SalesSaveManager;

        public StoreTransactionManager(ApplicationDbContext db,IMapper mapper,SaveManager<(SalesContainer, int)> SalesSaveManager)
        {
            _db = db;
            _mapper = mapper;
            _SalesSaveManager = SalesSaveManager;
        }




        public async Task<FeedBackWithMessages> CreateItemRawTransaction((SalesContainer sales,int saledId) param)
            => await _SalesSaveManager.SaveTransactionAsync(DoItemRawTransactions,param);



        public async Task<(SalesContainer vm, int saleId)> DoItemRawTransactions((SalesContainer vm, int saleId) param)
        {
            try
            {
                var TransDate = DateTime.Now;
                var StoreTransRawList = new List<StoreTransactionsRaw>();
                decimal CostItem = 0;
                foreach( var itemRaw in param.vm.SaleItemRawDetails )
                {
                    var raw = await _db.StoreItemsRaw.FindAsync(itemRaw.StoreItemId);
                    if( raw != null && raw.QTY < itemRaw.QTY )
                    {
                        throw new Exception($"كمية المادة الخام غير متوفر لهذا العنصر {raw.ItemName}");
                    }
                    await UpdateStoreItemRawTrans(raw.Id,itemRaw.QTY);
                    var currentQuantty = raw.QTY;
                    raw.UpdateQTY(itemRaw.QTY,false);
                    var unitPrice = raw.Balance / currentQuantty;
                    CostItem += itemRaw.QTY * unitPrice;
                    raw.UpdateBalance(itemRaw.QTY * unitPrice,false);
                    _db.Update(raw);
                    var StoreTransRaw = new StoreTransactionsRaw();
                    StoreTransRaw.TransType = StoreRawTransTypeEnum.Sales;
                    StoreTransRaw.StoreItemId = itemRaw.StoreItemId;
                    StoreTransRaw.Qty = itemRaw.QTY;
                    StoreTransRaw.QtyBalanceAfter = raw.QTY;
                    StoreTransRaw.AmountBalanceAfter = raw.Balance;
                    StoreTransRaw.TransDate = TransDate;
                    StoreTransRaw.SaleId = param.saleId;
                    StoreTransRawList.Add(StoreTransRaw);

                }
                await _db.AddRangeAsync(StoreTransRawList);
                await _db.SaveChangesAsync();
                return param;
            }catch(Exception ex )
            {
                throw ex;
            }
        }
        private async Task UpdateStoreItemRawTrans(int storeItemId,decimal usedQty)
        {
            var item = _db.StoreTransactionsRaw.First(s => s.StoreItemId == storeItemId &&
            s.TransType == StoreRawTransTypeEnum.Purchase && s.isEmpty == false);
            if( item == null ) throw new Exception("الكمية غير كافية.");
            if( item.RestQty >= usedQty && usedQty > 0 )
            {
                await CalcTotalWithrd(item,usedQty);
                return;
            }
            else if( item.RestQty < usedQty && usedQty > 0 )
            {
                var restOld = await CalcTotalWithrd(item,usedQty);
                await UpdateStoreItemRawTrans(storeItemId,( usedQty - restOld ));
            }
            else
            {
                return;
            }

        }
        private async Task<decimal> CalcTotalWithrd(StoreTransactionsRaw item,decimal usedQty)
        {
            var restOld = item.RestQty;
            var newRest = restOld - usedQty;
            if( newRest < 0 )
            {
                item.UpdateTotalWithrd(restOld,true);
                item.RestQty = 0;
            }
            else
            {
                item.UpdateTotalWithrd(usedQty,true);
                item.RestQty = newRest;
            }
            if( item.RestQty == 0 )
            {
                item.isEmpty = true;
            }
            _db.Update(item);
            await _db.SaveChangesAsync();
            return restOld;
        }


        public async Task<int> DoStoreTransManufacturing(ManufacturingContainer vm,int SourceId)
        {
            var StoreTransaction = new StoreTransactions()
            {
                SourceId = SourceId,
                DestinationId = vm.StoreLocationId,
                SourceType = StoreTransSourceTypes.Manufacturing,
                DestinationType = StoreTransSourceTypes.Store,
                TransDate = vm.ManufacturingInfoVM.BatchDate.ToEgyptionDate(),
                TransType = StoreTransTypeEnum.Manufacturing,
                Descrpition="",
            };
            await _db.AddAsync(StoreTransaction);
            await _db.SaveChangesAsync();
            return StoreTransaction.Id;
        }

        public async Task<int> DoStoreTransSales(SalesContainer vm, int SourceId)
        {
            var StoreTransaction = new StoreTransactions()
            {
                SourceId = SourceId,
                DestinationId = vm.Client.Id,
                SourceType = StoreTransSourceTypes.Store,
                DestinationType = StoreTransSourceTypes.Client,
                TransDate = vm.SaleInfo.SalesDate.ToEgyptionDate(),
                TransType = StoreTransTypeEnum.Sale,
                Descrpition = "",
                StoreLocationId = vm.SaleInfo.StoreLocationId,
            };
            await _db.AddAsync(StoreTransaction);
            await _db.SaveChangesAsync();
            return StoreTransaction.Id;
        }

        public async Task<int> DoStoreTansDestroy(StoreItemDestoryVM vm)
        {
            var StoreTransaction = new StoreTransactions()
            {
                SourceId = vm.StoreLocationId,
                DestinationId = vm.StoreLocationId,
                SourceType = StoreTransSourceTypes.Store,
                DestinationType = StoreTransSourceTypes.Store,
                TransDate = vm.DateOfDestroy.ToEgyptionDate(),
                TransType = StoreTransTypeEnum.Destory,
                Descrpition = vm.Description,
                StoreLocationId = vm.StoreLocationId,
            };
            await _db.AddAsync(StoreTransaction);
            await _db.SaveChangesAsync();
            return StoreTransaction.Id;
        }
    }
}
