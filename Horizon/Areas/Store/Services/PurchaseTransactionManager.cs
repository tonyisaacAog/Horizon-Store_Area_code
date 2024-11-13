using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.ViewModel.Transaction;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;
using Services;

namespace Horizon.Areas.Store.Services
{
    public class PurchaseTransactionManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PurchaseTransactionManager(ApplicationDbContext db,
                                            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task DoPurchaseTransactions (PurchaseContainer vm , int PurchaseId)
        {
            await DoPurchaseTransactionGeneral(vm.PurchaseDetails, PurchaseId);
        }

        public async Task DoPurchaseTransactionsForProduct(PurchaseContainerForProduct vm, int PurchaseId)
        {
            await DoPurchaseTransactionGeneral(vm.PurchaseDetails, PurchaseId);
        }

        public async Task DoPurchaseTransactionsForProduct(PurchaseContainer vm,int PurchaseId)
        {
            var StoreItemRawList = new List<PurchaseStoreTransactionVM>();
            vm.PurchaseStoreItemDetails.ForEach(item =>
            {
                var configurationProduct = _db.ItemConfgurations.Where(obj => obj.StoreItemId == item.StoreItemId).ToList();
                var storeItemRaws = configurationProduct.Select(obj => new PurchaseStoreTransactionVM
                {
                    StoreItemId = obj.StoreItemRawId,
                    Qty = obj.MinimumAmount * item.Qty,
                    UnitPrice = 1,
                    TransType = item.TransType
                });
                StoreItemRawList.AddRange(storeItemRaws);
            });
            await DoPurchaseTransactionGeneral(StoreItemRawList,PurchaseId);
        }



        public async Task<List<PurchaseStoreTransactionVM>> GetPurchasesDetails(int purchasesId)
        {
            return _mapper.Map<List<PurchaseStoreTransactionVM>>
                (await _db.StoreTransactionsRaw.Where(t => t.PurchaseId == purchasesId).Include(itemRaw=>itemRaw.StoreItems).ToListAsync());
        }

        private async Task DoPurchaseTransactionGeneral(List<PurchaseStoreTransactionVM> PurchaseDetails,int PurchaseId)
        {
            var TransDate = DateTime.Now;
            var StoreTransList = new List<StoreTransactionsRaw>();
            foreach (var item in PurchaseDetails)
            {
                var raw = await _db.StoreItemsRaw.FindAsync(item.StoreItemId);
                raw.UpdateQTY(item.Qty, true);
                raw.UpdateBalance(item.Qty * item.UnitPrice, true);
                _db.Update(raw);
                var StoreTrans = _mapper.Map<StoreTransactionsRaw>(item);
                StoreTrans.QtyBalanceAfter = raw.QTY;
                StoreTrans.AmountBalanceAfter = raw.Balance;
                StoreTrans.TransDate = TransDate;
                StoreTrans.PurchaseId = PurchaseId;
                StoreTrans.TransType = StoreRawTransTypeEnum.Purchase;
                StoreTrans.isEmpty = false;
                StoreTrans.RestQty = item.Qty;
                StoreTrans.TotalWithrd = 0;
                StoreTransList.Add(StoreTrans);
            }
            await _db.AddRangeAsync(StoreTransList);
            await _db.SaveChangesAsync();
        }

       



    }
}
