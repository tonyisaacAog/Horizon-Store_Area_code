using AutoMapper;
using BoldReports.Linq;
using Data.Services;
using Finance.CurrentAssetModule.Stores.Model.Main;
using Horizon.Areas.Purchases.Models;
using Horizon.Areas.Purchases.ViewModel;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Configuration;
using Horizon.Areas.Store.ViewModel.Main;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Horizon.Models.Shared;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure.Model;
using Services;
using System.Linq.Expressions;
using System.Security.Policy;

namespace Horizon.Areas.Store.Services
{
    public class ItemConfigureManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly GenericSettingsManager<StoreItem, StoreItemVM> _storeItemManager;
        private readonly SaveManager<ConfigurationContainer> _configureSaveManager;
 

        public ItemConfigureManager(ApplicationDbContext db,
            IMapper mapper,
            GenericSettingsManager<StoreItem, StoreItemVM> StoreItemManager,
            SaveManager<ConfigurationContainer> ConfigureSaveManager)
        {
            _db = db;
            _mapper = mapper;
            _storeItemManager = StoreItemManager;
            _configureSaveManager = ConfigureSaveManager;
        }


        //public async Task<ConfigurationContainer> ConfigurationDetails(int Id)
        //{
        //    var vm = new ConfigurationContainer();
        //    vm.StoreItemVM =  GetDataStoreItem(Id).Result.StoreItemVM;
        //    vm.ItemConfigurationVMs =await  GetAllConfiguration(Id);
        //    return vm;
        //}

        private async Task<List<ItemConfigurationVM>> GetAllConfiguration(int StoreItemId)
        {
            return _mapper.Map<List<ItemConfigurationVM>>
                (await _db.ItemConfgurations.Where(ic => ic.StoreItemId == StoreItemId).ToListAsync());
        }

        public async Task<ConfigurationContainer> GetDataStoreItem(int StoreItemId,Expression<Func<ItemConfguration,bool>> condition)
        {
            var vm = new ConfigurationContainer();
            var rand = new Random();
            vm.StoreItemVM = await _storeItemManager.CheckAndReturnWithRef(StoreItemId,
                (StoreItem)=>StoreItem.Family,
                (StoreItem) => StoreItem.StoreBrand);
            vm.ItemConfigurationVMs = await _db.ItemConfgurations.Include(x => x.StoreItem)
                .Include(x => x.StoreItemsRaw).Where(condition)
                .Select(x => new ItemConfigurationVM()
                {
                    Id=rand.Next(1,100),
                    StoreItemId = StoreItemId,
                    StoreItemRawId = x.StoreItemRawId,
                    MinimumAmount = x.MinimumAmount,
                    StoreItemName = x.StoreItem.ProductName,
                    StoreItemsRawName = x.StoreItemsRaw.ItemName,
                    RecordStatus= RecordStatus.UnChanged
                }).ToListAsync();
            vm.NumberProductCanMade = await GetNumProductCanMadeOfMatiaral(vm.ItemConfigurationVMs);
            return vm;
        }

        #region old funct GetDateStoreItem()
        //public async Task<ConfigurationContainer> GetDataStoreItem(int StoreItemId)
        //{
        //    var vm = new ConfigurationContainer();
        //    var rand = new Random();
        //    vm.StoreItemVM = await _storeItemManager.CheckAndReturnWithRef(StoreItemId,
        //        (StoreItem) => StoreItem.Family,
        //        (StoreItem) => StoreItem.StoreBrand);
        //    vm.ItemConfigurationVMs = await _db.ItemConfgurations.Include(x => x.StoreItem)
        //        .Include(x => x.StoreItemsRaw).Where(x => x.StoreItemId == StoreItemId)
        //        .Select(x => new ItemConfigurationVM()
        //        {
        //            Id = rand.Next(1, 100),
        //            StoreItemId = StoreItemId,
        //            StoreItemRawId = x.StoreItemRawId,
        //            MinimumAmount = x.MinimumAmount,
        //            StoreItemName = x.StoreItem.ProductName,
        //            StoreItemsRawName = x.StoreItemsRaw.ItemName,
        //            RecordStatus = RecordStatus.UnChanged
        //        }).ToListAsync();
        //    vm.NumberProductCanMade = await GetNumProductCanMadeOfMatiaral(vm.ItemConfigurationVMs);
        //    return vm;
        //}


        #endregion

        private async Task<int> GetNumProductCanMadeOfMatiaral(List<ItemConfigurationVM> itemConfigurationVM)
        {
            List<int> lst = new List<int>();
            foreach(var config in itemConfigurationVM)
            {
                var calc = (int)_db.StoreItemsRaw.FirstOrDefault(i => i.Id == config.StoreItemRawId).QTY / config.MinimumAmount;
                lst.Add(calc);
            }
            return lst.Count>0?lst.Min():0;
        }

        public async Task<FeedBackWithMessages> SaveConfiguration(ConfigurationContainer vm)
            => await _configureSaveManager.SaveTransactionAsync(SaveConfigurationFunc, vm);

        private async Task<ConfigurationContainer> SaveConfigurationFunc(ConfigurationContainer vm)
        {
            
            //var Configurations = _mapper.Map<List<ItemConfguration>>(vm.ItemConfigurationVMs);
            foreach (var item in vm.ItemConfigurationVMs)
            {
                var config = _mapper.Map<ItemConfguration>(item);
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
