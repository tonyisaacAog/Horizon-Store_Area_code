using AutoMapper;
using Data.Services;
using Horizon.Areas.Orders.Services;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Store.Services;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Model;
using Services;

namespace Horizon.Areas.Sales.Services
{
    public class SalesManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly SaveManager<SalesContainer> _SalesSaveManager;
        private readonly GenericSettingsManager<Client, ClientVM> _ClientManager;
        private readonly StoreTransDetailsManager _salesTransactionDetailsManager;
        private readonly StoreTransactionManager _saleTransactionManager;
        private readonly OrderManager _orderManager;


        public SalesManager(ApplicationDbContext db,
            IMapper mapper,
            SaveManager<SalesContainer> SalesSaveManager,
            GenericSettingsManager<Client, ClientVM> ClientManager,
            StoreTransDetailsManager salesTransactionDetailsManager,
            StoreTransactionManager saleTransactionManager,
            OrderManager orderManager)
        {
            _db = db;
            _mapper = mapper;
            _SalesSaveManager = SalesSaveManager;
            _ClientManager = ClientManager;
            _salesTransactionDetailsManager = salesTransactionDetailsManager;
            _saleTransactionManager = saleTransactionManager;
            _orderManager = orderManager;
        }

        public async Task<List<SaleVM>> GetAll()
            => _mapper.Map<List<SaleVM>>(await _db.Sales.Include(s => s.Client).ToListAsync());

        public async Task<SalesContainer> NewSales(int ClientId)
        {
            var vm = new SalesContainer();
            vm.Client = await _ClientManager.CheckAndReturn(ClientId);
            return vm;
        }

        public async Task<SalesContainer> NewSalesForOrder(int OrderId)
        {
            var vm = new SalesContainer();
            var order = await _orderManager.GetOrderWithDetailsById(OrderId);
            vm.Client = order.Client;
            order.OrderDetail.ForEach(line =>
            {
                vm.SaleDetails.Add(new Store.ViewModel.Transaction.SaleStoreTransactionVM
                {
                    Id = line.Id,
                    QTY = line.QTY,
                    UnitPrice = line.UnitPrice,
                    TransType = Finance.CurrentAssetModule.Store.Model.Settings.StoreTransTypeEnum.Sale,
                    StoreItemId =line.ProductId,
                    StoreItemName = line.ProductName
                });
            });
            return vm;
        }

        public async Task<SalesContainer> DetailsSales(int salesId)
        {
            SalesContainer salesContainer = new SalesContainer();

            var sale =  _db.Sales.Include(obj=>obj.SaleDetails).ThenInclude(obj=>obj.StoreItem)
                                 .Include(obj => obj.SaleDetails).ThenInclude(obj => obj.StoreItemsRaw)
                                 .Include(s => s.Client).FirstOrDefault(p => p.Id == salesId);

            var trans = _db.StoreTransactions.FirstOrDefault(p => p.SourceId == salesId && p.DestinationId == sale.ClientId);

            salesContainer.Client = _mapper.Map<ClientVM>(sale?.Client);

            salesContainer.SaleInfo = _mapper.Map<SaleInfoVM>(sale);

            var storeLocationame = _db.StoreLocations.FirstOrDefault(s => s.Id == trans.StoreLocationId);

            salesContainer.SaleInfo.StoreLocationName = storeLocationame?.LocationName;

            salesContainer.SaleDetails = sale.SaleDetails.Where(obj => obj.DetailType == DetailType.Product).Select(obj => new Store.ViewModel.Transaction.SaleStoreTransactionVM
            {
                Id = obj.Id,
                StoreItemId = (int)obj.StoreItemId,
                StoreItemName = obj.StoreItem.ProductNameAr,
                QTY = Convert.ToInt32(obj.Amount),
                UnitPrice = obj.UnitPrice,
            }).ToList();
            salesContainer.SaleItemRawDetails = sale.SaleDetails.Where(obj => obj.DetailType == DetailType.Item).Select(obj => new Store.ViewModel.Transaction.SaleStoreTransactionVM
            {
                Id = obj.Id,
                StoreItemId = (int)obj.StoreItemsRawId,
                StoreItemName = obj.StoreItemsRaw.ItemNameAr,
                QTY = Convert.ToInt32(obj.Amount),
                UnitPrice = obj.UnitPrice,
            }).ToList();
            //await _salesTransactionDetailsManager.GetSalesDetails(salesId);

            return salesContainer;
        }


        public async Task<FeedBackWithMessages> SaveSales(SalesContainer vm)
            => await _SalesSaveManager.SaveTransactionAsync(SaveSalesFunc, vm);

        private async Task<SalesContainer> SaveSalesFunc(SalesContainer vm)
        {
            var NewSales = _mapper.Map<Sale>(vm.SaleInfo);

            NewSales.ClientId = vm.Client.Id;
            NewSales.TotalAmount = vm.SaleDetails.Sum(obj => obj.UnitPrice * obj.QTY) + vm.SaleItemRawDetails.Sum(obj => obj.UnitPrice * obj.QTY);
          
            await _db.AddAsync(NewSales);

            await _db.SaveChangesAsync();

            await SaveDetailsForSale(NewSales.Id,vm);
            var StoreTransId = await _saleTransactionManager.DoStoreTransSales(vm, NewSales.Id);

            await _salesTransactionDetailsManager.DoSaleTransactionsDetails(vm, StoreTransId);
            await _saleTransactionManager.DoItemRawTransactions((vm, NewSales.Id));

            if( vm.IsSaleFromOrder )
            {
                var order = await _orderManager.ConvertOrderToSaleInvoice(vm.OrderId);
                if( !order.Done )
                {
                    throw new Exception(order.Messages.FirstOrDefault());
                }
            }

            return vm;
        }

        private async Task SaveDetailsForSale(int id,SalesContainer vm)
        {
            var detailStoreItemRaw = vm.SaleItemRawDetails.Select(obj => new SaleDetails
            {
                StoreItemsRawId = obj.StoreItemId,
                Amount = obj.QTY,
                UnitPrice = obj.UnitPrice,
                TotalSales = obj.QTY * obj.UnitPrice,
                SaleId = id,
                DetailType = DetailType.Item
            });
            var detailStoreItem = vm.SaleDetails.Select(obj => new SaleDetails
            {
                StoreItemId = obj.StoreItemId,
                Amount = obj.QTY,
                UnitPrice = obj.UnitPrice,
                TotalSales = obj.QTY * obj.UnitPrice,
                SaleId = id,
                DetailType = DetailType.Product
            });
            await _db.AddRangeAsync(detailStoreItemRaw);
            await _db.AddRangeAsync(detailStoreItem);
        }

    }

}
