using AutoMapper;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;

namespace Horizon.Areas.Store.Services
{
    public class StoreLocBalanceTransManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StoreLocBalanceTransManager(ApplicationDbContext db,
                                            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> UpdateStoreLocationBalanceForProduct(int ProductId, int StoreLocationId, int Qty, bool Plus)
        {
            var CheckExist = await _db.StoreLocationsBalance.AnyAsync(x => x.LocationId == StoreLocationId
                            && x.StoreItemId == ProductId);
            int CurrentQty = 0;
            if (CheckExist)
            {
                var ItemInStore = await _db.StoreLocationsBalance.FirstOrDefaultAsync(x =>
                                        x.LocationId == StoreLocationId && x.StoreItemId == ProductId);
                ItemInStore.UpdateQTY(Qty, Plus);
                CurrentQty = ItemInStore.QTY;
                _db.Update(ItemInStore);
               
            }
            else
            {
                var NewItem = new StoreLocationsBalance()
                {
                    StoreItemId = ProductId,
                    LocationId = StoreLocationId,
                    QTY = Qty,
                };
                CurrentQty = Qty;
                await _db.AddAsync(NewItem);
               
            }
            await _db.SaveChangesAsync();
            return CurrentQty;
        }

        public async Task<int> UpdateStoreLocationBalanceForDestroyProduct(int ProductId, int StoreLocationId, int DestroyedQty)
        {
            var CheckExist = await _db.StoreLocationsBalance.AnyAsync(x => x.LocationId == StoreLocationId
                            && x.StoreItemId == ProductId);
            int CurrentQty = 0;
            if (CheckExist)
            {
                var ItemInStore = await _db.StoreLocationsBalance.FirstOrDefaultAsync(x =>
                                        x.LocationId == StoreLocationId && x.StoreItemId == ProductId);
                ItemInStore.UpdateQTY(DestroyedQty, false);
                ItemInStore.UpdateDestroyedQTY(DestroyedQty, true);
                CurrentQty = ItemInStore.QTY;
                _db.Update(ItemInStore);
                await _db.SaveChangesAsync();
                return CurrentQty;
            }
            else
            {
                throw new Exception("لايوجد هذا المنتج فى هذا المخزن");
            }

        }


    }
}
