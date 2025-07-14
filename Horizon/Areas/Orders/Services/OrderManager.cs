using AutoMapper;
using Data.Services;
using Horizon.Areas.Manufacturing.Services;
using Horizon.Areas.Manufacturing.VewModels;
using Horizon.Areas.Orders.Models;
using Horizon.Areas.Orders.ViewModel;
using Horizon.Areas.Orders.ViewModel.Container;
using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Store.Services;
using Horizon.Areas.Store.ViewModel.Configuration;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Horizon.Models.Shared;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Model;
using Services;

namespace Horizon.Areas.Orders.Services
{
    public class OrderManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ItemConfigureManager _itemConfigureManager;
        private readonly GenericSettingsManager<Client,ClientVM> _clientManager;
        public readonly SaveManager<OrderContainer> _saveManager;
        public readonly SaveManager<OrderForPersonContainer> _saveOrderForPersonManager;
        public readonly SaveManager<OrderConfigureContainer> _saveManagerConfigue;
        private readonly IMapper _mapper;
        private readonly StoreItemManager _storeItemManager;
        public OrderManager(GenericSettingsManager<Client,ClientVM> clientManager,
             SaveManager<OrderContainer> saveManager,IMapper mapper,
             ApplicationDbContext db,ItemConfigureManager itemConfigureManager,
             SaveManager<OrderConfigureContainer> saveManagerConfigue,
             StoreItemManager storeItemManager,
             SaveManager<OrderForPersonContainer> saveOrderForPersonManager)
        {
            _clientManager = clientManager;
            _mapper = mapper;
            _saveManager = saveManager;
            _db = db;
            _itemConfigureManager = itemConfigureManager;
            _saveManagerConfigue = saveManagerConfigue;
            _storeItemManager = storeItemManager;
            _saveOrderForPersonManager = saveOrderForPersonManager;
        }

        public async Task<List<OrderVM>> GetOrders(OrderStatus? Status)
        {
            if( Status == null )
                return _mapper.Map<List<OrderVM>>(await _db.Orders.Include(c => c.Client).OrderBy(obj => obj.DeliveryOrder).ToListAsync());
            else
                return _mapper.Map<List<OrderVM>>(await _db.Orders.Include(c => c.Client).OrderBy(obj => obj.DeliveryOrder).Where(obj=>obj.OrderStatus == Status).ToListAsync());

        }

        public async Task<FeedBackWithMessages> UpdateNotesOrder(OrderVM ordervm)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(obj => obj.Id == ordervm.Id);
            if( order != null )
            {
                order.Notes = ordervm.Notes;
                await _db.SaveChangesAsync();
                return new FeedBackWithMessages { Done = true };
            }
            else
            {
                return new FeedBackWithMessages { Done = false,Messages = new List<string> { "امر الشغل غير موجود" } };
            }
        }
        public async Task<FeedBackWithMessages> ConvertOrderToSaleInvoice(int orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(obj => obj.Id == orderId);
            if( order != null )
            {
                order.IsInvoiceSale = true;
                await _db.SaveChangesAsync();
                return new FeedBackWithMessages { Done = true };
            }
            else
            {
                return new FeedBackWithMessages { Done = false,Messages = new List<string> { "امر الشغل غير موجود" } };
            }
        }
        public async Task<List<OrderVM>> GetProcessOrder()
        {
            var orderLst = await _db.Orders.Include(c => c.Client).Where(o => o.OrderStatus == OrderStatus.Process)
                .OrderBy(obj => obj.DeliveryOrder).ToListAsync();
            return _mapper.Map<List<OrderVM>>(orderLst);
        }
        public async Task<OrderVM> GetOrderById(int orderId)
        {
            return _mapper.Map<OrderVM>(await _db.Orders.Include(obj => obj.Client).FirstOrDefaultAsync(obj => obj.Id == orderId));
        }
        public async Task<OrderVM> GetOrderByDetailsId(int detailsId)
        {
            return _mapper.Map<OrderVM>(await _db.OrderDetails.Include(obj => obj.Order).ThenInclude(obj => obj.Client)
                .Where(obj => obj.Id == detailsId).Select(obj => obj.Order).FirstOrDefaultAsync());
        }
        public async Task<OrderContainer> GetOrderWithDetailsById(int orderId)
        {
            var vmContainer = new OrderContainer();
            var orderWithDEtails = await _db.Orders.Include(obj => obj.OrderDetails).ThenInclude(obj=>obj.Product)
                .Include(obj => obj.Client)
                .Where(obj => obj.Id == orderId).FirstOrDefaultAsync();
            vmContainer.Client = _mapper.Map<ClientVM>(orderWithDEtails.Client);
            vmContainer.OrderDetail = _mapper.Map<List<OrderDetailsVM>>(orderWithDEtails.OrderDetails);
            vmContainer.Order = _mapper.Map<OrderVM>(orderWithDEtails);
            return vmContainer;
        }
        public async Task ChangeOrderSatus(int orderId,OrderStatus orderStatus)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(obj => obj.Id == orderId);
            order.OrderStatus = orderStatus;
            await _db.SaveChangesAsync();
        }
        public async Task<OrderDetailsContainer> GetDetails(int orderId)
        {
            var orderContainer = new OrderDetailsContainer();
            var order = await _db.Orders.Include(c => c.Client).Include(d => d.OrderDetails).ThenInclude(p => p.Product).FirstOrDefaultAsync(o => o.Id == orderId);
            orderContainer.Order = _mapper.Map<OrderVM>(order);
            orderContainer.OrderDetail = _mapper.Map<List<OrderDetailsVM>>(order.OrderDetails);
            List<OrderConfigure> orderConfigure = new List<OrderConfigure>();
            foreach( var v in order.OrderDetails )
            {
                var configureLst = await _db.OrderConfigures.Include(i => i.StoreItems).Where(obj => obj.OrderDetailsId == v.Id && obj.IsNew == true).ToListAsync();
                if( configureLst.Count > 0 )
                    orderConfigure.AddRange(configureLst);
            }
            var orderConfigureVM = _mapper.Map<List<OrderConfigureVM>>(orderConfigure.ToList());

            orderContainer.OrderConfigure = orderConfigureVM;
            return orderContainer;
        }
        public async Task<OrderContainer> NewOrder(int ClientId)
        {
            var orderContainer = new OrderContainer();
            orderContainer.Client = _mapper.Map<ClientVM>(await _clientManager.CheckAndReturn(ClientId));
            return orderContainer;
        }
        public async Task<OrderForPersonContainer> NewOrderForPerson()
        {
            var orderContainer = new OrderForPersonContainer();
            return orderContainer;
        }

        public async Task<FeedBackWithMessages> SaveOrders(OrderContainer vm)
        {
            return await _saveManager.SaveTransactionAsync(SaveOrder,vm);
        }
        public async Task<FeedBackWithMessages> SaveOrdersForPerson(OrderForPersonContainer vm)
        {
            return await _saveOrderForPersonManager.SaveTransactionAsync(SaveOrderForPerson, vm);
        }

        private async Task<OrderForPersonContainer> SaveOrderForPerson(OrderForPersonContainer vm)
        {
            var orderId = await AddOrderForPerson(vm.Order, vm.Order.ClientId);
            await AddOrderDetails(vm.OrderDetail, orderId);
            return vm;
        }
        private async Task<OrderContainer> SaveOrder(OrderContainer vm)
        {
            var orderId = await AddOrder(vm.Order,vm.Client.Id);
            await AddOrderDetails(vm.OrderDetail,orderId);
            return vm;
        }
        private async Task<int> AddOrderForPerson(OrderForPersonVM ordervm, int clientId)
        {
            var order = _mapper.Map<Order>(ordervm);
            order.ClientId = clientId;
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            Order.GenerateSerial(order);
            await _db.SaveChangesAsync();
            return order.Id;
        }
        private async Task<int> AddOrder(OrderVM ordervm,int clientId)
        {
            var order = _mapper.Map<Order>(ordervm);
            order.ClientId = clientId;
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            Order.GenerateSerial(order);
            await _db.SaveChangesAsync();
            return order.Id;
        }
        private async Task AddOrderDetails(List<OrderDetailsVM> orderDetails,int orderId)
        {
            var orderdetails = _mapper.Map<List<OrderDetails>>(orderDetails);
            foreach( var orderdetail in orderdetails )
            {
                orderdetail.OrderId = orderId;
                await _db.OrderDetails.AddAsync(orderdetail);
                await _db.SaveChangesAsync();
            }
        }
        //private async Task AddOrderConfigure(OrderConfigure orderConfigure, int orderDetailId)
        //{

        //}



        //////////////////////////
        ////      configure
        /////////////////////////
        ///

        public async Task<OrderReportContainerVM> OrderReport(int orderId)
        {
            var report = new OrderReportContainerVM();
            var orderDetail = await _db.OrderDetails.Include(obj=>obj.Product).Include(c => c.OrderConfigure)
                                                    .ThenInclude(obj=>obj.StoreItems).Where(d => d.OrderId == orderId).ToListAsync();
            var order = await _db.Orders.Include(obj=>obj.Client).Where(obj => obj.Id == orderId).FirstOrDefaultAsync();           
            foreach(var configure in orderDetail )
            {
                if( configure.OrderConfigure.Count > 0 )
                {
                    var details = configure.OrderConfigure.Where(obj=>obj.StoreItems.RawItemTypeId != 1)
                                                          .Select(obj => new ItemRawReportVM { Name = obj.StoreItems.ItemName,Id = obj.StoreItemId,Quantity = obj.Qty * configure.QTY })
                                                          .ToList();
                    report.OrderConfigures.AddRange(details);
                }
                else
                {
                    var itemConfiguration = _db.ItemConfgurations.Include(obj=>obj.StoreItemsRaw)
                                                                 .Where(obj=>obj.StoreItemsRaw.RawItemTypeId !=1 )
                                                                 .Select(obj => new ItemRawReportVM { Name = obj.StoreItemsRaw.ItemName,Id = obj.StoreItemRawId,Quantity = obj.MinimumAmount * configure.QTY })
                                                                 .ToList();
                    report.OrderConfigures.AddRange(itemConfiguration);
                }
            }
            report.Order = _mapper.Map<OrderVM>(order);
            report.OrderConfigures = report.OrderConfigures.GroupBy(obj => obj.Id).Select((group,index) => new ItemRawReportVM { Id = index+1,Name = group.FirstOrDefault()?.Name ?? string.Empty,Quantity = group.Sum(obj => obj.Quantity) }).ToList();
            report.StoreItems = orderDetail.Select((obj,index) => new StoreItemReportVM { Id = index + 1,Name = obj.Product.ProductName,Quantity = obj.QTY,Notes = obj.Notes }).ToList();
            return report;
        }
        public async Task<OrderConfigureContainer> ExtraConfiguration(int detailsId)
        {

            var details = _db.OrderDetails.Include(c => c.OrderConfigure).FirstOrDefault(d => d.Id == detailsId);
            if( details.OrderConfigure.Count > 0 )
            {
                return await GetOrderConfiguretb(details);
            }
            else
            {
                return await GetItemsConfiguretb(details);
            }
        }

        private async Task<OrderConfigureContainer> GetOrderConfiguretb(OrderDetails details)
        {
            var configurations = await _db.OrderConfigures.Where(p => p.OrderDetailsId == details.Id).ToListAsync();
            var orderConfigureContainer = new OrderConfigureContainer();
            orderConfigureContainer.StoreItemVM = _mapper.Map<StoreItemVM>(await _storeItemManager.CheckAndReturnWithRef(details.ProductId,r => r.Family,r => r.StoreBrand));
            orderConfigureContainer.OrderConfigure = _mapper.Map<List<OrderConfigureVM>>(configurations);
            orderConfigureContainer.orderId = details.OrderId;
            orderConfigureContainer.OrderDetailId = details.Id;
            return orderConfigureContainer;
        }

        private async Task<OrderConfigureContainer> GetItemsConfiguretb(OrderDetails details)
        {
            var configurations = await _itemConfigureManager.GetDataStoreItem(details.ProductId,p => p.StoreItemId == details.ProductId);
            var orderConfigureContainer = new OrderConfigureContainer();
            orderConfigureContainer.StoreItemVM = _mapper.Map<StoreItemVM>(configurations.StoreItemVM);
            orderConfigureContainer.OrderConfigure = configurations.ItemConfigurationVMs.Select(c => new OrderConfigureVM()
            {
                //Id = c.Id,
                StoreItemId = c.StoreItemRawId,
                Qty = c.MinimumAmount,
                OrderDetailsId = details.Id,
                RecordStatus = RecordStatus.Added,
            }).ToList();
            orderConfigureContainer.orderId = details.OrderId;
            orderConfigureContainer.OrderDetailId = details.Id;
            return orderConfigureContainer;
        }

        public async Task<FeedBackWithMessages> SaveConfiguration(OrderConfigureContainer vm)
        {

            return await _saveManagerConfigue.SaveTransactionAsync(SaveOrderConfigure,vm);
        }

        private async Task<OrderConfigureContainer> SaveOrderConfigure(OrderConfigureContainer vm)
        {
            //var count =  _db.OrderConfigures.Where(c=> c.OrderDetailsId==vm.OrderDetailId).Count();
            //if (count == 0)
            //{
            //    var orderConfig = _mapper.Map<OrderConfigure>(vm.OrderConfigure);
            //    await _db.AddRangeAsync(orderConfig);
            //}
            //else
            //{
            var order = await _db.Orders.FirstOrDefaultAsync(obj => obj.Id == vm.orderId);
            if( order != null && order.IsProcess == true ) { throw new Exception("لا يمكن التعديل على امر شغل تم تاكيده"); }
            foreach( var item in vm.OrderConfigure )
            {
                var config = _mapper.Map<OrderConfigure>(item);
                item.Id = 0;
                switch( item.RecordStatus )
                {
                    case RecordStatus.Added:
                        await _db.AddAsync(config);
                        break;
                    case RecordStatus.Updated:
                        _db.Update(config);
                        break;
                    case RecordStatus.Deleted:
                        _db.Remove(config);
                        break;
                    case RecordStatus.UnChanged:
                        break;
                    default:
                        break;
                }
            }
            await _db.SaveChangesAsync();
            return vm;
        }
    }
}
