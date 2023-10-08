using AutoMapper;
using Finance.CurrentAssetModule.Store.Model.Main;
using Horizon.Data;
using Finance.CurrentAssetModule.Store.Model.Settings;
using Horizon.Areas.Manufacturing.VewModels;
using MyInfrastructure.Extensions;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Areas.Store.ViewModel.Main;
using BoldReports.RDL.DOM;
using Horizon.Areas.Store.ViewModel.Settings;

namespace Horizon.Areas.Store.Services
{
    public class StoreTransactionManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public StoreTransactionManager(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        public async Task<int> DoStoreTransManufacturing(ManufacturingContainer vm,int SourceId)
        {
            var StoreTransaction = new StoreTransactions()
            {
                SourceId = SourceId,
                DestinationId = vm.StoreLocationId,
                SourceType = StoreTransSourceTypes.Manufacturing,
                DestinationType = StoreTransSourceTypes.Store,
                TransDate = vm.ManufacturingInfoVM.BatchDate.ToEgyptionDate(),
                TransType = StoreTransTypeEnum.Manufacturing,
                Descrpition="",
            };
            await _db.AddAsync(StoreTransaction);
            await _db.SaveChangesAsync();
            return StoreTransaction.Id;
        }

        public async Task<int> DoStoreTransSales(SalesContainer vm, int SourceId)
        {
            var StoreTransaction = new StoreTransactions()
            {
                SourceId = SourceId,
                DestinationId = vm.Client.Id,
                SourceType = StoreTransSourceTypes.Store,
                DestinationType = StoreTransSourceTypes.Client,
                TransDate = vm.SaleInfo.SalesDate.ToEgyptionDate(),
                TransType = StoreTransTypeEnum.Sale,
                Descrpition = "",
                StoreLocationId = vm.SaleInfo.StoreLocationId,
            };
            await _db.AddAsync(StoreTransaction);
            await _db.SaveChangesAsync();
            return StoreTransaction.Id;
        }

        public async Task<int> DoStoreTansDestroy(StoreItemDestoryVM vm)
        {
            var StoreTransaction = new StoreTransactions()
            {
                SourceId = vm.StoreLocationId,
                DestinationId = vm.StoreLocationId,
                SourceType = StoreTransSourceTypes.Store,
                DestinationType = StoreTransSourceTypes.Store,
                TransDate = vm.DateOfDestroy.ToEgyptionDate(),
                TransType = StoreTransTypeEnum.Destory,
                Descrpition = vm.Description,
                StoreLocationId = vm.StoreLocationId,
            };
            await _db.AddAsync(StoreTransaction);
            await _db.SaveChangesAsync();
            return StoreTransaction.Id;
        }
    }
}
