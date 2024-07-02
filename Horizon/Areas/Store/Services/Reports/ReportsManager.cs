using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Horizon.Areas.Store.ViewModel.ItemRawReport;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Reports;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Areas.Store.ViewModel.Transaction;
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
                    .Where (trans => trans.StoreItemId == card.Search.StoreItemId
                            && trans.StoreTransactions.TransDate >= startDate
                            && trans.StoreTransactions.TransDate < endDate
                            && trans.StoreTransactions.TransType
                            != StoreTransTypeEnum.ChangeLocation).ToListAsync();

            foreach (var item in transDetails)
            {
                var ST = new StoreTransactionDetailsVM();
                ST.Id = item.Id;
                ST.QTY = item.QTY;
                ST.StoreItemId = item.StoreItemId;
                ST.TransDate = item.StoreTransactions.TransDate.ToEgyptianDate();
                ST.TransType = item.StoreTransactions.TransType;
                if (item.StoreTransactions.TransType == StoreTransTypeEnum.Sale)
                    ST.QtyAfter = item.QtyBalanceAfterSource??0;
                else if (item.StoreTransactions.TransType == StoreTransTypeEnum.Manufacturing)
                    ST.QtyAfter = item.QtyBalanceAfterDestination??0;
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
            if(countBefore>0)
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


            if (card.TransactionDetails.Count > 0)
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

            foreach (var item in transDetails)
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
                    item.TransType == StoreRawTransTypeEnum.Purchase? "مشتريات":
                    item.TransType == StoreRawTransTypeEnum.Sales?"مبيعات":"تالف";
                ST.UnitPrice = item.UnitPrice;
                if (item.TransType == StoreRawTransTypeEnum.Manfacturing)
                {
                    ST.ReferanceId = item.ManfacturingId;
                }
                else if (item.TransType == StoreRawTransTypeEnum.Purchase)
                {
                    ST.ReferanceId = item.PurchaseId;
                }
                card.StoreItemRawTransactions.Add(ST);
            }
             var listbefore =await  _db.StoreTransactionsRaw.Where
                    (trans => trans.StoreItemId == card.Search.StoreItemId
                    && trans.TransDate <startDate).OrderByDescending(x=>x.Id).ToListAsync();

            if (listbefore.Count() > 0) 
                card.Search.StartBalance = listbefore.FirstOrDefault()?.QtyBalanceAfter??0;
            else
                card.Search.StartBalance = 0;

            if (transDetails.Count() > 0)
                card.Search.EndBalance = transDetails.OrderByDescending(x => x.Id).FirstOrDefault()?.QtyBalanceAfter??0;
            else
                card.Search.EndBalance = 0;

            return card;
        }


    }
}
