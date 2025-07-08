using AutoMapper;
using Data.Services;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Areas.Store.ViewModel.Transaction;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Extensions;
using MyInfrastructure.Filters;
using MyInfrastructure.Model;
using Services;
using Syncfusion.DocIO.DLS;

namespace Horizon.Areas.Store.Services
{
    public class StoreItemRawManager : GenericSettingsManager<StoreItemsRaw, StoreItemRawVM>
    {

        private readonly SaveManager<StoreItemRawDestroyVM> _saveDestroyManager;

        public StoreItemRawManager(ApplicationDbContext db, IMapper mapper, SaveManager<StoreItemRawVM> saveManager,
             SaveManager<StoreItemRawDestroyVM> saveDestroyManager) : base(db, mapper, saveManager)
        {
            _saveDestroyManager = saveDestroyManager;
        }



        public async Task<List<StoreItemRawVM>> GetStoreItemsRawByIds(List<int> ids)
        {
            var storeItemsRaw = await _db.StoreItemsRaw.Where(obj => ids.Contains(obj.Id)).ToListAsync();
            var storeItemsRawVM = _mapper.Map<List<StoreItemRawVM>>(storeItemsRaw);
            return storeItemsRawVM;
        }
        public async Task<List<StoreItemRawVM>> GetStoreItemRawTeaks()
        {
            var teaks = await _db.StoreItemsRaw.Where(s => s.RawItemTypeId == 1).ToListAsync();
            var teaksVM = _mapper.Map<List<StoreItemRawVM>>(teaks);
            return teaksVM;
        }

        public async Task<List<StoreItemRawVM>> GetStoreItemRawNoTeaks()
        {
            var Noteaks =await _db.StoreItemsRaw.Where(s => s.RawItemTypeId > 1).ToListAsync();
            var NoteaksVM = _mapper.Map<List<StoreItemRawVM>>(Noteaks);
            return NoteaksVM;
        }

        [ModelValidationWithJsonFeedBackFilter]
        public async Task<FeedBackWithMessages> SaveDestroyOperation(StoreItemRawDestroyVM vm)
       => await _saveDestroyManager.SaveTransactionAsync(SaveDestroyAmountFunc, vm);




        //start point for all
        private async Task<StoreItemRawDestroyVM> SaveDestroyAmountFunc(StoreItemRawDestroyVM vm)
        {

            var DestroyedQty = vm.DestroyQty;
            var QtyBeforeDestory = vm.StoreItemRaw.QTY;
            var BalanceBeforeDestory = vm.StoreItemRaw.Balance;
            var unitPrice = BalanceBeforeDestory / QtyBeforeDestory;
            var BalanceAfterDestroy = DestroyedQty * unitPrice;
            var item = await _db.StoreItemsRaw.FindAsync(vm.StoreItemRaw.Id);
            item.UpdateQTY(DestroyedQty, false);
            item.UpdateBalance(BalanceAfterDestroy, false);
            item.UpdateDestroyedQty(DestroyedQty, true);
            _db.StoreItemsRaw.Update(item);

            var unitPriceTrans =  await UpdateStoreItemRawTrans(vm.PurchasingId, item.Id, vm.DestroyQty);
            await DoDestroyTrans(vm, item, unitPriceTrans);
        
            return vm;
        }


        private async Task<decimal?> UpdateStoreItemRawTrans(int PurchasingId, int storeItemId, decimal DestroyQty)
        {
            var item = _db.StoreTransactionsRaw.FirstOrDefault(s => s.StoreItemId == storeItemId &&
            s.TransType == StoreRawTransTypeEnum.Purchase && s.isEmpty == false&&s.PurchaseId==PurchasingId);
            if (item == null) throw new Exception("لا يوجد تكفى للتجميع.");
            if (item.RestQty >= DestroyQty && DestroyQty > 0)
            {
                await CalcDestroyAmount(item, DestroyQty);
                return item.UnitPrice;
            }
            else
            {
                throw new Exception("ادخل قيمة تالف اكبر من قيمة المتبقى للمادة.");
            }
            
        }
        private async Task CalcDestroyAmount(StoreTransactionsRaw item, decimal DestroyQty)
        {
            var restOld = item.RestQty;
            var newRest = restOld - DestroyQty;
            if (newRest >= 0)
            {
                item.UpdateDestroyAmount(DestroyQty, true);
                item.RestQty = newRest;
            }
            if (item.RestQty == 0)
            {
                item.isEmpty = true;
            }
            _db.Update(item);
            await _db.SaveChangesAsync();
        }
        private async Task DoDestroyTrans(StoreItemRawDestroyVM vm,StoreItemsRaw storeitemraw,decimal? unitPrice)
        {
            var TransDate = vm.DateOfDestroy.ToEgyptionDate();
            var StoreTrans = new StoreTransactionsRaw();
            StoreTrans.QtyBalanceAfter = storeitemraw.QTY;
            StoreTrans.AmountBalanceAfter = storeitemraw.Balance;
            StoreTrans.TransDate = TransDate;
            StoreTrans.PurchaseId = vm.PurchasingId;
            StoreTrans.TransType = StoreRawTransTypeEnum.Destoryed;
            StoreTrans.isEmpty = false;
            StoreTrans.RestQty = 0;
            StoreTrans.TotalWithrd = 0;
            StoreTrans.StoreItemId = storeitemraw.Id;
            StoreTrans.UnitPrice = unitPrice;
            StoreTrans.Qty = vm.DestroyQty;
            await _db.AddAsync(StoreTrans);
            await _db.SaveChangesAsync();
        }



        public async Task<(List<StoreItemRawVM> items,int totalCount)> GetStoreItemRawByName(string term,int page,int pageSize)
        {
            var query = _db.StoreItemsRaw
                 .Where(x => x.ItemName.Contains(term))
                 .OrderBy(x => x.ItemName);

            var totalCount = query.Count();
            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new StoreItemRawVM { Id = x.Id, ItemName = x.ItemName })
                .ToList();
            return (items, totalCount);
        }


    }
}
