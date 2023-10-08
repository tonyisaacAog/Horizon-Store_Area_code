﻿using AutoMapper;
using Data.Services;
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

        public SalesManager(ApplicationDbContext db,
            IMapper mapper,
            SaveManager<SalesContainer> SalesSaveManager,
            GenericSettingsManager<Client, ClientVM> ClientManager,
            StoreTransDetailsManager salesTransactionDetailsManager,
            StoreTransactionManager saleTransactionManager)
        {
            _db = db;
            _mapper = mapper;
            _SalesSaveManager = SalesSaveManager;
            _ClientManager = ClientManager;
            _salesTransactionDetailsManager = salesTransactionDetailsManager;
            _saleTransactionManager = saleTransactionManager;
        }

        public async Task<List<SaleVM>> GetAll()
            => _mapper.Map<List<SaleVM>>(await _db.Sales.Include(s => s.Client).ToListAsync());

        public async Task<SalesContainer> NewSales(int ClientId)
        {
            var vm = new SalesContainer();
            vm.Client = await _ClientManager.CheckAndReturn(ClientId);
            return vm;
        }

        public async Task<SalesContainer> DetailsSales(int salesId)
        {
            SalesContainer salesContainer = new SalesContainer();

            var sale =  _db.Sales.Include(s => s.Client).FirstOrDefault(p => p.Id == salesId);

            var trans = _db.StoreTransactions.FirstOrDefault(p => p.SourceId == salesId && p.DestinationId == sale.ClientId);

            salesContainer.Client = _mapper.Map<ClientVM>(sale?.Client);

            salesContainer.SaleInfo = _mapper.Map<SaleInfoVM>(sale);

            var storeLocationame = _db.StoreLocations.FirstOrDefault(s => s.Id == trans.StoreLocationId);

            salesContainer.SaleInfo.StoreLocationName = storeLocationame?.LocationName;

            salesContainer.SaleDetails = await _salesTransactionDetailsManager.GetSalesDetails(salesId);

            return salesContainer;
        }


        public async Task<FeedBackWithMessages> SaveSales(SalesContainer vm)
            => await _SalesSaveManager.SaveTransactionAsync(SaveSalesFunc, vm);

        private async Task<SalesContainer> SaveSalesFunc(SalesContainer vm)
        {
            var NewSales = _mapper.Map<Sale>(vm.SaleInfo);

            NewSales.ClientId = vm.Client.Id;

            await _db.AddAsync(NewSales);

            await _db.SaveChangesAsync();

            var StoreTransId = await _saleTransactionManager.DoStoreTransSales(vm, NewSales.Id);

            await _salesTransactionDetailsManager.DoSaleTransactionsDetails(vm, StoreTransId);

            return vm;
        }


    }

}
