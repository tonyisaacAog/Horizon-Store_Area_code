using AutoMapper;
using Data.Services;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Orders.ViewModel.Container;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;

namespace Horizon.Areas.Orders.Services
{
    public class OrderConfigureManager
    {
        private readonly ApplicationDbContext _db;
        public readonly SaveManager<OrderContainer> _saveManager;
        public readonly SaveManager<OrderConfigureContainer> _saveManagerConfigue;
        private readonly IMapper _mapper;
        public OrderConfigureManager(
             SaveManager<OrderContainer> saveManager, IMapper mapper,
             ApplicationDbContext db,
             SaveManager<OrderConfigureContainer> saveManagerConfigue)
        {
            _mapper = mapper;
            _saveManager = saveManager;
            _db = db;
            _saveManagerConfigue = saveManagerConfigue;
        }
        public async Task<ManufacturingContainer> GetExtraConfiguration(int detailsId)
        {

            var details = _db.OrderDetails.Include(c => c.OrderConfigure).ThenInclude(raw => raw.StoreItems).FirstOrDefault(d => d.Id == detailsId);

            if (details.OrderConfigure.Count > 0)
            {
                var storeitemvm = _mapper.Map<StoreItemVM>(await _db.StoreItems.Where(s => s.Id == details.ProductId).Include(f => f.Family).FirstOrDefaultAsync());
                var Container = new ManufacturingContainer();
                var itemconfiguration =
                    details.OrderConfigure.Select(obj => new ItemConfigurationVM()
                    {
                        Id = obj.StoreItemId,
                        MinimumAmount = (int)obj.Qty,
                        StoreItemsRawName = obj.StoreItems.ItemName,
                        StoreItemRawId = obj.StoreItemId,
                        UsedQTY = (int)obj.Qty * details.QTY,
                        CurrentQty = obj.StoreItems.QTY
                    }).ToList();
                Container.ProductConfigurations.ItemConfigurationVM = itemconfiguration;
                Container.ProductConfigurations.StoreItemVM = storeitemvm;
                Container.ProductConfigurations.StoreItemVM.Quantity = details.QTY;
                Container.RedirectUrl = "/Orders/Order/ProcessOrderDetails/" + details.OrderId;
                Container.param = detailsId;
                return Container;
            }
            else
            {
                var Container = await GetConfigurationProduct(details.ProductId);
                Container.RedirectUrl = "/Orders/Order/ProcessOrderDetails/" + details.OrderId;
                Container.ProductConfigurations.StoreItemVM.Quantity = details.QTY;
                Container.param = detailsId;
                return Container;
            }
        }



        public async Task<ManufacturingContainer> GetConfigurationProduct(int Id)
        {
            var vm = new ManufacturingContainer();

            vm.ProductConfigurations.StoreItemVM = _mapper.Map<StoreItemVM>(await _db.StoreItems.Where(s => s.Id == Id).Include(f => f.Family).FirstOrDefaultAsync());
            vm.ProductConfigurations.Id = Id;
            vm.ProductConfigurations.ItemConfigurationVM = await _db.ItemConfgurations.Include(p => p.StoreItemsRaw)
                .Where(d => d.StoreItemId == Id)
                .Select(obj => new ItemConfigurationVM
                {
                    Id = obj.StoreItemRawId,
                    MinimumAmount = obj.MinimumAmount,
                    StoreItemsRawName = obj.StoreItemsRaw.ItemName,
                    StoreItemRawId = obj.StoreItemRawId,
                    UsedQTY = 0,
                    CurrentQty = obj.StoreItemsRaw.QTY
                }).ToListAsync();

            return vm;
        }


        public async Task UpdateOrderDetailStatus(int detailsId)
        {
            try
            {

                var details = await _db.OrderDetails.FirstOrDefaultAsync(obj => obj.Id == detailsId);
                if (details.IsManufacturing)
                {
                    throw new Exception("قد تم تجميع هذا العنصر");
                }
                details.IsManufacturing = true;
                _db.Update(details);
                await _db.SaveChangesAsync();
                await CheckAndUpdateOrderStatus(details.OrderId);
            }
            catch { throw; }
        }

        public async Task CheckAndUpdateOrderStatus(int orderId)
        {
            try
            {

                var completeOrder = await _db.OrderDetails.Where(obj => obj.OrderId == orderId).AnyAsync(obj => obj.IsManufacturing == false);
                if (!completeOrder)
                {
                    var order = await _db.Orders.FirstOrDefaultAsync(obj => obj.Id == orderId);
                    if (order != null)
                    {
                        order.IsProcess = true;
                        _db.Update(order);
                        await _db.SaveChangesAsync();
                    }
                }
            }
            catch { throw; }
        }
    }
}
