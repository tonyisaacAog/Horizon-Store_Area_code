using AutoMapper;
using BaseEntities;
using Data.Services;
using Horizon.Areas.Store.Models.Settings;
using Horizon.Areas.Store.ViewModel.Settings;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyInfrastructure.Model;
using System.Linq.Expressions;

namespace Services
{
    public class GenericSettingsManager<TModel, TViewModel> where TModel : BaseEntity where TViewModel:BaseId
    {
        protected readonly ApplicationDbContext _db;
        protected readonly IMapper _mapper;
        protected readonly SaveManager<TViewModel> _saveManager;
        internal DbSet<TModel> _dbSet;
        public GenericSettingsManager(ApplicationDbContext db, IMapper mapper,
            SaveManager<TViewModel> saveManager)
        {
            _db = db;
            _dbSet = _db.Set<TModel>();
            _mapper = mapper;
            _saveManager = saveManager;

        }
        public virtual async Task<TViewModel> CheckAndReturn(int id)
        {
            return await CheckEntityExist(id) ?
               await GetById(id) : throw new Exception("لا توجد معلومات حول هذا العنصر");
        }

        public async Task<TViewModel> CheckAndReturnWithRef(int id, params Expression<Func<TModel, object>>[] referncesTobject)
        {
            return await CheckEntityExist(id) ?
               await GetByIdWithRefernces(id,referncesTobject) : throw new Exception("لا توجد معلومات حول هذا العنصر");
        }


        private async Task<bool> CheckEntityExist(int id)
          => await _dbSet.AnyAsync(t => t.Id == id);




        private async Task<TViewModel> GetById(int id)
           => _mapper.Map<TViewModel>(await _dbSet.FindAsync(id));

        private async Task<TViewModel> GetByIdWithRefernces(int id,params Expression<Func<TModel, object>>[] referncesTobject)
        {
            var entity = await _dbSet.FindAsync(id);
            foreach(var refrence in referncesTobject)
            {
                _db.Entry<TModel>(entity).Reference(refrence.GetPropertyAccess().Name).Load();
            }
            return _mapper.Map<TViewModel>(entity);
        } 

      
        public virtual async Task<List<TViewModel>> GetAll()
        => _mapper.Map<List<TViewModel>>(await _dbSet.ToListAsync());

        public virtual  IQueryable<TViewModel> GetByCondition(Func<TModel,bool> condition)
     => _mapper.Map<IQueryable<TViewModel>>( _dbSet.Where(condition).AsQueryable());


        public async Task<FeedBackWithMessages> SaveManagerData(TViewModel vm, bool IsDeleted = false)
        {
            if (IsDeleted)
                return await _saveManager.SaveTransactionAsync(DeleteFunc, vm);
            if (vm.Id > 0)
                return await _saveManager.SaveTransactionAsync(UpdateFunc, vm);
            else
                return await _saveManager.SaveTransactionAsync(AddNewFunc, vm);
        }
        private async Task<TViewModel> DeleteFunc(TViewModel vm)
        {
            await CheckEntityExist(vm.Id);
            _db.Remove(_mapper.Map<TModel>(vm));
            return vm;
        }

        private async Task<TViewModel> UpdateFunc(TViewModel vm)
        {
            var UpdatedRecord = _mapper.Map<TModel>(vm);
            _db.Update(UpdatedRecord);
            await _db.SaveChangesAsync();
            return vm;
        }

        private async Task<TViewModel> AddNewFunc(TViewModel vm)
        {
            var NewRecord = _mapper.Map<TModel>(vm);
            await _db.AddAsync(NewRecord);
            await _db.SaveChangesAsync();
            return vm;
        }
    }
}
