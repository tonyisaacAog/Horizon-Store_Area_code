using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.ItemRawReport;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Reports;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;


namespace Horizon.Areas.Store.Services.Reports
{
    public class ReportsManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ReportsManager(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<StoreItemRawAmountReportsVM>> GetAmountBalanceStoreItemRaw(int id,string WarningLimit)
        {
            var StoreItemRaw = _db.StoreItemsRaw.Include(obj => obj.RawItemType).AsQueryable();
            var StoreItemRawModel = new List<Models.Settings.StoreItemsRaw>();
            if( id == 0 )
                StoreItemRaw = StoreItemRaw;
            else
            {
                StoreItemRaw = StoreItemRaw
                    .Where(obj => obj.RawItemTypeId == id);
            }
            if( !string.IsNullOrEmpty(WarningLimit) && WarningLimit.ToLower().Trim() == "on" )
            {
                StoreItemRaw = StoreItemRaw.Where(obj => obj.WarningLimit > obj.QTY);
            }
            StoreItemRawModel = await StoreItemRaw.ToListAsync();
            var storeItemRawBalanceVM = _mapper.Map<List<StoreItemRawAmountReportsVM>>(StoreItemRawModel);
            return storeItemRawBalanceVM;
        }

        public async Task<List<StoreItemAmountInStoreReportsVM>> GetAmountBalanceStoreItemInStore(int storeLocationId=0)
        {
            var storeWithStoreItem = _db.StoreLocationsBalance.Include(obj => obj.StoreItemDetails).Include(obj => obj.Locations).AsQueryable();

            if( storeLocationId > 0 )
                storeWithStoreItem = storeWithStoreItem.Where(obj => obj.LocationId == storeLocationId);
                
            var storeItemBalanceVM = _mapper.Map<List<StoreItemAmountInStoreReportsVM>>((await storeWithStoreItem.ToListAsync()));
            return storeItemBalanceVM;
        }
        public async Task<List<StoreItemAmountReportsVM>> GetAmountBalanceStoreItem()
        {
            var storeWithStoreItem = await _db.StoreItems.ToListAsync();
            var storeItemBalanceVM = _mapper.Map<List<StoreItemAmountReportsVM>>(storeWithStoreItem);
            return storeItemBalanceVM;
        }
        public async Task<List<StoreItemAmountNotCollectReportVM>> GetAmountBalanceStoreItemNotCollectInGeneral()
        {
            var storeItemNotCollectContainer = new List<StoreItemAmountNotCollectReportVM>();
            var storeWithStoreItem = await _db.StoreItems.ToListAsync();
            foreach( var storeItem in storeWithStoreItem )
            {
                var itemConfigurations = await _db.ItemConfgurations.Where(obj => obj.StoreItemId == storeItem.Id).ToListAsync();
                var numberOfProduct = await GetNumProductCanMadeOfMatiaral(itemConfigurations);
                storeItemNotCollectContainer.Add(new StoreItemAmountNotCollectReportVM { StoreItemName = storeItem.ProductName,StoreItemMainQuantity = numberOfProduct,StoreItemRestQuantity=0 });
            }
            return storeItemNotCollectContainer;
        }

         public async Task<List<StoreItemAmountNotCollectReportVM>> GetAmountBalanceStoreItemNotCollectFromPurchaseQty(SearchForProductVM searchvm)
        {
            var storeItemNotCollectContainer = new List<StoreItemAmountNotCollectReportVM>();
            var storeWithStoreItem = _db.StoreItems.AsQueryable();
            if( searchvm.StoreItemId > 0 )
                storeWithStoreItem = storeWithStoreItem.Where(obj => obj.Id == searchvm.StoreItemId);
            var storeWithStoreItemLst = await storeWithStoreItem.ToListAsync();
            foreach (var storeItem in storeWithStoreItemLst )
            {
                var purchases = await _db.Purchasings.Where(obj => obj.StoreItemId == storeItem.Id
                                   && obj.PurchasingDate.Value.Date >= searchvm.StartDate.ToEgyptionDate().Date
                                   && obj.PurchasingDate.Value.Date < searchvm.EndDate.ToEgyptionDate().Date).ToListAsync();
                var itemConfigurations = await _db.ItemConfgurations.Include(obj => obj.StoreItemsRaw)
                    .Where(obj => obj.StoreItemId == storeItem.Id && obj.StoreItemsRaw.RawItemTypeId == 1).ToListAsync();
                foreach (var purchase in purchases)
                {
                    var storeTransForPurchase = await _db.StoreTransactionsRaw.Include(obj => obj.StoreItems).Where(obj => obj.PurchaseId == purchase.Id).ToListAsync();
                    var numberOfProduct = await GetNumProductCanMadeOfMatiaral(itemConfigurations, storeTransForPurchase,true);
                    if (numberOfProduct > 0)
                        storeItemNotCollectContainer.Add(new StoreItemAmountNotCollectReportVM { StoreItemName = storeItem.ProductName, StoreItemMainQuantity = numberOfProduct, PriceItemsRawPurchase = purchase.PriceItemsRaw });
                }
                //if (purchases.Count == 0)
                //{
                //    storeItemNotCollectContainer.Add(new StoreItemAmountNotCollectReportVM { StoreItemName = storeItem.ProductName, StoreItemMainQuantity = 0, PriceItemsRawPurchase = 0 });
                //}
            }
            return storeItemNotCollectContainer;
        }
        public async Task<List<StoreItemAmountNotCollectReportVM>> GetAmountBalanceStoreItemNotCollectFromPurchase(SearchForProductVM searchvm)
        {
            var storeItemNotCollectContainer = new List<StoreItemAmountNotCollectReportVM>();
            var storeWithStoreItem = _db.StoreItems.AsQueryable();
            if( searchvm.StoreItemId > 0 )
                storeWithStoreItem = storeWithStoreItem.Where(obj => obj.Id == searchvm.StoreItemId);
            var storeWithStoreItemLst = await storeWithStoreItem.ToListAsync();
            foreach( var storeItem in storeWithStoreItemLst )
            {
                var purchases = await _db.Purchasings
                        .Where(obj => obj.StoreItemId == storeItem.Id
                                   && obj.PurchasingDate.Value.Date >= searchvm.StartDate.ToEgyptionDate().Date
                                   && obj.PurchasingDate.Value.Date < searchvm.EndDate.ToEgyptionDate().Date).ToListAsync();
                var itemConfigurations = await _db.ItemConfgurations.Include(obj=>obj.StoreItemsRaw)
                    .Where(obj => obj.StoreItemId == storeItem.Id&&obj.StoreItemsRaw.RawItemTypeId ==1).ToListAsync();
                foreach( var purchase in purchases )
                {
                    var storeTransForPurchase = await _db.StoreTransactionsRaw.Include(obj=>obj.StoreItems).Where(obj => obj.PurchaseId == purchase.Id).ToListAsync();
                    var numberOfProduct = await GetNumProductCanMadeOfMatiaral(itemConfigurations,storeTransForPurchase);
                    if(numberOfProduct>0)
                     storeItemNotCollectContainer.Add(new StoreItemAmountNotCollectReportVM { StoreItemName = storeItem.ProductName, StoreItemMainQuantity = numberOfProduct,PriceItemsRawPurchase = purchase.PriceItemsRaw });
                }
                //if( purchases.Count == 0 )
                //{
                //    storeItemNotCollectContainer.Add(new StoreItemAmountNotCollectReportVM { StoreItemName = storeItem.ProductName,StoreItemRestQuantity = 0,StoreItemMainQuantity = 0,PriceItemsRawPurchase = 0 });
                //}
            }
            return storeItemNotCollectContainer;
        }
        private async Task<int> GetNumProductCanMadeOfMatiaral(List<ItemConfguration> itemConfigurationVM,List<StoreTransactionsRaw> storeTransactionsRaw,bool IsQty=false)
        {
            List<int> lst = new List<int>();
            foreach( var config in itemConfigurationVM )
            {
                if (IsQty)
                {
                    var calc = (int)storeTransactionsRaw.FirstOrDefault(i => i.StoreItemId == config.StoreItemRawId).Qty / config.MinimumAmount;
                    lst.Add(calc);
                }
                else
                {
                    var calc = (int)storeTransactionsRaw.FirstOrDefault(i => i.StoreItemId == config.StoreItemRawId).RestQty / config.MinimumAmount;
                    lst.Add(calc);
                }
            }
            return lst.Count > 0 ? lst.Min() : 0;
        }

        private async Task<int> GetNumProductCanMadeOfMatiaral(List<ItemConfguration> itemConfigurationVM)
        {
            List<int> lst = new List<int>();
            foreach( var config in itemConfigurationVM )
            {
                var calc = (int)_db.StoreItemsRaw.FirstOrDefault(i => i.Id == config.StoreItemRawId).QTY / config.MinimumAmount;
                lst.Add(calc);
            }
            return lst.Count > 0 ? lst.Min() : 0;
        }
        public async Task<ItemCardContainer> GetDataProduct(int ProductId)
        {
            var card = new ItemCardContainer();
            card.StoreItem =
                _mapper.Map<StoreItemVM>(await _db.StoreItems.Include(type => type.Family)
                .Include(brand => brand.StoreBrand)
                .FirstOrDefaultAsync(item => item.Id == ProductId));
            card.StoreLocationBalances = await GetDataStoreBalance(ProductId);
            return card;
        }

        private async Task<List<StoreLocationBalanceVM>> GetDataStoreBalance(int ProductId)
        {
            var StoreLocationBalanceLst =
             _mapper.Map<List<StoreLocationBalanceVM>>(
                 await _db.StoreLocationsBalance
                 .Include(type => type.Locations)
                 .Where(item => item.StoreItemId == ProductId)
                 .ToListAsync());
            return StoreLocationBalanceLst;
        }

        public async Task<TransactionContainer> GetTransactionProduct(TransactionContainer card)
        {
            var endDate = card.Search.EndDate.ToEgyptionDate().AddDays(1);
            var startDate = card.Search.StartDate.ToEgyptionDate();

            var transDetails =
                    await _db.StoreTransactionDetails.Include(s => s.StoreTransactions)
                    .Where(trans => trans.StoreItemId == card.Search.StoreItemId
                            && trans.StoreTransactions.TransDate >= startDate
                            && trans.StoreTransactions.TransDate < endDate
                            && trans.StoreTransactions.TransType
                            != StoreTransTypeEnum.ChangeLocation).ToListAsync();

            foreach( var item in transDetails )
            {
                var ST = new StoreTransactionDetailsVM();
                ST.Id = item.Id;
                ST.QTY = item.QTY;
                ST.StoreItemId = item.StoreItemId;
                ST.TransDate = item.StoreTransactions.TransDate.ToEgyptianDate();
                ST.TransType = item.StoreTransactions.TransType;
                if( item.StoreTransactions.TransType == StoreTransTypeEnum.Sale )
                    ST.QtyAfter = item.QtyBalanceAfterSource ?? 0;
                else if( item.StoreTransactions.TransType == StoreTransTypeEnum.Manufacturing )
                    ST.QtyAfter = item.QtyBalanceAfterDestination ?? 0;
                ST.TransTypeName =
                     item.StoreTransactions.TransType == StoreTransTypeEnum.Start ? "رصيد البداية" :
                    item.StoreTransactions.TransType == StoreTransTypeEnum.Manufacturing ? "تجميع" :
                    item.StoreTransactions.TransType == StoreTransTypeEnum.Buy ? "مشتريات" :
                    item.StoreTransactions.TransType == StoreTransTypeEnum.Sale ? "مبيعات" : "مرتجعات";
                ST.UnitPrice = item.UnitPrice;
                ST.ReferanceId = item.StoreTransactions.SourceId;
                card.TransactionDetails.Add(ST);
            }

            var countBefore = await _db.StoreTransactionDetails.Include(s => s.StoreTransactions)
                 .Where(x => x.StoreTransactions.TransDate < startDate && x.StoreItemId == card.Search.StoreItemId).CountAsync();
            if( countBefore > 0 )
            {
                var BeforeBalance = await _db.StoreTransactionDetails.Include(s => s.StoreTransactions)
                 .Where(x => x.StoreTransactions.TransDate < startDate && x.StoreItemId == card.Search.StoreItemId)
                .Skip(countBefore - 1)
                .Take(1).ToListAsync();
                card.Search.StartBalance = BeforeBalance.FirstOrDefault()?.QtyBalanceAfterDestination;
            }

            else
            {
                card.Search.StartBalance = 0;
            }


            if( card.TransactionDetails.Count > 0 )
            {
                card.Search.EndBalance = card.TransactionDetails?.OrderByDescending(x => x.Id)
                                                .FirstOrDefault()?.QtyAfter;
            }
            else
            {
                card.Search.EndBalance = 0;
            }

            return card;
        }


        public async Task<ItemRawContainer> GetDataStoreItemRaw(int StoreItemRawId)
        {
            var card = new ItemRawContainer();
            card.StoreItemRaw =
                _mapper.Map<StoreItemRawVM>(await _db.StoreItemsRaw.Include(type => type.RawItemType)
                .FirstOrDefaultAsync(item => item.Id == StoreItemRawId));
            return card;
        }




        public async Task<TransactionRawContainer> GetTransactionItemRaw(TransactionRawContainer card)
        {
            var endDate = card.Search.EndDate.ToEgyptionDate().AddDays(1);
            var startDate = card.Search.StartDate.ToEgyptionDate();

            var transDetails =
                    await _db.StoreTransactionsRaw.Where
                    (trans => trans.StoreItemId == card.Search.StoreItemId
                    && trans.TransDate >= startDate
                    && trans.TransDate < endDate).ToListAsync();

            foreach( var item in transDetails )
            {
                var ST = new StoreItemRawTransactionVM();
                ST.Id = item.Id;
                ST.QTY = item.Qty;
                ST.StoreItemId = item.StoreItemId;
                ST.TransDate = item.TransDate.ToEgyptianDate();
                ST.TransType = item.TransType;
                ST.QtyAfter = item.QtyBalanceAfter;
                ST.TransTypeName =
                    item.TransType == StoreRawTransTypeEnum.Manfacturing ? "تصنيع" :
                    item.TransType == StoreRawTransTypeEnum.Purchase ? "مشتريات" :
                    item.TransType == StoreRawTransTypeEnum.Sales ? "مبيعات" : "تالف";
                ST.UnitPrice = item.UnitPrice;
                if( item.TransType == StoreRawTransTypeEnum.Manfacturing )
                {
                    ST.ReferanceId = item.ManfacturingId;
                }
                else if( item.TransType == StoreRawTransTypeEnum.Purchase )
                {
                    ST.ReferanceId = item.PurchaseId;
                }
                card.StoreItemRawTransactions.Add(ST);
            }
            var listbefore = await _db.StoreTransactionsRaw.Where
                   (trans => trans.StoreItemId == card.Search.StoreItemId
                   && trans.TransDate < startDate).OrderByDescending(x => x.Id).ToListAsync();

            if( listbefore.Count() > 0 )
                card.Search.StartBalance = listbefore.FirstOrDefault()?.QtyBalanceAfter ?? 0;
            else
                card.Search.StartBalance = 0;

            if( transDetails.Count() > 0 )
                card.Search.EndBalance = transDetails.OrderByDescending(x => x.Id).FirstOrDefault()?.QtyBalanceAfter ?? 0;
            else
                card.Search.EndBalance = 0;

            return card;
        }

        public async Task<TransactionRawContainer> GetTransactionItemRawForManufactProduct(TransactionRawContainer card)
        {
            var endDate = card.Search.EndDate.ToEgyptionDate().AddDays(1);
            var startDate = card.Search.StartDate.ToEgyptionDate();

            var transDetails =
                    await _db.StoreTransactionsRaw.Include(obj=>obj.StoreItems).Where
                    (trans => trans.StoreItemId == card.Search.StoreItemId
                    && trans.TransType == StoreRawTransTypeEnum.Manfacturing
                    && trans.TransDate >= startDate
                    && trans.TransDate < endDate).ToListAsync();

            foreach( var item in transDetails )
            {
                var ST = new StoreItemRawTransactionVM();
                ST.Id = item.Id;
                ST.QTY = item.Qty;
                ST.StoreItemRawName = item.StoreItems.ItemName;
                ST.StoreItemId = item.StoreItemId;
                ST.TransDate = item.TransDate.ToEgyptianDate();
                ST.TransType = item.TransType;
                ST.QtyAfter = item.QtyBalanceAfter;
                ST.AmountBalanceAfter = item.Qty + item.QtyBalanceAfter;
                ST.TransTypeName =
                    item.TransType == StoreRawTransTypeEnum.Manfacturing ? "تصنيع" :
                    item.TransType == StoreRawTransTypeEnum.Purchase ? "مشتريات" :
                    item.TransType == StoreRawTransTypeEnum.Sales ? "مبيعات" : "تالف";
                ST.UnitPrice = item.UnitPrice;
                if( item.TransType == StoreRawTransTypeEnum.Manfacturing )
                {
                    ST.ReferanceId = item.ManfacturingId;
                }
               
                card.StoreItemRawTransactions.Add(ST);
            }
            var listbefore = await _db.StoreTransactionsRaw.Where
                   (trans => trans.StoreItemId == card.Search.StoreItemId && trans.TransType == StoreRawTransTypeEnum.Manfacturing
                   && trans.TransDate < startDate).OrderByDescending(x => x.Id).ToListAsync();

            if( listbefore.Count() > 0 )
                card.Search.StartBalance = listbefore.FirstOrDefault()?.QtyBalanceAfter ?? 0;
            else
                card.Search.StartBalance = 0;

            if( transDetails.Count() > 0 )
                card.Search.EndBalance = transDetails.OrderByDescending(x => x.Id).FirstOrDefault()?.QtyBalanceAfter ?? 0;
            else
                card.Search.EndBalance = 0;

            return card;
        }
        public async Task<TransactionRawContainerForProduct> GetTransactionItemRawForManufactProduct(SearchForProductVM search)
        {
            var container = new TransactionRawContainerForProduct();
            var productItemRaw = _db.ItemConfgurations.Where(obj => obj.StoreItemId == search.StoreItemId).ToList();
            foreach(var itemRaw in productItemRaw )
            {
                var searchvm = new TransactionRawContainer() { Search = new ViewModel.ItemRawReport.SearchVM { StartDate = search.StartDate,EndDate = search.EndDate,StoreItemId = itemRaw.StoreItemRawId } };
                var trans = await GetTransactionItemRawForManufactProduct(searchvm);
                container.StoreItemRawTransactions.Add(trans);
            }
            container.Search = search;
            return container;
        }
      


    }
}
