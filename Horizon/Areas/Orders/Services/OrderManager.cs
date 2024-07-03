﻿using AutoMapper;
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
        private readonly GenericSettingsManager<Client, ClientVM> _clientManager;
        public readonly SaveManager<OrderContainer> _saveManager;
        public readonly SaveManager<OrderConfigureContainer> _saveManagerConfigue;
        private readonly IMapper _mapper;
        private readonly StoreItemManager _storeItemManager;
        public OrderManager(GenericSettingsManager<Client, ClientVM> clientManager,
             SaveManager<OrderContainer> saveManager, IMapper mapper,
             ApplicationDbContext db, ItemConfigureManager itemConfigureManager,
             SaveManager<OrderConfigureContainer> saveManagerConfigue,
             StoreItemManager storeItemManager)
        {
            _clientManager = clientManager;
            _mapper = mapper;
            _saveManager = saveManager;
            _db = db;
            _itemConfigureManager = itemConfigureManager;
            _saveManagerConfigue = saveManagerConfigue;
            _storeItemManager = storeItemManager;
        }

        public async Task<List<OrderVM>> GetOrders()
        {
            return _mapper.Map<List<OrderVM>>(await _db.Orders.Include(c=>c.Client).OrderByDescending(obj=>obj.DeliveryOrder).ToListAsync());
        }

        public async Task<List<OrderVM>> GetProcessOrder()
        {
            var orderLst = await _db.Orders.Include(c=>c.Client).Where(o=>o.OrderStatus == OrderStatus.Process)
                .OrderByDescending(obj => obj.DeliveryOrder).ToListAsync();
            return _mapper.Map<List<OrderVM>>(orderLst);
        }

        public async Task ChangeOrderSatus(int orderId, OrderStatus orderStatus)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(obj => obj.Id == orderId);
            order.OrderStatus = orderStatus;
            await _db.SaveChangesAsync();
        }
        public async Task<OrderDetailsContainer> GetDetails(int orderId)
        {
            var orderContainer = new OrderDetailsContainer();
            var order = await _db.Orders.Include(c=>c.Client).Include(d => d.OrderDetails).ThenInclude(p=>p.Product).FirstOrDefaultAsync(o => o.Id == orderId);
            orderContainer.Order = _mapper.Map<OrderVM>(order);
            orderContainer.OrderDetail = _mapper.Map<List<OrderDetailsVM>>(order.OrderDetails);
            List<OrderConfigure> orderConfigure = new List<OrderConfigure>();
            foreach (var v in order.OrderDetails)
            {
                var configureLst= await _db.OrderConfigures.Include(i=>i.StoreItems).Where(obj=>obj.OrderDetailsId == v.Id&&obj.IsNew==true).ToListAsync();
                if(configureLst.Count>0)
                    orderConfigure.AddRange(configureLst);
            }
            var orderConfigureVM =  _mapper.Map<List<OrderConfigureVM>>(orderConfigure.ToList());

            orderContainer.OrderConfigure = orderConfigureVM;
            return orderContainer;
        }
        public async Task<OrderContainer> NewOrder(int ClientId)
        {
            var orderContainer = new OrderContainer();
            orderContainer.Client = _mapper.Map<ClientVM>(await _clientManager.CheckAndReturn(ClientId));
            return orderContainer;
        }

        public async Task<FeedBackWithMessages> SaveOrders(OrderContainer vm)
        {
           return await _saveManager.SaveTransactionAsync(SaveOrder, vm);
        }

        private async Task<OrderContainer> SaveOrder(OrderContainer vm)
        {
            var orderId = await AddOrder(vm.Order,vm.Client.Id);
            await AddOrderDetails(vm.OrderDetail, orderId);
            return vm;
        }

        private async Task<int> AddOrder(OrderVM ordervm,int clientId)
        {
            var order = _mapper.Map<Order>(ordervm);
            order.ClientId = clientId;
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order.Id;    
        }
        private async Task AddOrderDetails(List<OrderDetailsVM> orderDetails,int orderId)
        {
            var orderdetails = _mapper.Map<List<OrderDetails>>(orderDetails);
            foreach (var orderdetail in orderdetails)
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

     
        public async Task<OrderConfigureContainer> ExtraConfiguration(int detailsId)
        {

            var details = _db.OrderDetails.Include(c=>c.OrderConfigure).FirstOrDefault(d => d.Id == detailsId);
            if (details.OrderConfigure.Count > 0)
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
            var configurations = await _db.OrderConfigures.Where(p=>p.OrderDetailsId == details.Id).ToListAsync();
            var orderConfigureContainer = new OrderConfigureContainer();
            orderConfigureContainer.StoreItemVM = _mapper.Map<StoreItemVM>(await _storeItemManager.CheckAndReturnWithRef(details.ProductId,r=>r.Family,r=>r.StoreBrand));
            orderConfigureContainer.OrderConfigure = _mapper.Map<List<OrderConfigureVM>>(configurations);
            orderConfigureContainer.orderId = details.OrderId;
            orderConfigureContainer.OrderDetailId = details.Id;
            return orderConfigureContainer;
        }

        private async Task<OrderConfigureContainer> GetItemsConfiguretb(OrderDetails details)
        {
            var configurations = await _itemConfigureManager.GetDataStoreItem(details.ProductId, p => p.StoreItemId == details.ProductId);
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

            return await _saveManagerConfigue.SaveTransactionAsync(SaveOrderConfigure, vm);
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
            if(order != null&&order.IsProcess == true) { throw new Exception("لا يمكن التعديل على امر شغل تم تاكيده"); }
            foreach (var item in vm.OrderConfigure)
            {
                var config = _mapper.Map<OrderConfigure>(item);
                item.Id = 0;
                switch (item.RecordStatus)
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
