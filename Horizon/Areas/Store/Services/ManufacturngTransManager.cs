using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Main;
using Finance.CurrentAssetModule.Store.Model.Raw;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Store.Models.Raw;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyInfrastructure.Extensions;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Areas.Store.Services
{
    public class ManufacturngTransManager
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly StoreTransactionManager _StoreTransactionManager;
        private readonly StoreTransDetailsManager _StoreTransDetailsManager;

        public ManufacturngTransManager(ApplicationDbContext db,
                                            IMapper mapper,
                                            StoreTransactionManager storeTransactionManager,
                                            StoreTransDetailsManager StoreTransDetailsManager)
        {
            _db = db;
            _mapper = mapper;
            _StoreTransDetailsManager = StoreTransDetailsManager;
            _StoreTransactionManager = storeTransactionManager;
        }



        public async Task<decimal> DoItemRawTransactions(ManufacturingContainer vm, int ManufacturingId)
        {
            var TransDate = vm.ManufacturingInfoVM.BatchDate.ToEgyptionDate();
            var StoreTransRawList = new List<StoreTransactionsRaw>();
            decimal CostItem = 0;
            foreach (var itemRaw in vm.ProductConfigurations.ItemConfigurationVM)
            {
                var raw = await _db.StoreItemsRaw.FindAsync(itemRaw.StoreItemRawId);
                if (raw != null&&raw.QTY<itemRaw.UsedQTY)
                {
                    throw new Exception($"كمية المادة الخام غير متوفر لهذا العنصر {raw.ItemName}");
                }
                await UpdateStoreItemRawTrans(raw.Id,itemRaw.UsedQTY);
                var currentQuantty = raw.QTY;
                raw.UpdateQTY(itemRaw.UsedQTY, false);
                var unitPrice = raw.Balance / currentQuantty;
                CostItem += itemRaw.UsedQTY * unitPrice;
                raw.UpdateBalance(itemRaw.UsedQTY * unitPrice, false);
                _db.Update(raw);
                var StoreTransRaw = new StoreTransactionsRaw();
                StoreTransRaw.TransType = StoreRawTransTypeEnum.Manfacturing;
                StoreTransRaw.StoreItemId = itemRaw.StoreItemRawId;
                StoreTransRaw.Qty = itemRaw.UsedQTY;
                StoreTransRaw.QtyBalanceAfter = raw.QTY;
                StoreTransRaw.AmountBalanceAfter = raw.Balance;
                StoreTransRaw.TransDate = TransDate;
                StoreTransRaw.ManfacturingId = ManufacturingId;
                StoreTransRawList.Add(StoreTransRaw);

            }
            await _db.AddRangeAsync(StoreTransRawList);
            await _db.SaveChangesAsync();
            return CostItem;
        }
        private async Task UpdateStoreItemRawTrans(int storeItemId,decimal usedQty)
        {
            var item= _db.StoreTransactionsRaw.First(s=>s.StoreItemId==storeItemId&&
            s.TransType == StoreRawTransTypeEnum.Purchase&&s.isEmpty == false);
            if(item==null) throw new Exception("لا يوجد تكفى للتجميع.");
            if (item.RestQty >= usedQty&&usedQty>0)
            {
                await CalcTotalWithrd(item, usedQty);
                return;
            }
            else if (item.RestQty<usedQty&&usedQty>0)
            {
               var restOld =  await CalcTotalWithrd(item, usedQty);
                await UpdateStoreItemRawTrans(storeItemId, (usedQty-restOld));
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
            if (newRest < 0)
            {
                item.UpdateTotalWithrd(restOld, true);
                item.RestQty = 0;
            }
            else
            {
                item.UpdateTotalWithrd(usedQty, true);
                item.RestQty = newRest;
            }
            if (item.RestQty == 0)
            {
                item.isEmpty = true;
            }
            _db.Update(item);
            await _db.SaveChangesAsync();
            return restOld;
        }

    }
}
